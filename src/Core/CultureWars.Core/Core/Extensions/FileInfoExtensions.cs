﻿using System.IO;

namespace CultureWars.Core.Extensions
{
	public static class FileInfoExtensions
	{
		public static string GetFileNameWithoutExtension(
			this FileInfo fileInfo)
		{
			var length = fileInfo.Extension.Length;
			return fileInfo.Name.Substring(
				0,
				fileInfo.Name.Length - length);
		}
	}
}