using System.IO;
using System.Net;
using System.Threading.Tasks;
using Patcher.Controls;

namespace Patcher.Online.Server
{
	public class UpdateChecker
	{
		public string GetCurrentVersion
		{
			get
			{
				var versionFile = Path.Combine(Globals.Globals.appDataFolder, Globals.Globals.VersionFile);
				if (!File.Exists(versionFile))
					File.WriteAllText(versionFile, "0");
				return File.ReadAllText(versionFile);
			}
		}


		public string GetNewVersion
		{
			get
			{
				var client = new WebClient();
				var s      = client.OpenRead(Globals.Globals.Server_newVersionURL);
				var reader = new StreamReader(s);
				return reader.ReadToEnd();
			}
		}


		public bool CheckForUpdates()
		{
			return GetNewVersion != GetCurrentVersion;
		}

		public async Task DoUpdate(FileDownloader fileDownloader, CustomProgressBar progressBar)
		{
			var patchFile = Path.Combine(Globals.Globals.downloadsFolder, Globals.Globals.patchFileName);
			await fileDownloader.DownloadFileAsync(Globals.Globals.Server_newPatchURL,
				patchFile,
				progressBar);

			await fileDownloader.UnPack(patchFile, ".\\", true);

			File.WriteAllText(Path.Combine(Globals.Globals.appDataFolder, Globals.Globals.VersionFile), GetNewVersion);
		}
	}
}