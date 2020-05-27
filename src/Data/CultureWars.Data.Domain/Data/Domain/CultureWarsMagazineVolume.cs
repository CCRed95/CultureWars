using System.Collections.Generic;

namespace CultureWars.Data.Domain
{
	public class CultureWarsMagazineVolume
	{
		public int CultureWarsMagazineVolumeID { get; set; }

		public int Year { get; set; }

		public int VolumeNumber { get; set; }

		public ICollection<CultureWarsMagazineIssue> CultureWarsMagazineIssues { get; set; }
	}
}