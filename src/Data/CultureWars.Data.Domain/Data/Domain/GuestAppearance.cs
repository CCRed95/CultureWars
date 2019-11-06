using Ccr.Std.Core.Extensions;
using JetBrains.Annotations;

namespace CultureWars.Data.Domain
{
	public class GuestAppearance
	{
		public int GuestAppearanceID { get; set; }

		public int GuestID { get; set; }
		//[NotNull, ForeignKey("GuestID")]
		public virtual Guest Guest { get; set; }


		public int CultureWarsVideoID { get; set; }
		//[NotNull, ForeignKey("ShowMediaEntryID")]
		public virtual CultureWarsVideo CultureWarsVideo { get; set; }


		private GuestAppearance()
		{
		}

		public GuestAppearance(
			[NotNull] Guest guest,
			[NotNull] CultureWarsVideo cultureWarsVideo) 
		    : this()
		{
      guest.IsNotNull(nameof(guest));
      cultureWarsVideo.IsNotNull(nameof(cultureWarsVideo));

			GuestID = guest.GuestID;
			CultureWarsVideoID = cultureWarsVideo.CultureWarsVideoID;
		}
	}
}
