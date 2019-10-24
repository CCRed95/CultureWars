using System;
using System.Text.RegularExpressions;
using AngleSharp.Dom;

namespace CultureWars.API.YouTube.Domain
{
	public class YouTubePlaylistVideoItem
	{
		public int VideoPlaylistIndex { get; }

		public string VideoUrl { get; }

		public string NativeVideoID { get; }
		
		public YouTubeChannel CreatorChannel { get; }

		public string VideoTitle { get; }

		public TimeSpan PlaybackDuration { get; }


		public YouTubePlaylistVideoItem(
			int videoPlaylistIndex,
			string videoUrl,
			string nativeVideoUrl,
			YouTubeChannel creatorChannel,
			string videoTitle,
			TimeSpan playbackDuration)
		{
			VideoPlaylistIndex = videoPlaylistIndex;
			VideoUrl = videoUrl;
			NativeVideoID = nativeVideoUrl;
			CreatorChannel = creatorChannel;
			VideoTitle = videoTitle;
			PlaybackDuration = playbackDuration;
		}


		public static YouTubePlaylistVideoItem Scrape(
			IElement playlistVideoRenderer)
		{
			//var platlistIndexNode = playlistVideoRenderer.Children[1];
			//var playlistIndex = int.Parse(platlistIndexNode.TextContent);

			var videoLinkParent = playlistVideoRenderer.Children[2];
			var videoLink = videoLinkParent.Children[0];
			//			"a.yt-simple-endpoint.style-scope.ytd-playlist-video-renderer");
			var videoRelativeLink = videoLink.GetAttribute("href");

			var videoTitleSpan = playlistVideoRenderer.QuerySelector(
				"span#video-title.style-scope.ytd-playlist-video-renderer");

			var videoTitle = videoTitleSpan.TextContent.Trim();

			var _playlistVideoListVideoLinkPattern = new Regex(
				@"/watch\?v=([A-z0-9_-]*)&(amp;)?list=([A-z0-9_-]*)&[amp;]?index=([0-9]*)");

			var match = _playlistVideoListVideoLinkPattern
				.Match(videoRelativeLink);

			var videoId = match.Groups[1].Value;
			var playlistId = match.Groups[3].Value;
			var videoPlaylistIndexStr = match.Groups[4].Value;
			var videoPlaylistIndex = int.Parse(videoPlaylistIndexStr);

			return new YouTubePlaylistVideoItem(
				videoPlaylistIndex,
				$@"https://www.youtube.com/{videoRelativeLink}",
				videoId,
				null,
				videoTitle,
				TimeSpan.Zero);
		}
	}
}