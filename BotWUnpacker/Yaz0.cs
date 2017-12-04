using System;
using System.Linq;
//Based mostly off looking at multiple people's logic how they handled Yaz0 Decode/Encode

namespace BotWUnpacker
{
    public struct Yaz0
    {

        public static string lerror = ""; // Gets the last error

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

        static private string IntToHex(int num)
        {
            return num.ToString("X");
        }

        static private int HexToInt(String hex)
        {
            return int.Parse(hex, System.Globalization.NumberStyles.HexNumber);
        }
        #endregion

        #region Decode
        public static bool Decode(string inFile, string outFile)
        {
            try { return Decode(System.IO.File.ReadAllBytes(inFile), outFile); }
            catch(System.Exception e)
            {
                lerror = "An error occurred: " + e.Message;
                return false;
            }
        }

        public static bool Decode(byte[] inFile, string outFile)
        {
            //Yaz0 header 0x00 - 0x0F
            if (inFile[0] != 'Y' || inFile[1] != 'a' || inFile[2] != 'z' || inFile[3] != '0')
            {
                lerror = "File not Yaz0 encoded! " + "\n" + "( Your file header is: " + ((char)inFile[0]) + ((char)inFile[1]) + ((char)inFile[2]) + ((char)inFile[3]) + " )";
                return false;
            }
            uint uncompressedSize = Makeu32(inFile[4], inFile[5], inFile[6], inFile[7]); //0x04

            //Decode Logic
            uint sourcePos = 16; //start at 0x10
            uint writePos = 0; //destination to write data to
            byte[] decodedData = new byte[uncompressedSize + uncompressedSize]; 
            byte groupHead = 0; 
            uint validBitCount = 0;

            while (writePos < uncompressedSize)
            {
                if (validBitCount == 0)
                {
                    groupHead = inFile[sourcePos];
                    ++sourcePos;
                    validBitCount = 8;
                }
                if ((groupHead & 0x80) != 0)
                {
                    //straight copy
                    decodedData[writePos] = inFile[sourcePos];
                    writePos++;
                    sourcePos++;
                }
                else
                {
                    //RLE
                    byte b1 = inFile[sourcePos];
                    byte b2 = inFile[sourcePos + 1];
                    sourcePos += 2;
                    uint dist = ((uint)((b1 & 0xF) << 8) | b2); //chunk
                    uint copySource = writePos - (dist + 1); //copy
                    uint numBytes = (uint)b1 >> 4; //count
                    if (numBytes == 0)
                    {
                        numBytes = inFile[sourcePos] + (uint)0x12;
                        sourcePos++;
                    }
                    else
                    {
                        numBytes += 2;
                    }
                    //copy run
                    for (int i = 0; i < numBytes; ++i)
                    {
                        decodedData[writePos] = decodedData[copySource];
                        copySource++;
                        writePos++;
                    }
                }
                //use next bit from "code" byte
                groupHead <<= 1;
                validBitCount -= 1;
            }

            System.IO.StreamWriter stream;
            stream = new System.IO.StreamWriter(outFile);
            stream.BaseStream.Write(decodedData, 0, (int)uncompressedSize); //Write data
            stream.Close();
            stream.Dispose();

            return true;
        }
        #endregion

        #region Encode
        public static bool Encode(string inFile, string outFile)
        {
            try { return Encode(System.IO.File.ReadAllBytes(inFile), outFile); }
            catch (System.Exception e)
            {
                lerror = "An error occurred: " + e.Message;
                return false;
            }
        }

        public static bool Encode(byte[] inFile, string outFile)
        {
            //Yaz0 check
            if (inFile[0] == 'Y' || inFile[1] == 'a' || inFile[2] == 'z' || inFile[3] == '0')
            {
                lerror = "File already Yaz0 encoded!";
                return false;
            }
            uint uncompressedSize = (uint)inFile.Length; //0x04

            //Encode Logic


            return true;
        }
        #endregion


    }
}
