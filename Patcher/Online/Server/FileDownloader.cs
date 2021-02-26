using System.Drawing;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using Ionic.Zip;
using Patcher.Controls;

namespace Patcher.Online.Server
{
	public class FileDownloader
	{
		/// <summary>
		///     Check if the downloader is busy downloading something :)
		/// </summary>
		public bool isDownloading { get; private set; }

		/// <summary>
		///     Check if the extractor is busy extracting something :)
		/// </summary>
		public bool isExtracting { get; private set; }

		private float getProgress(float total, float current)
		{
			return current * 100 / total;
		}

		/// <summary>
		///     Download a file
		/// </summary>
		/// <param name="URL">The exact url (link) to the file</param>
		/// <param name="Location">The location where the file should be downloaded</param>
		/// <param name="progressBar">The progress bar that is updated while downloading (if null is ignored)</param>
		/// <returns></returns>
		public async Task DownloadFileAsync(string URL, string Location, CustomProgressBar progressBar)
		{
			progressBar.SetPercent = 0;
			progressBar.BackColor  = Color.FromArgb(35,  32,  39);
			progressBar.ForeColor  = Color.FromArgb(225, 132, 208);
			isDownloading          = true;
			var c = new WebClient();

			if (progressBar != null)
				c.DownloadProgressChanged += (sender, e) => { progressBar.SetPercent = e.ProgressPercentage; };
			c.DownloadFileCompleted += (sender, e) => isDownloading = false;
			await c.DownloadFileTaskAsync(URL, Location);
		}

		/// <summary>
		///     Unzip an archive
		/// </summary>
		/// <param name="ZipLocation">The location where the zip is</param>
		/// <param name="DestinationFolder">
		///     The destination folder. Location where the files will be unzipped (does not create any
		///     other folders then the archive has)
		/// </param>
		/// <param name="deleteZipFile">Should I delete the zip file after extraction ?</param>
		/// <param name="progressBar">The progress bar that is used to mark the progress of the extraction (if null is ignored)</param>
		/// <returns></returns>
		public async Task UnPack(string ZipLocation, string DestinationFolder,
		                         bool   deleteZipFile)
		{
			isExtracting = true;
			await Task.Run(() =>
			{
				using (var stream = File.Open(ZipLocation, FileMode.Open))
				{
					using (var file = ZipFile.Read(stream))
					{
						file.ExtractAll(DestinationFolder);
					}
				}
			}).ContinueWith(task =>
			{
				if (deleteZipFile)
					File.Delete(ZipLocation);
				isExtracting = false;
			});
		}
	}
}