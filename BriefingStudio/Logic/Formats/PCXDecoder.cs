using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace BriefingStudio.Logic.Formats
{
    class PCXDecoder
    {
        public PCXDecoder()
        {
        }

        public PCXHeader ParseHeader(byte[] header)
        {
            return new PCXHeader(header);
        }

        public Color[] GetDefaultPalette()
        {
            Color[] palette = new Color[256];
            palette[0] = Color.FromArgb(255, 0x00, 0x00, 0x00);
            palette[1] = Color.FromArgb(255, 0x00, 0x00, 0xAA);
            palette[2] = Color.FromArgb(255, 0x00, 0xAA, 0x00);
            palette[3] = Color.FromArgb(255, 0x00, 0xAA, 0xAA);
            palette[4] = Color.FromArgb(255, 0xAA, 0x00, 0x00);
            palette[5] = Color.FromArgb(255, 0xAA, 0x00, 0xAA);
            palette[6] = Color.FromArgb(255, 0xAA, 0x55, 0x00);
            palette[7] = Color.FromArgb(255, 0xAA, 0xAA, 0xAA);
            palette[8] = Color.FromArgb(255, 0x55, 0x55, 0x55);
            palette[9] = Color.FromArgb(255, 0x55, 0x55, 0xFF);
            palette[10] = Color.FromArgb(255, 0x55, 0xFF, 0x55);
            palette[11] = Color.FromArgb(255, 0x55, 0xFF, 0xFF);
            palette[12] = Color.FromArgb(255, 0xFF, 0x55, 0x55);
            palette[13] = Color.FromArgb(255, 0xFF, 0x55, 0xFF);
            palette[14] = Color.FromArgb(255, 0xFF, 0xFF, 0x55);
            palette[15] = Color.FromArgb(255, 0xFF, 0xFF, 0xFF);
            for (int i = 16; i < 256; ++i)
            {
                palette[i] = Color.Black;
            }
            return palette;
        }

        public Color ClosestColor(Color[] palette, Color color)
        {
            int minDist = Int32.MaxValue;
            Color closestColor = Color.Black;

            foreach (Color c in palette)
            {
                int dist = (c.R - color.R) * (c.R - color.R) + (c.G - color.G) * (c.G - color.G) + (c.B - color.B) * (c.B - color.B);
                if (minDist > dist)
                {
                    minDist = dist;
                    closestColor = c;
                }
            }

            return closestColor;
        }

        public Bitmap LoadPCX(Stream fs, out Color[] palette)
        {
            using (BinaryReader br = new BinaryReader(fs))
            {
                PCXHeader header = ParseHeader(br.ReadBytes(128));
                palette = GetDefaultPalette();
                if (header.Manufacturer != 0x0a)
                    throw new ArgumentException("file is not a valid PCX image");
                if (header.Encoding != 1)
                    throw new ArgumentException("only PCX encoding 1 is supported");
                if (header.NPlanes != 1)
                    throw new ArgumentException("only PCX with 1 plane is supported");
                if (header.BitsPerPixel != 8)
                    throw new ArgumentException("only PCX with 8bpp (256 colors) is supported");
                if (header.Version != 5)
                    throw new ArgumentException("only PCX version 5 is supported");

                // test extended palette
                fs.Seek(-769, SeekOrigin.End);
                if (br.ReadByte() == 0x0C)
                {
                    // has ext palette
                    byte[] pal = br.ReadBytes(768);
                    for (int i = 0; i < 255; ++i)
                    {
                        palette[i] = ReadRGB(pal, i * 3);
                    }
                }

                // read image data
                fs.Seek(128, SeekOrigin.Begin);

                int width = header.Xmax - header.Xmin + 1;
                int height = header.Ymax - header.Ymin + 1;
                int pixelsRead = 0;
                int pixelsTotal = width * height;

                Bitmap res = new Bitmap(width, height, PixelFormat.Format24bppRgb);
                BitmapData bmpData =
                    res.LockBits(new Rectangle(0, 0, width, height),
                    System.Drawing.Imaging.ImageLockMode.ReadWrite,
                    res.PixelFormat);
                IntPtr ptr = bmpData.Scan0;
                int bytes = Math.Abs(bmpData.Stride) * res.Height;
                byte[] rgbValues = new byte[bytes];
                System.Runtime.InteropServices.Marshal.Copy(ptr, rgbValues, 0, bytes);

                int x, y, p;
                Color clr;
                while (pixelsRead < pixelsTotal)
                {
                    int runLength = 1;
                    int runByte = br.ReadByte();
                    if ((runByte & 0xC0) == 0xC0)
                    {
                        runLength = runByte & 0x3F;
                        runByte = br.ReadByte();
                    }
                    for (int i = 0; i < runLength && pixelsRead < pixelsTotal; ++i)
                    {
                        y = Math.DivRem(pixelsRead, width, out x);
                        p = y * bmpData.Stride + (x * 3);
                        clr = palette[runByte];
                        rgbValues[p + 0] = clr.B;
                        rgbValues[p + 1] = clr.G;
                        rgbValues[p + 2] = clr.R;
                        ++pixelsRead;
                    }
                }

                System.Runtime.InteropServices.Marshal.Copy(rgbValues, 0, ptr, bytes);
                res.UnlockBits(bmpData);
                return res;
            }
        }

        public Bitmap LoadPCX(string filePath, out Color[] palette)
        {
            using (FileStream fs = File.Open(filePath, FileMode.Open))
            {
                return LoadPCX(fs, out palette);
            }
        }

        public Bitmap LoadPCX(byte[] contents, out Color[] palette)
        {
            using (MemoryStream ms = new MemoryStream(contents))
            {
                return LoadPCX(ms, out palette);
            }
        }

        public static Color ReadRGB(byte[] block, int v)
        {
            return Color.FromArgb(255, block[v], block[v + 1], block[v + 2]);
        }
    }
}
