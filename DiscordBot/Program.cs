using System;
using System.Windows.Forms;

namespace DiscordBot
{
	public class Program
	{
		/// <summary>
		///     The main entry point for the application.
		/// </summary>
		[STAThread]
		[Obsolete]
		public static void Main(string[] args)
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			AppDomain.CurrentDomain.AppendPrivatePath("Data\\lib");
			Application.Run(new Form1());
		}
	}
}
