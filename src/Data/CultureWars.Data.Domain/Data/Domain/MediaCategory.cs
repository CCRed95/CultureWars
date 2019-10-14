using System.Runtime.CompilerServices;
using Ccr.Std.Core.Extensions;

namespace CultureWars.Data.Domain
{
	public partial class MediaCategory
	{
	  public int MediaCategoryID { get; set; }

		public string MediaCategoryName { get; set; }


		public static MediaCategory Define(
			[CallerMemberName] string memberName = "")
		{
			return new MediaCategory(
				memberName.Replace("_", " "));
		}

		
		private MediaCategory() { }

		public MediaCategory(
			string mediaCategoryName) 
				: this()
		{
			mediaCategoryName.IsNotNull(nameof(mediaCategoryName));

			MediaCategoryName = mediaCategoryName;
		}

		private MediaCategory(
			int mediaCategoryID,
			string mediaCategoryName)
				: this(
					mediaCategoryName)
		{
			MediaCategoryID = mediaCategoryID;
		}
	}
}
