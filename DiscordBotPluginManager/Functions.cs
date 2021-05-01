using System;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Windows.Forms;

namespace DiscordBotPluginManager
{
    public static class Functions
    {
        public static readonly string dataFolder = @".\Data\Resources\";
        public static readonly string logFolder = @".\Output\Logs\";
        public static readonly string errFolder = @".\Output\Errors\";
        public static readonly string langFolder = @".\Data\Languages\";


        public static string readCodeFromFile(string fileName, SearchDirectory sd, string Code, char separator,
                                              char commentMark = '#') {
            if (sd == SearchDirectory.RESOURCES)
                return File.ReadAllLines(Path.Combine(dataFolder, fileName))
                           .Where(p => p.StartsWith(Code) && !p.StartsWith(commentMark.ToString()))
                           .First().Split(separator)[1] ?? null;
            if (sd == SearchDirectory.LANGUAGE)
                return File.ReadAllLines(Path.Combine(langFolder, fileName))
                           .Where(p => p.StartsWith(Code) && !p.StartsWith(commentMark.ToString()))
                           .First().Split(separator)[1] ?? null;
            return File.ReadAllLines(fileName)
                       .Where(p => p.StartsWith(Code) && !p.StartsWith(commentMark.ToString()))
                       .First().Split(separator)[1] ?? null;
        }

        public static string readZipFile(string FileName, string archFile,
                                         ZipSearchType type = ZipSearchType.ALL_TEXT, string searchPattern = null) {
            try
            {
                archFile = Path.Combine(dataFolder, archFile);
                using (var fileStream = new FileStream(archFile, FileMode.Open))
                using (var zipArchive = new ZipArchive(fileStream, ZipArchiveMode.Read))
                using (Stream stream = zipArchive.Entries.Where(l => l.Name == FileName || l.FullName == FileName)
                                                 .First().Open())
                using (var reader = new StreamReader(stream))
                {
                    string fileData = reader.ReadToEnd();
                    switch (type)
                    {
                        case ZipSearchType.ALL_TEXT:
                            return fileData;

                        case ZipSearchType.CODE:
                            if (searchPattern == null) throw new Exception("SearchPattern is invalid");
                            string r = fileData.Split('\n').Where(data => data.Split('\t')[0].Equals(searchPattern))
                                               .FirstOrDefault().Split('\t')[1] ?? null;
                            return r.Remove(r.Length - 1);
                        default:
                            throw new Exception("Unknown ZipSearchType");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Discord Bot Plugin Manager", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }

        public static Image ReadImage(string FileName, string archFile) {
            archFile = Path.Combine(dataFolder, archFile);
            using (var s = new FileStream(archFile, FileMode.Open))
            {
                var zip = new ZipArchive(s, ZipArchiveMode.Read);

                foreach (ZipArchiveEntry entry in zip.Entries)
                    if (entry.FullName == FileName || entry.Name == FileName)
                    {
                        Stream stream = entry.Open();
                        {
                            Image i = Image.FromStream(stream);
                            stream.Close();
                            return i;
                        }
                    }
            }


            return null;
        }

        public static void WriteLogFile(string LogMessage) {
            string logsPath = Path.Combine(logFolder, "Log.txt");
            if (!Directory.Exists(logFolder))
                Directory.CreateDirectory(logFolder);
            File.AppendAllText(logsPath, LogMessage + " \n");
        }

        public static void WriteErrFile(string ErrMessage) {
            string errPath = Path.Combine(errFolder, "Error.txt");
            if (!Directory.Exists(errFolder))
                Directory.CreateDirectory(errFolder);
            File.AppendAllText(errPath, ErrMessage + " \n");
        }

        public static void WriteToSettings(string file, string Code, string newValue, char separator) {
            string[] lines = File.ReadAllLines(file);
            File.Delete(file);
            bool ok = false;
            foreach (var line in lines)
                if (line.StartsWith(Code))
                { File.AppendAllText(file, Code + separator + newValue + "\r\n"); ok = true; }
                else File.AppendAllText(file, line + "\r\n");

            if (!ok)
                File.AppendAllText(file, Code + separator + newValue + "\r\n");
        }
    }
}