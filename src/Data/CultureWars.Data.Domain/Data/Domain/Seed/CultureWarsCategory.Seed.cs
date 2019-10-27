namespace CultureWars.Data.Domain
{
	public partial class CultureWarsCategory
	{
		/// <summary>
		///		Medjugorje Category Entity Declaration
		/// </summary>
		public static readonly CultureWarsCategory Medjugorje
			= new CultureWarsCategory("medjugorje", "Medjugorje");
		
		/// <summary>
		///		Interview Category Entity Declaration
		/// </summary>
		public static readonly CultureWarsCategory Interview
			= new CultureWarsCategory("interview", "Interview");
		
		/// <summary>
		///		OriginalContent Category Entity Declaration
		/// </summary>
		public static readonly CultureWarsCategory OriginalContent
			= new CultureWarsCategory("original-content", "Original Content");
		
		/// <summary>
		///		VideoShorts Category Entity Declaration
		/// </summary>
		public static readonly CultureWarsCategory VideoShorts
			= new CultureWarsCategory("video-shorts", "Video Shorts");
		
		/// <summary>
		///		CensoredVideos Category Entity Declaration
		/// </summary>
		public static readonly CultureWarsCategory CensoredVideos
			= new CultureWarsCategory("censored-videos", "Censored Videos");
	}
}