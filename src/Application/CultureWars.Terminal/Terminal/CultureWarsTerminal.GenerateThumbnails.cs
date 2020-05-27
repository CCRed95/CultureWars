using System;
using System.IO;
using CultureWars.Media.Conversion;

namespace CultureWars.Terminal
{
	public partial class CultureWarsTerminal
	{
		public static void GenerateThumbnails()
		{
			var directorySource = new DirectoryInfo(
				@"X:\media\shows\emichaeljones\youtube\videos\thumbs\");


			foreach (var directoryInfo in directorySource.GetDirectories())
			{
				foreach (var videoInfo in directoryInfo.GetFiles("*.mp4"))
				{
					Console.WriteLine($"Starting thumbnail generation on path: {videoInfo.FullName}");
					try
					{
						var thumbnails = ThumbnailGenerator.GenerateThumbnails(videoInfo);

						foreach (var thumbnail in thumbnails)
						{
							Console.WriteLine($"Thumbnail generated: {thumbnail.FullName}");
						}
						Console.WriteLine($"Completed thumbnail generation.");
					}
					catch (Exception ex)
					{
						Console.WriteLine($"Exception thrown: {ex}");
					}
				}
			}
			Console.WriteLine($"Completed thumbnail generation process.");
		}
	}
}