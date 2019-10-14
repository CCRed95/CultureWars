namespace CultureWars.API
{
	/// <summary>
	///		Interface that represents a single simple search query, independent of the search provider.
	/// </summary>
	public interface ISearchQuery
	{
		/// <summary>
		///		The query text string in which to search for.
		/// </summary>
		string Text { get; }

		/// <summary>
		///		Limits the search query results to a specific website.
		/// </summary>
		string SiteConstraint { get; }

		/// <summary>
		///		The number of search results to include per search page.
		/// </summary>
		int PageSize { get; }

		/// <summary>
		///		The zero-based page index offset.
		/// </summary>
		int Index { get; }

	}
}