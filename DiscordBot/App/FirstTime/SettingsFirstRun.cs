using DiscordBotPluginManager;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Channels;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DiscordBot.App.FirstTime
{
    public partial class SettingsFirstRun : Form
    {
        public SettingsFirstRun() {
            InitializeComponent();

            Load += (sender, e) => FormLoad();
        }
        private void FormLoad() {

            if (File.Exists(@".\Data\Resources\DiscordBotCore.data")) {
                textBox1.Text = Functions.readCodeFromFile(Path.Combine(Functions.dataFolder, "DiscordBotCore.data"), "BOT_TOKEN", '\t');
                textBox2.Text = Functions.readCodeFromFile(Path.Combine(Functions.dataFolder, "DiscordBotCore.data"), "BOT_PREFIX", '\t');
            } else {
                textBox1.Clear();
                textBox2.Clear();
            }
            foreach (var file in Directory.EnumerateFiles(Functions.langFolder)) {
                string langName = Functions.readCodeFromFile(file, "LANGUAGE_NAME", '=');
                comboBox1.Items.Add(langName);
            }

            if (comboBox1.Items.Count == 0) {
                DialogResult = DialogResult.Abort;
                this.Close();
            }



            button1.Click += (sender, e) => {
                if (textBox1.Text.Length < 50) {
                    MessageBox.Show("Invalid discord bot token !", "Discord Bot", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (textBox2.Text.Length != 1) {
                    MessageBox.Show("Invalid Discord bot prefix !", "Discord Bot", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (comboBox1.SelectedItem.ToString() is null) {
                    MessageBox.Show("Invalid language selected", "Discord Bot", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                DialogResult = DialogResult.OK;

                File.WriteAllText(@".\Data\Resources\DiscordBotCore.data",
                    $"BOT_TOKEN\t{textBox1.Text}\nBOT_PREFIX\t{textBox2.Text}\n");
                File.WriteAllText(@".\Data\Resources\DiscordBotSettings.data", "BotLanguage=" + comboBox1.SelectedItem.ToString());
                this.Close();
            };

            button2.Click += (sender, e) => {
                Process.Start("https://discord.com/developers/applications/");
            };
        }


    }
}
