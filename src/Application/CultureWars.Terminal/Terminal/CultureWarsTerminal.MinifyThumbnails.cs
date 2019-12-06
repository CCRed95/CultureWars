using System;
using System.IO;
using System.Linq;
using Ccr.Std.Core.Extensions;

namespace CultureWars.Terminal
{
	public partial class CultureWarsTerminal
	{
		public static void MinifyThumbnails()
		{
			var inputDirectorySource = new DirectoryInfo(
				@"X:\media\shows\emichaeljones\youtube\videos\thumbs-Minified\");

			var outputDirectorySource = new DirectoryInfo(
				@"X:\media\shows\emichaeljones\youtube\videos\thumbs-packed\");

			if (!outputDirectorySource.Exists)
				outputDirectorySource.Create();

			Console.WriteLine($"Starting thumbnail minifier on path: {inputDirectorySource.FullName.SQuote()}");
			Console.WriteLine($"                  packing to target: {outputDirectorySource.FullName.SQuote()}");
			Console.WriteLine();

			foreach (var directoryInfo in inputDirectorySource.GetDirectories())
			{
				var index = 0;
				foreach (var videoThumbsDirectory in directoryInfo.GetDirectories())
				{
					var primaryThumbnail = videoThumbsDirectory
					                       .GetFiles("i.jpg")
					                       .SingleOrDefault();

					if (primaryThumbnail == null)
					{
						Console.WriteLine($"ERROR: No primary thumbnail found for directory {videoThumbsDirectory}");
					}
					else
					{
						var targetPath = outputDirectorySource.FullName +
							$@"{directoryInfo.Name}.{videoThumbsDirectory.Name}.primary.jpg";

						primaryThumbnail.CopyTo(targetPath);
					}

					index++;
				}
				if (index != 10)
				{
					Console.WriteLine($"WARNING: Directory {directoryInfo.FullName.Quote()} had {index} packed thumbnails, expected 10.");
				}
			}
			Console.WriteLine($"Completed thumbnail generation process.");
		}
	}
}
