using System.Collections.Generic;
using AngleSharp;
using Ccr.Std.Core.Extensions;
using CultureWars.API.YouTube.Domain;
using JetBrains.Annotations;

namespace CultureWars.API.YouTube.Scraping
{
	public class YouTubePlaylistPageScraper
	{
		private const string domainPrefix = "https://www.";
		private const string domainName = "youtube";
		private const string domainSuffix = ".com";
		
		public static readonly string domain =
			$"{domainPrefix}{domainName}{domainSuffix}";


		protected readonly string _playlistID;


		public string PlaylistUrl { get; }

		
		public YouTubePlaylistPageScraper(
			[NotNull] string playlistID)
		{
			playlistID.IsNotNull(nameof(playlistID));
			_playlistID = playlistID;

			PlaylistUrl = new DomainFragment(domain)
				.Builder
				.WithPath("playlist")
				.WithParameter("list", _playlistID)
				.Build();
		}

		/*

function scanPlaylistLinks() {
	alert('Beginning playlist mining operation.');
	
	let playlistContents = document.querySelector(
		"#contents > ytd-playlist-video-list-renderer.style-scope > div#contents.style-scope.ytd-playlist-video-list-renderer");
		
	let videoList = playlistContents.querySelectorAll(
		"ytd-playlist-video-renderer.style-scope");

	for (let playlistVideoRenderer of videoList)
	{
		let videoLinkParent = playlistVideoRenderer.children[2];
		let videoLink = videoLinkParent.children[0];
//			"a.yt-simple-endpoint.style-scope.ytd-playlist-video-renderer");
		let videoRelativeLink = videoLink.getAttribute("href");

		let videoTitleSpan = playlistVideoRenderer.querySelector(
			"span#video-title.style-scope.ytd-playlist-video-renderer");
		let videoTitle = videoTitleSpan.textContent.trim();

		let _playlistVideoListVideoLinkPattern =
			/watch\?v=([A-z0-9_-]*)&(amp;)?list=([A-z0-9_-]*)&[amp;]?index=([0-9]*)/i;

		let match = _playlistVideoListVideoLinkPattern
			.exec(videoRelativeLink);

		let videoId = match[1];
		let playlistId = match[3];
		let videoPlaylistIndexStr = match[4];
		let videoPlaylistIndex = parseInt(videoPlaylistIndexStr);

		if (videoTitle.toLowerCase().includes('deleted video'))
		{
			discoverVideoStreamAlternative(videoId, videoPlaylistIndex, videoTitle);
		}
	}
}
		 */

		public IEnumerable<YouTubePlaylistVideoItem> GetYouTubeVideos()
		{
			var context = BrowsingContext
				.New(
					Configuration
						.Default
						.WithDefaultLoader());

			using (var document = context
				.OpenAsync(PlaylistUrl)
				.GetAwaiter()
				.GetResult())
			{
				var ytdItemSectionRenderer = document
					.QuerySelector(
						"#page-manager > ytd-browse > ytd-two-column-browse-results-renderer");
				//var pageManager = playlistContent.QuerySelector("#page-manager");
				//var ytdBrowse = pageManager.QuerySelector("ytd-browse");
				//var ytdItemSectionRenderer = ytdBrowse.QuerySelector("ytd-two-column-browse-results-render");
				var primary = ytdItemSectionRenderer
					.QuerySelector("div#primary.style-scope");

				var sectionListRenderer = primary
					.QuerySelector("ytd-section-list-renderer.style-scope");

				var sectionListContents = sectionListRenderer
					.QuerySelector("#contents.style-scope");

				var sectionListContents2 = sectionListContents
					.QuerySelector("#contents.style-scope");

				var playlistVideoListRenderer = sectionListContents2
					.QuerySelector("ytd-playlist-video-list-renderer.style-scope");

				var playlistInnerContents = playlistVideoListRenderer
					.QuerySelector(
						"#contents.style-scope.ytd-playlist-video-list-renderer");

				var videoListInner = playlistInnerContents
					.QuerySelectorAll(
					"#contents.ytd-playlist-video-list-renderer.style-scope");


				//"ytd-playlist-video-list-renderer.style-scope > " +
				//	"div#contents.style-scope.ytd-playlist-video-list-renderer");

				//var playlistContents = document.QuerySelector(
				//	"#contents > " +
				//	"ytd-playlist-video-list-renderer.style-scope > " +
				//	"div#contents.style-scope.ytd-playlist-video-list-renderer");

				//var videoList = playlistContents.QuerySelectorAll(
				//	"ytd-playlist-video-renderer.style-scope");
				
				foreach (var playlistVideoRenderer in videoListInner)
				{
					yield return YouTubePlaylistVideoItem.Scrape(
						playlistVideoRenderer);

					//var platlistIndexNode = playlistVideoRenderer.Children[1];
					//var playlistIndex = int.Parse(platlistIndexNode.TextContent);

					//var videoLinkParent = playlistVideoRenderer.Children[2];
					//var videoLink = videoLinkParent.Children[0];
					////			"a.yt-simple-endpoint.style-scope.ytd-playlist-video-renderer");
					//var videoRelativeLink = videoLink.GetAttribute("href");

					//var videoTitleSpan = playlistVideoRenderer.QuerySelector(
					//	"span#video-title.style-scope.ytd-playlist-video-renderer");

					//var videoTitle = videoTitleSpan.TextContent.Trim();

					//var _playlistVideoListVideoLinkPattern = new Regex(
					//	@"/watch\?v=([A-z0-9_-]*)&(amp;)?list=([A-z0-9_-]*)&[amp;]?index=([0-9]*)");

					//var match = _playlistVideoListVideoLinkPattern
					//	.Match(videoRelativeLink);

					//var videoId = match.Groups[1].Value;
					//var playlistId = match.Groups[3].Value;
					//var videoPlaylistIndexStr = match.Groups[4].Value;
					//var videoPlaylistIndex = int.Parse(videoPlaylistIndexStr);

					//yield return new YouTubeVideo
					//{


					//};
				}
			}
		}
	}
}
