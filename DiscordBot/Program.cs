using S = System;
using F = System.Windows.Forms;

namespace DiscordBot
{
    public class Program
    {
        /// <summary>
        ///     The main entry point for the application.
        /// </summary>
        [S::STAThread]
        [S::Obsolete]
        public static void Main(string[] args) {

            F::Application.EnableVisualStyles();
            F::Application.SetCompatibleTextRenderingDefault(false);
            S::AppDomain.CurrentDomain.AppendPrivatePath("Data\\lib");
            F::Application.Run(new Form1());
        }
    }
}
