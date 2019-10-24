using System;
using System.Collections.Generic;
using CultureWars.API.GoogleArchives.JsonParsing.Converters;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace CultureWars.API.GoogleArchives.JsonParsing.Domain.Json
{
	public class Snippet
	{
		[JsonProperty("categoryId")]
		[JsonConverter(typeof(StringToIntConverter))]
		public int CategoryId { get; set; }

		[JsonProperty("channelId")]
		public string ChannelId { get; set; }

		[JsonProperty("channelTitle")]
		public string ChannelTitle { get; set; }

		[JsonProperty("description")]
		public string Description { get; set; }

		[JsonProperty("liveBroadcastContent")]
		[JsonConverter(typeof(StringEnumConverter))]
		public LiveBroadcastContentTypes LiveBroadcastContent { get; set; }

		[JsonProperty("localized")]
		public Localized Localized { get; set; }

		[JsonProperty("publishedAt")]
		//[JsonConverter(typeof())]
		public DateTime VideoPublishedDate { get; set; }

		[JsonProperty("tags")]
		public List<string> VideoTags { get; set; }

		[JsonProperty("thumbnails")]
		public Thumbnails Thumbnails { get; set; }

		[JsonProperty("title")]
		public string Title { get; set; }
	}
}