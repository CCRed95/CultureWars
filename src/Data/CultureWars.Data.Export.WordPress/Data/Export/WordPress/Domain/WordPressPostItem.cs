using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using CultureWars.Core.Extensions;
using CultureWars.Data.Domain;

namespace CultureWars.Data.Export.WordPress.Domain
{
	public class WordPressPostItem
		: IWordPressItem
	{
		public int PostID { get; }

		public string PostName { get; }

		public string PostTitle { get; }

		public string PostLink { get; }

		public string PostType
		{
			get => "post";
		}

		public CultureWarsAuthor Author { get; }

		public DateTime PostDateTime { get; }

		public IReadOnlyList<CultureWarsCategory> Categories { get; }

		public IReadOnlyList<CultureWarsTag> Tags { get; }

		public string PostContent { get; }
		
		public string PostExcerpt { get; }


		/// <inheritdoc />
		public WordPressPostItem(
			int postId,
			string postName,
			string postTitle,
			string postLink,
			CultureWarsAuthor author,
			DateTime postDateTime,
			IEnumerable<CultureWarsCategory> categories,
			IEnumerable<CultureWarsTag> tags,
			string postContent,
			string postExcerpt)
		{
			PostID = postId.EnforceNotNull(nameof(postId));
			PostName = postName.EnforceNotNull(nameof(postName));
			PostTitle = postTitle.EnforceNotNull(nameof(postTitle));
			PostLink = postLink.EnforceNotNull(nameof(postLink));
			Author = author.EnforceNotNull(nameof(author));
			PostDateTime = postDateTime.EnforceNotNull(nameof(postDateTime));
			Categories = categories.EnforceNotNull(nameof(categories)).ToList();
			Tags = tags.EnforceNotNull(nameof(tags)).ToList();
			PostContent = postContent.EnforceNotNull(nameof(postContent));
			PostExcerpt = postExcerpt.EnforceNotNull(nameof(postExcerpt));
		}

		
		private static XElement CreateNode(
			string elementName,
			string value)
		{
			return new XElement(elementName, value);
		}

		private static XElement CreateNode(
			string ns,
			string elementName,
			string value)
		{
			return new XElement($"{ns}:{elementName}", value);
		}

		private static XElement CreateNode(
			string elementName,
			XCData value)
		{
			return new XElement(elementName, value);
		}

		private static XElement CreateNode(
			string ns,
			string elementName,
			XCData value)
		{
			return new XElement($"{ns}:{elementName}", value);
		}

		//TODO finish this
		/// <inheritdoc />
		public XElement ToXElement()
		{
			var item = new XElement("item");

			item.Add(CreateNode("link", PostLink));
			item.Add(CreateNode("title", PostTitle));

			item.Add(CreateNode("wp", "post_name", PostName));
			item.Add(CreateNode("wp", "post_type", "post"));
			item.Add(CreateNode("wp", "post_id", PostLink));
			//item.Add(CreateNode("wp", "status", Status));

			item.Add(CreateNode("content", "encoded", new XCData(PostContent)));
			item.Add(CreateNode("excerpt", "encoded", new XCData(PostExcerpt)));

			var pubDateStr = PostDateTime.ToString("ddd, dd MMM yyyy HH:mm:ss") + " +0000";
			var postDateStr = PostDateTime.ToString("yyyy-MM-dd HH:mm:ss");
			var gmtPostDateStr = PostDateTime.ToString("yyyy-MM-dd HH:mm:ss");

			item.Add(CreateNode("pubDate", pubDateStr));
			item.Add(CreateNode("wp", "post_date", postDateStr));
			item.Add(CreateNode("wp", "post_date_gmt", gmtPostDateStr));

			item.Add(CreateNode("dc", "creator", Author.AuthorLogin));
		
			return item;
		}

	}
}