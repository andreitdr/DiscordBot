using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using Discord;
using DiscordBot.Discord.Core;
using DiscordBotPluginManager;

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
			if (args.Length == 0)
				Application.Run(new Form1());
			else
				HandleInput(args).Wait();
		}

		private static async Task ResetSettings()
		{
			string[] files = Directory.GetFiles(@".\Data\Resources");
			foreach (string file in files) File.Delete(file);
		}

		private static async Task NoGUI(Boot discordbooter)
		{
			while (true)
			{
				string[] data = Console.ReadLine().Split(' ');
				switch (data[0])
				{
					case "/shutdown":
					case "/sd":
						if (discordbooter.client.ConnectionState == ConnectionState.Connected)
							await discordbooter.ShutDown().ContinueWith(t =>
							{
								Console.WriteLine("[INFO] Disconnected !");
								Environment.Exit(0);
							});

						break;
					case "/loadplugins":
					case "/lp":
						var loader = new PluginLoader(discordbooter.client);
						loader.onADDLoad += (name, success, exception) =>
						{
							if (success) Console.WriteLine("[ADDON] Successfully loaded addon : " + name);
							else
								Console.WriteLine("[ADDON] Failed to load ADDON : " + name + " because " +
												  exception.Message);
						};
						loader.onCMDLoad += (name, success, exception) =>
						{
							if (success) Console.WriteLine("[CMD] Successfully loaded addon : " + name);
							else
								Console.WriteLine("[CMD] Failed to load ADDON : " + name + " because " +
												  exception.Message);
						};
						loader.onEVELoad += (name, success, exception) =>
						{
							if (success) Console.WriteLine("[EVENT] Successfully loaded addon : " + name);
							else
								Console.WriteLine("[EVENT] Failed to load ADDON : " + name + " because " +
												  exception.Message);
						};
						loader.LoadPlugins();
						break;
				}
			}
		}

		private static async Task<Boot> StartNoGUI()
		{
			Console.Clear();
			string token =
				Functions.readCodeFromFile(Path.Combine(Functions.dataFolder, "DiscordBotCore.data"), "BOT_TOKEN",
										   '\t');
			string prefix = Functions.readCodeFromFile(Path.Combine(Functions.dataFolder, "DiscordBotCore.data"),
													   "BOT_PREFIX",
													   '\t');
			Console.WriteLine("Starting bot with " + token + " " + prefix);
			var discordbooter = new Boot(token, prefix);

			await discordbooter.Awake(true);

			return discordbooter;
		}

		private static async Task ClearFolder(string d)
		{
			string[] files    = Directory.GetFiles(d);
			int      fileNumb = files.Length;
			for (var i = 0; i < fileNumb; i++)
			{
				File.Delete(files[i]);
				Console.WriteLine("Deleting : " + files[i]);
			}
		}

		private static void ReplaceText(string file, string code, string value)
		{
			try
			{
				var      f    = false;
				string[] text = File.ReadAllLines(file);
				foreach (string line in text)
					if (line.StartsWith(code))
					{
						line.Replace(line.Split('\t')[1], value);
						f = true;
					}

				if (f)
					File.WriteAllLines(@".\Data\Resources\DiscordBotCore.data", text);
				else throw new FileNotFoundException();
			}
			catch (FileNotFoundException)
			{
				File.AppendAllText(file, code + "\t" + value + "\n");
			}
		}

		private static async Task HandleInput(string[] args)
		{
			int len                               = args.Length;
			for (var i = 0; i < len; i++) args[i] = args[i].ToLower();
			if (len == 1)
				switch (args[0])
				{
					case "--reset-settings":
						await ResetSettings();

						break;
					case "--help":
					case "-help":
						Console.WriteLine(
							"-- help | -help \n--reset-full\n--reset-settings\n--set-token [token]\n--set-prefix [prefix]");
						break;
					case "--nogui":
						Boot b = await StartNoGUI();
						await NoGUI(b);
						break;
					case "--reset-full":
						await ClearFolder(".\\Data\\Resources\\");
						await ClearFolder(".\\Output\\Logs\\");
						await ClearFolder(".\\Output\\Errors");
						await ClearFolder(".\\Data\\Languages\\");
						await ClearFolder(".\\Data\\Plugins\\Addons");
						await ClearFolder(".\\Data\\Plugins\\Commands");
						await ClearFolder(".\\Data\\Plugins\\Events");

						break;

					default:
						Application.Run(new Form1());
						break;
				}

			else if (len == 2)
				switch (args[0])
				{
					case "--set-token":
						ReplaceText(@".\Data\Resources\DiscordBotCore.data", "BOT_TOKEN", args[1]);

						break;
					case "--set-prefix":
						ReplaceText(@".\Data\Resources\DiscordBotCore.data", "BOT_PREFIX", args[1]);

						break;
					default:
						Application.Run(new Form1());
						break;
				}

			else if (len == 4)
				switch (args[0])
				{
					case "--set-token":
						switch (args[2])
						{
							case "--set-prefix":
								ReplaceText(@".\Data\Resources\DiscordBotCore.data", "BOT_TOKEN",  args[1]);
								ReplaceText(@".\Data\Resources\DiscordBotCore.data", "BOT_PREFIX", args[3]);

								break;
						}

						break;
					case "--set-ptefix":
						switch (args[2])
						{
							case "--set-token":
								ReplaceText(@".\Data\Resources\DiscordBotCore.data", "BOT_TOKEN",  args[1]);
								ReplaceText(@".\Data\Resources\DiscordBotCore.data", "BOT_PREFIX", args[3]);

								break;
						}

						break;
					default:
						Application.Run(new Form1());
						break;
				}
			else
				Application.Run(new Form1());
		}
	}
}
