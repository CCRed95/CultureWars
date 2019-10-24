using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace CultureWars.API.GoogleArchives.JsonParsing.Domain.Json
{
	public class ProcessingDetails
	{
		[JsonProperty("processingStatus")]
		[JsonConverter(typeof(StringEnumConverter))]
		public VideoProcessingStatus VideoProcessingStatus { get; set; }
	}
}