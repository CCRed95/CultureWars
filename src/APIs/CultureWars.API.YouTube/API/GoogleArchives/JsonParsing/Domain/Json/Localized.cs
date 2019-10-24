using Newtonsoft.Json;

namespace CultureWars.API.GoogleArchives.JsonParsing.Domain.Json
{
	public class Localized
	{
		[JsonProperty("description")]
		public string Description { get; set; }

		[JsonProperty("title")]
		public string Title { get; set; }
	}
}