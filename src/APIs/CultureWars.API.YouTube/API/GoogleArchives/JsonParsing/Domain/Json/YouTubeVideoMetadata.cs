using Newtonsoft.Json;

namespace CultureWars.API.GoogleArchives.JsonParsing.Domain.Json
{
	public class YouTubeVideoMetadata
	{
		[JsonProperty("contentDetails")]
		public ContentDetails ContentDetails { get; set; }

		[JsonProperty("etag")]
		public string ETag { get; set; }

		[JsonProperty("fileDetails")]
		public FileDetails FileDetails { get; set; }

		[JsonProperty("id")]
		public string VideoID { get; set; }

		[JsonProperty("kind")]
		public string Kind { get; set; }

		[JsonProperty("player")]
		public Player Player { get; set; }

		[JsonProperty("processingDetails")]
		public ProcessingDetails ProcessingDetails { get; set; }

		[JsonProperty("snippet")]
		public Snippet Snippet { get; set; }

		[JsonProperty("statistics")]
		public Statistics Statistics { get; set; }

		[JsonProperty("status")]
		public Status Status { get; set; }

		[JsonProperty("topicDetails")]
		public TopicDetails TopicDetails { get; set; }
	}
}