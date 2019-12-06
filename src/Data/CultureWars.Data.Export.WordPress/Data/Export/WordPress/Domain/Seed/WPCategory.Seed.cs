namespace CultureWars.Data.Export.WordPress.Domain
{
	public partial class WPCategory
	{
		/// <summary>
		///		Medjugorje Category Entity Declaration
		/// </summary>
		public static readonly WPCategory Medjugorje
			= new WPCategory("medjugorje", "Medjugorje");
		
		/// <summary>
		///		Interview Category Entity Declaration
		/// </summary>
		public static readonly WPCategory Interview
			= new WPCategory("interview", "Interview");
		
		/// <summary>
		///		OriginalContent Category Entity Declaration
		/// </summary>
		public static readonly WPCategory OriginalContent
			= new WPCategory("original-content", "Original Content");
		
		/// <summary>
		///		VideoShorts Category Entity Declaration
		/// </summary>
		public static readonly WPCategory VideoShorts
			= new WPCategory("video-shorts", "Video Shorts");
		
		/// <summary>
		///		CensoredVideos Category Entity Declaration
		/// </summary>
		public static readonly WPCategory CensoredVideos
			= new WPCategory("censored-videos", "Censored Videos");
	}
}