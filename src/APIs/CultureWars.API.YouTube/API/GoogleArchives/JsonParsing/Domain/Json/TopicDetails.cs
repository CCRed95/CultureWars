using System.Collections.Generic;
using Newtonsoft.Json;

namespace CultureWars.API.GoogleArchives.JsonParsing.Domain.Json
{
	public class TopicDetails
	{
		[JsonProperty("relevantTopicIds")]
		public List<string> RelevantTopicIds { get; set; }

		[JsonProperty("topicCategories")]
		public List<string> TopicCategories { get; set; }

		[JsonProperty("topicIds")]
		public List<string> TopicIds { get; set; }
	}
}