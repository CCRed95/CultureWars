using System;
using System.Collections.Generic;
using System.Linq;
using CultureWars.Core.Extensions;
using CultureWars.Data.Export.WordPress.Domain.Infrastructure;
using CultureWars.Data.Export.WordPress.Domain.Infrastructure.Builders;
using CultureWars.Data.Export.WordPress.Domain.ValueEnums;
using CultureWars.Data.Export.WordPress.XmlWriter;

namespace CultureWars.Data.Export.WordPress.Domain
{
	public class WPPostItem
		: IWPItem,
			IWPXmlStreamWritable
	{
		public int PostID { get; }

		public string PostName { get; }

		public string PostTitle { get; }

		public string PostLink { get; }

		public WPStatus PostStatus { get; }

		public string PostType
		{
			get => "post";
		}

		public WPAuthor Author { get; }

		public DateTime PublicationDate { get; }

		public DateTime PostDate { get; }

		public DateTime PostDateGMT { get; }

		public IReadOnlyList<WPCategory> Categories { get; }

		public IReadOnlyList<WPTerm> Terms { get; }

		public IReadOnlyList<WPPostComment> PostComments { get; }

		public string PostContent { get; }

		public string PostExcerpt { get; }

		public int ThumbnailId { get; }


		public static WPPostItemBuilder Builder
		{
			get => new WPPostItemBuilder();
		}


		/// <inheritdoc />
		public WPPostItem(
			int postId,
			string postName,
			string postTitle,
			string postLink,
			WPStatus postStatus,
			WPAuthor author,
			DateTime publicationDate,
			DateTime postDate,
			DateTime postDateGMT,
			IEnumerable<WPCategory> categories,
			IEnumerable<WPTerm> terms,
			IEnumerable<WPPostComment> postComments,
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
			Terms = terms.EnforceNotNull(nameof(terms)).ToList();
			PostComments = postComments.EnforceNotNull(nameof(postComments)).ToList();
			PostContent = postContent.EnforceNotNull(nameof(postContent));
			PostExcerpt = postExcerpt.EnforceNotNull(nameof(postExcerpt));
			ThumbnailId = thumbnailId;
		}


		/// <inheritdoc />
		public void WriteToXmlStream(
			XmlStreamWriter writer)
		{
			var excerptNs = FXNsRef.FromName("excerpt");
			var contentNs = FXNsRef.FromName("content");
			var dcNs = FXNsRef.FromName("dc");
			var wpNs = FXNsRef.FromName("wp");

			var pubDateStr = PublicationDate.ToString("ddd, dd MMM yyyy HH:mm:ss") + " +0000";
			var postDateStr = PostDate.ToString("yyyy-MM-dd HH:mm:ss");
			var gmtPostDateStr = PostDateGMT.ToString("yyyy-MM-dd HH:mm:ss");

			writer.WriteStartElement("item")
						.WriteInlineElement("title", PostTitle)
						.WriteInlineElement("link", PostLink)
						.WriteInlineCDataElement(contentNs, "encoded", PostContent)
						.WriteInlineCDataElement(excerptNs, "encoded", PostExcerpt)
						.WriteInlineElement(wpNs, "post_name", PostName)
						.WriteInlineElement(wpNs, "post_type", PostType)
						.WriteInlineElement(wpNs, "post_id", PostID.ToString())
						.WriteInlineElement(wpNs, "status", PostStatus.StatusName)
						.WriteInlineElement("pubdate", pubDateStr)
						.WriteInlineElement(wpNs, "post_date", postDateStr)
						.WriteInlineElement(wpNs, "post_date_gmt", gmtPostDateStr)
						.WriteInlineElement(dcNs, "creator", Author.Login)
						.WriteInlineElement(wpNs, "comment_status", "open");

			foreach (var term in Terms)
			{
				writer.WriteStartElement(
								"category",
								("domain", "post_tag"),
								("nicename", term.TagFriendlyName))
							.WriteCDataElement(term.TagName)
							.WriteEndElement();
			}

			foreach (var category in Categories)
			{
				writer.WriteStartElement(
								"category",
								("domain", "category"),
								("nicename", category.CategoryNiceName))
							.WriteCDataElement(category.CategoryName)
							.WriteEndElement();
			}

			writer.WriteStartElement(wpNs, "postmeta")
						.WriteInlineElement(wpNs, "meta_key", "_thumbnail_id")
						.WriteInlineCDataElement(wpNs, "meta_value", ThumbnailId.ToString())
						.WriteEndElement();

			writer.WriteEndElement();
		}
	}
}