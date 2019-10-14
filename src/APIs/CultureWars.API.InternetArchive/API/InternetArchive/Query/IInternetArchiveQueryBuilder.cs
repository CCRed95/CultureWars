using CultureWars.API.InternetArchive.Domain;

namespace CultureWars.API.InternetArchive.Query
{
	public interface IInternetArchiveQueryBuilder
	{
		InternetArchiveQueryBuilder WithUploader(
			string uploader);

		InternetArchiveQueryBuilder WithSubject(
			string subject);

		InternetArchiveQueryBuilder WithFields(
			params IQueryField[] fields);

		InternetArchiveQueryBuilder WithSort(
			IQueryField field,
			IASortDirection direction);

		InternetArchiveQueryBuilder WithRows(
			uint rowCount);

		InternetArchiveQueryBuilder OnPageNumber(
			uint pageNumber);

		InternetArchiveQueryBuilder WithOutputKind(
			APIDataOutputKind dataOutputKind);

		InternetArchiveQueryBuilder WithCallback(
			string callback);

		InternetArchiveQueryBuilder WithShouldSave(
			bool shouldSave);
	}
}