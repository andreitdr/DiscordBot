using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Patcher.Controls;
using Patcher.Online.Server;

namespace Patcher
{
	public partial class Form1 : Form
	{
		private readonly FileDownloader fileDownloader;

		private readonly CustomProgressBar progressBar;
		private          Point             mousedownpoint = Point.Empty;

		public Form1()
		{
			InitializeComponent();
			fileDownloader = new FileDownloader();
			progressBar    = new CustomProgressBar(panel1, panel2);

			progressBar.percentChanged += percent => { label2.Text = percent + "%"; };

			MouseDown += (sender, e) => { mousedownpoint = new Point(e.X, e.Y); };

			MouseMove += (sender, e) =>
			{
				if (mousedownpoint.IsEmpty)
					return;
				Location = new Point(Location.X + (e.X - mousedownpoint.X), Location.Y + (e.Y - mousedownpoint.Y));
			};

			MouseUp += (sender, e) => { mousedownpoint = Point.Empty; };

			Load += async (sender, e) =>
			{
				Directory.CreateDirectory(Globals.Globals.appDataFolder);
				Directory.CreateDirectory(Globals.Globals.downloadsFolder);
				var update        = "noupdate";
				var updateChecker = new UpdateChecker();


				if (updateChecker.CheckForUpdates())
				{
					if (File.Exists(".\\Data\\Resources\\DiscordBotCore.data.bak"))
						File.WriteAllText(".\\Data\\Resources\\DiscordBotCore.data.bak",
							File.ReadAllText(".\\Data\\Resources\\DiscordBotCore.data"));
					await updateChecker.DoUpdate(fileDownloader, progressBar);
					update = "updated";
					if (File.Exists(".\\Data\\Resources\\DiscordBotCore.data.bak"))
					{
						File.WriteAllText(".\\Data\\Resources\\DiscordBotCore.data",
							File.ReadAllText(".\\Data\\Resources\\DiscordBotCore.data.bak"));
						File.Delete(".\\Data\\Resources\\DiscordBotCore.data.bak");
					}
				}

				StartApplication(update);
				Close();
			};
		}

		private void StartApplication(string args)
		{
			using (var p = new Process())
			{
				p.StartInfo.FileName              = "cmd.exe";
				p.StartInfo.UseShellExecute       = false;
				p.StartInfo.RedirectStandardInput = true;

				p.Start();

				var wr = p.StandardInput;
				wr.WriteLine($"main.bot {args}");
				wr.WriteLine("exit");
				p.Close();
			}
		}
	}
}