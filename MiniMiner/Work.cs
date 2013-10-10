using System;
using System.Security.Cryptography;
using System.Text;

namespace MiniMiner
{
    public class Work : IDisposable
    {
		private Pool _pool;
		private SHA256Managed _hasher;
		private readonly long _ticks;
		private readonly long _nonceOffset;
		public byte[] Data;
		public byte[] Current;
		public uint Nonce{ get; private set;}
        public int WorkerID { get; set; }
		string _paddedData;
        private uint _batchSize;

        public Work(Pool pool)
        {
            Data = pool.ParseData();
            Current = (byte[])Data.Clone();
            _nonceOffset = Data.Length - 4;
            _ticks = DateTime.Now.Ticks;
            _hasher = new SHA256Managed();
			_pool = pool;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private bool _isDisposed;
        protected virtual void Dispose(bool isDisposing)
        {
            if (!_isDisposed)
            {
                _pool = null;
                _isDisposed = true;
            }
        }

        internal bool FindShare(uint batchSize)
        {
            _batchSize = batchSize;
            for(;batchSize > 0; batchSize--)
            {
                BitConverter.GetBytes(Nonce).CopyTo(Current, _nonceOffset);
                var doubleHash = Sha256(Sha256(Current));

                var zeroBytes = 0; /* count trailing bytes that are zero */
                for (var i = 31; i >= 28; i--, zeroBytes++)
                    if(doubleHash[i] > 0)
                        break;

                //standard share difficulty matched! (target:ffffffffffffffffffffffffffffffffffffffffffffffffffffffff00000000)
                if(zeroBytes == 4)
                    return true;

                //increase
                if (++Nonce == uint.MaxValue)
                    Nonce = 0;
            }
            return false;
        }

        private byte[] Sha256(byte[] input)
        {
            return _hasher.ComputeHash(input, 0, input.Length);
        }

        public byte[] Hash
        {
            get { return Sha256(Sha256(Current)); }
        }

        public long Age 
        {
            get { return DateTime.Now.Ticks - _ticks; }
        }

		public void CalculateShare()
		{
			var data = Utils.EndianFlip32BitChunks(Utils.ToString(Current));
			_paddedData = Utils.AddPadding(data);
		}

		public bool SendShare()
		{
			return _pool.SendShare (_paddedData);
		}

        private static DateTime _lastPrint = DateTime.Now;
        public string GetCurrentStateString()
        {
            var sb = new StringBuilder();
            sb.Append("Worker " + WorkerID + " Data: " + Utils.ToString(Data) + "\r\n");
            sb.Append(
                string.Concat("Nonce: ",
                Utils.ToString(Nonce), "/",
                Utils.ToString(uint.MaxValue), " ",
                (((double)Nonce / uint.MaxValue) * 100).ToString("F2"), "% \r\n"));
            sb.Append("Hash: " + Utils.ToString(Hash) + "\r\n");
            var span = DateTime.Now - _lastPrint;
            sb.Append("Speed: " + (int)((_batchSize / 1000) / span.TotalSeconds) + "Kh/s \r\n");
            _lastPrint = DateTime.Now;
            return sb.ToString();
        }
    }
}