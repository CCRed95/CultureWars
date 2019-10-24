using System;

namespace CultureWars.API.YouTube.Domain
{
	public class YouTubeVideo
	{
		public int YouTubeVideoID { get; set; }

		public string YouTubeNativeVideoID { get; set; }

		public YouTubeChannel CreatorChannel { get; set; }

		public string Title { get; set; }

		public string Description { get; set; }

		public DateTime PublishedAt { get; set; }
	}
	
}
/*
 * 
		public string IDKind { get; set; }

		public string Kind { get; set; }
		
 */

//public string PublishedAtRaw { get; set; }

//public string LiveBroadcastContent { get; set; }