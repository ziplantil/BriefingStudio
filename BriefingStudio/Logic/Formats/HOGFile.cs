using System;
using System.IO;
using System.Text;

namespace BriefingStudio
{
    class HOGFile : IDisposable
    {
        string fileName;
        FileStream myFs;
        BinaryReader br;
        private volatile Object fileLock = new Object();

        public HOGFile(string fileName)
        {
            this.fileName = fileName;
            myFs = File.Open(fileName, FileMode.Open, FileAccess.Read, FileShare.Read);
            br = new BinaryReader(myFs);
            ValidateHOG(myFs);
        }

        public void Dispose()
        {
            myFs.Close();
        }

        private void ValidateHOG(FileStream fs)
        {
            byte[] header = new byte[3];
            if (fs.Read(header, 0, 3) != 3)
            {
                throw new ArgumentException("file is not a valid .HOG file");
            }

            // DHF : 44 48 46
            if (header[0] != 0x44 || header[1] != 0x48 || header[2] != 0x46)
            {
                throw new ArgumentException("file is not a valid .HOG file");
            }
        }

        private static string ConvertFileName(byte[] header)
        {
            int nullLength = Array.IndexOf<Byte>(header, 0);
            if (nullLength < 0)
            {
                nullLength = 13;
            }
            return Encoding.ASCII.GetString(header, 0, nullLength);
        }

        private HOGResource ReadResource()
        {
            byte[] header = br.ReadBytes(17);
            if (header.Length < 17)
            {
                return null;
            }
            long offset = myFs.Position;
            int length = Utils.ToLittleEndianInt32(header, 13);
            myFs.Seek(length, SeekOrigin.Current);
            return new HOGResource(ConvertFileName(header), offset, length);
        }

        public bool HasFile(string name)
        {
            lock (fileLock)
            {
                myFs.Seek(3, SeekOrigin.Begin);
                HOGResource res;
                while ((res = ReadResource()) != null)
                {
                    if (res.Name.Equals(name, StringComparison.CurrentCultureIgnoreCase))
                    {
                        return true;
                    }
                }
                return false;
            }
        }

        public byte[] GetFile(string name)
        {
            lock (fileLock)
            {
                myFs.Seek(3, SeekOrigin.Begin);
                HOGResource res;
                while ((res = ReadResource()) != null)
                {
                    if (res.Name.Equals(name, StringComparison.CurrentCultureIgnoreCase))
                    {
                        myFs.Seek(res.Offset, SeekOrigin.Begin);
                        return br.ReadBytes(res.Length);
                    }
                }
                return null;
            }
        }

        public void PutFile(string name, byte[] data)
        {
            lock (fileLock)
            {
                // ok, write attempt
                try
                {
                    myFs.Seek(3, SeekOrigin.Begin);
                    HOGResource res;

                    // create new temp HOG file
                    string tmpFn = Path.GetTempFileName();

                    using (FileStream gs = new FileStream(tmpFn, FileMode.Create, FileAccess.Write))
                    using (BinaryWriter bw = new BinaryWriter(gs))
                    {
                        bool append = true;
                        bw.Write(new byte[] { 0x44, 0x48, 0x46 }); // DHF
                        while ((res = ReadResource()) != null)
                        {
                            bw.Write(Utils.ASCIIToBytesPad(res.Name, 13));
                            if (res.Name.Equals(name, StringComparison.CurrentCultureIgnoreCase))
                            {
                                // ok, replace existing
                                bw.Write(Utils.Int32LittleEndianToBytes(data.Length));
                                bw.Write(data);
                                append = false;
                            }
                            else
                            {
                                bw.Write(Utils.Int32LittleEndianToBytes(res.Length));
                                myFs.Seek(res.Offset, SeekOrigin.Begin);
                                Utils.StreamCopy(gs, myFs, res.Length);
                            }
                        }

                        if (append)
                        {
                            bw.Write(Utils.ASCIIToBytesPad(name, 13));
                            bw.Write(Utils.Int32LittleEndianToBytes(data.Length));
                            bw.Write(data);
                        }
                    }

                    myFs.Close();
                    File.Replace(tmpFn, fileName, fileName + ".bkp");
                }
                finally
                {
                    myFs = File.Open(fileName, FileMode.Open, FileAccess.Read, FileShare.Read);
                    br = new BinaryReader(myFs);
                }
            }
        }
    }
}
