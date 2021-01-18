using System;
using System.Windows.Forms;

namespace DiscordBot
{
    internal class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        [Obsolete]
        private static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            AppDomain.CurrentDomain.AppendPrivatePath("Data\\lib");
            Application.Run(new Form1());
        }
    }
}