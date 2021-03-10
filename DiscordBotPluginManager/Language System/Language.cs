using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace DiscordBotPluginManager.Language_System
{
	public class Language
	{
		public static Language ActiveLanguage = null;

		private Language(string fileName, Dictionary<string, string> words, string LanguageName)
		{
			this.fileName     = fileName;
			this.LanguageName = LanguageName;
			LanguageWords     = words;
		}

		public string LanguageName { get; }

		public string fileName { get; }

		public Dictionary<string, string> LanguageWords { get; }

		public static Language CreateLanguageFromFile(string LanguageFileLocation)
		{
			if (!LanguageFileLocation.EndsWith(".lng"))
			{
				MessageBox.Show("Failed to load Language from file: " + LanguageFileLocation +
				                "\nFile extension is not .lng");
				return null;
			}

			string[] lines        = File.ReadAllLines(LanguageFileLocation);
			var      languageName = "Unknown";
			var      words        = new Dictionary<string, string>();

			foreach (string line in lines)
			{
				if (line.StartsWith("#"))
					continue;
				if (line.Length < 4)
					continue;
				string[] sLine = line.Split('=');

				if (sLine[0] == "LANGUAGE_NAME")
				{
					languageName = sLine[1];
					continue;
				}

				//MessageBox.Show(LanguageFileLocation + "\n" + languageName + "\n" + line.Split('=')[0]);
				words.Add(sLine[0], sLine[1]);
			}

			return new Language(LanguageFileLocation, words, languageName);
		}

		public string FormatText(string text, params string[] args)
		{
			int l                            = args.Length;
			for (var i = 0; i < l; i++) text = text.Replace($"{i}", args[i]);
			return text;
		}
	}
}