using System;
using System.Linq;
using System.IO;
//Modified code From Uwizard SARC master branch as of 10/16/2017, 


namespace BotwUnpacker
{
    public struct SARC
    {
        public static string lerror = ""; // Gets the last error

        #region Structures
        private struct SarcNode //SARC node row (extract only)
        {
            public uint hash;
            public byte unknown;
            public uint offset, start, end;

            public SarcNode(uint fhash, byte unk, uint foffset, uint fstart, uint fend)
            {
                hash = fhash; //NOT USED for extracting
                unknown = unk; //NOT USED for extracting
                offset = foffset; //NOT USED for extracting
                start = fstart;
                end = fend;
            }
        }

        private struct NodeHash //Build only
        {
            public uint hash;
            public int index;
            public string fileName;

            public NodeHash(uint h, int i, string f)
            {
                hash = h;
                index = i;
                fileName = f;
            }
        }

        private struct NodeInfo //Build only
        {
            public string filename, realname;
            public uint namesize;

            public NodeInfo(string inFileName, string inRealName, uint inNameSize)
            {
                filename = inFileName;
                realname = inRealName;
                namesize = inNameSize;
            }

        }

        private struct NodeData //Build only
        {
            public byte[] data;
            public uint startPos, endPos, padding;

            public NodeData(byte[] inData, uint inStartPos, uint inEndPos)
            {
                data = inData;
                startPos = inStartPos;
                endPos = inEndPos;
                padding = 0; //blank 
            }
        }
        #endregion

        #region Conversions
        private static ushort Makeu16(byte b1, byte b2) //16-bit change (ushort, 0xFFFF)
        {
            return Makeu16(b1, b2, false);
        }

        private static ushort Makeu16(byte b1, byte b2, bool swapEndian) //16-bit change (ushort, 0xFFFF)
        {
            if (swapEndian)
                return SwapShortEndian((ushort)(((ushort)b1 << 8) | (ushort)b2));
            else
                return (ushort)(((ushort)b1 << 8) | (ushort)b2);
        }

        private static uint Makeu32(byte b1, byte b2, byte b3, byte b4) //32-bit change (uint, 0xFFFFFFFF)
        {
            return Makeu32(b1, b2, b3, b4, false);
        }

        private static uint Makeu32(byte b1, byte b2, byte b3, byte b4, bool swapEndian) //32-bit change (uint, 0xFFFFFFFF)
        {
            if (swapEndian)
                return SwapIntEndian(((uint)b1 << 24) | ((uint)b2 << 16) | ((uint)b3 << 8) | (uint)b4);
            else
                return ((uint)b1 << 24) | ((uint)b2 << 16) | ((uint)b3 << 8) | (uint)b4;
        }

        private static byte[] Breaku16(ushort u16) //Break from 16-bits into 2 bytes (0xFF, 0xFF)
        {
            return Breaku16(u16, false);
        }

        private static byte[] Breaku16(ushort u16, bool swapEndian) //Break from 16-bits into 2 bytes (0xFF, 0xFF)
        {
            if (swapEndian)
                return new byte[] { (byte)(u16 & 0xFF), (byte)(u16 >> 8) };
            else
                return new byte[] { (byte)(u16 >> 8), (byte)(u16 & 0xFF) };
        }

        private static byte[] Breaku32(uint u32) //Break from 32-bits into 4 bytes (0xFF, 0xFF, 0xFF, 0xFF)
        {
            return Breaku32(u32, false);
        }

        private static byte[] Breaku32(uint u32, bool swapEndian) //Break from 32-bits into 4 bytes (0xFF, 0xFF, 0xFF, 0xFF)
        {
            if (swapEndian)
                return new byte[] { (byte)(u32 & 0xFF), (byte)((u32 >> 8) & 0xFF), (byte)((u32 >> 16) & 0xFF), (byte)(u32 >> 24) };
            else
                return new byte[] { (byte)(u32 >> 24), (byte)((u32 >> 16) & 0xFF), (byte)((u32 >> 8) & 0xFF), (byte)(u32 & 0xFF) };
        }

        static private string IntToHex(int num)
        {
            return num.ToString("X");
        }

        static private int HexToInt(String hex)
        {
            return int.Parse(hex, System.Globalization.NumberStyles.HexNumber);
        }

        public static uint SwapIntEndian(uint value)
        {
            var b1 = (value >> 0) & 0xff;
            var b2 = (value >> 8) & 0xff;
            var b3 = (value >> 16) & 0xff;
            var b4 = (value >> 24) & 0xff;

            return b1 << 24 | b2 << 16 | b3 << 8 | b4 << 0;
        }

        public static ushort SwapShortEndian(ushort value)
        {
            var b1 = (value >> 0) & 0xff;
            var b2 = (value >> 8) & 0xff;

            return (ushort)(b1 << 8 | b2 << 0);
        }
        #endregion

        #region Constructors
        private static void MakeDirExist(string dir)
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

        private static uint CalcHash(string name)
        {
            ulong result = 0;
            for (int i = 0; i < name.Length; i++)
            {
                result = (((byte)name[i]) + (result * 0x65)) & 0xFFFFFFFF;
            }
            return (uint)(result & 0xFFFFFFFF);
        }

        private static string[] GetFiles(string dir)
        {
            if (dir == "") dir = System.Environment.CurrentDirectory;
            return System.IO.Directory.GetFiles(dir);
        }

        private static byte[] AddPadding(byte[] dataBuild, NodeData nodeData) //Add padding to adjust to Nintendo's logic
        {
            int baseValue = nodeData.data.Length % 16;
            if (baseValue == 4 || baseValue == 12)
                dataBuild = dataBuild.Concat(new byte[] { 0x00, 0x00, 0x00, 0x00 }).ToArray();
            return dataBuild;
        }

        private static byte[] AddPadding(byte[] dataBuild, uint padding) //Add padding 
        {
            byte[] pad = new byte[padding];
            dataBuild = dataBuild.Concat(pad).ToArray(); //padding data
            return dataBuild;
        }

        private static byte[] AddPadding(byte[] dataBuild, uint padding, NodeData nodeData) //Add padding
        {
            byte[] pad = new byte[padding];
            dataBuild = dataBuild.Concat(pad).ToArray(); //padding data
            return dataBuild;
        }

        private static byte[] FixNodeSize(byte[] nodeData, uint oriSize) //Used only for making the custom file size match the orignal (if custom is smaller)
        {
            if (nodeData[0] == 'Y' || nodeData[1] == 'a' || nodeData[2] == 'z' || nodeData[3] == '0') //Skip the nodes that are encoded..
            {
                return nodeData;
            }

            if (nodeData.Length < oriSize)
            {
                byte[] pad = new byte[oriSize - (uint)nodeData.Length];
                nodeData = nodeData.Concat(pad).ToArray();
                return nodeData;
            }
            else
                return FixNodeSize(nodeData);
        }

        private static byte[] FixNodeSize(byte[] nodeData) //I found that nintendo has a consistant pattern where every node must be divisable by 4
        {                                                  //Additionally there are padding rules for this, but here we are just fixing the size. 
            if (nodeData[0] == 'Y' || nodeData[1] == 'a' || nodeData[2] == 'z' || nodeData[3] == '0') //Skip the nodes that are encoded..
            {
                return nodeData;
            }
            int baseValue = nodeData.Length % 16;
            if (baseValue % 4 == 0)
            {
                return nodeData;
            }
            else
            {
                byte[] pad = new byte[baseValue % 4];
                nodeData = nodeData.Concat(pad).ToArray();
                return nodeData;
            }
        }
        #endregion

        #region Extract
        public static bool Extract(string inFile, string outDir) //EXTRACT ----------------------------------------------------------------------
        {
            try
            {
                return Extract(System.IO.File.ReadAllBytes(inFile), outDir, false, false, inFile); //default
            }
            catch (Exception e) //usually because file is in use
            {
                lerror = e.Message;
                return false;
            }
        }

        public static bool Extract(string inFile, string outDir, bool autoDecode)
        {
            try
            {
                return Extract(System.IO.File.ReadAllBytes(inFile), outDir, autoDecode, false, inFile); //autoDecode
            }
            catch (Exception e) //usually because file is in use
            {
                lerror = e.Message;
                return false;
            }
        }

        public static bool Extract(string inFile, string outDir, bool autoDecode, bool nodeDecode)
        {
            try
            {
                return Extract(System.IO.File.ReadAllBytes(inFile), outDir, autoDecode, nodeDecode, inFile); //autoDecode + nodeDecode
            }
            catch (Exception e) //usually because file is in use
            {
                lerror = e.Message;
                return false;
            }
        }

        public static bool Extract(byte[] inFile, string outDir, bool autoDecode, bool nodeDecode, string inFileName)
        {

            //SARC header 0x00 - 0x13
            if (("" + ((char)inFile[0]) + ((char)inFile[1]) + ((char)inFile[2]) + ((char)inFile[3])) != "SARC")
            {
                if (inFile[0] == 'Y' && inFile[1] == 'a' && inFile[2] == 'z' && inFile[3] == '0')
                {
                    if (autoDecode)
                    {
                        string outFile = Yaz0.DecodeOutputFileRename(inFileName);
                        Yaz0.Decode(inFileName, outFile);
                        return Extract(outFile, outDir, autoDecode); //recursively run the code again
                    }
                    else
                    {
                        lerror = "Yaz0 file encoded, please decode it first";
                        return false;
                    }
                }
                else
                {
                    lerror = "Not a SARC archive! Missing SARC header at 0x00" + "\n" + "( Your file header is: " + ((char)inFile[0]) + ((char)inFile[1]) + ((char)inFile[2]) + ((char)inFile[3]) + " )";
                    return false;
                }
            }

            ushort bom = Makeu16(inFile[6], inFile[7]); //Byte Order (Big or Little Endian)
            bool isLittleEndian = false;
            if (bom == 65534) //Is the byte order little endian? (Switch version)
                isLittleEndian = true;
            int pos = 4; //0x04
            ushort hdr = Makeu16(inFile[pos], inFile[pos + 1], isLittleEndian); //SARC Header length
            pos += 4; //0x08
            uint fileSize = Makeu32(inFile[pos], inFile[pos + 1], inFile[pos + 2], inFile[pos + 3], isLittleEndian);
            pos += 4; //0x0C
            uint dataOffset = Makeu32(inFile[pos], inFile[pos + 1], inFile[pos + 2], inFile[pos + 3], isLittleEndian); //Data offset start position
            pos += 4; //0x10
            uint unknown = Makeu16(inFile[pos], inFile[pos + 1], isLittleEndian); //unknown, always 1? (01 00 00 00) or (00 01 00 00)  
            pos += 4; //0x14

            //SFAT header 0x14 - 0x1F
            if (inFile[pos] != 'S' || inFile[pos + 1] != 'F' || inFile[pos + 2] != 'A' || inFile[pos + 3] != 'T')
            {
                lerror = "Unknown file table! (Missing SFAT header at 0x14)";
                return false;
            }
            pos += 4; //0x18
            ushort hdr2 = Makeu16(inFile[pos], inFile[pos + 1], isLittleEndian); //SFAT Header length
            pos += 2; //0x1A
            ushort nodeCount = Makeu16(inFile[pos], inFile[pos + 1], isLittleEndian); //Node Cluster count
            pos += 2; //0x1C
            uint hashr = Makeu32(inFile[pos], inFile[pos + 1], inFile[pos + 2], inFile[pos + 3], isLittleEndian); //Hash multiplier, always 0x65 (so it seems for BotW)
            pos += 4; //0x20

            SarcNode[] nodes = new SarcNode[nodeCount];
            SarcNode tmpnode = new SarcNode();

            for (int i = 0; i < nodeCount; i++) //Node cluster 
            {
                tmpnode.hash = Makeu32(inFile[pos], inFile[pos + 1], inFile[pos + 2], inFile[pos + 3], isLittleEndian);
                pos += 4; //0x?4
                if (isLittleEndian)
                {
                    tmpnode.offset = Makeu32(inFile[pos], inFile[pos + 1], inFile[pos + 2], 0, isLittleEndian); //Node SFNT filename offset divided by 4 (not used)
                    tmpnode.unknown = inFile[pos]; //unknown, always 0x01? (not used in this case)
                }
                else
                {
                    tmpnode.unknown = inFile[pos]; //unknown, always 0x01? (not used in this case)
                    tmpnode.offset = Makeu32(0, inFile[pos + 1], inFile[pos + 2], inFile[pos + 3]); //Node SFNT filename offset divided by 4 (not used)
                }
                pos += 4; //0x?8
                tmpnode.start = Makeu32(inFile[pos], inFile[pos + 1], inFile[pos + 2], inFile[pos + 3], isLittleEndian); //Start Data offset position
                pos += 4; //0x?C
                tmpnode.end = Makeu32(inFile[pos], inFile[pos + 1], inFile[pos + 2], inFile[pos + 3], isLittleEndian); //End Data offset position
                pos += 4; //0x?0
                nodes[i] = tmpnode; //Store node data into array
            }

            if (inFile[pos] != 'S' || inFile[pos + 1] != 'F' || inFile[pos + 2] != 'N' || inFile[pos + 3] != 'T')
            {
                string posOffset = "0x" + pos.ToString("X");
                lerror = "Unknown file name table! (Missing SFNT header at " + posOffset + ")";
                return false;
            }
            pos += 4; //0x?4
            ushort hdr3 = Makeu16(inFile[pos], inFile[pos + 1], isLittleEndian); //SFNT Header length, always 0x08
            pos += 2; //0x?6
            ushort unk2 = Makeu16(inFile[pos], inFile[pos + 1], isLittleEndian); //unknown, always 0x00?
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
                MakeDirExist(System.IO.Path.GetDirectoryName(outDir + "/" + fileNames[i]));
                stream = new System.IO.StreamWriter(outDir + "/" + fileNames[i]);
                stream.BaseStream.Write(inFile, (int)(nodes[i].start + dataOffset), (int)(nodes[i].end - nodes[i].start)); //Write 
                stream.Close();
                stream.Dispose();
                if (nodeDecode)
                {
                    Yaz0.Decode(outDir + "/" + fileNames[i], Yaz0.DecodeOutputFileRename(outDir + "/" + fileNames[i]));
                }
            }
            GC.Collect();
            return true;
        } //--------------------------------------------------------------------------------------------------------------------------------------------
        #endregion

        #region Build
        public static bool Build(string inDir, string outFile)
        {
            return Build(inDir, outFile, 0, false); //if no fixed data offset is set (default 0) and no endian is set (default big endian)
        }

        public static bool Build(string inDir, string outFile, uint dataFixedOffset)
        {
            return Build(inDir, outFile, dataFixedOffset, false); //if no endian is set (default big endian)
        }

        public static bool Build(string inDir, string outFile, bool isLittleEndian)
        {
            return Build(inDir, outFile, 0, isLittleEndian); //if no fixed data offset is set (defaut 0)
        }

        public static bool Build(string inDir, string outFile, uint dataFixedOffset, bool isLittleEndian) //BUILD ----------------------------------------------------------------------
        {
            try
            {
                //Setup
                string[] inDirFiles = System.IO.Directory.GetFiles(inDir == "" ? System.Environment.CurrentDirectory : inDir, "*.*", System.IO.SearchOption.AllDirectories);
                NodeInfo[] nodeInfo = new NodeInfo[inDirFiles.Length];

                //Node storage logic
                uint totalNamesLength = 0;
                uint numFiles = (uint)inDirFiles.Length;
                for (int i = 0; i < numFiles; i++) //collect node information
                {
                    string realname = inDirFiles[i];
                    string fileName = inDirFiles[i].Replace(inDir + System.IO.Path.DirectorySeparatorChar.ToString(), "");
                    fileName = fileName.Replace("\\", "/"); //HURP DERP NINTENDUR (This fixes the hash calculation!)
                    uint namesize = (uint)fileName.Length;
                    namesize += (4 - (namesize % 4));
                    totalNamesLength += namesize;
                    nodeInfo[i] = new NodeInfo(fileName, realname, namesize);
                }
                uint namePaddingToAdd = 0;
                if (totalNamesLength % 0x10 != 0)
                {
                    namePaddingToAdd = 0x10 % (totalNamesLength % 0x10); //padding to add for 
                    totalNamesLength += namePaddingToAdd;
                }

                //Node hash calculation and sorting logic
                NodeHash[] hashesUnsorted = new NodeHash[numFiles];
                for (int i = 0; i < numFiles; i++)
                {
                    hashesUnsorted[i] = new NodeHash(CalcHash(nodeInfo[i].filename), i, ""); //Store and calculate unsorted hashes
                }
                uint lastHash;
                bool[] hashSortedFlag = new bool[hashesUnsorted.Length];
                NodeHash[] hashes = new NodeHash[hashesUnsorted.Length];
                int dhi = 0;
                for (int i = 0; i < hashes.Length; i++) //sort nodes by hash calculation
                {
                    lastHash = uint.MaxValue;
                    for (int j = 0; j < hashesUnsorted.Length; j++)
                    {
                        if (hashSortedFlag[j]) continue;
                        if (hashesUnsorted[j].hash < lastHash)
                        {
                            dhi = j;
                            lastHash = hashesUnsorted[j].hash;
                        }
                    }
                    hashSortedFlag[dhi] = true;
                    hashes[i] = hashesUnsorted[dhi];
                }

                //Node data build and position logic
                NodeData[] nodeData = new NodeData[numFiles];
                uint nodePosition = 0;
                for (int i = 0; i < numFiles; i++)
                {
                    nodeData[hashes[i].index].startPos = nodePosition; //start position after padding
                    nodeData[hashes[i].index].data = System.IO.File.ReadAllBytes(nodeInfo[hashes[i].index].realname);
                    nodePosition += (uint)nodeData[hashes[i].index].data.Length;
                    nodeData[hashes[i].index].endPos = nodePosition; //end position before padding
                    if (i != numFiles - 1) //As long as it's not the last node, add padding data
                    {
                        if ((nodeData[hashes[i].index].data.Length % 4) != 0) 
                            nodeData[hashes[i].index].padding = 4 - (uint)(nodeData[hashes[i].index].data.Length % 4);
                    }
                        nodePosition += nodeData[hashes[i].index].padding;
                }


                uint fileSize = 0;
                fileSize += 0x20; //SARC + SFAT reserve
                fileSize += 0x10 * (uint)numFiles; //nodeInfo reserve 
                fileSize += 0x08; //SFNT reserve
                fileSize += totalNamesLength; //names of files 
                uint nodeDataStart = fileSize; //node data table offset
                if (dataFixedOffset > nodeDataStart) //if fixed data offset is larger than generated start...
                {
                    namePaddingToAdd += (dataFixedOffset - nodeDataStart);
                    fileSize += (dataFixedOffset - nodeDataStart);
                    nodeDataStart = dataFixedOffset;
                }
                for (int i = 0; i < numFiles; i++)
                {
                    fileSize += (uint)(nodeData[hashes[i].index].data.Length);
                    fileSize += nodeData[hashes[i].index].padding;
                }

                //Write logic
                System.IO.StreamWriter stream = new System.IO.StreamWriter(outFile);
                //SARC ---
                if (isLittleEndian)
                    stream.BaseStream.Write(new byte[] { 83, 65, 82, 67, 0x14, 0x00, 0xFF, 0xFE }, 0, 8); //Write Fixed SARC (Little Endian) 
                else
                    stream.BaseStream.Write(new byte[] { 83, 65, 82, 67, 0x00, 0x14, 0xFE, 0xFF }, 0, 8); //Write Fixed SARC (Big Endian)
                stream.BaseStream.Write(Breaku32(fileSize, isLittleEndian), 0, 4); //Write 0x08 split bytes of file size
                stream.BaseStream.Write(Breaku32(nodeDataStart, isLittleEndian), 0, 4); //Write 0x0C split bytes of data table start offset
                //SFAT ---
                if (isLittleEndian)
                    stream.BaseStream.Write(new byte[] { 0x00, 0x01, 0x00, 0x00, 83, 70, 65, 84, 0x0C, 0x00 }, 0, 10); //Write Fixed SFAT (Little Endian)
                else
                    stream.BaseStream.Write(new byte[] { 0x01, 0x00, 0x00, 0x00, 83, 70, 65, 84, 0x00, 0x0C }, 0, 10); //Write Fixed SFAT (Big Endian)
                stream.BaseStream.Write(Breaku16((ushort)numFiles, isLittleEndian), 0, 2); //Write 0x1A split bytes of number of nodes/files
                stream.BaseStream.Write(Breaku32(0x65, isLittleEndian), 0, 4); //Write Fixed Hash Multiplier 
                uint strpos = 0;
                //Node ---
                for (int i = 0; i < numFiles; i++)
                {
                    stream.BaseStream.Write(Breaku32(hashes[i].hash, isLittleEndian), 0, 4); //Node Hash 
                    if (isLittleEndian)
                    {
                        stream.BaseStream.Write(Breaku32((strpos / 4), isLittleEndian), 0, 3); //Node filename offset position (divided by 4)
                        stream.BaseStream.WriteByte(0x01); //Node Fixed Unknown (is enabled flag?)
                    }
                    else
                    {
                        stream.BaseStream.WriteByte(0x01); //Node Fixed Unknown (is enabled flag?)
                        stream.BaseStream.Write(Breaku32((strpos >> 2)), 1, 3); //Node filename offset position (divided by 4)
                    }
                    strpos += nodeInfo[hashes[i].index].namesize;
                    stream.BaseStream.Write(Breaku32(nodeData[hashes[i].index].startPos, isLittleEndian), 0, 4); //Node start data offset position
                    stream.BaseStream.Write(Breaku32(nodeData[hashes[i].index].endPos, isLittleEndian), 0, 4); //Node end data offset position
                }
                GC.Collect();
                //SFNT ---
                if (isLittleEndian)
                    stream.BaseStream.Write(new byte[] { 83, 70, 78, 84, 0x08, 0x00, 0x00, 0x00 }, 0, 8); //Write fixed SFNT header (Little endian)
                else
                    stream.BaseStream.Write(new byte[] { 83, 70, 78, 84, 0x00, 0x08, 0x00, 0x00 }, 0, 8); //Write fixed SFNT header (Bid Endian)
                for (int i = 0; i < numFiles; i++)
                {
                    string fileName = nodeInfo[hashes[i].index].filename;
                    for (int j = 0; j < fileName.Length; j++)
                    {
                        stream.BaseStream.WriteByte((byte)fileName[j]); //Write file names
                    }
                    int namePadding = (int)nodeInfo[hashes[i].index].namesize - nodeInfo[hashes[i].index].filename.Length; //short padding for file offset location (to be divisible by 4)
                    if ((i == numFiles - 1) && (namePadding == 16)) //if the last filename conveniently ended at 0x0F, then do not add any padding
                        namePadding = 0;
                    for (int j = 0; j < namePadding; j++)
                        stream.BaseStream.WriteByte(0);
                }

                for (int i = 0; i < namePaddingToAdd; i++)
                {
                    stream.BaseStream.WriteByte(0); //pad end of names
                }

                

                //Data ---
                for (int i = 0; i < numFiles; i++)
                {
                    stream.BaseStream.Write(nodeData[hashes[i].index].data, 0, nodeData[hashes[i].index].data.Length);
                    if (nodeData[hashes[i].index].padding != 0)
                    {
                        for(int j = 0; j < nodeData[hashes[i].index].padding; j++)
                            stream.BaseStream.WriteByte(0);
                    }
                }

                stream.Close();
                stream.Dispose();
                GC.Collect();
            }
            catch (System.Exception e)
            {
                lerror = "An error occurred: " + e.Message;
                return false;
            }

            return true;
        } //--------------------------------------------------------------------------------------------------------------------------------------------
        #endregion

        #region FileOperations
        public static Boolean IsSarcFile (string file)
        {
            byte[] inFile = System.IO.File.ReadAllBytes(file);
            if (("" + ((char)inFile[0]) + ((char)inFile[1]) + ((char)inFile[2]) + ((char)inFile[3])) == "SARC")
                return true;
            else
                return false;
        }

        public static Boolean IsYaz0File(string file)
        {
            byte[] inFile = System.IO.File.ReadAllBytes(file);
            if (("" + ((char)inFile[0]) + ((char)inFile[1]) + ((char)inFile[2]) + ((char)inFile[3])) == "Yaz0")
                return true;
            else
                return false;
        }

        public static Boolean IsLittleEndian(string file)
        {
            byte[] inFile = System.IO.File.ReadAllBytes(file);
            if (Makeu16(inFile[6], inFile[7]) == 65534)
                return true;
            else
                return false;
        }

        public static int GetFileNodeCount(string file) //Get # of Nodes
        {
            try
            {
                if (IsSarcFile(file))
                {
                    byte[] inFile = System.IO.File.ReadAllBytes(file);
                    int nodeCount = 0;
                    nodeCount = Makeu16(inFile[0x1A], inFile[0x1B], IsLittleEndian(file));
                    return nodeCount;
                }
                else
                    return -1;
            }
            catch
            {
                return -1;
            }
        }

        public static int GetFileDataOffset(string file)
        {
            if (IsSarcFile(file))
            { 
                byte[] inFile = System.IO.File.ReadAllBytes(file);
                uint dataStartPos = Makeu32(inFile[0x0C], inFile[0x0D], inFile[0x0E], inFile[0x0F], IsLittleEndian(file));
                return (int)dataStartPos;
            }
            else
                return -1;
        }

        public static string[] GetFileNodeType(string file) //Get Types
        {
            
            if (IsSarcFile(file))
            {
                byte[] inFile = System.IO.File.ReadAllBytes(file);
                int nodeCount = GetFileNodeCount(file);
                int pos;
                uint[] nodeDataStartPos = new uint[nodeCount];
                string[] nodeTypes = new string[nodeCount];
                uint dataStartPos = Makeu32(inFile[0x0C], inFile[0x0D], inFile[0x0E], inFile[0x0F], IsLittleEndian(file));
                pos = 0x28;
                for (int i = 0; i < nodeCount; i++)
                {
                    nodeDataStartPos[i] = Makeu32(inFile[pos], inFile[pos + 1], inFile[pos + 2], inFile[pos + 3], IsLittleEndian(file)) + dataStartPos;
                    pos += 0x10;
                    nodeTypes[i] = ((char)inFile[nodeDataStartPos[i]]) + "" + ((char)inFile[nodeDataStartPos[i] + 1]) + "" + ((char)inFile[nodeDataStartPos[i] + 2]) + "" + ((char)inFile[nodeDataStartPos[i] + 3]);
                }
                return nodeTypes;
            }
            else
                return null;
        }

        public static uint[] GetFileNodeSizes(string file) //Get File Sizes
        {
            if (IsSarcFile(file))
            {
                byte[] inFile = System.IO.File.ReadAllBytes(file);
                int nodeCount = GetFileNodeCount(file);
                int pos;
                uint[] nodeDataStartPos = new uint[nodeCount];
                uint[] nodeDataEndPos = new uint[nodeCount];
                uint dataStartPos = Makeu32(inFile[0x0C], inFile[0x0D], inFile[0x0E], inFile[0x0F], IsLittleEndian(file));
                uint[] nodeSizes = new uint[nodeCount];
                pos = 0x28;
                for (int i = 0; i < nodeCount; i++) //Get data start and end positions 
                {
                    nodeDataStartPos[i] = Makeu32(inFile[pos], inFile[pos + 1], inFile[pos + 2], inFile[pos + 3], IsLittleEndian(file)) + dataStartPos;
                    pos += 4;
                    nodeDataEndPos[i] = Makeu32(inFile[pos], inFile[pos + 1], inFile[pos + 2], inFile[pos + 3], IsLittleEndian(file)) + dataStartPos;
                    pos += 0x10 - 4;
                    nodeSizes[i] = nodeDataEndPos[i] - nodeDataStartPos[i];
                }
                return nodeSizes;
            }
            else
                return null;
        }

        public static uint[] GetFileNodePaddings(string file) //Get Paddings
        {
            if (IsSarcFile(file))
            {
                byte[] inFile = System.IO.File.ReadAllBytes(file);
                int nodeCount = GetFileNodeCount(file);
                int pos;
                uint[] nodeDataStartPos = new uint[nodeCount];
                uint[] nodeDataEndPos = new uint[nodeCount];
                uint dataStartPos = Makeu32(inFile[0x0C], inFile[0x0D], inFile[0x0E], inFile[0x0F], IsLittleEndian(file));
                uint[] nodePaddings = new uint[nodeCount];
                pos = 0x28;
                for (int i = 0; i < nodeCount; i++) //Get data start and end positions 
                {
                    nodeDataStartPos[i] = Makeu32(inFile[pos], inFile[pos + 1], inFile[pos + 2], inFile[pos + 3], IsLittleEndian(file)) + dataStartPos;
                    pos += 4;
                    nodeDataEndPos[i] = Makeu32(inFile[pos], inFile[pos + 1], inFile[pos + 2], inFile[pos + 3], IsLittleEndian(file)) + dataStartPos;
                    pos += 0x10 - 4;
                }
                for (int i = 0; i < nodeCount; i++) //Calculate padding 
                {
                    if (i != (nodeCount - 1))
                        nodePaddings[i] = nodeDataStartPos[i + 1] - nodeDataEndPos[i];
                    else
                        nodePaddings[i] = (uint)inFile.Length - nodeDataEndPos[i];

                }
                return nodePaddings;
            }
            else
                return null;
        }
        public static string[] GetFileNodePaths(string file) //Get Paths
        {
            if (IsSarcFile(file))
            {
                byte[] inFile = System.IO.File.ReadAllBytes(file);
                int nodeCount = GetFileNodeCount(file);
                int pos = 0;
                uint[] offsetPos = new uint[nodeCount];
                string[] nodePaths = new string[nodeCount];
                pos = (nodeCount * 0x10) + 0x20 + 8;
                //System.Windows.Forms.MessageBox.Show("Pos: 0x" + pos.ToString("X")); //debug
                for (int i = 0; i < nodeCount; i++)
                {
                    while (inFile[pos] != 0x00)
                    {
                        nodePaths[i] += (char)inFile[pos];
                        pos++;
                    }
                    while (inFile[pos] == 0x00)
                        pos++;
                }
                return nodePaths;
            }
            else
                return null;
        }

        public static void UpdateFileSize(string file) //Update file size 
        {
            byte[] inFile = System.IO.File.ReadAllBytes(file);
            int pos = 8;
            for (int i = pos; i < (pos + 4); i++)
                inFile[i] = Breaku32(Convert.ToUInt32(inFile.Length), IsLittleEndian(file))[i - pos];
            System.IO.StreamWriter stream = new System.IO.StreamWriter(file); //StreamWriter doesn't like to be used to overwrite data :(
            stream.BaseStream.Write(inFile, 0, inFile.Length); //Just write the updated byte array
            stream.Close(); //Save
            stream.Dispose(); //End stream
        }

        public static void UpdateFilePadding(string file, int nodeId, int padding) //Update node paddings
        {
            byte[] inFile = System.IO.File.ReadAllBytes(file);
            int nodeCount = GetFileNodeCount(file);
            int pos;
            uint tempNodeStartPos;
            uint tempNodeEndPos;
            uint dataStartPos = Makeu32(inFile[0x0C], inFile[0x0D], inFile[0x0E], inFile[0x0F], IsLittleEndian(file));
            uint[] nodePaddings = new uint[nodeCount];
            nodePaddings = GetFileNodePaddings(file);
            int calculatedPadding = (int)nodePaddings[nodeId] + padding;

            pos = 0x28 + (0x10 * nodeId); //get position of the node
            pos += 4; //line of node's end data position
            uint breakPoint = Makeu32(inFile[pos], inFile[pos + 1], inFile[pos + 2], inFile[pos + 3], IsLittleEndian(file)) + dataStartPos; //find break point to split and rebuild
            byte[] startDataBuild = new byte[breakPoint + calculatedPadding];
            byte[] endDataBuild = new byte[inFile.Length - (breakPoint + nodePaddings[nodeId])];
            Array.Copy(inFile, startDataBuild, breakPoint); //copy before changed padding
            Array.Copy(inFile, (breakPoint + nodePaddings[nodeId]), endDataBuild, 0, inFile.Length - (breakPoint + nodePaddings[nodeId])); //copy after changed padding
            pos += 0x10 - 4; //next node line at start pos
            for (int i = nodeId + 1; i < nodeCount; i++) //change the nodes below it
            {
                tempNodeStartPos = (uint)((Makeu32(inFile[pos], inFile[pos + 1], inFile[pos + 2], inFile[pos + 3], IsLittleEndian(file))) + padding);
                for (int j = pos; j < (pos + 4); j++)
                    startDataBuild[j] = Breaku32(tempNodeStartPos, IsLittleEndian(file))[j - pos]; //Update calculation of new node data Start position
                pos += 4;
                tempNodeEndPos = (uint)((Makeu32(inFile[pos], inFile[pos + 1], inFile[pos + 2], inFile[pos + 3], IsLittleEndian(file))) + padding);
                for (int j = pos; j < (pos + 4); j++)
                    startDataBuild[j] = Breaku32(tempNodeEndPos, IsLittleEndian(file))[j - pos]; //Update calculation of new node data End position
                pos += 0x10 - 4;
            }
            System.IO.StreamWriter stream = new System.IO.StreamWriter(file); 
            stream.BaseStream.Write(startDataBuild, 0, startDataBuild.Length);
            stream.BaseStream.Write(endDataBuild, 0, endDataBuild.Length);
            stream.Close(); //Save
            stream.Dispose(); //End stream
        }
        #endregion

        #region FolderOperations
        public static string[] GetFolderFilePaths(string folder)
        {
            string[] folderDir = System.IO.Directory.GetFiles(folder == "" ? System.Environment.CurrentDirectory : folder, "*.*", System.IO.SearchOption.AllDirectories);
            string[] filePaths = new string[folderDir.Length];
            for (int i = 0; i < folderDir.Length; i++)
            {
                filePaths[i] = folderDir[i].Replace(folder + System.IO.Path.DirectorySeparatorChar.ToString(), "");
                filePaths[i] = filePaths[i].Replace("\\", "/");
            }
            filePaths = CalculateHashAndSort(filePaths);
            return filePaths;
        }

        public static string[] CalculateHashAndSort(string[] files) //Calculate Hash and Sort
        {
            NodeHash[] hashesUnsorted = new NodeHash[files.Length];
            for (int i = 0; i < files.Length; i++)
            {
                hashesUnsorted[i] = new NodeHash(CalcHash(files[i]), i, files[i]); //Store and calculate unsorted hashes
            }
            uint lastHash;
            bool[] hashSortedFlag = new bool[hashesUnsorted.Length];
            NodeHash[] hashes = new NodeHash[hashesUnsorted.Length];
            int dhi = 0;
            for (int i = 0; i < hashes.Length; i++) //sort nodes by hash calculation
            {
                lastHash = uint.MaxValue;
                for (int j = 0; j < hashesUnsorted.Length; j++)
                {
                    if (hashSortedFlag[j]) continue;
                    if (hashesUnsorted[j].hash < lastHash)
                    {
                        dhi = j;
                        lastHash = hashesUnsorted[j].hash;
                    }
                }
                hashSortedFlag[dhi] = true;
                hashes[i] = hashesUnsorted[dhi];
            }
            for (int i = 0; i < files.Length; i++)
            {
                files[i] = hashes[i].fileName;
            }
            return files;
        }

        public static uint[] GetFolderFileSizes(string folder) //Get Folder's files and their sizes
        {
            string[] folderDir = System.IO.Directory.GetFiles(folder == "" ? System.Environment.CurrentDirectory : folder, "*.*", System.IO.SearchOption.AllDirectories);
            string[] filePaths = new string[folderDir.Length];
            uint[] fileSizes = new uint[folderDir.Length];
            FileInfo[] fileInfo = new FileInfo[folderDir.Length];
            filePaths = GetFolderFilePaths(folder);

            for (int i = 0; i < folderDir.Length; i++) //I know this looks stupid, but trust me on this...
            {
                filePaths[i] = filePaths[i].Replace("/", "\\");
                filePaths[i] = folder + System.IO.Path.DirectorySeparatorChar.ToString() + filePaths[i];
                fileInfo[i] = new FileInfo(filePaths[i]);
                fileSizes[i] = (uint)fileInfo[i].Length;
            }
            return fileSizes;
        }

        public static string[] GetFolderFileTypes(string folder)
        {
            string[] folderDir = System.IO.Directory.GetFiles(folder == "" ? System.Environment.CurrentDirectory : folder, "*.*", System.IO.SearchOption.AllDirectories);
            string[] filePaths = new string[folderDir.Length];
            string[] fileTypes = new string[folderDir.Length];
            FileInfo[] fileInfo = new FileInfo[folderDir.Length];
            filePaths = GetFolderFilePaths(folder);

            for (int i = 0; i < folderDir.Length; i++)
            {
                filePaths[i] = filePaths[i].Replace("/", "\\");
                filePaths[i] = folder + System.IO.Path.DirectorySeparatorChar.ToString() + filePaths[i];
                fileInfo[i] = new FileInfo(filePaths[i]);
                byte[] fileData = new byte[fileInfo[i].Length];
                fileData = System.IO.File.ReadAllBytes(filePaths[i]);
                fileTypes[i] = (char)fileData[0] + "" + (char)fileData[1] + "" + (char)fileData[2] + "" + (char)fileData[3];
            }
            return fileTypes;
        }
        #endregion
    }
}