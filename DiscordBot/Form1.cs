using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

using DiscordBot.App.FirstTime;
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
		private Boot discordBooter;

		public Form1()
		{
			InitializeComponent();
			Directory.CreateDirectory(dataFolder);
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
				readCodeFromFile("DiscordBotSettings.data", SearchDirectory.RESOURCES, "BotLanguage", '=');

			if (language != null)
				LoadLanguage(language);
			else LoadLanguage("English");
		}

		private void LoadLanguage(string name)
		{
			foreach (string file in Directory.EnumerateFiles(langFolder))
				if (readCodeFromFile(new FileInfo(file).Name, SearchDirectory.LANGUAGE,
					"LANGUAGE_NAME",                          '=') == name)
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
					readCodeFromFile("DiscordBotCore.data", SearchDirectory.RESOURCES, "BOT_TOKEN", '\t') ?? null;
				textBoxPrefix.Text =
					readCodeFromFile("DiscordBotCore.data", SearchDirectory.RESOURCES, "BOT_PREFIX", '\t') ?? null;
			}
			catch
			{
				Directory.CreateDirectory(@".\Data\Resources");
				if (Language.ActiveLanguage == null)
				{
					MessageBox.Show("Invalid Token");
					textBoxPrefix.ReadOnly = false;
					textBoxToken.ReadOnly = false;
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

			FormClosing += (sender, e) =>
			{
                try
				{
					File.WriteAllText(Path.Combine(dataFolder, "DiscordBotSettings.data"),
							"BotLanguage=" + Language.ActiveLanguage.LanguageName);

				}catch(Exception ex)
                {
					Functions.WriteLogFile(ex.Message);
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
					var loader = new PluginLoader();
					richTextBox1.AppendText(Language.ActiveLanguage.LanguageWords["PLUGIN_LOADING_START"] + "\n");
					loader.onCMDLoad += (name, success, exception) =>
					{
						if (success)
							richTextBox1.AppendText(
								Language.ActiveLanguage.FormatText(
									Language.ActiveLanguage.LanguageWords["PLUGIN_LOAD_SUCCESS"], name) + "\n");
						else
							richTextBox1.AppendText(
								Language.ActiveLanguage.FormatText(
									Language.ActiveLanguage.LanguageWords["PLUGIN_LOAD_FAIL"], name,
									exception.Message) + "\n");
					};
					loader.onADDLoad += (name, success, exception) =>
					{
						if (success)
							richTextBox1.AppendText(
								Language.ActiveLanguage.FormatText(
									Language.ActiveLanguage.LanguageWords["ADDON_LOAD_SUCCESS"], name) + "\n");
						else
							richTextBox1.AppendText(
								Language.ActiveLanguage.FormatText(
									Language.ActiveLanguage.LanguageWords["ADDON_LOAD_FAIL"], name,
									exception.Message) + "\n");
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

			downloadNewLanguageToolStripMenuItem.Click += (sender, e) => { new DiscordBotPluginManager.Online.LanguageList().ShowDialog(); };

			languageToolStripMenuItem.Click += (sender, e) =>
			{
				languageToolStripMenuItem.DropDownItems.Clear();

				foreach (string file in Directory.EnumerateFiles(langFolder))
				{
					string langName = readCodeFromFile(new FileInfo(file).Name, SearchDirectory.LANGUAGE,
						"LANGUAGE_NAME",                                        '=');
					ToolStripItem ms = languageToolStripMenuItem.DropDownItems.Add(langName);
					ms.Click += (_, __) => { LoadLanguageFile(file); };
				}

				languageToolStripMenuItem.DoubleClickEnabled = false;
				languageToolStripMenuItem.ShowDropDown();
			};
		}
	}
}