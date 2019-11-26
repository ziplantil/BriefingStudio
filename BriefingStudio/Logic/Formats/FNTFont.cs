using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.CompilerServices;

namespace BriefingStudio
{
    public class FNTFont
    {
        protected short cwidth;
        protected short cheight;
        protected short flags;
        protected char minchar;
        protected char maxchar;
        protected byte[][] fontData;
        protected short[] widths;
        protected Dictionary<int, int> kerns;
        protected bool isProportional;
        protected bool hasKerning;
        protected char lastChar;
        protected bool colored;
        protected readonly byte[] palette;
        protected Bitmap buffer;
        protected Graphics bufferGraphics;
        protected Rectangle bufferRect;

        public FNTFont(FNTFont font)
        {
            this.cwidth = font.cwidth;
            this.cheight = font.cheight;
            this.flags = font.flags;
            this.minchar = font.minchar;
            this.maxchar = font.maxchar;
            this.fontData = font.fontData;
            this.widths = font.widths;
            this.kerns = font.kerns;
            this.isProportional = font.isProportional;
            this.hasKerning = font.hasKerning;
            this.colored = font.colored;
            this.palette = font.palette;
            this.buffer = new Bitmap(font.buffer);
            this.bufferGraphics = Graphics.FromImage(this.buffer);
            this.bufferRect = new Rectangle(new Point(0, 0), this.buffer.Size);
        }

        public FNTFont(Stream stream)
        {
            kerns = new Dictionary<int, int>();
            using (BinaryReader br = new BinaryReader(stream))
            {
                byte[] first8 = br.ReadBytes(8);
                // PSFN : 50 53 46 4e
                if (first8.Length < 8 || first8[0] != 0x50 || first8[1] != 0x53 || first8[2] != 0x46 || first8[3] != 0x4e)
                    throw new ArgumentException("file is not a valid Parallax FNT font");
                cwidth = br.ReadInt16();
                cheight = br.ReadInt16();
                flags = br.ReadInt16();
                colored = (flags & 1) != 0;
                palette = colored ? new byte[768] : null;

                isProportional = (flags & 2) != 0;
                hasKerning = (flags & 4) != 0;

                br.ReadInt16(); // baseline, not used right now
                minchar = Convert.ToChar(br.ReadByte());
                maxchar = Convert.ToChar(br.ReadByte());
                br.ReadInt16(); // byte width, not used right now
                int dataPointer = br.ReadInt32() + 8;
                br.ReadInt32(); // chars - always 0
                int widthPointer = br.ReadInt32() + 8;
                int kernPointer = br.ReadInt32() + 8;

                int charCount = maxchar - minchar + 1;

                // widths
                widths = new short[charCount];
                if (isProportional)
                {
                    stream.Seek(widthPointer, SeekOrigin.Begin);
                    for (int i = 0; i < charCount; ++i)
                    {
                        widths[i] = br.ReadInt16();
                    }
                }
                else
                {
                    for (int i = 0; i < charCount; ++i)
                    {
                        widths[i] = cwidth;
                    }
                }

                int maxWidth = cwidth;
                for (int i = 0; i < charCount; ++i)
                {
                    maxWidth = Math.Max(widths[i], maxWidth);
                }
                buffer = new Bitmap(maxWidth, cheight, PixelFormat.Format32bppArgb);
                bufferGraphics = Graphics.FromImage(buffer);
                bufferRect = new Rectangle(new Point(0, 0), this.buffer.Size);

                if (hasKerning)
                {
                    // kerns
                    stream.Seek(kernPointer, SeekOrigin.Begin);
                    while (true)
                    {
                        char first = Convert.ToChar(br.ReadByte());
                        if (first >= 0xFF)
                        {
                            break;
                        }
                        char second = Convert.ToChar(br.ReadByte());
                        kerns[(first << 8) + second] = br.ReadByte();
                    }
                }

                fontData = new byte[charCount][];

                stream.Seek(dataPointer, SeekOrigin.Begin);
                for (int i = 0; i < charCount; ++i)
                {
                    int byteWidth = colored ? widths[i] : (widths[i] + 7) / 8;
                    fontData[i] = br.ReadBytes(byteWidth * cheight);
                }

                if (colored)
                {
                    stream.Seek(-palette.Length, SeekOrigin.End);
                    palette = br.ReadBytes(palette.Length);
                }
            }
            ResetKerning();
        }

        public void ResetKerning()
        {
            lastChar = '\0';
        }

        public bool IsCharInFont(char c)
        {
            return minchar <= c && c <= maxchar;
        }

        public int GetCharWidth(char c)
        {
            if (IsCharInFont(c))
            {
                return widths[c - minchar];
            }
            else
            {
                return isProportional ? cwidth / 2 : cwidth;
            }
        }

        public int GetCharHeight()
        {
            return cheight;
        }

        private int GetKernOffset(char c)
        {
            int kernIndex = (lastChar << 8) + c;
            if (hasKerning && kerns.ContainsKey(kernIndex))
            {
                return kerns[kernIndex] - GetCharWidth(lastChar);
            }
            else
            {
                return 0;
            }
        }

        public virtual int MeasureWidth(string s)
        {
            char oldLastChar = lastChar;
            int x = 0, w;

            ResetKerning();
            foreach (char c in s)
            {
                w = GetCharWidth(c);
                x += w;
                x += GetKernOffset(c);
                lastChar = c;
            }

            lastChar = oldLastChar;
            return x + 1;
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public virtual void DrawCharacterRaw(Bitmap b, char c, Color clr, ref int x, int y)
        {
            int thisWidth = GetCharWidth(c);

            if (IsCharInFont(c))
            {
                byte[] charData = fontData[c - minchar];
                if (x < -thisWidth || x > b.Width || y < -cheight || y > b.Height)
                    return;

                bufferGraphics.Clear(Color.Transparent);
                BitmapData data = buffer.LockBits(bufferRect, System.Drawing.Imaging.ImageLockMode.ReadWrite, buffer.PixelFormat);

                byte cr = clr.R;
                byte cg = clr.G;
                byte cb = clr.B;

                int cptr = 0;
                IntPtr ptr = data.Scan0;
                int bytes = data.Stride * data.Height;
                byte[] rgbValues = new byte[bytes];
                System.Runtime.InteropServices.Marshal.Copy(ptr, rgbValues, 0, bytes);

                for (int yo = 0; yo < cheight; ++yo)
                {
                    int p = yo * data.Stride;
                    if (colored)
                    {
                        for (int xo = 0; xo < thisWidth; ++xo)
                        {
                            byte color = charData[cptr++];
                            if (color < 255)
                            {
                                rgbValues[p + xo * 4] = (byte)(palette[color * 3 + 2] << 2);
                                rgbValues[p + xo * 4 + 1] = (byte)(palette[color * 3 + 1] << 2);
                                rgbValues[p + xo * 4 + 2] = (byte)(palette[color * 3] << 2);
                                rgbValues[p + xo * 4 + 3] = 255;
                            }
                        }
                    }
                    else
                    {
                        for (int xo = 0; xo < thisWidth; xo += 8)
                        {
                            byte sliver = charData[cptr++];
                            for (int xs = 0; xs < 8; ++xs)
                            {
                                if (xo + xs >= thisWidth)
                                    break;
                                if ((sliver & 0x80) != 0)
                                {
                                    rgbValues[p + (xo + xs) * 4] = cb;
                                    rgbValues[p + (xo + xs) * 4 + 1] = cg;
                                    rgbValues[p + (xo + xs) * 4 + 2] = cr;
                                    rgbValues[p + (xo + xs) * 4 + 3] = 255;
                                }
                                sliver <<= 1;
                            }
                        }
                    }
                }

                System.Runtime.InteropServices.Marshal.Copy(rgbValues, 0, ptr, bytes);
                buffer.UnlockBits(data);

                // blit
                Graphics.FromImage(b).DrawImage(buffer, x, y);
            }

            x += thisWidth;
            x += GetKernOffset(c);
            lastChar = c;
        }

        public virtual void DrawCharacter(Bitmap b, char c, Color fg, Color bg, ref int x, int y, bool shadow)
        {
            if (shadow)
            {
                int dummy = x + 1;
                DrawCharacterRaw(b, c, bg, ref x, y);
                DrawCharacterRaw(b, c, fg, ref dummy, y);
            }
            else
            {
                DrawCharacterRaw(b, c, fg, ref x, y);
            }
        }
    }
}
