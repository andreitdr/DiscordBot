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

namespace DiscordBot.App.Theme
{
    public partial class ThemeSelector : Form
    {
        private bool FinishedLoading;
        private string downloadURL = "https://sethdiscordbot.000webhostapp.com/Storage/Discord%20Bot/Theme";
        public ThemeSelector()
        {
            InitializeComponent();
            Load += (sender, e) => OnLoad();
        }

        struct DataGridLine
        {
            public string themeCathegory;
            public string themeName;
            public string dwLink;
        }

        private async void OnLoad()
        {

            Task.Run(StartLoopLoading);

            await Task.Run(async () =>
            {
                DataGridLine[] lines = await LoadThemes();
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

        private void LoadGridView(DataGridLine[] dataGridLines)
        {
            int lines = dataGridLines.Length;
            for (int i = 0; i < lines; i++)
            {
                Button b = new Button();
                b.Text = "Download";
                b.Name = "DownloadThemeButton" + i;
                dataGridView1.Rows.Add((i + 1).ToString(), dataGridLines[i].themeName, dataGridLines[i].themeCathegory, b);
            }

            dataGridView1.CellClick += (sender, e) =>
            {
                if (e.ColumnIndex == 3 && e.RowIndex >= 0)
                {
                    //MessageBox.Show(dataGridLines[e.RowIndex].Link);
                    Directory.CreateDirectory(".\\Data\\Themes\\" + dataGridLines[e.RowIndex].themeCathegory);
                    string argument = "/dw=\"" + dataGridLines[e.RowIndex].dwLink + "\" /file=\"" + ".\\DaData\\Themes\\" + dataGridLines[e.RowIndex].themeCathegory + "\\" + dataGridLines[e.RowIndex].themeName + ".dbtheme" + "\"";
                    System.Diagnostics.Process.Start(".\\Patcher.exe", argument);
                }
            };
            dataGridView1.Refresh();
        }

        private async Task<DataGridLine[]> LoadThemes()
        {
            WebClient client = new WebClient();
            Stream s = await client.OpenReadTaskAsync(downloadURL);
            string text = await new StreamReader(s).ReadToEndAsync();

            string[] lines = text.Split('\n');
            int len = lines.Length;
            DataGridLine[] dgLines = new DataGridLine[len];

            for (int i = 0; i < len; i++)
            {
                string[] str = lines[i].Split(',');
                dgLines[i] = new DataGridLine() { themeName = str[0], dwLink = str[2], themeCathegory = str[1] };
            }

            return dgLines;
        }
    }
}
