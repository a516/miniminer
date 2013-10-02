using System;
using System.Text;

namespace MiniMiner
{
    public static class Utils
    {
        public static byte[] ToBytes(string input)
        {
            var bytes = new byte[input.Length / 2];
            for (int i = 0, j = 0; i < input.Length; j++, i += 2)
                bytes[j] = byte.Parse(input.Substring(i, 2), System.Globalization.NumberStyles.HexNumber);
            return bytes;
        }

        public static string ToString(byte[] input)
        {
            var result = new StringBuilder();
            foreach (var b in input)
                result.Append(b.ToString("x2"));
            return result.ToString();
        }

        public static string ToString(uint value)
        {
            var result = new StringBuilder();
            foreach (var b in BitConverter.GetBytes(value))
                result.Append(b.ToString("x2"));

            return result.ToString();
        }

        public static string EndianFlip32BitChunks(string input)
        {
            //32 bits = 4*4 bytes = 4*4*2 chars
            var result = new StringBuilder();
            for (var i = 0; i < input.Length; i += 8)
                for (var j = 0; j < 8; j += 2)
                {
                    //append byte (2 chars)
                    result.Append(input[i - j + 6]);
                    result.Append(input[i - j + 7]);
                }
            return result.ToString();        
        }

        public static string RemovePadding(string input)
        {
            //payload length: final 64 bits in big-endian - 0x0000000000000280 = 640 bits = 80 bytes = 160 chars
            return input.Substring(0, 160);
        }

        public static string AddPadding(string input)
        {
            //add the padding to the payload. It never changes.
            return input + "000000800000000000000000000000000000000000000000000000000000000000000000000000000000000080020000";
        }
    }
}