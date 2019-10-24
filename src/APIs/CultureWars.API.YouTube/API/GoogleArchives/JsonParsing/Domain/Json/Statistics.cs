using CultureWars.API.GoogleArchives.JsonParsing.Converters;
using Newtonsoft.Json;

namespace CultureWars.API.GoogleArchives.JsonParsing.Domain.Json
{
	public class Statistics
	{
		[JsonProperty("commentCount")]
		[JsonConverter(typeof(StringToIntConverter))]
		public int Comments { get; set; }

		[JsonProperty("dislikeCount")]
		[JsonConverter(typeof(StringToIntConverter))]
		public int Dislikes { get; set; }

		[JsonProperty("favoriteCount")]
		[JsonConverter(typeof(StringToIntConverter))]
		public int Favorites { get; set; }

		[JsonProperty("likeCount")]
		[JsonConverter(typeof(StringToIntConverter))]
		public int Likes { get; set; }

		[JsonProperty("viewCount")]
		[JsonConverter(typeof(StringToIntConverter))]
		public int Views { get; set; }
	}
}