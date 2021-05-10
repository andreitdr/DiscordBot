using System;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using DiscordBot.App.FirstTime;
using DiscordBot.App.Theme;
using DiscordBot.Discord.Core;
using DiscordBotPluginManager;
using DiscordBotPluginManager.Language_System;
using DiscordBotPluginManager.Online;
using DiscordBotPluginManager.Plugins;
using static DiscordBotPluginManager.Functions;

namespace DiscordBot
{
	public partial class Form1 : Form
	{
		private Point mousedownpoint = Point.Empty;

		private Boot discordBooter;

		public Form1()
		{
			InitializeComponent();
			Directory.CreateDirectory(dataFolder);
			Directory.CreateDirectory(langFolder);
			Directory.CreateDirectory(errFolder);
			Directory.CreateDirectory(logFolder);
			Directory.CreateDirectory(pakFolder);
			DetectLanguage();
			LoadTexts();
			Load += (sender, e) => FormLoaded();
		}

		private void DetectLanguage()
		{
			if (!File.Exists(Path.Combine(dataFolder, "DiscordBotSettings.data")))
			{
				LoadLanguage("English");
				File.WriteAllText(Path.Combine(dataFolder, "DiscordBotSettings.data"), "BotLanguage=English");
				return;
			}

			string language =
				readCodeFromFile(Path.Combine(dataFolder, "DiscordBotSettings.data"), "BotLanguage", '=');

			if (language != null)
				LoadLanguage(language);
			else LoadLanguage("English");
		}

		private void LoadLanguage(string name)
		{
			foreach (string file in Directory.EnumerateFiles(langFolder))
				if (readCodeFromFile(file,
									 "LANGUAGE_NAME", '=') == name)
					Language.ActiveLanguage = Language.CreateLanguageFromFile(file);
			if (Language.ActiveLanguage == null)
			{
				new LanguageDownloadFirst().ShowDialog();
				DetectLanguage();
				return;
			}

			LoadTextsOnForm();
		}

		private void LoadLanguageFile(string filePath)
		{
			Language.ActiveLanguage = Language.CreateLanguageFromFile(filePath);
			LoadTextsOnForm();
		}

		private void LoadTexts()
		{
			try
			{
				textBoxToken.Text =
					readCodeFromFile(Path.Combine(dataFolder, "DiscordBotCore.data"), "BOT_TOKEN", '\t') ?? null;
				textBoxPrefix.Text =
					readCodeFromFile(Path.Combine(dataFolder, "DiscordBotCore.data"), "BOT_PREFIX", '\t') ?? null;
			}
			catch
			{
				Directory.CreateDirectory(@".\Data\Resources");
				if (Language.ActiveLanguage == null)
				{
					MessageBox.Show("Invalid Token");
					textBoxPrefix.ReadOnly = false;
					textBoxToken.ReadOnly  = false;
					return;
				}

				MessageBox.Show(Language.ActiveLanguage.LanguageWords["INVALID_TOKEN"],
								Language.ActiveLanguage.LanguageWords["INVALID_TOKEN_TITLE"], MessageBoxButtons.OK,
								MessageBoxIcon.Error);

				textBoxPrefix.ReadOnly = false;
				textBoxToken.ReadOnly  = false;
			}
		}

		private void LoadTextsOnForm()
		{
			if (Language.ActiveLanguage == null) return;
			groupBox1.Text             = Language.ActiveLanguage.LanguageWords["FORM_GROUPBOX_BOT_INFORMATION"];
			labelToken.Text            = Language.ActiveLanguage.LanguageWords["FORM_LABEL_TOKEN"];
			labelPrefix.Text           = Language.ActiveLanguage.LanguageWords["FORM_LABEL_PREFIX"];
			labelClipboardCopy.Text    = Language.ActiveLanguage.LanguageWords["FORM_LABEL_COPY_CLIPBOARD"];
			labelFailedLoadPlugin.Text = Language.ActiveLanguage.LanguageWords["FORM_LABEL_FAIL_LOAD_PLUGINS"];
			buttonCopyToken.Text       = Language.ActiveLanguage.LanguageWords["FORM_BUTTON_COPY_TOKEN"];
			buttonManagePlugins.Text   = Language.ActiveLanguage.LanguageWords["FORM_BUTTON_MANAGE_PLUGINS"];
			buttonReloadPlugins.Text   = Language.ActiveLanguage.LanguageWords["FORM_BUTTON_RELOAD_PLUGINS"];
			buttonStartBot.Text        = Language.ActiveLanguage.LanguageWords["FORM_BUTTON_START"];
			Text                       = Language.ActiveLanguage.LanguageWords["FORM_TITLE"];
		}

		private void FormLoaded()
		{
			buttonCopyToken.AutoSize     = true;
			buttonManagePlugins.AutoSize = true;
			buttonReloadPlugins.AutoSize = true;
			buttonStartBot.AutoSize      = true;
			groupBox1.AutoSize           = true;
			AutoSize                     = true;

			InitializeFormControls();
			InitializeTheme();
			initializeFiles();

			FormClosing += (sender, e) =>
			{
				try
				{
					File.WriteAllText(Path.Combine(dataFolder, "DiscordBotSettings.data"),
									  "BotLanguage=" + Language.ActiveLanguage.LanguageName);
				}
				catch (Exception ex)
				{
					WriteLogFile(ex.Message);
				}
			};

			buttonCopyToken.Click += async (sender, e) =>
			{
				if (labelClipboardCopy.Visible)
					return;
				Clipboard.SetText(textBoxToken.Text);
				labelClipboardCopy.Visible = true;
				await Task.Delay(2000);
				labelClipboardCopy.Visible = false;
			};

			buttonStartBot.Click += async (sender, e) =>
			{
				if (discordBooter != null)
					return;

				if (!textBoxPrefix.ReadOnly)
				{
					string prefix = textBoxPrefix.Text;
					if (prefix.Length != 1) return;

					string token = textBoxToken.Text;
					if (token == null || token.Length != 59)
						return;

					textBoxPrefix.ReadOnly = true;
					textBoxToken.ReadOnly  = true;

					File.WriteAllText(@".\Data\Resources\DiscordBotCore.data",
									  $"BOT_TOKEN\t{token}\nBOT_PREFIX\t{prefix}\n");
				}

				discordBooter = new Boot(textBoxToken.Text, textBoxPrefix.Text,
										 richTextBox1, labelConnectionStatus);
				await discordBooter.Awake();
			};

			buttonReloadPlugins.Click += async (sender, e) =>
			{
				if (labelConnectionStatus.Text != "ONLINE")
				{
					if (labelFailedLoadPlugin.Visible) return;
					labelFailedLoadPlugin.Visible = true;
					await Task.Delay(2000);
					labelFailedLoadPlugin.Visible = false;
				}
				else
				{
					var loader = new PluginLoader(discordBooter.client);
					richTextBox1.AppendText(Language.ActiveLanguage.LanguageWords["PLUGIN_LOADING_START"] + "\n");
					loader.onCMDLoad += (name, success, exception) =>
					{
						if (success)
						{
							richTextBox1.AppendText(Language.ActiveLanguage.FormatText(
														Language.ActiveLanguage.LanguageWords["PLUGIN_LOAD_SUCCESS"],
														name) + "\n");
							WriteLogFile(Language.ActiveLanguage.FormatText(
											 Language.ActiveLanguage.LanguageWords["PLUGIN_LOAD_SUCCESS"], name));
						}
						else
						{
							richTextBox1.AppendText(
								Language.ActiveLanguage.FormatText(
									Language.ActiveLanguage.LanguageWords["PLUGIN_LOAD_FAIL"], name,
									exception.Message) + "\n");
							WriteLogFile(Language.ActiveLanguage.FormatText(
											 Language.ActiveLanguage.LanguageWords["PLUGIN_LOAD_FAIL"], name,
											 exception.Message));
						}
					};

					loader.onADDLoad += (name, success, exception) =>
					{
						if (success)
						{
							richTextBox1.AppendText(Language.ActiveLanguage.FormatText(
														Language.ActiveLanguage.LanguageWords["ADDON_LOAD_SUCCESS"],
														name) + "\n");
							WriteLogFile(Language.ActiveLanguage.FormatText(
											 Language.ActiveLanguage.LanguageWords["ADDON_LOAD_SUCCESS"], name));
						}
						else
						{
							richTextBox1.AppendText(
								Language.ActiveLanguage.FormatText(
									Language.ActiveLanguage.LanguageWords["ADDON_LOAD_FAIL"], name,
									exception.Message) + "\n");
							WriteLogFile(Language.ActiveLanguage.FormatText(
											 Language.ActiveLanguage.LanguageWords["ADDON_LOAD_FAIL"], name,
											 exception.Message));
						}
					};

					loader.onEVELoad += (name, success, exception) =>
					{
						if (success)
						{
							richTextBox1.AppendText(Language.ActiveLanguage.FormatText(
														Language.ActiveLanguage.LanguageWords["EVENT_LOAD_SUCCESS"],
														name) + "\n");
							WriteLogFile(Language.ActiveLanguage.FormatText(
											 Language.ActiveLanguage.LanguageWords["EVENT_LOAD_SUCCESS"], name));
						}
						else
						{
							richTextBox1.AppendText(
								Language.ActiveLanguage.FormatText(
									Language.ActiveLanguage.LanguageWords["EVENT_LOAD_FAIL"], name,
									exception.Message) + "\n");
							WriteLogFile(Language.ActiveLanguage.FormatText(
											 Language.ActiveLanguage.LanguageWords["EVENT_LOAD_FAIL"], name,
											 exception.Message));
						}
					};

					buttonManagePlugins.Click += (o, args) =>
					{
						buttonManagePlugins.Enabled = false;
						new Plugins_Manager(discordBooter.client, ForeColor, BackColor).ShowDialog();
						buttonManagePlugins.Enabled = true;
					};
					loader.LoadPlugins();


					buttonReloadPlugins.Enabled = false;
				}
			};

			languageToolStripMenuItem.Click += (sender, e) =>
			{
				languageToolStripMenuItem.DropDownItems.Clear();

				foreach (string file in Directory.EnumerateFiles(langFolder))
				{
					string langName = readCodeFromFile(file,
													   "LANGUAGE_NAME", '=');
					ToolStripItem ms = languageToolStripMenuItem.DropDownItems.Add(langName);
					ms.Click += (_, __) => { LoadLanguageFile(file); };
				}

				ToolStripItem menuitem = languageToolStripMenuItem.DropDownItems.Add("Download new Language");
				menuitem.Click += (_, __) => { new LanguageList().ShowDialog(); };

				languageToolStripMenuItem.DoubleClickEnabled = false;
				languageToolStripMenuItem.ShowDropDown();
			};

			downloadPluginsToolStripMenuItem.Click += (sender, e) => { new PluginsList().ShowDialog(); };
		}

		private void initializeFiles()
		{
			if (File.Exists(Path.Combine(logFolder, "Log.txt")))
				File.Delete(Path.Combine(logFolder, "Log.txt"));
		}

		private void InitializeFormControls()
		{
			menuStrip1.MouseDown += (sender, e) => { mousedownpoint = new Point(e.X, e.Y); };
			menuStrip1.MouseMove += (sender, e) =>
			{
				if (mousedownpoint.IsEmpty)
					return;
				Location = new Point(Location.X + (e.X - mousedownpoint.X), Location.Y + (e.Y - mousedownpoint.Y));
			};
			menuStrip1.MouseUp += (sender, e) => { mousedownpoint = Point.Empty; };

			//Design
			panelExit.Paint += (sender, e) =>
			{
				Graphics z   = e.Graphics;
				var      pen = new Pen(Color.FromArgb(230, 179, 70));
				z.DrawLine(pen, 7, 7,  19, 19);
				z.DrawLine(pen, 7, 19, 19, 7);
				z.DrawLine(pen, 8, 7,  20, 19);
				z.DrawLine(pen, 8, 19, 20, 7);
			};

			panelMin.Paint += (sender, e) =>
			{
				Graphics z      = e.Graphics;
				Color    myclor = Color.FromArgb(230, 179, 70);
				var      brush  = new SolidBrush(myclor);
				var      pen    = new Pen(Color.FromArgb(230, 179, 70));
				z.DrawRectangle(pen, 7, 16, 12, 4);
				z.FillRectangle(brush, 7, 16, 12, 4);
			};

			//Click Event
			panelExit.Click += (sender, e) => Environment.Exit(0);
			panelMin.Click  += (sender, e) => WindowState = FormWindowState.Minimized;


			//Mouse events
			panelExit.MouseEnter += (sender, e) => panelExit.BackColor = Color.Red;
			panelExit.MouseLeave += (sender, e) => panelExit.BackColor = Color.Transparent;

			panelMin.MouseEnter += (sender, e) => panelMin.BackColor = Color.AliceBlue;
			panelMin.MouseLeave += (sender, e) => panelMin.BackColor = Color.Transparent;
		}

		private void InitializeTheme()
		{
			var dark = new Theme("Dark", Color.FromArgb(25, 25, 25), Color.Coral, Color.Transparent, Color.Transparent);
			var light = new Theme("Light", Color.WhiteSmoke, Color.Black, Color.Transparent, Color.Transparent);
			string theme;
			try
			{
				theme = readCodeFromFile(Path.Combine(dataFolder, "DiscordBotSettings.data"), "THEME", '=');
			}
			catch
			{
				theme = null;
			}


			if (theme == null || theme == "Light") light.SetTheme(this);
			else dark.SetTheme(this);

			darkToolStripMenuItem.Click += (sender, e) =>
			{
				dark.SetTheme(this);
				WriteToSettings(dataFolder + "DiscordBotSettings.data", "THEME", "Dark", '=');
			};
			lightToolStripMenuItem.Click += (sender, e) =>
			{
				light.SetTheme(this);
				WriteToSettings(dataFolder + "DiscordBotSettings.data", "THEME", "Light", '=');
			};
		}
	}
}
