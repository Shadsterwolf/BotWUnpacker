using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;


namespace BotWUnpacker
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
                Application.Run(new Form1()); //Open application
            }
            else
            {
                AttachConsole(-1); //Pass to parent console that sent the arguments
                ConsoleHandler.Initialize(args);
                SendKeys.SendWait("{ENTER}"); //Thank you StackOverflow (Rob L)
            }
                
        }
        
    }

}
