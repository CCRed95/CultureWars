using Ccr.Std.Core.Extensions;
using JetBrains.Annotations;

namespace CultureWars.Data.Domain
{
	public class GuestAppearance
	{
		public int GuestAppearanceID { get; set; }

    //public int? GuestID { get; set; }
    //[NotNull, ForeignKey("GuestID")]
    public virtual Guest Guest { get; set; }


   // public int? ShowMediaEntryID { get; set; }
	  //[NotNull, ForeignKey("ShowMediaEntryID")]
	  public virtual ShowMediaEntry ShowMediaEntry { get; set; }
		
    
    private GuestAppearance() { }

		public GuestAppearance(
			[NotNull] Guest guest,
			[NotNull] ShowMediaEntry showMediaEntry) 
		    : this()
		{
      guest.IsNotNull(nameof(guest));
      showMediaEntry.IsNotNull(nameof(showMediaEntry));

			Guest = guest;
		  ShowMediaEntry = showMediaEntry;
		}
	}
}
