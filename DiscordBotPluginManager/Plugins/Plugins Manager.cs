

using Discord.WebSocket;

using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Runtime.Remoting.Channels;
using System.Threading.Tasks;
using System.Windows.Forms;

using static DiscordBotPluginManager.PluginLoader;

namespace DiscordBotPluginManager.Plugins
{
    public partial class Plugins_Manager : Form
    {
        public Plugins_Manager(LoadReport loadReport, DiscordSocketClient discordClient,
                                System.Drawing.Color c1, System.Drawing.Color c2
        )
        {
            InitializeComponent();
            SetTheme(this, c1, c2);
            Load += (sender, e) => LoadForm(loadReport, discordClient);

        }

        private void LoadAddonGrid(string[] files)
        {
            int k = 1;

            foreach (var file in files)
            {
                var FName = new FileInfo(file).Name;
                var d = PluginLoader.Addons.GetRange(k - 1, 1).First().Description;
                dataGridAddons.Rows.Add(k, FName, d ?? "Unknown Description", true);
                k++;
            }
        }

        private void LoadCMDGrid(string[] files)
        {
            int k = 1;

            foreach (var file in files)
            {
                var FName = new FileInfo(file).Name;
                var d = PluginLoader.Plugins.GetRange(k, 1).First().Description;
                dataGridCommands.Rows.Add(k, FName, d ?? "Unknown Description", true);
                k++;
            }
        }

        private async void LoadForm(LoadReport report, DiscordSocketClient client)
        {
            dataGridAddons.Rows.Clear();
            dataGridCommands.Rows.Clear();

            await Task.Delay(1);

            LoadAddonGrid(report.loadedAddonsS);
            LoadCMDGrid(report.loadedCommandsS);

            Text = "Entry Latency : " + client.Latency;

        }

        public static void SetTheme(Control form, System.Drawing.Color ForeColor, System.Drawing.Color BackColor)
        {
            form.BackColor = BackColor;
            form.ForeColor = ForeColor;
            foreach (Control control in form.Controls)
                SetTheme(control, ForeColor, BackColor);
        }
    }
}
