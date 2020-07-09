using LibDescent.Data;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BriefingStudio.Logic.Formats
{
    public class PCXDecoder
    {
        public Bitmap LoadPCX(Stream fs, out System.Drawing.Color[] palette)
        {
            PCXImage img = new PCXImage();
            img.LoadPCX(fs, out LibDescent.Data.Color[] rawPalette);

            Bitmap res = new Bitmap(img.Width, img.Height, PixelFormat.Format24bppRgb);
            BitmapData bmpData =
                res.LockBits(new Rectangle(0, 0, img.Width, img.Height),
                System.Drawing.Imaging.ImageLockMode.ReadWrite,
                res.PixelFormat);
            IntPtr ptr = bmpData.Scan0;
            int bytes = Math.Abs(bmpData.Stride) * res.Height;
            System.Runtime.InteropServices.Marshal.Copy(img.Data, 0, ptr, bytes);
            res.UnlockBits(bmpData);

            palette = Utils.LDPaletteToGDIPalette(rawPalette);
            return res;
        }

        public Bitmap LoadPCX(string filePath, out System.Drawing.Color[] palette)
        {
            using (FileStream fs = File.Open(filePath, FileMode.Open))
            {
                return LoadPCX(fs, out palette);
            }
        }

        public Bitmap LoadPCX(byte[] contents, out System.Drawing.Color[] palette)
        {
            using (MemoryStream ms = new MemoryStream(contents))
            {
                return LoadPCX(ms, out palette);
            }
        }
    }
}
