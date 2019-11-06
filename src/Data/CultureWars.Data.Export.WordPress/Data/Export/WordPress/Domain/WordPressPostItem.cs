using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Ccr.Std.Core.Extensions;
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

		public WordPressStatus PostStatus { get; }

		public string PostType
		{
			get => "post";
		}

		public CultureWarsAuthor Author { get; }

		public DateTime PublicationDate { get; }

		public DateTime PostDate { get; }

		public DateTime PostDateGMT { get; }

		public IReadOnlyList<CultureWarsCategory> Categories { get; }

		public IReadOnlyList<CultureWarsTag> Tags { get; }

		public IReadOnlyList<PostComment> PostComments { get; }
		
		public string PostContent { get; }
		
		public string PostExcerpt { get; }

		public int ThumbnailId { get; }


		/// <inheritdoc />
		public WordPressPostItem(
			int postId,
			string postName,
			string postTitle,
			string postLink,
			WordPressStatus postStatus,
			CultureWarsAuthor author,
			DateTime publicationDate,
			DateTime postDate,
			DateTime postDateGMT,
			IEnumerable<CultureWarsCategory> categories,
			IEnumerable<CultureWarsTag> tags,
			string postContent,
			string postExcerpt,
			int thumbnailId)
		{
			PostID = postId.EnforceNotNull(nameof(postId));
			PostName = postName.EnforceNotNull(nameof(postName));
			PostTitle = postTitle.EnforceNotNull(nameof(postTitle));
			PostLink = postLink.EnforceNotNull(nameof(postLink));
			PostStatus = postStatus;
			Author = author.EnforceNotNull(nameof(author));
			PublicationDate = publicationDate.EnforceNotNull(nameof(publicationDate));
			PostDate = postDate.EnforceNotNull(nameof(postDate));
			PostDateGMT = postDateGMT.EnforceNotNull(nameof(postDateGMT));
			Categories = categories.EnforceNotNull(nameof(categories)).ToList();
			Tags = tags.EnforceNotNull(nameof(tags)).ToList();
			PostContent = postContent.EnforceNotNull(nameof(postContent));
			PostExcerpt = postExcerpt.EnforceNotNull(nameof(postExcerpt));
			ThumbnailId = thumbnailId;
		}
		

		/// <inheritdoc />
		public XElement ToXElement()
		{
			var pubDateStr = PublicationDate.ToString("ddd, dd MMM yyyy HH:mm:ss") + " +0000";
			var postDateStr = PostDate.ToString("yyyy-MM-dd HH:mm:ss");
			var gmtPostDateStr = PostDateGMT.ToString("yyyy-MM-dd HH:mm:ss");

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

			foreach (var category in Categories)
			{
				var categoryNode = new XElement(XName.Get("category"));
				categoryNode.SetAttributeValue(XName.Get("domain"), "category");
				categoryNode.SetAttributeValue(XName.Get("nicename"), category.CategoryNiceName);
				categoryNode.Add(new XCData(category.CategoryName));

				item.Add(categoryNode);
			}

			foreach (var tag in Tags)
			{
				var categoryNode = new XElement(XName.Get("category"));
				categoryNode.SetAttributeValue(XName.Get("domain"), "post_tag");
				categoryNode.SetAttributeValue(XName.Get("nicename"), tag.TagFriendlyName);
				categoryNode.Add(new XCData(tag.HtmlEncodedTagName));

				item.Add(categoryNode);
			}

			foreach (var postComment in PostComments)
			{
				var comment = postComment.ToXElement();
				item.Add(comment);
			}

			var postMetaNode = new XElement(XName.Get("postmeta", "wp"));

			var metaKey = new XElement(
				XName.Get("meta_key", "wp"), 
				"_thumbnail_id");

			var metaValue = new XElement(
				XName.Get("meta_value", "wp"),
				new XCData(ThumbnailId.ToString()));

			postMetaNode.Add(metaKey);
			postMetaNode.Add(metaValue);

			item.Add(postMetaNode);

			return item;
		}


		public static WordPressPostItem FromXElement(
			XElement xElement)
		{
			if (xElement.Name.LocalName != "item")
				throw new NotSupportedException(
					$"The {nameof(XElement).SQuote()} element name {xElement.Name.ToString().SQuote()} is " +
					$"not valid for this method. Expected name \"item\".");

			var title = xElement.GetSubElement("title");
			var link = xElement.GetSubElement("link");
			var contentEncoded = xElement.GetSubElement("content", "encoded");
			var excerptEncoded = xElement.GetSubElement("excerpt", "encoded");
			var postName = xElement.GetSubElement("wp", "post_name");
			var postId = xElement.GetIntSubElement("wp", "post_id");
			var statusStr = xElement.GetSubElement("wp", "status");
			var pubDate = xElement.GetDateTimeSubElement("pubDate", "ddd, dd MMM yyyy HH:mm:ss +0000");
			var postDate = xElement.GetDateTimeSubElement("wp", "post_date", "yyyy-MM-dd HH:mm:ss");
			var postDateGmt = xElement.GetDateTimeSubElement("wp", "post_date_gmt", "yyyy-MM-dd HH:mm:ss");
			var thumbnailId = xElement.GetPostMetaThumbnailId();
			var postCategories = xElement.GetPostCategories().ToArray();
			var postTags = xElement.GetPostTags().ToArray();
			var creatorStr = xElement.GetSubElement("dc", "creator");

			var status = WordPressStatus.FromName(statusStr);
			var creator = CultureWarsAuthor.EMichaelJones;
		
			return new WordPressPostItem(
				postId,
				postName,
				title,
				link,
				status,
				CultureWarsAuthor.EMichaelJones,
				pubDate,
				postDate,
				postDateGmt,
				postCategories,
				postTags,
				contentEncoded,
				excerptEncoded,
				thumbnailId);
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
//if (!xElement.TryGetSubElement("title", out var title))
//	throw new XmlException(
//		$"Could not find element \"title\" as string.");

//if (!xElement.TryGetSubElement("link", out var link))
//	throw new XmlException(
//		$"Could not find element \"link\" as string.");

//if (!xElement.TryGetSubElement("content", "encoded", out var contentEncoded))
//	throw new XmlException(
//		$"Could not find element \"content:encoded\" as string.");

//if (!xElement.TryGetSubElement("excerpt", "encoded", out var excerptEncoded))
//	throw new XmlException(
//		$"Could not find element \"excerpt:encoded\" as string.");

//if (!xElement.TryGetSubElement("wp", "post_name", out var postName))
//	throw new XmlException(
//		$"Could not find element \"wp:post_name\" as string.");

//if (!xElement.TryGetSubElement("wp", "post_type", out var postType))
//	throw new XmlException(
//		$"Could not find element \"wp:post_type\" as string.");

//if (!xElement.TryGetIntSubElement("wp", "post_id", out var postId))
//	throw new XmlException(
//		$"Could not find element \"wp:post_id\" as string.");

//if (!xElement.TryGetSubElement("wp", "status", out var status))
//	throw new XmlException(
//		$"Could not find element \"wp:status\" as string.");

//if (!xElement.TryGetDateTimeSubElement("pubDate", "ddd, dd MMM yyyy HH:mm:ss +0000", out var pubDate))
//	throw new XmlException(
//		$"Could not find element \"pubDate\" as DateTime in format \"ddd, dd MMM yyyy HH:mm:ss +0000\".");

//if (!xElement.TryGetDateTimeSubElement("wp", "post_date", "yyyy-MM-dd HH:mm:ss", out var postDate))
//	throw new XmlException(
//		$"Could not find element \"wp:post_date\" as DateTime in format \"yyyy-MM-dd HH:mm:ss\".");

//if (!xElement.TryGetDateTimeSubElement("wp", "post_date_gmt", "yyyy-MM-dd HH:mm:ss", out var postDateGmt))
//	throw new XmlException(
//		$"Could not find element \"wp:post_date_gmt\" as DateTime in format \"yyyy-MM-dd HH:mm:ss\".");
