//using System;
//using System.Diagnostics;
//using System.IO;
//using CultureWars.Core.Extensions;

//namespace CultureWars.Media.Conversion
//{
//	public class MediaConverter
//	{
//		public static FileInfo ConvertMP3ToFLV(
//			FileInfo sourceAudioMp3File)
//		{
//			var targetVideoName = sourceAudioMp3File.GetFileNameWithoutExtension();
//			//var audioShellObject = new AudioShellObject(sourceAudioMp3File);
//			//var bitrate = audioShellObject.AudioEncodingBitrate;
//			var targetVideoFile = new FileInfo(
//				$@"{sourceAudioMp3File.Directory.FullName}\{targetVideoName}.flv");

//			var command =
//				$"ffmpeg -y -i {sourceAudioMp3File.FullName} " +
//				$"-f flv " +
//				$"-acodec libmp3lame " +
//				$"-ab {bitrate}k " +
//				$"-ac 1 {targetVideoName}.flv";

//			using (var p = new Process())
//			{
//				p.StartInfo.UseShellExecute = false;
//				//p.StartInfo.CreateNoWind ow = true;
//				p.StartInfo.RedirectStandardOutput = true;
//				p.StartInfo.FileName = Path.Combine(
//					AppDomain.CurrentDomain.BaseDirectory, 
//					"\\Tools\\ffmpeg.exe");
//				p.StartInfo.Arguments = command;
//				p.Start();
//				p.WaitForExit();
//			}

//			return targetVideoFile;
//		}
//	}
//}
