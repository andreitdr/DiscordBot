using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using DiscordBot.Discord.Core;
using DiscordBotPluginManager;
using DiscordBotPluginManager.Plugins;
using static DiscordBotPluginManager.Functions;

namespace DiscordBot
{
	public partial class Form1 : Form
	{
		private Boot discordBooter;
		private bool initClickMethod;

		public Form1()
		{
			InitializeComponent();
			LoadTexts();
			Load += (sender, e) => FormLoaded();
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
				MessageBox.Show("Inser your token and the prefix and then press Start bot.",
					"Discord Bot", MessageBoxButtons.OK, MessageBoxIcon.Error);

				textBoxPrefix.ReadOnly = false;
				textBoxToken.ReadOnly  = false;
			}
		}

		private void FormLoaded()
		{
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
					var prefix = textBoxPrefix.Text;
					if (prefix.Length != 1) return;

					var token = textBoxToken.Text;
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
					richTextBox1.AppendText("[PLUGIN] Initializing plugin system...\n");
					loader.onCMDLoad += (name, success, exception) =>
					{
						if (success)
							richTextBox1.AppendText("[PLUGIN] Command " + name + " successfully initialized\n");
						else
							richTextBox1.AppendText("Command " + name + " failed to load. Reason: " +
							                        exception.Message);
					};
					loader.onADDLoad += (name, success, exception) =>
					{
						if (success)
							richTextBox1.AppendText("[PLUGIN] Addon " + name + " successfully initialized\n");
						else
							richTextBox1.AppendText("Addon " + name + " failed to load. Reason: " + exception.Message);
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
		}
	}
}