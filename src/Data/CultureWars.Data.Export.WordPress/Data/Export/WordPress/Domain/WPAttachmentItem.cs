using System;
using Ccr.Std.Core.Extensions;
using CultureWars.Core.Extensions;
using CultureWars.Data.Export.WordPress.Domain.Infrastructure;
using CultureWars.Data.Export.WordPress.Domain.Infrastructure.Builders;
using CultureWars.Data.Export.WordPress.XmlWriter;

namespace CultureWars.Data.Export.WordPress.Domain
{
	public class WPAttachmentItem
		: IWPItem,
			IWPXmlStreamWritable
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

		public WPAuthor Author { get; }

		public DateTime PublicationDate { get; }

		public DateTime PostDate { get; }

		public DateTime PostDateGMT { get; }

		public string PostContent { get; }

		public string PostExcerpt { get; }


		public static WPAttachmentItemBuilder Builder
		{
			get => new WPAttachmentItemBuilder();
		}


		/// <inheritdoc />
		public WPAttachmentItem(
			int postId,
			string postName,
			string postTitle,
			string postLink,
			string attachmentUrl,
			string status,
			WPAuthor author,
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


		/// <inheritdoc />
		public void WriteToXmlStream(
			XmlStreamWriter writer)
		{
			var excerptNs = FXNsRef.FromName("excerpt");
			var contentNs = FXNsRef.FromName("content");
			var wfwNs = FXNsRef.FromName("wfw");
			var dcNs = FXNsRef.FromName("dc");
			var wpNs = FXNsRef.FromName("wp");
			
			var pubDateStr = PublicationDate.ToString("ddd, dd MMM yyyy HH:mm:ss") + " +0000";
			var postDateStr = PostDate.ToString("yyyy-MM-dd HH:mm:ss");
			var gmtPostDateStr = PostDateGMT.ToString("yyyy-MM-dd HH:mm:ss");

			writer.WriteStartElement("item")
			      .WriteInlineElement(wfwNs, "attachment_url", AttachmentUrl)
			      .WriteInlineElement("link", PostLink)
			      .WriteInlineElement("title", PostTitle)
			      .WriteInlineElement(wpNs, "post_name", PostName)
			      .WriteInlineElement(wpNs, "post_type", PostType)
			      .WriteInlineElement(wpNs, "post_id", PostID.ToString())
			      .WriteInlineElement(wpNs, "post_name", PostName)
			      .WriteInlineElement(wpNs, "status", Status)
			      .WriteInlineCDataElement(contentNs, "encoded", PostContent)
			      .WriteInlineCDataElement(excerptNs, "encoded", PostExcerpt)
			      .WriteInlineElement("pubdate", pubDateStr)
			      .WriteInlineElement(wpNs, "post_date", postDateStr)
			      .WriteInlineElement(wpNs, "post_date_gmt", gmtPostDateStr)
			      .WriteInlineElement(dcNs, "creator", Author.Login);

			writer.WriteEndElement();
		}
	}
}