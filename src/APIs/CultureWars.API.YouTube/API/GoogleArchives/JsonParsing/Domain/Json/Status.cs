using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace CultureWars.API.GoogleArchives.JsonParsing.Domain.Json
{
	public class Status
	{
		[JsonProperty("embeddable")]
		public bool IsEmbeddable { get; set; }

		[JsonProperty("license")]
		public string License { get; set; }

		[JsonProperty("privacyStatus")]
		[JsonConverter(typeof(StringEnumConverter))]
		public PrivacyStatus PrivacyStatus { get; set; }

		[JsonProperty("publicStatsViewable")]
		public bool ArePublicStatsViewable { get; set; }

		[JsonProperty("uploadStatus")]
		[JsonConverter(typeof(StringEnumConverter))]
		public UploadStatus UploadStatus { get; set; }
	}
}