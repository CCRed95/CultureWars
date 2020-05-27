using CultureWars.API.InternetArchive.Domain;
using CultureWars.API.Web;
using CultureWars.Core.Extensions;

namespace CultureWars.Terminal.Utilities
{
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