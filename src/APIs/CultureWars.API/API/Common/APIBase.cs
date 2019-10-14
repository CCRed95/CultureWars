using System.Collections.Generic;
using CultureWars.API.Common.Query;
using CultureWars.API.Infrastructure;

namespace CultureWars.API.Common
{
  public abstract class APIBase<TResult, TQueryBuilder>
    where TQueryBuilder
      : IQueryBuilder
  {
    protected abstract DomainFragment RequestBuilder { get; }


    public abstract IEnumerable<TResult> Query(
      TQueryBuilder queryBuilder);
  }
}
