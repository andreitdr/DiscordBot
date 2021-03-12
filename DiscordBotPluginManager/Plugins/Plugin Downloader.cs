using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Windows.Forms;

namespace DiscordBotPluginManager.Plugins
{
	public partial class Plugin_Downloader : Form
	{
		private readonly string pluginListURL = "";

		public Plugin_Downloader()
		{
			InitializeComponent();

			Load += (sender, e) => onLoad();
		}

		private List<Plugin> getPlugins(string URL)
		{
			var plugins = new List<Plugin>();
			var client  = new WebClient();
			var reader  = new StreamReader(client.OpenRead(URL));
			while (!reader.EndOfStream)
			{
				string[] line = reader.ReadLine().Split(',');
				plugins.Add(new Plugin
				{
					name = line[0], description = line[1], type = int.Parse(line[2]), link = line[3], fileName = line[4]
				});
			}


			return plugins;
		}

		public void onLoad()
		{
			List<Plugin> plugins = getPlugins(pluginListURL);

			foreach (Plugin p in plugins)
			{
				var b = new Button {Text = "Download"};
				b.Click += (sender, e) =>
				{
					DownloadPlugin(p.link, p.fileName,
						p.type == 1
							? DiscordBotPluginManager.PluginType.COMMAND
							: DiscordBotPluginManager.PluginType.ADDON);
				};
				dataGridView1.Rows.Add(p.name, p.type == 1 ? "Command" : "Addon", p.description, b);
			}
		}

		private void DownloadPlugin(string pluginURL, string fileName, PluginType pluginType)
		{
			if (pluginType == DiscordBotPluginManager.PluginType.COMMAND)
				Process.Start("Patcher.exe", $"/dw=\"{pluginURL}\" /file=.\\Data\\Plugins\\Commands\\{fileName}");
			if (pluginType == DiscordBotPluginManager.PluginType.ADDON)
				Process.Start("Patcher.exe", $"/dw=\"{pluginURL}\" /file=.\\Data\\Plugins\\Addons\\{fileName}");
		}

		private struct Plugin
		{
			public string name;
			public string description;
			public string link;
			public string fileName;
			public int    type;
		}
	}
}