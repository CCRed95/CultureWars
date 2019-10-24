using System.Collections.Generic;

namespace CultureWars.Data.Domain
{
	public class CultureWarsVideo
	{
		public int CultureWarsVideoID { get; set; }

		public string VideoSourceUrl { get; set; }

		public ICollection<VideoTimeStampedTopic> VideoTimeStampedTopics { get; set; }

		public ICollection<VideoTag> VideoTags { get; set; }
	}
}