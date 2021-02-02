using Discord.WebSocket;

using DiscordBot.Discord.Core;

using DiscordBotPluginManager;
using DiscordBotPluginManager.Plugins;

using System;
using System.ComponentModel;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

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
                    return;
                }
                else
                {
                    PluginLoader loader = new PluginLoader();
                    var plgs = loader.LoadPlugins(richTextBox1);
                    if (!initClickMethod)
                    {
                        buttonManagePlugins.Click += (Sender, arg) =>
                          {
                              Task.Run(async () =>
                                   new Plugins_Manager(plgs, discordBooter.client).ShowDialog());

                          };
                        initClickMethod = true;
                    }
                    foreach (var v in PluginLoader.Addons)
                    {
                        v.Execute(this);
                    }

                    buttonReloadPlugins.Enabled = false;
                }
            };



        }
    }
}