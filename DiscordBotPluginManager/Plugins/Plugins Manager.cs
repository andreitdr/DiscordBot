using Discord.WebSocket;
using System.Threading.Tasks;
using System.Windows.Forms;

using static DiscordBotPluginManager.PluginLoader;

namespace DiscordBotPluginManager.Plugins
{
    public partial class Plugins_Manager : Form
    {
        public Plugins_Manager(DiscordSocketClient discordClient,
                                System.Drawing.Color ForeColor, System.Drawing.Color BackColor
        )
        {
            InitializeComponent();
            SetTheme(this, ForeColor, BackColor);
            Load += (sender, e) => LoadForm(discordClient);

        }

        private void LoadAddonGrid(DBAddon[] addons)
        {
            int len = addons.Length;
            for (int i = 0; i < len; i++)
            {
                var description = addons[i].Description;
                var Name        = addons[i].Name;
                dataGridAddons.Rows.Add(i, Name ?? "Unknown Name", description ?? "Unknown Description", true);
            }
        }

        private void LoadCMDGrid(DBPlugin[] plugins)
        {
            int len = plugins.Length;
            for (int i = 0; i < len; i++)
            {
                var description = plugins[i].Description;
                var command     = plugins[i].Command;
                var usage       = plugins[i].Usage;
                dataGridCommands.Rows.Add(i, command ?? "Unknown Command", description ?? "Unknown Description", usage ?? "Unknwon Usage", true);

            }
        }

        private async void LoadForm(DiscordSocketClient client)
        {
            dataGridAddons.Rows.Clear();
            dataGridCommands.Rows.Clear();

            await Task.Delay(1);
            
            LoadAddonGrid(Addons.ToArray());
            LoadCMDGrid(PluginLoader.Plugins.ToArray());

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
