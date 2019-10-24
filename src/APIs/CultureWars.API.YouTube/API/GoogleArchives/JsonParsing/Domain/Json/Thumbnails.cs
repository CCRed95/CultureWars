using Newtonsoft.Json;

namespace CultureWars.API.GoogleArchives.JsonParsing.Domain.Json
{
	public class Thumbnails
	{
		[JsonProperty("default")]
		public Default Default { get; set; }

		[JsonProperty("high")]
		public High HighResolution { get; set; }

		[JsonProperty("maxres")]
		public Maxres MaxResolution { get; set; }

		[JsonProperty("medium")]
		public Medium MediumResolution { get; set; }

		[JsonProperty("standard")]
		public Standard StandardResolution { get; set; }
	}
}