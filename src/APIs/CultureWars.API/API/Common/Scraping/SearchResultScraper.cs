using AngleSharp.Dom;

namespace CultureWars.API.Common.Scraping
{
  public abstract class SearchResultScraper<TEntity>
    : ISearchResultScraper<TEntity>
  {
    public abstract TEntity Scrape(
      IElement htmlNode);
  }
}
