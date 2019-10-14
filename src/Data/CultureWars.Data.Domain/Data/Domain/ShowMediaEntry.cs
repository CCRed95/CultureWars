using System;
using System.Collections.Generic;

namespace CultureWars.Data.Domain
{
  public class ShowMediaEntry
  {
    public int ShowMediaEntryID { get; set; }
		
    public DateTime? PublicationDate { get; set; }

    public string Title { get; set; }

	  public string Subtitle { get; set; }

	  public int? ShowNumber { get; set; }

	  public string ShowIdentifier { get; set; }

	  public string ShowPageUrl { get; set; }

		public string EmbeddedContentSourceUrl { get; set; }
  
    public string EmbeddedContentSource { get; set; }

    public string Description { get; set; }

		public string Author { get; set; }

		public string Creator { get; set; }
		
		public string Summary { get; set; }

		public string ThumnailImageUrl { get; set; }
		

		public virtual ICollection<GuestAppearance> GuestAppearances { get; set; }
		
		public virtual ICollection<MediaCategory> MediaCategories { get; set; }
		
		public virtual ICollection<ShowMediaTranscriptSlice> ShowMediaTranscriptSlices { get; set; }

		
		public ShowMediaEntry()
    {
      GuestAppearances = new HashSet<GuestAppearance>();
      MediaCategories = new HashSet<MediaCategory>();
			ShowMediaTranscriptSlices = new HashSet<ShowMediaTranscriptSlice>();
		}
  }
}
