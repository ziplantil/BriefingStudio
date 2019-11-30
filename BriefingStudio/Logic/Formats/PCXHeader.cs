using System;
using System.Drawing;

namespace BriefingStudio.Logic.Formats
{
    public class PCXHeader
    {
        public byte Manufacturer;
        public byte Version;
        public byte Encoding;
        public byte BitsPerPixel;
        public short Xmin;
        public short Ymin;
        public short Xmax;
        public short Ymax;
        public short Hdpi;
        public short Vdpi;
        public Color[] ColorMap;
        public byte NPlanes;
        public short BytesPerLine;

        public PCXHeader(byte[] block)
        {
            Manufacturer = block[0];
            Version = block[1];
            Encoding = block[2];
            BitsPerPixel = block[3];
            Xmin = BitConverter.ToInt16(block, 4);
            Ymin = BitConverter.ToInt16(block, 6);
            Xmax = BitConverter.ToInt16(block, 8);
            Ymax = BitConverter.ToInt16(block, 10);
            Hdpi = BitConverter.ToInt16(block, 12);
            Vdpi = BitConverter.ToInt16(block, 14);
            ColorMap = new Color[16];
            for (int i = 0; i < 16; ++i)
            {
                ColorMap[i] = PCXDecoder.ReadRGB(block, 16 + 3 * i);
            }
            NPlanes = block[65];
            Vdpi = BitConverter.ToInt16(block, 66);
        }
    }
}