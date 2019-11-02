using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using CultureWars.Core.Extensions;
using CultureWars.Data.Domain;
using CultureWars.Extensions;

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

		public IReadOnlyList<PostComment> PostComments { get; }


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
		
		/// <inheritdoc />
		public XElement ToXElement()
		{
			var pubDateStr = PostDateTime.ToString("ddd, dd MMM yyyy HH:mm:ss") + " +0000";
			var postDateStr = PostDateTime.ToString("yyyy-MM-dd HH:mm:ss");
			var gmtPostDateStr = PostDateTime.ToString("yyyy-MM-dd HH:mm:ss");

			var item = new XElement("item")
				.SubElement("link", PostLink)
				.SubElement("title", PostTitle)
				.SubElement("wp", "post_name", PostName)
				.SubElement("wp", "post_type", PostType)
				.SubElement("wp", "post_id", PostID.ToString())
				.SubElement("content", "encoded", new XCData(PostContent))
				.SubElement("excerpt", "encoded", new XCData(PostExcerpt))
				.SubElement("pubDate", pubDateStr)
				.SubElement("wp", "post_date", postDateStr)
				.SubElement("wp", "post_date_gmt", gmtPostDateStr)
				.SubElement("dc", "creator", Author.AuthorLogin);
		
			return item;
		}

	}
}
/*
			item.Add(CreateNode("link", PostLink));
			item.Add(CreateNode("title", PostTitle));

			item.Add(CreateNode("wp", "post_name", PostName));
			item.Add(CreateNode("wp", "post_type", "post"));
			item.Add(CreateNode("wp", "post_id", PostLink));
			//item.Add(CreateNode("wp", "status", Status));

			item.Add(CreateNode("content", "encoded", new XCData(PostContent)));
			item.Add(CreateNode("excerpt", "encoded", new XCData(PostExcerpt)));

			item.Add(CreateNode("pubDate", pubDateStr));
			item.Add(CreateNode("wp", "post_date", postDateStr));
			item.Add(CreateNode("wp", "post_date_gmt", gmtPostDateStr));

			item.Add(CreateNode("dc", "creator", Author.AuthorLogin));*/
