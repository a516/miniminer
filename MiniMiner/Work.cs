using System;
using System.Collections.Generic;
using System.Security.Cryptography;

namespace MiniMiner
{
    public class Work
    {
		private Pool _pool;
		private readonly SHA256Managed _hasher;
		private readonly long _ticks;
		private readonly long _nonceOffset;
		public byte[] Data;
		public byte[] Current;
		public uint Nonce{ get; private set;}
		string paddedData;

        public Work(Pool pool)
        {
            Data = pool.ParseData();
            Current = (byte[])Data.Clone();
            _nonceOffset = Data.Length - 4;
            _ticks = DateTime.Now.Ticks;
            _hasher = new SHA256Managed();
			_pool = pool;
        }
        
        internal bool FindShare(uint batchSize)
        {
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
                if(++Nonce == uint.MaxValue)
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
			paddedData = Utils.AddPadding(data);
		}

		public bool SendShare()
		{
			return _pool.SendShare (paddedData);
		}
    }
}