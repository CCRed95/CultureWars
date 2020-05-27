using System;

namespace CultureWars.Data.Domain
{
	public class VideoTimeStampedTag
	{
		public int VideoTimeStampedTagID { get; set; }

		public int CultureWarsVideoID { get; set; }
		// FK -> ID
		public CultureWarsVideo CultureWarsVideo { get; set; }

		public TimeSpan TimeStamp { get; set; }

		public int CultureWarsTagID { get; set; }
		// FK -> ID
		//public CultureWarsTag CultureWarsTag { get; set; }
	}
}
