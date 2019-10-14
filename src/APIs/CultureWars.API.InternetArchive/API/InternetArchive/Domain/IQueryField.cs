using System;
using JetBrains.Annotations;

namespace CultureWars.API.InternetArchive.Domain
{
	public interface IQueryField
	{
		[NotNull]
		string FieldName { get; }

		[NotNull]
		string MemberName { get; }

		Type ValueType { get; }
	}
}