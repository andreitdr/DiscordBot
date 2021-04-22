using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DiscordBotPluginManager.Online
{
    public partial class LanguageList : Form
    {
        private bool FinishedLoading = false;
        private readonly string LanguagesURL = "https://sethdiscordbot.000webhostapp.com/Storage/Discord%20Bot/Languages";
        public LanguageList()
        {
            InitializeComponent();
            this.Load +=(sender,e) =>  FormLoad();
        }

        private async Task<DataGridLine[]> GetDataFromServer()
        {
            
            WebClient c = new WebClient();
            var result = await c.OpenReadTaskAsync(LanguagesURL);
            string data = await new StreamReader(result).ReadToEndAsync();

            
            // Name    Size    Link
            string[] lines = data.Split('\n');
            int len = lines.Length;
            DataGridLine[] l = new DataGridLine[len];


            for (int i = 0; i<len;i++)
            {
                string[] s = lines[i].Split(',');
                l[i] = new DataGridLine() { Name = s[0], FileSize = s[1], Link = s[2] };
            }

            return l;
        }

        private struct DataGridLine
        {
            public string Name;
            public string Link;
            public string FileSize;
        }

        private void LoadGridView(DataGridLine[] dataGridLines)
        {
            int lines = dataGridLines.Length;
            for(int i = 0; i< lines; i++)
            {
                Button b = new Button();
                b.Text = "Download";
                b.Name = "DownloadPluginButton" + i;
                dataGridView1.Rows.Add(i.ToString(), dataGridLines[i].Name, dataGridLines[i].FileSize, b);
            }

            dataGridView1.CellClick += (sender, e) =>
            {
                if(e.ColumnIndex == 3 && e.RowIndex >= 0)
                {
                    //MessageBox.Show(dataGridLines[e.RowIndex].Link);
                    string argument = "/dw=\"" + dataGridLines[e.RowIndex].Link + "\" /file=\"" + ".\\DaData\\Languages\\" + dataGridLines[e.RowIndex].Name + ".lng" + "\"";
                    System.Diagnostics.Process.Start(".\\Patcher.exe", argument);
                }
            };
            dataGridView1.Refresh();
        }

        private async void FormLoad()
        {
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

        private async Task StartLoopLoading()
        {
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
