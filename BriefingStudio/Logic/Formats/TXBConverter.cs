using System.IO;

namespace BriefingStudio
{
    class TXBConverter
    {
        public static string DecodeTXB(byte[] txb)
        {
            string res = "";
            foreach (byte b in txb)
            {
                if (b == 0x0a)
                {
                    res += "\n";
                }
                else
                {
                    int v = 0xa7 ^ (((b & 0x3f) << 2) | ((b & 0xc0) >> 6));
                    res += (char)v;
                }
            }
            return res;
        }

        public static byte[] EncodeTXB(string txt)
        {
            MemoryStream temp = new MemoryStream();
            foreach (char c in txt)
            {
                if (c == '\r')
                {
                }
                else if (c == '\n')
                {
                    temp.WriteByte(0x0a);
                }
                else
                {
                    int v = (int)c;
                    temp.WriteByte((byte)(0xe9 ^ (((v & 0xfc) >> 2) | ((v & 0x03) << 6))));
                }
            }
            return temp.ToArray();
        }
    }
}
