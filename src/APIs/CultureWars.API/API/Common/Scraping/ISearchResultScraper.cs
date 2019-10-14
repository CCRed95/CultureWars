using AngleSharp.Dom;

namespace CultureWars.API.Common.Scraping
{
  public interface ISearchResultScraper<out TValue>
  {
    TValue Scrape(IElement htmlNode);
  }
}
