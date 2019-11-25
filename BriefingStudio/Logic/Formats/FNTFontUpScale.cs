using System;
using System.Drawing;
using System.Drawing.Imaging;

namespace BriefingStudio
{
    public class FNTFontUpScale : FNTFont
    {
        public FNTFontUpScale(FNTFont font) : base(font)
        {
        }


        public override void DrawCharacterRaw(Bitmap b, char c, Color clr, ref int x, int y)
        {
            int thisWidth = GetCharWidth(c);

            if (IsCharInFont(c))
            {
                byte[] charData = fontData[c - minchar];

                BitmapData data = b.LockBits(new Rectangle(x, y, thisWidth * 2, cheight * 2), System.Drawing.Imaging.ImageLockMode.ReadWrite, b.PixelFormat);
                byte cr = clr.R;
                byte cg = clr.G;
                byte cb = clr.B;

                int cptr = 0;
                IntPtr ptr = data.Scan0;
                int bytes = Math.Abs(data.Stride) * cheight * 2;
                byte[] rgbValues = new byte[bytes];
                System.Runtime.InteropServices.Marshal.Copy(ptr, rgbValues, 0, bytes);

                for (int yo = 0; yo < cheight; ++yo)
                {
                    int p = yo * 2 * data.Stride;
                    int q = (yo * 2 + 1) * data.Stride;
                    for (int xo = 0; xo < thisWidth; xo += 8)
                    {
                        byte sliver = charData[cptr++];
                        for (int xs = 0; xs < 8; ++xs)
                        {
                            if (xo + xs >= thisWidth)
                                break;
                            if ((sliver & 0x80) != 0)
                            {
                                rgbValues[p + (xo + xs) * 6] = cb;
                                rgbValues[p + (xo + xs) * 6 + 1] = cg;
                                rgbValues[p + (xo + xs) * 6 + 2] = cr;
                                rgbValues[p + (xo + xs) * 6 + 3] = cb;
                                rgbValues[p + (xo + xs) * 6 + 4] = cg;
                                rgbValues[p + (xo + xs) * 6 + 5] = cr;
                                rgbValues[q + (xo + xs) * 6] = cb;
                                rgbValues[q + (xo + xs) * 6 + 1] = cg;
                                rgbValues[q + (xo + xs) * 6 + 2] = cr;
                                rgbValues[q + (xo + xs) * 6 + 3] = cb;
                                rgbValues[q + (xo + xs) * 6 + 4] = cg;
                                rgbValues[q + (xo + xs) * 6 + 5] = cr;
                            }
                            sliver <<= 1;
                        }
                    }
                }

                System.Runtime.InteropServices.Marshal.Copy(rgbValues, 0, ptr, bytes);
                b.UnlockBits(data);
            }

            x += thisWidth * 2;
            x += GetKernOffset(c) * 2;
            lastChar = c;
        }
    }
}