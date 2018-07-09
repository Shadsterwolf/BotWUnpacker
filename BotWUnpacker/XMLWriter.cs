using System;
using System.IO;
using System.Text;
using System.Xml;
//Modified code From Uwizard master branch as of 10/16/2017


namespace BotwUnpacker
{
    public struct XMLWriter
    {
        #region Structures
        private struct SarcNode //SARC node row
        {
            public uint hash;
            public byte unknown;
            public uint offset, start, end;

            public SarcNode(uint fhash, byte unk, uint foffset, uint fstart, uint fend)
            {
                hash = fhash; 
                unknown = unk; 
                offset = foffset;
                start = fstart;
                end = fend;
            }
        }
        #endregion

        #region Conversions
        private static ushort Makeu16(byte b1, byte b2) //16-bit change (ushort, 0xFFFF)
        {
            return (ushort)(((ushort)b1 << 8) | (ushort)b2);
        }

        private static uint Makeu32(byte b1, byte b2, byte b3, byte b4) //32-bit change (uint, 0xFFFFFFFF)
        {
            return ((uint)b1 << 24) | ((uint)b2 << 16) | ((uint)b3 << 8) | (uint)b4;
        }

        private static byte[] Breaku16(ushort u16) //Byte change from 16-bits (byte, 0xFF, 0xFF)
        {
            return new byte[] { (byte)(u16 >> 8), (byte)(u16 & 0xFF) };
        }

        private static byte[] Breaku32(uint u32) //Byte change from 32-bits (byte, 0xFF, 0xFF, 0xFF, 0xFF)
        {
            return new byte[] { (byte)(u32 >> 24), (byte)((u32 >> 16) & 0xFF), (byte)((u32 >> 8) & 0xFF), (byte)(u32 & 0xFF) };
        }
        #endregion

        #region SaveXml
         //SAVE ----------------------------------------------------------------------
        public static bool SaveXml(string inFileName, string outDir)
        {
            byte[] inFile = File.ReadAllBytes(inFileName);
            XmlTextWriter writer = new XmlTextWriter(outDir, null);
            writer.Formatting = Formatting.Indented;

            writer.WriteStartElement("PACK");
            writer.WriteStartElement("SARC");
            //SARC header 0x00 - 0x13
            if (inFile[0] != 'S' || inFile[1] != 'A' || inFile[2] != 'R' || inFile[3] != 'C')
            {
                return false;
            }
            int pos = 4; //0x04
            ushort hdr = Makeu16(inFile[pos], inFile[pos + 1]); //SARC Header length
            writer.WriteElementString("HeaderLengthBytes", hdr.ToString());
            pos += 2; //0x06
            ushort bom = Makeu16(inFile[pos], inFile[pos + 1]); //Byte Order Mark
            writer.WriteElementString("ByteOrderMark", "0x" + bom.ToString("X"));
            if (bom != 65279) //Check 0x06 for Byte Order Mark (if not 0xFEFF)
            {
                return false;
            }
            pos += 2; //0x08
            uint fileSize = Makeu32(inFile[pos], inFile[pos + 1], inFile[pos + 2], inFile[pos + 3]);
            writer.WriteElementString("FileSizeBytes", Convert.ToString(fileSize));
            pos += 4; //0x0C
            uint dataOffset = Makeu32(inFile[pos], inFile[pos + 1], inFile[pos + 2], inFile[pos + 3]); //Data offset start position
            writer.WriteElementString("DataTableOffset", "0x" + dataOffset.ToString("X"));
            pos += 4; //0x10
            uint unknown = Makeu32(inFile[pos], inFile[pos + 1], inFile[pos + 2], inFile[pos + 3]); //unknown, always 0x01?
            writer.WriteElementString("Unknown", "0x" + unknown.ToString("X"));
            pos += 4; //0x14
            writer.WriteEndElement(); //</SARC>

            writer.WriteStartElement("SFAT");
            //SFAT header 0x14 - 0x1F
            if (inFile[pos] != 'S' || inFile[pos + 1] != 'F' || inFile[pos + 2] != 'A' || inFile[pos + 3] != 'T')
            {
                return false;
            }
            pos += 4; //0x18
            ushort hdr2 = Makeu16(inFile[pos], inFile[pos + 1]); //SFAT Header length
            writer.WriteElementString("HeaderLengthBytes", hdr2.ToString());
            pos += 2; //0x1A
            ushort nodeCount = Makeu16(inFile[pos], inFile[pos + 1]); //Node Cluster count
            writer.WriteElementString("NodeCount", Convert.ToString(nodeCount));
            pos += 2; //0x1C
            uint hashr = Makeu32(inFile[pos], inFile[pos + 1], inFile[pos + 2], inFile[pos + 3]); //Hash multiplier, always 0x65
            writer.WriteElementString("HashMultiplier", "0x" + hashr.ToString("X"));
            pos += 4; //0x20
            SarcNode[] nodes = new SarcNode[nodeCount];
            SarcNode tmpnode = new SarcNode();
            for (int i = 0; i < nodeCount; i++) //Node cluster 
            {
                writer.WriteStartElement("NodeInfo" + i);
                tmpnode.hash = Makeu32(inFile[pos], inFile[pos + 1], inFile[pos + 2], inFile[pos + 3]);
                writer.WriteElementString("Hash", "0x" + (tmpnode.hash).ToString("X"));
                pos += 4; //0x?4
                tmpnode.unknown = inFile[pos]; //unknown, always 0x01? (not used in this case)
                writer.WriteElementString("Unknown", "0x" + (tmpnode.unknown).ToString("X"));
                pos += 1; //0x?5
                tmpnode.offset = Makeu32(0, inFile[pos], inFile[pos + 1], inFile[pos + 2]); //Node SFNT filename offset divided by 4
                writer.WriteElementString("FileNameOffset", "0x" + ((tmpnode.offset * 4) + hdr + hdr2 + (nodeCount * 0x10) + 8).ToString("X"));
                pos += 3; //0x?8
                tmpnode.start = Makeu32(inFile[pos], inFile[pos + 1], inFile[pos + 2], inFile[pos + 3]); //Start Data offset position
                writer.WriteElementString("DataStartOffset", "0x" + (tmpnode.start + dataOffset).ToString("X"));
                pos += 4; //0x?C
                tmpnode.end = Makeu32(inFile[pos], inFile[pos + 1], inFile[pos + 2], inFile[pos + 3]); //End Data offset position
                writer.WriteElementString("DataEndOffset", "0x" + (tmpnode.end + dataOffset).ToString("X"));
                pos += 4; //0x?0
                nodes[i] = tmpnode; //Store node data into array
                writer.WriteEndElement(); //</NodeInfo>
            }
            writer.WriteEndElement(); //</SFAT>

            writer.WriteStartElement("SFNT");
            if (inFile[pos] != 'S' || inFile[pos + 1] != 'F' || inFile[pos + 2] != 'N' || inFile[pos + 3] != 'T')
            {
                return false;
            }
            pos += 4; //0x?4
            ushort hdr3 = Makeu16(inFile[pos], inFile[pos + 1]); //SFNT Header length
            writer.WriteElementString("HeaderLength", hdr3.ToString("X"));
            pos += 2; //0x?6
            ushort unk2 = Makeu16(inFile[pos], inFile[pos + 1]); //unknown, always 0x00?
            writer.WriteElementString("Unknown", unk2.ToString("X"));
            pos += 2; //0x?8
            writer.WriteEndElement(); //</SFNT>

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
                writer.WriteElementString("NodeFile" + i, tempName);
            }
            

            for (int i = 0; i < nodeCount; i++) //Write files based from node information
            {
                writer.WriteStartElement("NodeHexData" + i);
                writer.WriteBinHex(inFile, (int)(nodes[i].start + dataOffset), (int)(nodes[i].end - nodes[i].start));
                writer.WriteEndElement(); //</NodeData>
            }
            

            writer.WriteEndElement(); //</PACKfile>
            writer.Close();
            GC.Collect();
            return true;
        } //--------------------------------------------------------------------------------------------------------------------------------------------
        #endregion
    }
}