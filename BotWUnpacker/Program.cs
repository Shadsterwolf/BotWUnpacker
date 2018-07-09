using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;


namespace BotwUnpacker
{
    static class Program
    {
        [DllImport("kernel32.dll")] //Import for console
        static extern bool AttachConsole(int dwProcessId); //Pass to console magic, since "AllocConsole" would just make seperate console window...
        [STAThread]
        static void Main(string[] args)
        {
            if (args.Length == 0) //Determine if to utilize application or pass console arguments
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new FrmMain()); //Open application
            }
            else if (args.Length > 0 && (!args[0].StartsWith("/"))) //Drag n' drop (no slash command executed)
            {
                for (int i = 0; i < args.Length; i++)
                {
                    if (File.Exists(args[i])) 
                        ConsoleHandler.DragAndDropFile(args[i]);
                    else if (Directory.Exists(args[i]))
                        ConsoleHandler.DragAndDropFolder(args[i]);
                }
            }
            else
            {
                AttachConsole(-1); //Pass to parent console that sent the arguments
                ConsoleHandler.Commands(args);
                SendKeys.SendWait("{ENTER}"); //Thank you StackOverflow (Rob L)
            }
                
        }
        
    }

}
