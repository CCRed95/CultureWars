using System;
using System.Globalization;
using System.Xml.Linq;
using CultureWars.Core.Extensions;
using CultureWars.Data.Domain;

namespace CultureWars.Data.Export.WordPress.Domain
{
	public class WordPressAttachmentItem
		: IWordPressItem
	{
		public int PostID { get; }

		public string PostName { get; }

		public string PostTitle { get; }

		public string PostLink { get; }

		public string PostType
		{
			get => "attachment";
		}

		public string AttachmentUrl { get; }

		public string Status { get; }

		public CultureWarsAuthor Author { get; }

		public DateTime PublicationDate { get; }

		public DateTime PostDate { get; }

		public DateTime PostDateGMT { get; }

		public string PostContent { get; }

		public string PostExcerpt { get; }


		/// <inheritdoc />
		public WordPressAttachmentItem(
			int postId,
			string postName,
			string postTitle,
			string postLink,
			string attachmentUrl,
			string status,
			CultureWarsAuthor author,
			DateTime publicationDate,
			DateTime postDate,
			DateTime postDateGmt,
			string postContent,
			string postExcerpt)
		{
			PostID = postId.EnforceNotNull(nameof(postId));
			PostName = postName.EnforceNotNull(nameof(postName));
			PostTitle = postTitle.EnforceNotNull(nameof(postTitle));
			PostLink = postLink.EnforceNotNull(nameof(postLink));
			AttachmentUrl = attachmentUrl.EnforceNotNull(nameof(attachmentUrl));
			Status = status.EnforceNotNull(nameof(status));
			Author = author.EnforceNotNull(nameof(author));
			PublicationDate = publicationDate.EnforceNotNull(nameof(publicationDate));
			PostDate = postDate.EnforceNotNull(nameof(postDate));
			PostDateGMT = postDateGmt.EnforceNotNull(nameof(postDateGmt));
			PostContent = postContent.EnforceNotNull(nameof(postContent));
			PostExcerpt = postExcerpt.EnforceNotNull(nameof(postExcerpt));
		}


		public static WordPressAttachmentItem FromXElement(
			XElement attachmentItem)
		{
			var attachmentUrl = attachmentItem.Element(XName.Get("attachment_url", "wp"))?.Value;
			var postLink = attachmentItem.Element(XName.Get("link"))?.Value;
			var postTitle = attachmentItem.Element(XName.Get("title"))?.Value;

			var postName = attachmentItem.Element(XName.Get("post_name", "wp"))?.Value;
			var postType = attachmentItem.Element(XName.Get("post_type", "wp"))?.Value;
			var postIdStr = attachmentItem.Element(XName.Get("post_id", "wp"))?.Value;

			if (!int.TryParse(postIdStr, out var postId))
				throw new FormatException();

			var status = attachmentItem.Element(XName.Get("status", "wp"))?.Value;

			var postContentCData = attachmentItem.Element(XName.Get("encoded", "content"))?.Value;
			var postExcerptCData = attachmentItem.Element(XName.Get("encoded", "excerpt"))?.Value;

			var pubDateStr = attachmentItem.Element(XName.Get("pubDate"))?.Value.Replace(" +0000", "");
			var postDateStr = attachmentItem.Element(XName.Get("post_date", "wp"))?.Value;
			var gmtPostDateStr = attachmentItem.Element(XName.Get("post_date_gmt", "wp"))?.Value;

			if (!DateTime.TryParseExact(pubDateStr, "ddd, dd MMM yyyy HH:mm:ss", 
				CultureInfo.InvariantCulture, DateTimeStyles.None, out var pubDate))
				throw new FormatException();

			if (!DateTime.TryParseExact(postDateStr, "yyyy-MM-dd HH:mm:ss",
				CultureInfo.InvariantCulture, DateTimeStyles.None, out var postDate))
				throw new FormatException();

			if (!DateTime.TryParseExact(gmtPostDateStr, "yyyy-MM-dd HH:mm:ss",
				CultureInfo.InvariantCulture, DateTimeStyles.None, out var gmtPostDate))
				throw new FormatException();
			
			var authorLogin = attachmentItem.Element(XName.Get("creator", "dc"))?.Value;
			var author = CultureWarsAuthor.EMichaelJones;

			return new WordPressAttachmentItem(
				postId,
				postName,
				postTitle,
				postLink,
				attachmentUrl,
				status,
				author, 
				pubDate,
				postDate,
				gmtPostDate,
				postContentCData,
				postExcerptCData);
		}

		public XElement ToXElement()
		{
			var item = new XElement("item");

			return null;
		}
	}
}

//dynamic xml = new Xml();
//xml.item(
//	Xml.Fragment(
//		t =>
//		{
//			xml.@wp_attachment_url(AttachmentUrl);
//			xml.link(PostLink);
//			xml.title(PostTitle);

//			xml.@wp_post_name(PostName);
//			xml.@wp_post_type(PostType);
//			xml.@wp_post_id(PostID);
//			xml.@wp_status(Status);

//			xml.@content_encoded(new XCData(PostContent));
//			xml.@excerpt_encoded(new XCData(PostExcerpt));

//			var pubDateStr = PostDateTime.ToString("ddd, dd MMM yyyy HH:mm:ss") + " +0000";
//			var postDateStr = PostDateTime.ToString("yyyy-MM-dd HH:mm:ss");
//			var gmtPostDateStr = PostDateTime.ToString("yyyy-MM-dd HH:mm:ss");

//			xml.pubDate(pubDateStr);
//			xml.@wp_post_date(postDateStr);
//			xml.@wp_post_date_gmt(gmtPostDateStr);

//			xml.@dc_creator(Author.AuthorLogin);
//		}));

/*
			item.Add(CreateNode("wp", "attachment_url", AttachmentUrl));
			item.Add(CreateNode("link", PostLink));
			item.Add(CreateNode("title", PostTitle));

			item.Add(CreateNode("wp", "post_name", PostName));
			item.Add(CreateNode("wp", "post_type", "post"));
			item.Add(CreateNode("wp", "post_id", PostLink));
			item.Add(CreateNode("wp", "status", Status));

			item.Add(CreateNode("content", "encoded", new XCData(PostContent)));
			item.Add(CreateNode("excerpt", "encoded", new XCData(PostExcerpt)));

			//var pubDateStr = PostDateTime.ToString("ddd, dd MMM yyyy HH:mm:ss") + " +0000";
			//var postDateStr = PostDateTime.ToString("yyyy-MM-dd HH:mm:ss");
			//var gmtPostDateStr = PostDateTime.ToString("yyyy-MM-dd HH:mm:ss");

			item.Add(CreateNode("pubDate", pubDateStr));
			item.Add(CreateNode("wp", "post_date", postDateStr));
			item.Add(CreateNode("wp", "post_date_gmt", gmtPostDateStr));

			item.Add(CreateNode("dc", "creator", Author.AuthorLogin));

			return item;*/
