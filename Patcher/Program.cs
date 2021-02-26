using System;
using System.Reflection;
using System.Windows.Forms;

namespace Patcher
{
	internal static class Program
	{
		/// <summary>
		///     The main entry point for the application.
		/// </summary>
		[STAThread]
		private static void Main()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			AppDomain.CurrentDomain.AssemblyResolve += CurrentDomain_AssemblyResolve;
			Application.Run(new Form1());
		}

		private static Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
		{
			using (var stream = Assembly.GetExecutingAssembly()
			                            .GetManifestResourceStream("Patcher.Res.DotNetZip.dll"))
			{
				var ba = new byte[stream.Length];
				stream.Read(ba, 0, ba.Length);
				return Assembly.Load(ba);
			}
		}
	}
}