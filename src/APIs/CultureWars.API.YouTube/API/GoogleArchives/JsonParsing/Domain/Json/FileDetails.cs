using Newtonsoft.Json;

namespace CultureWars.API.GoogleArchives.JsonParsing.Domain.Json
{
	public class FileDetails
	{
		[JsonProperty("fileName")]
		public string FileName { get; set; }
	}
}