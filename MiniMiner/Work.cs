using System;
using System.Collections.Generic;
using System.Security.Cryptography;

namespace MiniMiner
{
    public class Work
    {
        private const int Queuecount = 4;
        private static readonly Queue<Work> WorkQueue = new Queue<Work>();

        public static Work GetWork(Pool pool)
        {
            if (WorkQueue.Count == 0)
                for (var x = 0; x < Queuecount; ++x)
                    WorkQueue.Enqueue(new Work(pool.ParseData()));

            WorkQueue.Enqueue(new Work(pool.ParseData()));
            return WorkQueue.Dequeue();
        }

        private Work(byte[] data)
        {
            Data = data;
            Current = (byte[])data.Clone();
            _nonceOffset = Data.Length - 4;
            _ticks = DateTime.Now.Ticks;
            _hasher = new SHA256Managed();
        }

        private readonly SHA256Managed _hasher;
        private readonly long _ticks;
        private readonly long _nonceOffset;
        public byte[] Data;
        public byte[] Current;

        internal bool FindShare(ref uint nonce, uint batchSize)
        {
            for(;batchSize > 0; batchSize--)
            {
                BitConverter.GetBytes(nonce).CopyTo(Current, _nonceOffset);
                var doubleHash = Sha256(Sha256(Current));

                var zeroBytes = 0; /* count trailing bytes that are zero */
                for (var i = 31; i >= 28; i--, zeroBytes++)
                    if(doubleHash[i] > 0)
                        break;

                //standard share difficulty matched! (target:ffffffffffffffffffffffffffffffffffffffffffffffffffffffff00000000)
                if(zeroBytes == 4)
                    return true;

                //increase
                if(++nonce == uint.MaxValue)
                    nonce = 0;
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
    }
}