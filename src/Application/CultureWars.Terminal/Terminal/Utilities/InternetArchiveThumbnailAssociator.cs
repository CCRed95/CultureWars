using System;
using System.Collections.Generic;
using System.Linq;
using CultureWars.API.InternetArchive.Domain;
using CultureWars.API.InternetArchive.Interpreters;
using CultureWars.API.Web;
using CultureWars.Core.Extensions;

namespace CultureWars.Terminal.Utilities
{
	public static class InternetArchiveThumbnailAssociator
	{
		public static InternetArchiveFile GetMiddleAssociatedThumbnailFile(
			InternetArchiveItem iaItem,
			InternetArchiveFile iaFile)
		{
			var associatedThumbnailFiles = GetAssociatedThumbnailFiles(iaItem, iaFile)
				.ToArray();

			var centerIndex = (int)Math.Floor(associatedThumbnailFiles.Length / 2d);

			return associatedThumbnailFiles[centerIndex];
		}


		public static IEnumerable<InternetArchiveFile> GetAssociatedThumbnailFiles(
			InternetArchiveItem iaItem,
			InternetArchiveFile iaFile)
		{
			var thumbnailFiles = ArchiveFileInterpreter.ScrapeArchiveThumbnailFiles(iaItem);

			foreach (var thumbnailFile in thumbnailFiles)
			{
				var thumbnailFileName = thumbnailFile.Title.RemoveFileExtension();
				var videoFileName = iaFile.Title.RemoveFileExtension();

				if (thumbnailFileName.StartsWith(videoFileName))
					yield return thumbnailFile;
			}
		}
	}

	public static class AlternativeThumbnailAssociator
	{
		public static string GetPrimaryAssociatedThumbnailFileUrl(
			InternetArchiveFile iaFile)
		{
			var fileNameWithoutExtension = iaFile.FileName.RemoveFileExtension();
			var groupIndex = iaFile.OwnerArchiveItem.Identifier.Replace("emj-archive-", "");
			var expectedThumbnailName = $"{groupIndex}.{fileNameWithoutExtension}.thumbs.primary.jpg";
			var expectedThumbnailUrlUnEncoded =
				$"https://archive.org/download/emj-archive-thumbs-primary/{expectedThumbnailName}";
			var expectedThumbnailUrl = expectedThumbnailUrlUnEncoded.UrlEncode();
			return expectedThumbnailUrl;
		}

	}
}


//WordPressPostItem item)
//var decodedFileName = iaFile.FilePathUrl.UrlDecode();
//var iaItemIdentifier = iaFile.OwnerArchiveItem.Identifier;
//var iaItemPath = $"https://archive.org/download/{iaItemIdentifier}/";
//var itemThumbnailDirectory = iaItemPath + $"{iaItemIdentifier}.thumbs/";

//var distances = thumbnailFiles
//	.Select(
//		t => (
//			distance: StringDistanceAlgorithms
//				.LevenshteinDistance(
//					t.FileName.Replace(".mp4", "", true, CultureInfo.CurrentCulture),
//					decodedFileName), 
//			iaFile: t))
//	.OrderBy(t => t.distance)
//	.ToArray();

//var jsonFile = distances.First();

//matchedDistance = jsonFile.distance;
//return jsonFile.jsonFile;