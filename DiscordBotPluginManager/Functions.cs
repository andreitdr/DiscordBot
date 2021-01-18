using Discord.WebSocket;

using System;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Windows.Forms;

namespace DiscordBotPluginManager
{
    public static class Functions
    {
        private static string dataFolder = @".\Data\Resources\";
        private static string logFolder = @".\Output\Logs\";
        private static string errFolder = @".\Output\Errors\";

        public static string readCodeFromFile(string fileName, SearchDirectory sd, string Code, char separator, char commentMark = '#')
        {
            if (sd == SearchDirectory.RESOURCES)
                return File.ReadAllLines(Path.Combine(dataFolder, fileName))
                    .Where(p => p.StartsWith(Code) && !p.StartsWith(commentMark.ToString()))
                    .First().Split(separator)[1] ?? null;
            else
                return File.ReadAllLines(fileName)
                    .Where(p => p.StartsWith(Code) && !p.StartsWith(commentMark.ToString()))
                    .First().Split(separator)[1] ?? null;
        }

        public static string readZipFile(string FileName, string archFile = "DiscordBot.pak", ZipSearchType type = ZipSearchType.ALL_TEXT, string searchPattern = null)
        {
            try
            {
                archFile = Path.Combine(dataFolder, archFile);
                using (var fileStream = new FileStream(archFile, FileMode.Open))
                using (var zipArchive = new ZipArchive(fileStream, ZipArchiveMode.Read))
                using (var stream = zipArchive.Entries.Where(l => l.Name == FileName || l.FullName == FileName).First().Open())
                using (var reader = new StreamReader(stream))
                {
                    string fileData = reader.ReadToEnd();
                    switch (type)
                    {
                        case ZipSearchType.ALL_TEXT:
                            return fileData;

                        case ZipSearchType.CODE:
                            if (searchPattern == null) throw new Exception("SearchPattern is invalid");
                            string r = fileData.Split('\n').Where(data => data.Split('\t')[0].Equals(searchPattern)).FirstOrDefault().Split('\t')[1] ?? null;
                            return r.Remove(r.Length - 1);

                        default:
                            throw new Exception("Unknown ZipSearchType");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Discord Bot Plugin Manager", MessageBoxButtons.YesNo, MessageBoxIcon.Error);
                return null;
            }
        }

        public static SocketVoiceChannel GetVoiceChannel(SocketGuildUser user) => user.VoiceChannel;

        public static async void ConnectToChannel(SocketVoiceChannel channel) => await channel.ConnectAsync();

        public static void WriteLogFile(string LogMessage)
        {
            string logsPath = Path.Combine(logFolder, "Log.txt");
            if (!Directory.Exists(logFolder))
                Directory.CreateDirectory(logFolder);
            File.AppendAllText(logsPath, LogMessage + " \n");
        }

        public static void WriteErrFile(string ErrMessage)
        {
            string errPath = Path.Combine(errFolder, "Error.txt");
            if (!Directory.Exists(errFolder))
                Directory.CreateDirectory(errFolder);
            File.AppendAllText(errPath, ErrMessage + " \n");
        }
    }
}