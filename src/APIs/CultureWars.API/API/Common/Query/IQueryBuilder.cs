using CultureWars.API.Infrastructure;

namespace CultureWars.API.Common.Query
{
  public interface IQueryBuilder
  {
    string BuildRequestUrl(DomainFragment requestBuilder);
  }
}