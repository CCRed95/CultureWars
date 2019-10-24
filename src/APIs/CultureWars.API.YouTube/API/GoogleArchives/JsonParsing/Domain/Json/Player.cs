using Newtonsoft.Json;

namespace CultureWars.API.GoogleArchives.JsonParsing.Domain.Json
{
	public class Player
	{
		[JsonProperty("embedHtml")]
		public string HtmlEmbedCode { get; set; }
	}
}