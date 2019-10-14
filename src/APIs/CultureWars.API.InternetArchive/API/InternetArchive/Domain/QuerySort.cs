using Ccr.Std.Core.Extensions;

namespace CultureWars.API.InternetArchive.Domain
{
	public class QuerySort
	{
		//private const int _maxQueryFields = 3;
		private readonly IQueryField _queryField;
		public string _customField = null;
		private IASortDirection _sortDirection = IASortDirection.Ascending;


		public IQueryField Field
		{
			get => _queryField;
		}

		public string CustomField
		{
			get => _customField;
			//set => _customField = value;
		}

		public IASortDirection SortDirection
		{
			get => _sortDirection;
			//set => _sortDirection = value;
		} 


		public QuerySort(
			IQueryField field,
			IASortDirection sortDirection)
		{
			_queryField = field;
			_sortDirection = sortDirection;
			
			//_queryFields = new IQueryField[_maxQueryFields - 1];
		}


		public override string ToString()
		{
			if (!CustomField.IsNullOrEmptyEx())
				return $"{CustomField}+{(SortDirection.SortKey)}";

			return $"{Field.FieldName}+{SortDirection.SortKey}";

			//return $"{Query.FieldMap[Field]}+{(Ascending ? "asc" : "desc")}";
		}
	}
}