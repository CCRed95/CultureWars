using Newtonsoft.Json;

namespace CultureWars.API.GoogleArchives.JsonParsing.Domain.Json
{
	public class Default
	{
		[JsonProperty("height")]
		public int Height { get; set; }

		[JsonProperty("width")]
		public int Width { get; set; }

		[JsonProperty("url")]
		public string Url { get; set; }
	}
}