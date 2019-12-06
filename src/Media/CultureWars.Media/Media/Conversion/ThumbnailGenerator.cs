using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using Ccr.Std.Core.Extensions;
using CultureWars.Core.Extensions;
using CultureWars.Media.Metadata;

namespace CultureWars.Media.Conversion
{
	public class ThumbnailGenerator
	{
		public static FileInfo[] GenerateThumbnails(
			FileInfo sourceVideoFile)
		{
			var targetVideoName = sourceVideoFile.GetFileNameWithoutExtension();
      if (sourceVideoFile.Directory == null)
        throw new DirectoryNotFoundException();

      var targetVideoThumbnailFolder = new DirectoryInfo(
	      $@"{sourceVideoFile.Directory.FullName}\{targetVideoName}.thumbs\");

      if (!targetVideoThumbnailFolder.Exists)
        targetVideoThumbnailFolder.Create();

      var videoShellObject = new VideoShellObject(sourceVideoFile);
			var videoDuration = videoShellObject.Duration;
      var videoDurationTicks = videoDuration.Ticks / 35;
			var thumbnailSkipDuration = new TimeSpan(videoDurationTicks);
			var fps = $"1/{(int)Math.Floor(thumbnailSkipDuration.TotalSeconds)}";

			var command =
				$"-i {sourceVideoFile.FullName.Quote()} " +
				$"-vf fps={fps} " +
				$@"""{targetVideoThumbnailFolder.FullName}\%06d.jpg"""; 

			Console.WriteLine($"exc: {command}");
			//	return new FileInfo[]{};

		//	var fileName = Path.Combine(
	//			AppDomain.CurrentDomain.BaseDirectory,
//				"\\Tools\\ffmpeg.exe");

			var fileName = @"C:\Tools\ffmpeg.exe";

			using (var p = new Process())
			{
				p.StartInfo.UseShellExecute = false;
				//p.StartInfo.CreateNoWind ow = true;
				p.StartInfo.RedirectStandardOutput = true;
				p.StartInfo.FileName = fileName;
				p.StartInfo.Arguments = command;
				p.Start();
				p.WaitForExit();
			}

			return targetVideoThumbnailFolder.GetFiles(".jpg").ToArray();
		}

  }
}

//var targetVideoFile = new FileInfo(
//	$@"{targetVideoThumbnailFolder.FullName}\{targetVideoName}.flv");