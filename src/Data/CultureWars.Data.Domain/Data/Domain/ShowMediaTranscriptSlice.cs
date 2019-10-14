using System;
using JetBrains.Annotations;

namespace CultureWars.Data.Domain
{
	public class ShowMediaTranscriptSlice
	{
		public int ShowMediaTranscriptSliceID { get; set; }


		//public int ShowMediaEntryID { get; set; }
		//[NotNull, ForeignKey("ShowMediaEntryID")]
		public virtual ShowMediaEntry ShowMediaEntry { get; set; }
		

		public TimeSpan TranscriptSliceTimeKey { get; set; }
		
		public string TranscriptSliceText { get; set; }
	}
}