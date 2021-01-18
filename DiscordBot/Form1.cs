using DiscordBot.Discord.Core;

using DiscordBotPluginManager;

using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

using static DiscordBotPluginManager.Functions;

namespace DiscordBot
{
    public partial class Form1 : Form
    {
        private Boot discordBooter;

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
                textBoxToken.Text = readCodeFromFile("DiscordBotCore.data", SearchDirectory.RESOURCES, "BOT_TOKEN", '\t') ?? null;
                textBoxPrefix.Text = readCodeFromFile("DiscordBotCore.data", SearchDirectory.RESOURCES, "BOT_PREFIX", '\t') ?? null;
            }
            catch
            {
                Directory.CreateDirectory(@".\Data\Resources");
                File.WriteAllText(@".\Data\Resources\DiscordBotCore.data", "#Discord bot data\nBOT_TOKEN\t\"YOUR TOKEN HERE\"\nBOT_PREFIX\t!");
                MessageBox.Show("Edit file : .\\Data\\Resources\\DiscordBotCore.data. Insert your token (and prefix)",
                                            "Discord Bot", MessageBoxButtons.OK, MessageBoxIcon.Error);
                System.Environment.Exit(0);
            }
        }

        private void FormLoaded()
        {
            buttonCopyToken.Click += (sender, e) =>
            {
                Clipboard.SetText(textBoxToken.Text);
                labelClipboardCopy.Visible = true;
                Task.Run(async () =>
                {
                    await Task.Delay(2000);
                }).Wait();
                labelClipboardCopy.Visible = false;
            };

            buttonStartBot.Click += async (sender, e) =>
            {
                if (discordBooter != null)
                    return;
                discordBooter = new Boot(textBoxToken.Text, textBoxPrefix.Text, richTextBox1, labelConnectionStatus);
                await discordBooter.Awake();
            };

            buttonReloadPlugins.Click += async (sender, e) =>
            {
                if (labelConnectionStatus.Text != "ONLINE")
                {
                    labelFailedLoadPlugin.Visible = true;
                    await Task.Delay(2000);
                    labelFailedLoadPlugin.Visible = false;
                    return;
                }
                else
                {
                    PluginLoader loader = new PluginLoader();
                    var plgs = loader.LoadPlugins(richTextBox1);
                    if (plgs[0] != 1)
                        labelFailedLoadPlugin.Text = "Loaded " + plgs[0].ToString() + " plugins !";
                    else labelFailedLoadPlugin.Text = "Loaded 1 plugin !";

                    if (plgs[1] != 1)
                        labelFailedLoadPlugin.Text = "Loaded " + plgs[1].ToString() + " addons !";
                    else labelFailedLoadPlugin.Text = "Loaded 1 addon !";

                    labelFailedLoadPlugin.Visible = true;

                    foreach (var v in PluginLoader.Addons)
                    {
                        v.Execute(this);
                    }
                }
            };
        }
    }
}