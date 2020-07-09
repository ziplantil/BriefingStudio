using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace BriefingStudio.Logic.Formats
{
    public class FNTRendererUpScale : FNTRenderer
    {
        Bitmap frame = null;
        Graphics frameGraphics;
        Rectangle frameRect;

        public FNTRendererUpScale(LibDescent.Data.Font font) : base(font)
        {
        }

        public override void DrawCharacterRaw(Bitmap b, char c, Color clr, ref int x, int y)
        {
            if (this.font.IsCharInFont(c))
            {
                // call DrawCharacterRaw and upscale
                if (frame == null)
                {
                    frame = new Bitmap(this.font.Width * 4, this.font.Height, PixelFormat.Format32bppArgb);
                    frameGraphics = Graphics.FromImage(frame);
                    frameRect = new Rectangle(new Point(0, 0), frame.Size);
                }
                frameGraphics.Clear(Color.Transparent);
                int ox = 0;

                base.DrawCharacterRaw(frame, c, clr, ref ox, 0);
                if (ox > 0)
                {
                    using (Graphics g = Graphics.FromImage(b))
                    {
                        g.InterpolationMode = InterpolationMode.NearestNeighbor;    
                        g.DrawImage(frame, new Rectangle(x, y, frame.Width * 2 + 1, frame.Height * 2 + 1), frameRect, GraphicsUnit.Pixel);
                    }
                    x += ox * 2;
                }
            }
        }

        public override void DrawCharacter(Bitmap b, char c, Color fg, Color bg, ref int x, int y, bool shadow)
        {
            if (shadow)
            {
                int dummy = x + 2;
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