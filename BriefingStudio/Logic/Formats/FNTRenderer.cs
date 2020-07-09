using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.CompilerServices;

namespace BriefingStudio.Logic.Formats
{
    public class FNTRenderer
    {
        internal LibDescent.Data.Font font;
        protected Bitmap buffer;
        protected Graphics bufferGraphics;
        protected Rectangle bufferRect;
        private char prevChar;

        public FNTRenderer(LibDescent.Data.Font fnt)
        {
            font = fnt;
            buffer = new Bitmap(fnt.MaxWidth, fnt.Height, PixelFormat.Format32bppArgb);
            bufferGraphics = Graphics.FromImage(buffer);
            bufferRect = new Rectangle(new Point(0, 0), this.buffer.Size);
            Reset();
        }

        public void Reset()
        {
            this.prevChar = '\0';
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public virtual void DrawCharacterRaw(Bitmap b, char c, Color clr, ref int x, int y)
        {
            int thisWidth = font.GetCharWidth(c);

            if (font.GetCharacterOffset(c, out int offset, out int size))
            {
                //byte[] charData = font.fontData[c - minchar];
                if (x < -thisWidth || x > b.Width || y < -this.font.Height || y > b.Height)
                    return;

                bufferGraphics.Clear(Color.Transparent);
                BitmapData data = buffer.LockBits(bufferRect, System.Drawing.Imaging.ImageLockMode.ReadWrite, buffer.PixelFormat);

                byte cr = clr.R;
                byte cg = clr.G;
                byte cb = clr.B;

                int cptr = offset;
                IntPtr ptr = data.Scan0;
                int bytes = data.Stride * data.Height;
                byte[] rgbValues = new byte[bytes];
                System.Runtime.InteropServices.Marshal.Copy(ptr, rgbValues, 0, bytes);

                for (int yo = 0; yo < font.Height; ++yo)
                {
                    int p = yo * data.Stride;
                    if ((font.Flags & LibDescent.Data.Font.FT_COLOR) != 0)
                    {
                        for (int xo = 0; xo < thisWidth; ++xo)
                        {
                            byte color = font.FontData[cptr++];
                            if (color < 255)
                            {
                                rgbValues[p + xo * 4] = (byte)(font.Palette[color * 3 + 2] << 2);
                                rgbValues[p + xo * 4 + 1] = (byte)(font.Palette[color * 3 + 1] << 2);
                                rgbValues[p + xo * 4 + 2] = (byte)(font.Palette[color * 3] << 2);
                                rgbValues[p + xo * 4 + 3] = 255;
                            }
                        }
                    }
                    else
                    {
                        for (int xo = 0; xo < thisWidth; xo += 8)
                        {
                            byte sliver = font.FontData[cptr++];
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
            x += font.GetKernOffset(c, prevChar);
            prevChar = c;
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
