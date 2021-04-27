using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DiscordBotPluginManager.Online
{
    public partial class PluginsList : Form
    {
        private bool FinishedLoading = false;
        private readonly string LanguagesURL = "https://sethdiscordbot.000webhostapp.com/Storage/Discord%20Bot/Plugins";
        public PluginsList() {
            InitializeComponent();

            Load += (sender, e) => FormLoad();
        }

        private async Task<DataGridLine[]> GetDataFromServer() {

            WebClient c = new WebClient();
            c.UseDefaultCredentials = true;
            var result = await c.OpenReadTaskAsync(LanguagesURL);
            string data = await new StreamReader(result).ReadToEndAsync();

            // Name    Desc   type    Link
            string[] lines = data.Split('\n');
            int len = lines.Length;
            DataGridLine[] l = new DataGridLine[len];


            for (int i = 0; i < len; i++)
            {
                string[] s = lines[i].Split(',');
                l[i] = new DataGridLine() { Name = s[0], Description = s[1], Type = s[2], Link = s[3] };
            }

            return l;
        }

        private struct DataGridLine
        {
            public string Name;
            public string Description;
            public string Type;
            public string Link;
        }

        private void LoadGridView(DataGridLine[] dataGridLines) {
            int lines = dataGridLines.Length;
            for (int i = 0; i < lines; i++)
            {
                Button b = new Button();
                b.Text = "Download";
                b.Name = "DownloadPluginButton" + i;
                dataGridView1.Rows.Add((i + 1).ToString(), dataGridLines[i].Name, dataGridLines[i].Description, dataGridLines[i].Type, b);
            }

            dataGridView1.CellClick += (sender, e) =>
            {
                if (e.ColumnIndex == 4 && e.RowIndex >= 0)
                {
                    Directory.CreateDirectory(@".\Data\Plugins\Commands");
                    Directory.CreateDirectory(@".\Data\Plugins\Addons");
                    Directory.CreateDirectory(@".\Data\Plugins\Events");
                    if (dataGridLines[e.RowIndex].Type == "Command")
                    {
                        string argument = "/dw=\"" + dataGridLines[e.RowIndex].Link + "\" /file=\"" + ".\\DaData\\Plugins\\Commands\\" + dataGridLines[e.RowIndex].Name + ".dll" + "\"";
                        System.Diagnostics.Process.Start(".\\Patcher.exe", argument);
                    }
                    if (dataGridLines[e.RowIndex].Type == "Addon")
                    {
                        string argument = "/dw=\"" + dataGridLines[e.RowIndex].Link + "\" /file=\"" + ".\\DaData\\Plugins\\Addons\\" + dataGridLines[e.RowIndex].Name + ".dll" + "\"";
                        System.Diagnostics.Process.Start(".\\Patcher.exe", argument);
                    }
                    if (dataGridLines[e.RowIndex].Type == "Event")
                    {
                        string argument = "/dw=\"" + dataGridLines[e.RowIndex].Link + "\" /file=\"" + ".\\DaData\\Plugins\\Events\\" + dataGridLines[e.RowIndex].Name + ".dll" + "\"";
                        System.Diagnostics.Process.Start(".\\Patcher.exe", argument);
                    }
                }
            };
            dataGridView1.Refresh();
        }

        private async void FormLoad() {
            Task.Run(StartLoopLoading);

            await Task.Run(async () =>
            {
                DataGridLine[] lines = await GetDataFromServer();
                LoadGridView(lines);
            }).ContinueWith(task =>
            {
                FinishedLoading = true;
                label1.Visible = false;
                dataGridView1.Visible = true;
            });
        }

        private async Task StartLoopLoading() {
            FinishedLoading = false;
            label1.Visible = true;
            dataGridView1.Visible = false;


            int ms = 1000;
            while (!FinishedLoading)
            {
                label1.Text = "Loading";
                await Task.Delay(ms);
                label1.Text = "Loading.";
                await Task.Delay(ms);
                label1.Text = "Loading..";
                await Task.Delay(ms);
                label1.Text = "Loading...";
                await Task.Delay(ms);

            }
        }
    }
}
