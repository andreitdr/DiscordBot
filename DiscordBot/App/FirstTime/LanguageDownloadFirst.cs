using DiscordBotPluginManager.Online;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DiscordBot.App.FirstTime
{
    public partial class LanguageDownloadFirst : Form
    {
        public LanguageDownloadFirst() {
            InitializeComponent();

            Load += (sender, e) => FormLoad();
        }

        private bool closeForced = true;
        private void FormLoad() {

            FormClosing += (sender, e) => {
                if (closeForced) Environment.Exit(0);

            };


            button1.Click += (sender, e) => {
                this.Hide();

                if (new SettingsFirstRun().ShowDialog() == DialogResult.OK) {
                    closeForced = false;
                    this.Close();
                    return;
                }
                this.Close();
            };

            button2.Click += (sender, e) => {
                panel1.Controls.Clear();

                var form = new LanguageList();
                form.TopLevel = false;
                form.Dock = DockStyle.Fill;
                panel1.Controls.Add(form);
                form.FormBorderStyle = FormBorderStyle.None;
                form.Show();
            };

            Text = "Select your Language";
            button2.PerformClick();
        }
    }
}
