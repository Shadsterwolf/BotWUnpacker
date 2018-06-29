using System;
using System.IO;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace BotWUnpacker
{
    public struct ConsoleHandler
    {
        #region DragAndDrop
        static public void DragAndDrop(string[] args)
        {
            for (int i = 0; i < args.Length; i++)
            {
                if (File.Exists(args[i]))
                {
                    if (PACK.IsSarcFile(args[i]))
                        ConsoleUnpack(args[i]);
                    else if (PACK.IsYaz0File(args[i]))
                        ConsoleDecode(args[i]);
                    //else
                        //ConsoleEncode(args[i]);
                }
            }
        }
        #endregion

        #region Commands
        static public void Commands(string[] args)
        {
            {
                Console.WriteLine(); //blankline
                switch (args[0])
                {
                    case "/d":
                        ConsoleDecode(args);
                        break;
                    case "/e":
                        break;
                    case "/u":
                        ConsoleUnpack(args);
                        break;
                    case "/b":
                        break;
                    case "/?":
                        ConsoleHelp();
                        break;
                    default:
                        Console.WriteLine("For help type /?");
                        break;
                }
            }
        }
        #endregion

        #region ConsoleDecode
        static private void ConsoleDecode(string arg)
        {
            string[] args = new[] { "", arg }; //skip for click'n'drag
            ConsoleDecode(args);
        }

        static private void ConsoleDecode(string[] args) //Console Decode
        {
            if (args.Length == 2 && File.Exists(args[1]))

                if (Yaz0.Decode(args[1], Yaz0.DecodeOutputFileRename(args[1])))
                    Console.WriteLine("Decode Successful");
                else
                    Console.WriteLine("Decode error: " + Yaz0.lerror);
            else if (args.Length == 3 && File.Exists(args[1]) && File.Exists(args[2]))
                if (Yaz0.Decode(args[1], args[2]))
                    Console.WriteLine("Decode Successful");
                else
                    Console.WriteLine("Decode error: " + Yaz0.lerror);
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Error: Incorrect use of Decode command.");
                Console.WriteLine("/d <Input File> [Output File Path]");
                Console.ForegroundColor = ConsoleColor.White;
            }
        }
        #endregion

        #region ConsoleEncode
        static private void ConsoleEncode(string arg)
        {
            string[] args = new[] { "", arg }; //skip for click'n'drag
            ConsoleEncode(args);
        }

        static private void ConsoleEncode(string[] args) //Console Encode
        {
            if (args.Length == 2 && File.Exists(args[1]))

                if (Yaz0.Decode(args[1], Yaz0.DecodeOutputFileRename(args[1])))
                    Console.WriteLine("Encode Successful");
                else
                    Console.WriteLine("Encode error: " + Yaz0.lerror);
            else if (args.Length == 3 && File.Exists(args[1]) && File.Exists(args[2]))
                if (Yaz0.Decode(args[1], args[2]))
                    Console.WriteLine("Encode Successful");
                else
                    Console.WriteLine("Encode error: " + Yaz0.lerror);
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Error: Incorrect use of Decode command.");
                Console.WriteLine("/d <Input File> [Output File Path]");
                Console.ForegroundColor = ConsoleColor.White;
            }
        }
        #endregion

        #region ConsoleUnpack
        static private void ConsoleUnpack(string arg)
        {
            string[] args = new[] { "", arg }; //skip for click'n'drag
            ConsoleUnpack(args);
        }
        static private void ConsoleUnpack(string[] args)
        {
            if (args.Length == 2 && File.Exists(args[1]))
                if (PACK.Extract(args[1], Path.GetDirectoryName(args[1]) + "\\" + Path.GetFileNameWithoutExtension(args[1])))
                    Console.WriteLine("Unpack Successful");
                else
                    Console.WriteLine("Unpack error: " + PACK.lerror);
            else if (args.Length == 3 && File.Exists(args[1]) && Directory.Exists(args[2]))
                if (PACK.Extract(args[1], args[2]))
                    Console.WriteLine("Unpack Successful");
                else
                    Console.WriteLine("Unpack error: " + PACK.lerror);
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Error: Incorrect use of Unpack command.");
                Console.WriteLine("/u <Input File> [Output File Path]");
                Console.ForegroundColor = ConsoleColor.White;
            }
        }
        #endregion

        #region ConsoleHelp
        static private void ConsoleHelp()
        {
            Console.WriteLine("\t" + "Usage:");
            Console.WriteLine("\t" + "   " + "/command");
            Console.WriteLine("\t" + "   " + "<required argument>");
            Console.WriteLine("\t" + "   " + "[optional argument]");
            Console.WriteLine("");
            Console.WriteLine("\t" + "Command list:");
            Console.WriteLine("\t" + "   " + "---Decode---");
            Console.WriteLine("\t" + "   " + "/d <Input File> [Output File]");
            Console.WriteLine("\t" + "   " + "---Encode---");
            Console.WriteLine("\t" + "   " + "/e <Input File> [Output File]");
            Console.WriteLine("\t" + "   " + "---Unpack---");
            Console.WriteLine("\t" + "   " + "/u <Input File> [Output Folder]");
            Console.WriteLine("\t" + "   " + "---Build---");
            Console.WriteLine("\t" + "   " + "/b <Input Folder> [Output File]");
        }
        #endregion
    }
}
