using System;
using System.IO;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace BotWUnpacker
{
    public struct ConsoleHandler
    {
        static public void Initialize(string[] args)
        {
            Console.WriteLine(); //blankline
            switch (args[0])
            {
                case "/decode":
                case "/d":
                    ConsoleDecode(args);
                    break;
                case "/encode":
                case "/e":
                    break;
                case "/?":
                    ConsoleHelp();
                    break;
                default:
                    Console.WriteLine("For help type /?");
                    break;
            }
        }

        static private void ConsoleDecode(string[] args) //Console Decode
        {
            if (args.Length == 2 && File.Exists(args[1]))
                if (Yaz0.Decode(args[1], args[1]))
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
                Console.WriteLine("Error: Incorrect use of decode command.");
                Console.WriteLine("/decode (/d) <Input File> [Output File Path]");
            }
        }

        static private void ConsoleHelp()
        {
            Console.WriteLine("\t" + "Usage:");
            Console.WriteLine("\t" + "   " + "/command");
            Console.WriteLine("\t" + "   " + "<required argument>");
            Console.WriteLine("\t" + "   " + "[optional argument]");
            Console.WriteLine("");
            Console.WriteLine("\t" + "Command list:");
            Console.WriteLine("\t" + "   " + "---Decode---");
            Console.WriteLine("\t" + "   " + "/decode <Input File> [Output File]");
            Console.WriteLine("\t" + "   " + "/d <Input File> [Output File]");
        }
    }
}
