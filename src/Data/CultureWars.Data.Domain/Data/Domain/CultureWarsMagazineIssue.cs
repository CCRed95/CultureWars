using System.Collections.Generic;

namespace CultureWars.Data.Domain
{
	public class CultureWarsMagazineIssue
	{
		public int CultureWarsMagazineIssueID { get; set; }

		public int VolumeNumber { get; set; }

		public int IssueNumber { get; set; }
		
		//public int CultureWarsMagazineVolumeID { get; set; }
		// ForeignKey
		public CultureWarsMagazineVolume CultureWarsMagazineVolume { get; set; }
		
		public ICollection<CultureWarsMagazineArticle> CultureWarsMagazineArticles { get; set; }
	}
}
