using AngleSharp;

namespace CultureWars.API.YouTube.Domain
{
	public class YouTubeUrl
	{
		public string YouTubeVideoUrl { get; }

		public string VideoID { get; }



		public YouTubeUrl(
			string youtubeVideoUrl)
		{
			YouTubeVideoUrl = youtubeVideoUrl;
		}


		public string GetTranscript()
		{
			var context = BrowsingContext
				.New(
					Configuration
						.Default
						.WithDefaultLoader());

			using (var document = context
				.OpenAsync(YouTubeVideoUrl)
				.GetAwaiter()
				.GetResult())
			{
				//var maincontent = document
				//	.QuerySelector("ol#b_results");

				//var items = maincontent
				//	.QuerySelectorAll("li.b_algo");
			}

			return null;
		}
	}
}
