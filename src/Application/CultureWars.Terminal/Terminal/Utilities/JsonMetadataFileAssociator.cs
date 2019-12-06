using System.Globalization;
using System.IO;
using System.Linq;
using CultureWars.API.Web;
using CultureWars.Core.Algorithms;

namespace CultureWars.Terminal.Utilities
{
	public static class JsonMetadataFileAssociator
	{
		public static DirectoryInfo _baseDirectory = new DirectoryInfo(
			$@"X:\media\shows\emichaeljones\youtube\videos\renamed formatted videos\");

		public static FileInfo GetAssociatedJsonFile(
			string groupIndex,
			string mp4FileName,
			out int matchedDistance)
		{
			var groupIndexDirectory = new DirectoryInfo(
				_baseDirectory.FullName + $@"\{groupIndex}\");

			var decodedFileName = mp4FileName.UrlDecode();

			var distances = groupIndexDirectory
				.GetFiles("*.json")
				.Select(
					t => (
						distance: StringDistanceAlgorithms
							.LevenshteinDistance(
								t.Name.Replace(".mp4", ".json", true, CultureInfo.CurrentCulture),
								decodedFileName), jsonFile: t))
				.OrderBy(t => t.distance)
				.ToArray();

			var jsonFile = distances.First();

			matchedDistance = jsonFile.distance;
			return jsonFile.jsonFile;
		}
	}
}
