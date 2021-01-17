using DiscordBot.Online.Patcher;

using System.IO;
using System.Windows.Forms;

namespace DiscordBot.Online
{
    public class PluginDownloader
    {
        public void DownloadPlugin(string downloadURL, string PluginName, DiscordBotPluginManager.PluginType type, ProgressBar bar)
        {
            FileDownloader downloader = new FileDownloader();
            bar.Value = 0;
            var folder = @".\Data\Plugins\";
            switch (type)
            {
                case DiscordBotPluginManager.PluginType.ADDON:
                    folder = Path.Combine(folder, "Addons\\");
                    break;
                case DiscordBotPluginManager.PluginType.COMMAND:
                    folder = Path.Combine(folder, "Commands\\");
                    break;
                default:
                    return;
            }

            folder = Path.Combine(folder, PluginName + ".dll");

            downloader.DownloadProgressChanged += (sender, e) =>
            {
                bar.Value = e.ProgressPercentage;
            };

            downloader.DownloadFileCompleted += (sender, e) =>
            {
                bar.Visible = false;
                MessageBox.Show("Successfully downloaded plugin: " + PluginName, "Discord Bot", MessageBoxButtons.OK, MessageBoxIcon.Information);
            };

            downloader.DownloadFileAsync(downloadURL, folder);
        }
    }
}
