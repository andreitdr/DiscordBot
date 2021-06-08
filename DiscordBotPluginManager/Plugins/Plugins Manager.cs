using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

using Discord.WebSocket;

using static DiscordBotPluginManager.PluginLoader;

namespace DiscordBotPluginManager.Plugins
{
    public partial class Plugins_Manager : Form
    {
        public Plugins_Manager(DiscordSocketClient discordClient,
                               Color ForeColor, Color BackColor
        )
        {
            InitializeComponent();
            SetTheme(this, ForeColor, BackColor);
            Load += (sender, e) => LoadForm(discordClient);
        }

        private void LoadAddonGrid(DBAddon[] addons)
        {
            int len = addons.Length;
            for (var i = 0; i < len; i++)
            {
                string description = addons[i].Description;
                string Name = addons[i].Name;
                dataGridAddons.Rows.Add(i, Name ?? "Unknown Name", description ?? "Unknown Description", true);
            }
        }

        private void LoadCMDGrid(DBPlugin[] plugins)
        {
            int len = plugins.Length;
            for (var i = 0; i < len; i++)
            {
                string description = plugins[i].Description;
                string command = plugins[i].Command;
                string usage = plugins[i].Usage;
                dataGridCommands.Rows.Add(i, command ?? "Unknown Command", description ?? "Unknown Description",
                    usage ?? "Unknwon Usage", true);
            }
        }

        private async void LoadForm(DiscordSocketClient client)
        {
            dataGridAddons.Rows.Clear();
            dataGridCommands.Rows.Clear();

            await Task.Delay(1);
            if (Addons.Count != 0)
                LoadAddonGrid(Addons.ToArray());
            if (PluginLoader.Plugins.Count != 0)
                LoadCMDGrid(PluginLoader.Plugins.ToArray());

            //buttonDownloadPlugin.Click += (sender, e) => { new Plugin_Downloader().ShowDialog(); };
            Text = "Entry Latency : " + client.Latency;
        }

        public static void SetTheme(Control form, Color ForeColor, Color BackColor)
        {
            form.BackColor = BackColor;
            form.ForeColor = ForeColor;
            foreach (Control control in form.Controls)
                SetTheme(control, ForeColor, BackColor);
        }
    }
}