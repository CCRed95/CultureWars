using System.Collections.Generic;

namespace CultureWars.Data.Domain
{
	public class CultureWarsVideo
	{
		public int CultureWarsVideoID { get; set; }

		public string VideoSourceUrl { get; set; }

		public ICollection<GuestAppearance> GuestAppearances { get; set; }

		public ICollection<VideoTimeStampedTag> VideoTimeStampedTags { get; set; }

		//public ICollection<WPCategory> CultureWarsCategories { get; set; }

//		public ICollection<WPTerm> CultureWarsTags { get; set; }


		public CultureWarsVideo()
		{
			GuestAppearances = new HashSet<GuestAppearance>();
			VideoTimeStampedTags = new HashSet<VideoTimeStampedTag>();
		//	CultureWarsCategories = new HashSet<CultureWarsCategory>();
	//		CultureWarsTags = new HashSet<CultureWarsTag>();
		}
	}
}