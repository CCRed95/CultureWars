using System;
using CultureWars.Data.Domain;

namespace CultureWars.Data.Export.WordPress.Domain
{
	public interface IWordPressItem
		: IWordPressXmlItem
	{
		int PostID { get; }

		string PostName { get; }

		string PostTitle { get; }

		string PostLink { get; }

		string PostType { get; }

		CultureWarsAuthor Author { get; }

		DateTime PublicationDate { get; }

		DateTime PostDate { get; }

		DateTime PostDateGMT { get; }

		string PostContent { get; }

		string PostExcerpt { get; }
	}
}