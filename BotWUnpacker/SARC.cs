
//Modified code From Uwizard master branch as of 10/16/2017


namespace BotWUnpacker
{
    public struct SARC
    {
        public static string lerror = ""; // Gets the last error that occurred in this struct. Similar to the C perror().

        #region Structures
        private struct SarcNode //SARC node row (extract only)
        {
            public uint hash;
            public byte unknown;
            public uint offset, start, end;

            public SarcNode(uint fhash, byte unk, uint foffset, uint fstart, uint fend)
            {
                hash = fhash; //NOT USED
                unknown = unk; //NOT USED
                offset = foffset; //NOT USED
                start = fstart;
                end = fend;
            }
        }

        private struct fileHash
        {
            public uint hash;
            public int index;

            public fileHash(uint h, int i)
            {
                hash = h;
                index = i;
            }
        }

        private struct filedata
        {
            public string filename, realname;
            public uint filesize, namesize; 
            public int filenum;

            public filedata(string _filename, string _realname, uint _filesize, uint _namesize, int _filenum)
            {
                filename = _filename;
                realname = _realname;
                filesize = _filesize;
                namesize = _namesize;
                filenum = _filenum;
            }
        }
        #endregion

        #region Conversions
        private static ushort makeu16(byte b1, byte b2) //16-bit change (ushort, 0xFFFF)
        {
            return (ushort)(((ushort)b1 << 8) | (ushort)b2);
        }

        private static uint makeu32(byte b1, byte b2, byte b3, byte b4) //32-bit change (uint, 0xFFFFFFFF)
        {
            return ((uint)b1 << 24) | ((uint)b2 << 16) | ((uint)b3 << 8) | (uint)b4;
        }

        private static byte[] breaku16(ushort u16) //Byte change from 16-bits (byte, 0xFF, 0xFF)
        {
            return new byte[] { (byte)(u16 >> 8), (byte)(u16 & 0xFF) };
        }

        private static byte[] breaku32(uint u32) //Byte change from 32-bits (byte, 0xFF, 0xFF, 0xFF, 0xFF)
        {
            return new byte[] { (byte)(u32 >> 24), (byte)((u32 >> 16) & 0xFF), (byte)((u32 >> 8) & 0xFF), (byte)(u32 & 0xFF) };
        }

        private static void makeDirExist(string dir)
        {
            string dirPath = System.IO.Path.GetFullPath(dir);
            int numDirs = 0;
            for (int i = 0; i < dirPath.Length; i++)
                if (dirPath[i] == '\\') numDirs++;

            for (int j = numDirs; j >= 0; j--)
            {
                string tmp = dirPath;
                for (int k = 0; k < j; k++)
                    tmp = System.IO.Path.GetDirectoryName(tmp);
                if (!System.IO.Directory.Exists(tmp))
                    System.IO.Directory.CreateDirectory(tmp);
            }
        }
        #endregion

        #region Constructors
        private static uint calchash(string name)
        {
            ulong result = 0;
            for (int i = 0; i < name.Length; i++)
            {
                result = (((byte)name[i]) + (result * 0x65)) & 0xFFFFFFFF;
            }
            return (uint)(result & 0xFFFFFFFF);
        }

        private static string[] getfiles(string dir)
        {
            if (dir == "") dir = System.Environment.CurrentDirectory;
            return System.IO.Directory.GetFiles(dir);
        }

        private static string[] getdirs(string dir)
        {
            if (dir == "") dir = System.Environment.CurrentDirectory;
            return System.IO.Directory.GetDirectories(dir);
        }

        private static uint getfilesize(string fpath)
        {
            System.IO.StreamReader sr = new System.IO.StreamReader(fpath);
            uint fs = (uint)sr.BaseStream.Length;
            sr.Close();
            sr.Dispose();
            return fs;
        }
        #endregion

        #region Extract
        public static bool Extract(string inFile, string outDir) //EXTRACT ----------------------------------------------------------------------
        {
            return Extract(System.IO.File.ReadAllBytes(inFile), outDir); //default
        }

        public static bool Extract(byte[] inFile, string outDir)
        {

            //SARC header 0x00 - 0x13
            if (inFile[0] != 'S' || inFile[1] != 'A' || inFile[2] != 'R' || inFile[3] != 'C')
            {
                lerror = "Not a SARC archive! (Missing SARC header at 0x00)";
                return false;
            }
            int pos = 4; //0x04
            ushort hdr = makeu16(inFile[pos], inFile[pos + 1]); //SARC Header length
            pos += 2; //0x06
            ushort bom = makeu16(inFile[pos], inFile[pos + 1]); //Byte Order Mark
            if (bom != 65279) //Check 0x06 for Byte Order Mark (if not 0xFEFF)
            {
                if (bom == 65518) lerror = "Unable to support Little Endian! (Byte Order Mark 0x06)";
                else lerror = "Unknown SARC header (Byte Order Mark 0x06)";
                return false;
            }
            pos += 2; //0x08
            uint fileSize = makeu32(inFile[pos], inFile[pos + 1], inFile[pos + 2], inFile[pos + 3]); 
            pos += 4; //0x0C
            uint dataOffset = makeu32(inFile[pos], inFile[pos + 1], inFile[pos + 2], inFile[pos + 3]); //Data offset start position
            pos += 4; //0x10
            uint unknown = makeu32(inFile[pos], inFile[pos + 1], inFile[pos + 2], inFile[pos + 3]); //unknown, always 0x01?
            pos += 4; //0x14

            //SFAT header 0x14 - 0x1F
            if (inFile[pos] != 'S' || inFile[pos + 1] != 'F' || inFile[pos + 2] != 'A' || inFile[pos + 3] != 'T')
            {
                lerror = "Unknown file table! (Missing SFAT header at 0x14)";
                return false;
            }
            pos += 4; //0x18
            ushort hdr2 = makeu16(inFile[pos], inFile[pos + 1]); //SFAT Header length
            pos += 2; //0x1A
            ushort nodeCount = makeu16(inFile[pos], inFile[pos + 1]); //Node Cluster count
            pos += 2; //0x1C
            uint hashr = makeu32(inFile[pos], inFile[pos + 1], inFile[pos + 2], inFile[pos + 3]); //Hash multiplier, always 0x65
            pos += 4; //0x20

            SarcNode[] nodes = new SarcNode[nodeCount];
            SarcNode tmpnode = new SarcNode();

            for (int i = 0; i < nodeCount; i++) //Node cluster 
            {
                tmpnode.hash = makeu32(inFile[pos], inFile[pos + 1], inFile[pos + 2], inFile[pos + 3]);
                pos += 4; //0x?4
                tmpnode.unknown = inFile[pos]; //unknown, always 0x01? (not used in this case)
                pos += 1; //0x?5
                tmpnode.offset = makeu32(0, inFile[pos], inFile[pos + 1], inFile[pos + 2]); //Node SFNT filename offset divided by 4 (not used)
                pos += 3; //0x?8
                tmpnode.start = makeu32(inFile[pos], inFile[pos + 1], inFile[pos + 2], inFile[pos + 3]); //Start Data offset position
                pos += 4; //0x?C
                tmpnode.end = makeu32(inFile[pos], inFile[pos + 1], inFile[pos + 2], inFile[pos + 3]); //End Data offset position
                pos += 4; //0x?0
                nodes[i] = tmpnode; //Store node data into array
            }

            if (inFile[pos] != 'S' || inFile[pos + 1] != 'F' || inFile[pos + 2] != 'N' || inFile[pos + 3] != 'T')
            {
                string posOffset = "0x" + pos.ToString("X");
                lerror = "Unknown file name table! (Missing SFNT header at " + posOffset +")";
                return false;
            }
            pos += 4; //0x?4
            ushort hdr3 = makeu16(inFile[pos], inFile[pos + 1]); //SFNT Header length, always 0x08
            pos += 2; //0x?6
            ushort unk2 = makeu16(inFile[pos], inFile[pos + 1]); //unknown, always 0x00?
            pos += 2; //0x?8

            string[] fileNames = new string[nodeCount];
            string tempName;

            for (int i = 0; i < nodeCount; i++) //Get file names for each node
            {
                tempName = ""; //reset for each file
                while (inFile[pos] != 0)
                {
                    tempName = tempName + ((char)inFile[pos]).ToString(); //Build temp string for each letter
                    pos += 1;
                }
                while (inFile[pos] == 0) //ignore every 0 byte, because why bother calculating the SFNT header offset anyway?
                    pos += 1;
                fileNames[i] = tempName; //Take built string and store it in the array
            }

            if (!System.IO.Directory.Exists(outDir)) System.IO.Directory.CreateDirectory(outDir); //folder creation
            System.IO.StreamWriter stream;

            for (int i = 0; i < nodeCount; i++) //Write files based from node information
            {
                makeDirExist(System.IO.Path.GetDirectoryName(outDir + "/" + fileNames[i]));
                stream = new System.IO.StreamWriter(outDir + "/" + fileNames[i]);
                stream.BaseStream.Write(inFile, (int)(nodes[i].start + dataOffset), (int)(nodes[i].end - nodes[i].start)); //Write 
                stream.Close();
                stream.Dispose();
            }

            return true;
        } //--------------------------------------------------------------------------------------------------------------------------------------------
        #endregion

        #region Build
        public static bool build(string inDir, string outFile) //BUILD ----------------------------------------------------------------------
        {
            return build(inDir, outFile, 0x100);
        }

        public static bool build(string inDir, string outFile, uint padding) 
        {
            if (!System.IO.Directory.Exists(inDir)) return false;

            string[] inDirFiles = System.IO.Directory.GetFiles(inDir == "" ? System.Environment.CurrentDirectory : inDir, "*.*", System.IO.SearchOption.AllDirectories);

            filedata[] fileDataList = new filedata[inDirFiles.Length];
            uint nodeFileSize = 0;
            uint lennames = 0;
            int numFiles = inDirFiles.Length;
            uint fileSize;
            for (int i = 0; i < inDirFiles.Length; i++)
            {
                string realname = inDirFiles[i];
                string fileName = inDirFiles[i].Replace(inDir + System.IO.Path.DirectorySeparatorChar.ToString(), "");
                fileName = fileName.Replace("\\","/"); //HURP DERP

                fileSize = getfilesize(realname);
                if (fileSize % padding > 0) fileSize += (padding - (fileSize % padding));
                uint namesize = (uint)fileName.Length;
                namesize += (4 - (namesize % 4));
                lennames += namesize;
                fileDataList[i] = new filedata(fileName, realname, fileSize, namesize, numFiles);
            }

            fileHash[] hashesUnsorted = new fileHash[numFiles];

            for (int i = 0; i < numFiles; i++)
            {
                hashesUnsorted[i] = new fileHash(calchash(fileDataList[i].filename), i); //Calc Hash
            }

            //Sort hash calculations of the file name
            uint lhash;
            bool[] hashes_done = new bool[hashesUnsorted.Length];
            fileHash[] hashes = new fileHash[hashesUnsorted.Length];
            int dhi = 0;
            for (int i = 0; i < hashes.Length; i++)
            {
                lhash = uint.MaxValue;
                for (int j = 0; j < hashesUnsorted.Length; j++)
                {
                    if (hashes_done[j]) continue;
                    if (hashesUnsorted[j].hash < lhash)
                    {
                        dhi = j;
                        lhash = hashesUnsorted[j].hash;
                    }
                }
                hashes_done[dhi] = true;
                hashes[i] = hashesUnsorted[dhi];
            }

            for (int i = 0; i < numFiles; i++)
            {
                nodeFileSize += fileDataList[hashes[i].index].filesize;
            }
            uint lastfile = getfilesize(fileDataList[hashes[hashes.Length - 1].index].realname);
            nodeFileSize += lastfile;
            fileSize = (uint)(128 + (16 * numFiles) + 8 + lennames);
            uint padSFAT = (padding - (fileSize % padding));
            uint datastart = padSFAT + fileSize;
            fileSize += (uint)(padSFAT + nodeFileSize);

            //Write 
            System.IO.StreamWriter stream = new System.IO.StreamWriter(outFile);
            stream.BaseStream.Write(new byte[] { 83, 65, 82, 67, 0x00, 0x14, 0xFE, 0xFF }, 0, 8); //Write Fixed SARC Big Endian header
            stream.BaseStream.Write(breaku32(fileSize), 0, 4); //Write 0x08 split bytes of file size
            stream.BaseStream.Write(breaku32(datastart), 0, 4); //Write 0x0C split bytes of data table start offset
            stream.BaseStream.Write(new byte[] { 0x01, 0x00, 0x00, 0x00, 83, 70, 65, 84, 0x00, 0x0C }, 0, 10); //Write Fixed SFAT header
            stream.BaseStream.Write(breaku16((ushort)numFiles), 0, 2); //Write 0x1A split bytes of number of nodes/files
            stream.BaseStream.Write(breaku32(0x65), 0, 4); //Write Fixed Hash Multiplier 
            uint strpos = 0, filepos = 0;
            //SFAT node info
            for (int i = 0; i < numFiles; i++)
            {
                stream.BaseStream.Write(breaku32(hashes[i].hash), 0, 4); //Node Hash 
                stream.BaseStream.WriteByte(0x01); //Node Fixed Unknown
                stream.BaseStream.Write(breaku32((strpos >> 2)), 1, 3); //Node filename offset position (divided by 4)
                strpos += fileDataList[hashes[i].index].namesize; 
                stream.BaseStream.Write(breaku32(filepos), 0, 4); //Node start data offset position
                fileSize = getfilesize(fileDataList[hashes[i].index].realname);
                stream.BaseStream.Write(breaku32(filepos + fileSize), 0, 4); //Node end data offset position
                filepos += fileDataList[hashes[i].index].filesize;
            }

            stream.BaseStream.Write(new byte[] { 83, 70, 78, 84, 0x00, 0x08, 0x00, 0x00 }, 0, 8); //Write Fixed SFNT headerr
            for (int i = 0; i < numFiles; i++)
            {
                string fileName = fileDataList[hashes[i].index].filename;
                for (int j = 0; j < fileName.Length; j++)
                {
                    stream.BaseStream.WriteByte((byte)fileName[j]); //Write file names
                }
                int numpad0 = (int)fileDataList[hashes[i].index].namesize - fileDataList[hashes[i].index].filename.Length; //short padding for file offset location (to be divisible by 4)
                for (int j = 0; j < numpad0; j++)
                    stream.BaseStream.WriteByte(0);
            }
            for (int i = 0; i < padSFAT; i++) //Padding
                stream.BaseStream.WriteByte(0);

            byte[] tmp;
            for (int i = 0; i < numFiles; i++)
            {
                tmp = System.IO.File.ReadAllBytes(fileDataList[hashes[i].index].realname);
                stream.BaseStream.Write(tmp, 0, tmp.Length);
                fileSize = (uint)tmp.Length;
                if (i < numFiles - 1)
                {
                    int numpad0 = (int)(fileDataList[hashes[i].index].filesize - fileSize);
                    for (int j = 0; j < numpad0; j++) //Padding
                        stream.BaseStream.WriteByte(0);
                }
            }

            stream.Close();
            stream.Dispose();


            return true;
        } //--------------------------------------------------------------------------------------------------------------------------------------------
        #endregion
    }
}