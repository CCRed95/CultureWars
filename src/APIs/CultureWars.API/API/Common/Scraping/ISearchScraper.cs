using System.Collections.Generic;

namespace CultureWars.API.Common.Scraping
{
  public interface ISearchScraper
  {
    IEnumerable<TEntity> Scrape<TEntity>(
      string htmlContent);
  }
}
