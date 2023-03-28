using System;
using System.IO;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace BotwUnpacker
{
    public struct ConsoleHandler
    {
        #region DragAndDrop
        static public void DragAndDropFile(string arg)
        {
            if (SARC.IsSarcFile(arg) && !(Directory.Exists(Path.GetDirectoryName(arg) + "\\" + Path.GetFileNameWithoutExtension(arg)))) //dont overwrite existing folders
                ConsoleUnpack(arg);
            else if (SARC.IsYaz0File(arg))
                ConsoleDecode(arg);
            else if (Yaz0.IsKnownDecodedExtension(arg))
                ConsoleEncode(arg);
        }

        static public void DragAndDropFolder(string arg)
        {
            if (Directory.Exists(arg))
                ConsoleBuildWiiU(arg);
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
                    case "/decode":
                        ConsoleDecode(args);
                        break;
                    case "/e":
                    case "/encode":
                        ConsoleEncode(args);
                        break;
                    case "/u":
                    case "/unpack":
                        ConsoleUnpack(args);
                        break;
                    case "/b":
                    case "/build":
                    case "/bu":
                    case "/buildwiiu":
                        ConsoleBuildWiiU(args);
                        break;
                    case "/bs":
                    case "/buildswitch":
                        ConsoleBuildSwitch(args);
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
            else if (args.Length == 3 && File.Exists(args[1]))
                if (Yaz0.Decode(args[1], args[2]))
                    Console.WriteLine("Decode Successful");
                else
                    Console.WriteLine("Decode error: " + Yaz0.lerror);
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Error: Incorrect use of Decode command.");
                Console.WriteLine("/d <Input File> [Output File]");
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
            {
                if (Yaz0.Encode(args[1], Yaz0.EncodeOutputFileRename(args[1])))
                    Console.WriteLine("Encode Successful");
                else
                    Console.WriteLine("Encode error: " + Yaz0.lerror);
            }
            else if (args.Length == 3 && File.Exists(args[1]))
            {
                if (Yaz0.Encode(args[1], args[2]))
                    Console.WriteLine("Encode Successful");
                else
                    Console.WriteLine("Encode error: " + Yaz0.lerror);
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Error: Incorrect use of Decode command.");
                Console.WriteLine("/e <Input File> [Output File]");
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
            {
                if (SARC.Extract(args[1], Path.GetDirectoryName(args[1]) + "\\" + Path.GetFileNameWithoutExtension(args[1])))
                    Console.WriteLine("Unpack Successful");
                else
                    Console.WriteLine("Unpack error: " + SARC.lerror);
            }
            else if (args.Length == 3 && File.Exists(args[1]))
            {
                if (SARC.Extract(args[1], args[2]))
                    Console.WriteLine("Unpack Successful");
                else
                    Console.WriteLine("Unpack error: " + SARC.lerror);
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Error: Incorrect use of Unpack command.");
                Console.WriteLine("/u <Input File> [Output Folder]");
                Console.ForegroundColor = ConsoleColor.White;
            }
        }
        #endregion

        #region ConsoleBuildWiiU
        static private void ConsoleBuildWiiU(string arg)
        {
            string[] args = new[] { "", arg }; //skip for click'n'drag
            ConsoleBuildWiiU(args);
        }
        static private void ConsoleBuildWiiU(string[] args)
        {
            if (args.Length == 2 && Directory.Exists(args[1]))
            {
                if (SARC.Build(args[1], args[1].Remove(args[1].LastIndexOf("\\")) + "\\" + Path.GetFileName(args[1]) + ".pack"))
                    Console.WriteLine("Build Successful");
                else
                    Console.WriteLine("Build error: " + SARC.lerror);
            }
            else if (args.Length == 3 && Directory.Exists(args[1]))
            {
                if (SARC.Build(args[1], args[2]))
                    Console.WriteLine("Build Successful");
                else
                    Console.WriteLine("Build error: " + SARC.lerror);
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Error: Incorrect use of Build command.");
                Console.WriteLine("/b <Input Folder> [Output File]");
                Console.ForegroundColor = ConsoleColor.White;
            }
        }
        #endregion

        #region ConsoleBuildSwitch
        static private void ConsoleBuildSwitch(string arg)
        {
            string[] args = new[] { "", arg }; //skip for click'n'drag
            ConsoleBuildSwitch(args);
        }
        static private void ConsoleBuildSwitch(string[] args)
        {
            if (args.Length == 2 && Directory.Exists(args[1]))
            {
                if (SARC.Build(args[1], args[1].Remove(args[1].LastIndexOf("\\")) + "\\" + Path.GetFileName(args[1]) + ".pack", true))
                    Console.WriteLine("Build Successful");
                else
                    Console.WriteLine("Build error: " + SARC.lerror);
            }
            else if (args.Length == 3 && Directory.Exists(args[1]))
            {
                if (SARC.Build(args[1], args[2], true))
                    Console.WriteLine("Build Successful");
                else
                    Console.WriteLine("Build error: " + SARC.lerror);
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Error: Incorrect use of Build command.");
                Console.WriteLine("/bs <Input Folder> [Output File]");
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
            Console.WriteLine("");
            Console.WriteLine("\t" + "Examples:");
            Console.WriteLine("\t" + "   " + "BotwUnpacker.exe /d \"C:\\OrignalFiles\\WiiU\\Model.sbacktorpack\" \"C:\\CustomFiles\\WiiU\\LinkModel\\Model.backtorpack\" ");
            Console.WriteLine("\t" + "   " + "BotwUnpacker.exe /u \"C:\\CustomFiles\\WiiU\\LinkModel\\Model.backtorpack\" ");
            Console.WriteLine("\t" + "   " + "BotwUnpacker.exe /b \"C:\\CustomFiles\\WiiU\\LinkModel\\Model\" \"C:\\CustomFiles\\WiiU\\Model.backtorpack\" ");
            Console.WriteLine("\t" + "   " + "BotwUnpacker.exe /bs \"C:\\CustomFiles\\Switch\\LinkModel\\Model\"");
        }
        #endregion

    }
}
