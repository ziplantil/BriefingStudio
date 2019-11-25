using System;
using System.IO;
using System.Text;

namespace BriefingStudio
{
    internal static class Utils
    {
        internal static int ToLittleEndianInt32(byte[] data, int offset)
        {
            return data[offset] | (data[offset + 1] << 8) | (data[offset + 2] << 16) | (data[offset + 3] << 24);
        }

        internal static byte[] ASCIIToBytesPad(string name, int length)
        {
            byte[] result = new byte[length];
            for (int i = 0; i < length; ++i)
            {
                result[i] = 0;
            }

            Encoding.ASCII.GetBytes(name, 0, Math.Min(name.Length, length - 1), result, 0);
            return result;
        }

        internal static byte[] Int32LittleEndianToBytes(int n)
        {
            return new byte[] { (byte)((n) & 0xFF), (byte)((n >> 8) & 0xFF), (byte)((n >> 16) & 0xFF), (byte)((n >> 24) & 0xFF) };
        }

        internal static void StreamCopy(FileStream dst, FileStream src, int n)
        {
            byte[] buffer = new byte[65536];
            int read;
            while (n > 0)
            {
                read = src.Read(buffer, 0, buffer.Length);
                if (read <= 0)
                {
                    break;
                }
                dst.Write(buffer, 0, read);
                n -= read;
            }
        }
    }
}