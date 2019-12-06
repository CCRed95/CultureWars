using System;
namespace CultureWars.Data.Export.WordPress.Domain.Infrastructure
{
	public interface IWPItem
		//: IWPXmlItem
	{
		int PostID { get; }

		string PostName { get; }

		string PostTitle { get; }

		string PostLink { get; }

		string PostType { get; }

		WPAuthor Author { get; }

		DateTime PublicationDate { get; }

		DateTime PostDate { get; }

		DateTime PostDateGMT { get; }

		string PostContent { get; }

		string PostExcerpt { get; }
	}
}