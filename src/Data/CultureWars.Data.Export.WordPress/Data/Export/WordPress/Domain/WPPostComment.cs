using System;
using CultureWars.Core.Extensions;
using CultureWars.Data.Export.WordPress.Domain.Infrastructure;
using CultureWars.Data.Export.WordPress.XmlWriter;

namespace CultureWars.Data.Export.WordPress.Domain
{
	public class WPPostComment
		: IWPXmlStreamWritable
	{
		public int CommentId { get; }

		public int IsApproved { get; }

		public string AuthorEmail { get; }

		public string Author { get; }

		public string AuthorIP { get; }

		public DateTime CommentDate { get; }

		public DateTime CommentDateGMT { get; }

		public string Content { get; }

		public int ParentId { get; }


		public WPPostComment(
			int commentId,
			int isApproved,
			string author,
			string authorEmail,
			string authorIP,
			DateTime commentDate,
			DateTime commentDateGMT,
			string content,
			int parentId)
		{
			CommentId = commentId;
			IsApproved = isApproved;
			Author = author.EnforceNotNull(nameof(author));
			AuthorEmail = authorEmail.EnforceNotNull(nameof(authorEmail));
			AuthorIP = authorIP.EnforceNotNull(nameof(authorIP));
			CommentDate = commentDate;
			CommentDateGMT = commentDateGMT;
			Content = content.EnforceNotNull(nameof(content));
			ParentId = parentId;
		}

		
		/// <inheritdoc />
		public void WriteToXmlStream(
			XmlStreamWriter writer)
		{
			var wpNs = FXNsRef.FromName("wp");

			var commentDateStr = CommentDate.ToString("yyyy-MM-dd HH:mm:ss");
			var gmtCommentDateStr = CommentDateGMT.ToString("yyyy-MM-dd HH:mm:ss");

			writer.WriteStartElement(wpNs, "comment")
			      .WriteInlineElement(wpNs, "comment_id", CommentId.ToString())
			      .WriteInlineElement(wpNs, "comment_approved", "1")
			      .WriteInlineElement(wpNs, "comment_author_email", AuthorEmail)
			      .WriteInlineCDataElement(wpNs, "comment_author", Author)
			      .WriteInlineElement(wpNs, "comment_author_url", "")
			      .WriteInlineElement(wpNs, "comment_author_IP", AuthorIP)
			      .WriteInlineElement(wpNs, "comment_date", commentDateStr)
			      .WriteInlineElement(wpNs, "comment_date_gmt", gmtCommentDateStr)
			      .WriteInlineCDataElement(wpNs, "comment_content", Content)
			      .WriteInlineElement(wpNs, "comment_type", "")
			      .WriteInlineElement(wpNs, "comment_parent", ParentId.ToString());

			writer.WriteEndElement();
		}
	}
}
/*		public XElement ToXElement()
		{
			var comment = new XElement("wp:comment")
				.SubElement("wp", "comment_id", CommentId)
				.SubElement("wp", "comment_approved", IsApproved)
				.SubElement("wp", "comment_author", new XCData(Author))
				.SubElement("wp", "comment_author_email", AuthorEmail)
				.SubElement("wp", "comment_author_IP", AuthorIP)
				.SubElement("wp", "comment_date", CommentDate)
				.SubElement("wp", "comment_date_gmt", CommentDateGMT)
				.SubElement("wp", "comment_content", new XCData(Content))
				.SubElement("wp", "comment_type")
				.SubElement("wp", "comment_parent", ParentId);

			return comment;
		}

		public static WPPostComment FromXElement(
			XElement xElement)
		{
			if (xElement.Name.LocalName != "comment")
				throw new NotSupportedException(
					$"The {nameof(XElement).SQuote()} element name {xElement.Name.ToString().SQuote()} is " +
					$"not valid for this method. Expected name \"comment\".");

			var commentId = xElement.GetIntSubElement("wp", "comment_id");
			var commentApproved = xElement.GetIntSubElement("wp", "comment_approved");
			var commentAuthor = xElement.GetSubElement("wp", "comment_author");
			var commentDate = xElement.GetDateTimeSubElement("wp", "comment_date", "yyyy-MM-dd HH:mm:ss");
			var commentContent = xElement.GetSubElement("wp", "comment_content");
			var commentDateGmt = xElement.GetDateTimeSubElement("wp", "comment_date_gmt", "yyyy-MM-dd HH:mm:ss");
			var parentId = xElement.GetIntSubElement("wp", "comment_parent");


			////if (!xElement.TryGetIntSubElement("wp", "comment_approved", out var commentApproved))
			////	throw new XmlException(
			////		$"Could not find element \"wp:comment_approved\" as integer.");

			//if (!xElement.TryGetSubElement("wp", "comment_author", out var commentAuthor))
			//	throw new XmlException(
			//		$"Could not find element \"wp:comment_author\" as string.");

			//if (!xElement.TryGetDateTimeSubElement("wp", "comment_date", "yyyy-MM-dd HH:mm:ss", out var commentDate))
			//	throw new XmlException(
			//		$"Could not find element \"wp:comment_date\" as DateTime in format \"yyyy-MM-dd HH:mm:ss\".");

			//if (!xElement.TryGetSubElement("wp", "comment_content", out var commentContent))
			//	throw new XmlException(
			//		$"Could not find element \"wp:comment_content\" as string.");

			//if (!xElement.TryGetDateTimeSubElement("wp", "comment_date_gmt", "yyyy-MM-dd HH:mm:ss", out var commentDateGmt))
			//	throw new XmlException(
			//		$"Could not find element \"wp:comment_date_gmt\" as DateTime in format \"yyyy-MM-dd HH:mm:ss\".");

			//if (!xElement.TryGetIntSubElement("wp", "comment_parent", out var parentId))
			//	throw new XmlException(
			//		$"Could not find element \"wp:comment_parent\" as integer.");

			return new WPPostComment(
				commentId,
				commentApproved,
				commentAuthor,
				"", 
				"",
				commentDate, 
				commentDateGmt, 
				commentContent, 
				parentId);
		}*/
