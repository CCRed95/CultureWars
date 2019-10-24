using System;
using CultureWars.API.GoogleArchives.JsonParsing.Converters;
using Newtonsoft.Json;

namespace CultureWars.API.GoogleArchives.JsonParsing.Domain.Json
{
	public class ContentDetails
	{
		[JsonProperty("caption")]
		public bool HasCaption { get; set; }

		[JsonProperty("definition")]
		[JsonConverter(typeof(VideoQualityConverter))]
		public VideoQuality VideoQuality { get; set; }

		[JsonProperty("dimension")]
		[JsonConverter(typeof(VideoDimensionConverter))]
		public VideoDimension VideoDimensions { get; set; }

		[JsonProperty("duration")]
		[JsonConverter(typeof(StringToISO8601TimeSpanConverter))]
		public TimeSpan PlaybackDuration { get; set; }

		[JsonProperty("hasCustomThumbnail")]
		public bool HasCustomThumbnail { get; set; }

		[JsonProperty("licensedContent")]
		public bool IsLicensedContent { get; set; }

		[JsonProperty("projection")]
		[JsonConverter(typeof(VideoProjectionConverter))]
		public VideoProjection VideoProjection { get; set; }
	}
}
