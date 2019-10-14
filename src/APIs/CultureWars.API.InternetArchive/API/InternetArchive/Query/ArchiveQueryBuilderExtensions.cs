using Ccr.Std.Core.Extensions;
using CultureWars.API.InternetArchive.Domain;

namespace CultureWars.API.InternetArchive.Query
{
  public static class InternetArchiveQueryBuilderExtensions
  {
    public static InternetArchiveQueryBuilder WithUploader(
      this InternetArchiveQueryBuilder @this,
      string uploader)
    {
      return @this
        .As<IInternetArchiveQueryBuilder>()
        .WithUploader(
          uploader);
    }

    public static InternetArchiveQueryBuilder WithSubject(
      this InternetArchiveQueryBuilder @this,
      string subject)
    {
      return @this
        .As<IInternetArchiveQueryBuilder>()
        .WithSubject(
          subject);
    }

    public static InternetArchiveQueryBuilder WithFields(
      this InternetArchiveQueryBuilder @this,
      params IQueryField[] fields)
    {
      return @this
        .As<IInternetArchiveQueryBuilder>()
        .WithFields(
	        fields);
    }

    public static InternetArchiveQueryBuilder WithSort(
      this InternetArchiveQueryBuilder @this,
      IQueryField field,
      IASortDirection direction)
    {
      return @this
        .As<IInternetArchiveQueryBuilder>()
        .WithSort(
	        field,
	        direction);
    }

    public static InternetArchiveQueryBuilder WithRows(
      this InternetArchiveQueryBuilder @this,
      uint rowCount)
    {
      return @this
        .As<IInternetArchiveQueryBuilder>()
        .WithRows(
          rowCount);
    }

    public static InternetArchiveQueryBuilder OnPageNumber(
      this InternetArchiveQueryBuilder @this,
      uint pageNumber)
    {
      return @this
        .As<IInternetArchiveQueryBuilder>()
        .OnPageNumber(
          pageNumber);
    }

    public static InternetArchiveQueryBuilder WithOutputKind(
      this InternetArchiveQueryBuilder @this,
      APIDataOutputKind dataOutputKind)
    {
      return @this
        .As<IInternetArchiveQueryBuilder>()
        .WithOutputKind(
          dataOutputKind);
    }

    public static InternetArchiveQueryBuilder WithCallback(
      this InternetArchiveQueryBuilder @this,
      string callback)
    {
      return @this
        .As<IInternetArchiveQueryBuilder>()
        .WithCallback(
          callback);
    }

    public static InternetArchiveQueryBuilder WithShouldSave(
      this InternetArchiveQueryBuilder @this,
      bool shouldSave)
    {
      return @this
        .As<IInternetArchiveQueryBuilder>()
        .WithShouldSave(
          shouldSave);
    }
  }
}