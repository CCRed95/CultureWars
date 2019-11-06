using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Ccr.Std.Core.Extensions;
using CultureWars.API.GoogleArchives.JsonParsing.Domain.Json;
using JetBrains.Annotations;

namespace CultureWars.API.GoogleArchives.JsonParsing
{
	public class YouTubeVideoFlattenedMetadata
	{
		[NotNull]
		private readonly YouTubeVideoMetadata _metadata;
		private string[] _tagListSorted;


		public string VideoID
		{
			get => _metadata.VideoID;
		}

		public string Title
		{
			get => _metadata.Snippet.Title;
		}

		public string Description
		{
			get => _metadata.Snippet.Description;
		}

		public TimeSpan PlaybackDuration
		{
			get => _metadata.ContentDetails.PlaybackDuration;
		}

		public int Views
		{
			get => _metadata.Statistics.Views;
		}

		public int Likes
		{
			get => _metadata.Statistics.Likes;
		}

		public int Dislikes
		{
			get => _metadata.Statistics.Dislikes;
		}

		public int Comments
		{
			get => _metadata.Statistics.Comments;
		}

		public int Favorites
		{
			get => _metadata.Statistics.Favorites;
		}

		public string OriginalVideoFileName
		{
			get => _metadata.FileDetails.FileName;
		}

		public DateTime VideoPublishedDate
		{
			get => _metadata.Snippet.VideoPublishedDate;
		}

		//public string JSONMetadataFileName { get; }

		public IReadOnlyList<string> TagList
		{
			get => _tagListSorted ??
				(_tagListSorted = _metadata
					?.Snippet
					?.VideoTags
					?.OrderBy(t => t)
					?.Distinct()
					?.ToArray());
		}


		public YouTubeVideoFlattenedMetadata(
			[NotNull] YouTubeVideoMetadata metadata)
		{
			metadata.IsNotNull(nameof(metadata));
			_metadata = metadata;
		}

		public string GetStrLineAsCsv()
		{
			var sb = new StringBuilder();

			sb.Append($"{VideoID.Quote()},");
			sb.Append($"{Title.Quote()},");
			//sb.Append($"{Description.Quote()},");

			var playbackDurationStr = $"{PlaybackDuration.Hours:0}" +
				$":{PlaybackDuration.Minutes:00}" +
				$":{PlaybackDuration.Seconds:00}" +
				$".{PlaybackDuration.Milliseconds:000}";

			sb.Append($"{playbackDurationStr.Quote()},");
			sb.Append($"{Views.ToString().Quote()},");
			sb.Append($"{Likes.ToString().Quote()},");
			sb.Append($"{Dislikes.ToString().Quote()},");
			sb.Append($"{Comments.ToString().Quote()},");
			sb.Append($"{Favorites.ToString().Quote()},");
			sb.Append($"{OriginalVideoFileName.Quote()},");
			sb.Append($"{VideoPublishedDate.ToString("O").Quote()},");

			var tagSb = new StringBuilder();
			foreach (var tag in TagList)
			{
				tagSb.Append($"{tag}, ");
			}
			var tagStrContent = tagSb.ToString()
				.TrimEnd(' ', ',');

			sb.Append($"{tagStrContent.Quote()}");

			var line = sb.ToString();
			return line;
		}


		public string GetStrLineAsList()
		{
			var sb = new StringBuilder();

			sb.AppendLine($"yt:VideoID:             {VideoID}");
			sb.AppendLine($"  yt:Title:             {Title}");
			//sb.Append($"{Description.Quote()},");

			var playbackDurationStr = $"{PlaybackDuration.Hours:0}" +
				$":{PlaybackDuration.Minutes:00}" +
				$":{PlaybackDuration.Seconds:00}" +
				$".{PlaybackDuration.Milliseconds:000}";

			sb.AppendLine($"  yt:Duration:          {playbackDurationStr}");
			sb.AppendLine($"  yt:Views:             {Views}");
			sb.AppendLine($"  yt:Likes:             {Likes}");
			sb.AppendLine($"  yt:Dislikes:          {Dislikes}");
			sb.AppendLine($"  yt:Comments:          {Comments}");
			sb.AppendLine($"  yt:Favorites:         {Favorites}");
			sb.AppendLine($"  yt:OrigVidFileName:   {OriginalVideoFileName}");
			sb.AppendLine($"  yt:VideoPubDate:      {VideoPublishedDate:O}");

			var tagStrContent = "";

			if (TagList != null)
			{
				var tagSb = new StringBuilder();
				foreach (var tag in TagList)
				{
					tagSb.Append($"{tag}, ");
				}
				tagStrContent = tagSb.ToString()
					.TrimEnd(' ', ',');
			}

			sb.AppendLine($"  yt:tags:              {tagStrContent}");

			var line = sb.ToString();
			return line;
		}


		public StreamWriter WriteLineToCsv(
			StreamWriter streamWriter)
		{
			var sb = new StringBuilder();

			sb.Append($"{VideoID.Quote()},");
			sb.Append($"{Title.Quote()},");
			sb.Append($"{Description.Quote()},");

			var playbackDurationStr = $"\t{PlaybackDuration.Hours:0}" +
				$":{PlaybackDuration.Minutes:00}" +
				$":{PlaybackDuration.Seconds:00}" +
				$".{PlaybackDuration.Milliseconds:000}";

			sb.Append($"{playbackDurationStr.Quote()},");
			sb.Append($"{Views.ToString().Quote()},");
			sb.Append($"{Likes.ToString().Quote()},");
			sb.Append($"{Dislikes.ToString().Quote()},");
			sb.Append($"{Comments.ToString().Quote()},");
			sb.Append($"{Favorites.ToString().Quote()},");
			sb.Append($"{OriginalVideoFileName.Quote()},");
			sb.Append($"{VideoPublishedDate.ToString("O").Quote()},");

			var tagSb = new StringBuilder();
			foreach (var tag in TagList)
			{
				tagSb.Append($"{tag}, ");
			}
			var tagStrContent = tagSb.ToString()
				.TrimEnd(' ', ',');

			sb.Append($"{tagStrContent.Quote()}");

			var line = sb.ToString();

			streamWriter.WriteLine(line);

			return streamWriter;

			//sb.Append($"{videoInfo.snippet.description.Replace("\r", "\\r").Replace("\n", "\\n").Replace("\"", "\\\"").Replace(",", " ").Quote()},");
			//var ts = XmlConvert.ToTimeSpan(videoInfo.contentDetails.duration);
			//var tsStr = $"\t{ts.Hours:0}" +
			//	$":{ts.Minutes:00}" +
			//	$":{ts.Seconds:00}" +
			//	$".{ts.Milliseconds:000}";
			//sb.Append($"{tsStr.Quote()},");
			//sb.Append($"{videoInfo.statistics.viewCount.Quote()},");
			//sb.Append($"{videoInfo.statistics.likeCount.Quote()},");
			//sb.Append($"{videoInfo.statistics.dislikeCount.Quote()},");
			//sb.Append($"{videoInfo.statistics.commentCount.Quote()},");
			//sb.Append($"{videoInfo.statistics.favoriteCount.Quote()},");
			//sb.Append($"{videoInfo.fileDetails.fileName.Quote()},");
			//sb.Append($"{jsonFile.Name.Quote()}");
			//if (videoInfo.snippet.tags != null)


		}

	}
}