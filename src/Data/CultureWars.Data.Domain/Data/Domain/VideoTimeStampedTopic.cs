using System;

namespace CultureWars.Data.Domain
{
	public class VideoTimeStampedTopic
	{
		public int VideoTimeStampedTopicID { get; set; }

		public int CultureWarsVideoID { get; set; }

		public TimeSpan TimeStamp { get; set; }
	}
}
