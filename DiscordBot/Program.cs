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
        private static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            AppDomain.CurrentDomain.AppendPrivatePath("Data\\Managed");
            // new Program().Start();
            Application.Run(new Form1());
        }

        /*        public void Start()
                {
                    AppDomain.CurrentDomain.AssemblyResolve += CurrentDomain_AssemblyResolve;
                }

                System.Reflection.Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
                {
                    string dllName = args.Name.Contains(",") ? args.Name.Substring(0, args.Name.IndexOf(',')) : args.Name.Replace(".dll", "");

                    dllName = dllName.Replace(".", "_");

                    if (dllName.EndsWith("_resources")) return null;

                    System.Resources.ResourceManager rm = new System.Resources.ResourceManager(GetType().Namespace + ".Properties.Resources", System.Reflection.Assembly.GetExecutingAssembly());

                    byte[] bytes = (byte[])rm.GetObject(dllName);

                    return System.Reflection.Assembly.Load(bytes);
                }*/
    }
}