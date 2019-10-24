using System.IO;
using CultureWars.API.GoogleArchives.JsonParsing.Domain.Json;
using Newtonsoft.Json;

namespace CultureWars.API.GoogleArchives.JsonParsing
{
	public static class JsonYouTubeMetadataParser
	{
		public static YouTubeVideoMetadata ParseJsonYouTubeMetadata(
			FileInfo jsonMetadataFile)
		{
			using (var reader = jsonMetadataFile.OpenText())
			{
				var jsonText = reader.ReadToEnd()
					.Trim('[', ']');

				var videoInfo = JsonConvert.DeserializeObject<YouTubeVideoMetadata>(
					jsonText);

				return videoInfo;
			}
		}
	}
}
	 