using System;
using System.Xml.Linq;
using CultureWars.Data.Domain;

namespace CultureWars.Data.Export.WordPress.Domain
{
	public interface IWordPressItem
	{
		int PostID { get; }

		string PostName { get; }

		string PostTitle { get; }

		string PostLink { get; }

		string PostType { get; }

		CultureWarsAuthor Author { get; }

		DateTime PostDateTime { get; }

		string PostContent { get; }

		string PostExcerpt { get; }


		XElement ToXElement();
	}
}