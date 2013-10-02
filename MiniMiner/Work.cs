﻿using System;
using System.Security.Cryptography;

namespace MiniMiner
{
    public class Work
    {
        public Work(byte[] data)
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

                //count trailing bytes that are zero
                var zeroBytes = 0;
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