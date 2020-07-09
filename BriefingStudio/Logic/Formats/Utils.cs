using System;
using System.IO;
using System.Text;

namespace BriefingStudio.Logic.Formats
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
                read = src.Read(buffer, 0, Math.Min(n, buffer.Length));
                if (read <= 0)
                {
                    break;
                }
                dst.Write(buffer, 0, read);
                n -= read;
            }
        }

        internal static LibDescent.Data.Color GDIColorToLDColor(System.Drawing.Color color)
        {
            return new LibDescent.Data.Color(color.A, color.R, color.G, color.B);
        }

        internal static LibDescent.Data.Color[] GDIPaletteToLDPalette(System.Drawing.Color[] palette)
        {
            LibDescent.Data.Color[] array = new LibDescent.Data.Color[palette.Length];
            for (int i = 0; i < array.Length; ++i)
                array[i] = GDIColorToLDColor(palette[i]);
            return array;
        }

        internal static System.Drawing.Color LDColorToGDIColor(LibDescent.Data.Color color)
        {
            return System.Drawing.Color.FromArgb(color.A, color.R, color.G, color.B);
        }

        internal static System.Drawing.Color[] LDPaletteToGDIPalette(LibDescent.Data.Color[] palette)
        {
            System.Drawing.Color[] array = new System.Drawing.Color[palette.Length];
            for (int i = 0; i < array.Length; ++i)
                array[i] = LDColorToGDIColor(palette[i]);
            return array;
        }
    }
}