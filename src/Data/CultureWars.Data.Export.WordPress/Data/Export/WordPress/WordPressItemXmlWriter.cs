using System;
using System.Collections.Generic;
using CultureWars.Data.Domain;
using CultureWars.Data.Export.WordPress.Domain;
using JetBrains.Annotations;

namespace CultureWars.Data.Export.WordPress
{
	public class WordPressItemXmlWriter
		: WordPressXmlWriterBase
	{
		protected WordPressXmlWriter WordPressXmlWriter;


		public WordPressItemXmlWriter(
			[NotNull] WordPressXmlWriter wordPressTextWriter)
			: base(wordPressTextWriter)
		{
			WordPressXmlWriter = wordPressTextWriter;
		}


		public WordPressItemXmlWriter WriteTitle(
			string title)
		{
			return WriteInlineElement("title", title);
		}

		public WordPressItemXmlWriter WritePostLink(
			string postLink)
		{
			return WriteInlineElement("link", postLink);
		}

		public WordPressItemXmlWriter WritePostName(
			string postName)
		{
			return WriteInlineElement("wp", "post_name", postName);
		}

		public WordPressItemXmlWriter WritePostType(
			string postType)
		{
			return WriteInlineElement("wp", "post_Type", postType);
		}

		public WordPressItemXmlWriter WritePostId(
			int postId)
		{
			return WriteInlineElement("wp", "post_id", postId.ToString());
		}

		public WordPressItemXmlWriter WriteParentPost(
			WordPressPost wordPressPost)
		{
			return WriteInlineElement("wp", "post_parent", wordPressPost.PostID.ToString());
		}

		public WordPressItemXmlWriter WritePostStatus(
			string postStatus)
		{
			return WriteInlineElement("wp", "status", postStatus);
		}

		public WordPressItemXmlWriter WriteContentEncoded(
			string contentEncoded)
		{
			_xmlTextWriter.WriteStartElement("content", "encoded");
			_xmlTextWriter.WriteCData(contentEncoded);
			_xmlTextWriter.WriteEndElement();
			return this;
		}

		public WordPressItemXmlWriter WriteExcerptEncoded(
			string excerptEncoded)
		{
			_xmlTextWriter.WriteStartElement("excerpt", "encoded");
			_xmlTextWriter.WriteCData(excerptEncoded);
			_xmlTextWriter.WriteEndElement();
			return this;
		}

		public WordPressItemXmlWriter WritePostDate(
			DateTime postDate)
		{
			var pubDateStr = postDate.ToString("ddd, dd MMM yyyy HH:mm:ss") + " +0000";
			var postDateStr = postDate.ToString("yyyy-MM-dd HH:mm:ss");
			var gmtPostDateStr = postDate.ToString("yyyy-MM-dd HH:mm:ss");

			WriteInlineElement("pubDate", pubDateStr);
			WriteInlineElement("wp", "post_date", postDateStr);
			WriteInlineElement("wp", "post_date_gmt", gmtPostDateStr);
			return this;
		}

		public WordPressItemXmlWriter WriteTag(
			CultureWarsTag tag)
		{
			_xmlTextWriter.WriteStartElement("category");
			_xmlTextWriter.WriteAttributeString("domain", "post_tag");
			_xmlTextWriter.WriteAttributeString("nicename", tag.HtmlEncodedTagName);
			_xmlTextWriter.WriteCData(tag.TagFriendlyName);
			_xmlTextWriter.WriteEndElement();
			return this;
		}

		public WordPressItemXmlWriter WriteTags(
			IEnumerable<CultureWarsTag> tags)
		{
			foreach (var tag in tags)
				WriteTag(tag);
			return this;
		}

		public WordPressItemXmlWriter WriteCategory(
			CultureWarsCategory category)
		{
			_xmlTextWriter.WriteStartElement("category");
			_xmlTextWriter.WriteAttributeString("domain", "category");
			_xmlTextWriter.WriteAttributeString("nicename", category.CategoryName);
			_xmlTextWriter.WriteCData(category.CategoryNiceName);
			_xmlTextWriter.WriteEndElement();
			return this;
		}

		public WordPressItemXmlWriter WriteContent(
			string postContent)
		{
			_xmlTextWriter.WriteStartElement("content", "encoded");
			_xmlTextWriter.WriteCData(postContent);
			_xmlTextWriter.WriteEndElement();
			return this;
		}

		public WordPressItemXmlWriter WriteExcerpt(
			string postExcerpt)
		{
			_xmlTextWriter.WriteStartElement("excerpt", "encoded");
			_xmlTextWriter.WriteCData(postExcerpt);
			_xmlTextWriter.WriteEndElement();
			return this;
		}

		public WordPressItemXmlWriter WriteAuthor(
			CultureWarsAuthor author)
		{
			return WriteInlineElement("dc", "creator", author.AuthorLogin);
		}

		public WordPressItemXmlWriter WriteCommentStatus(
			string commentStatus)
		{
			return WriteInlineElement("wp", "comment_status", commentStatus);
		}

		public WordPressItemXmlWriter WritePostMeta(
			int thumbnailId)
		{
			_xmlTextWriter.WriteStartElement("wp", "postmeta");
			_xmlTextWriter.WriteAttributeString("wp", "meta_key", "_thumbnail_id");
			_xmlTextWriter.WriteAttributeString("wp", "meta_value", thumbnailId.ToString());
			_xmlTextWriter.WriteEndElement();
			return this;
		}

		public WordPressXmlWriter WriteEndItem()
		{
			_xmlTextWriter.WriteEndElement();
			return WordPressXmlWriter;
		}


		protected WordPressItemXmlWriter WriteInlineElement(
			string elementName,
			string value)
		{
			_xmlTextWriter.WriteStartElement(elementName);
			_xmlTextWriter.WriteString(value);
			_xmlTextWriter.WriteEndElement();
			return this;
		}

		protected WordPressItemXmlWriter WriteInlineElement(
			string ns,
			string elementName,
			string value)
		{
			_xmlTextWriter.WriteStartElement(elementName, ns);
			_xmlTextWriter.WriteString(value);
			_xmlTextWriter.WriteEndElement();
			return this;
		}
	}


	public class WordPressItemXmlReader
		: WordPressXmlReaderBase
	{
		protected WordPressXmlReader WordPressXmlReader;


		public WordPressItemXmlWriter(
			[NotNull] WordPressXmlReader wordPressTextReader)
				: base(wordPressTextReader)
		{
			WordPressXmlReader = wordPressTextReader;
		}


		public WordPressItemXmlWriter WriteTitle(
			string title)
		{
			return WriteInlineElement("title", title);
		}

		public WordPressItemXmlWriter WritePostLink(
			string postLink)
		{
			return WriteInlineElement("link", postLink);
		}

		public WordPressItemXmlWriter WritePostName(
			string postName)
		{
			return WriteInlineElement("wp", "post_name", postName);
		}

		public WordPressItemXmlWriter WritePostType(
			string postType)
		{
			return WriteInlineElement("wp", "post_Type", postType);
		}

		public WordPressItemXmlWriter WritePostId(
			int postId)
		{
			return WriteInlineElement("wp", "post_id", postId.ToString());
		}

		public WordPressItemXmlWriter WriteParentPost(
			WordPressPost wordPressPost)
		{
			return WriteInlineElement("wp", "post_parent", wordPressPost.PostID.ToString());
		}

		public WordPressItemXmlWriter WritePostStatus(
			string postStatus)
		{
			return WriteInlineElement("wp", "status", postStatus);
		}

		public WordPressItemXmlWriter WriteContentEncoded(
			string contentEncoded)
		{
			_xmlTextWriter.WriteStartElement("content", "encoded");
			_xmlTextWriter.WriteCData(contentEncoded);
			_xmlTextWriter.WriteEndElement();
			return this;
		}

		public WordPressItemXmlWriter WriteExcerptEncoded(
			string excerptEncoded)
		{
			_xmlTextWriter.WriteStartElement("excerpt", "encoded");
			_xmlTextWriter.WriteCData(excerptEncoded);
			_xmlTextWriter.WriteEndElement();
			return this;
		}

		public WordPressItemXmlWriter WritePostDate(
			DateTime postDate)
		{
			var pubDateStr = postDate.ToString("ddd, dd MMM yyyy HH:mm:ss" + "+0000");
			var postDateStr = postDate.ToString("yyyy-MM-dd HH:mm:ss");
			var gmtPostDateStr = postDate.ToString("yyyy-MM-dd HH:mm:ss");

			WriteInlineElement("pubDate", pubDateStr);
			WriteInlineElement("wp", "post_date", postDateStr);
			WriteInlineElement("wp", "post_date_gmt", gmtPostDateStr);
			return this;
		}

		public WordPressItemXmlWriter WriteTag(
			CultureWarsTag tag)
		{
			_xmlTextWriter.WriteStartElement("category");
			_xmlTextWriter.WriteAttributeString("domain", "post_tag");
			_xmlTextWriter.WriteAttributeString("nicename", tag.HtmlEncodedTagName);
			_xmlTextWriter.WriteCData(tag.TagFriendlyName);
			_xmlTextWriter.WriteEndElement();
			return this;
		}

		public WordPressItemXmlWriter WriteTags(
			IEnumerable<CultureWarsTag> tags)
		{
			foreach (var tag in tags)
				WriteTag(tag);
			return this;
		}

		public WordPressItemXmlWriter WriteCategory(
			CultureWarsCategory category)
		{
			_xmlTextWriter.WriteStartElement("category");
			_xmlTextWriter.WriteAttributeString("domain", "category");
			_xmlTextWriter.WriteAttributeString("nicename", category.CategoryName);
			_xmlTextWriter.WriteCData(category.CategoryNiceName);
			_xmlTextWriter.WriteEndElement();
			return this;
		}

		public WordPressItemXmlWriter WriteContent(
			string postContent)
		{
			_xmlTextWriter.WriteStartElement("content", "encoded");
			_xmlTextWriter.WriteCData(postContent);
			_xmlTextWriter.WriteEndElement();
			return this;
		}

		public WordPressItemXmlWriter WriteExcerpt(
			string postExcerpt)
		{
			_xmlTextWriter.WriteStartElement("excerpt", "encoded");
			_xmlTextWriter.WriteCData(postExcerpt);
			_xmlTextWriter.WriteEndElement();
			return this;
		}

		public WordPressItemXmlWriter WriteAuthor(
			CultureWarsAuthor author)
		{
			return WriteInlineElement("dc", "creator", author.AuthorLogin);
		}

		public WordPressItemXmlWriter WriteCommentStatus(
			WordPressCommentStatus commentStatus)
		{
			return WriteInlineElement("wp", "comment_status", commentStatus.StatusName);
		}

		public WordPressItemXmlWriter WritePostMeta(
			int thumbnailId)
		{
			_xmlTextWriter.WriteStartElement("wp", "postmeta");
			_xmlTextWriter.WriteAttributeString("wp", "meta_key", "_thumbnail_id");
			_xmlTextWriter.WriteAttributeString("wp", "meta_value", thumbnailId.ToString());
			_xmlTextWriter.WriteEndElement();
			return this;
		}

		public WordPressXmlWriter WriteEndItem()
		{
			_xmlTextWriter.WriteEndElement();
			return WordPressXmlWriter;
		}


		protected WordPressItemXmlWriter WriteInlineElement(
			string elementName,
			string value)
		{
			_xmlTextWriter.WriteStartElement(elementName);
			_xmlTextWriter.WriteString(value);
			_xmlTextWriter.WriteEndElement();
			return this;
		}

		protected WordPressItemXmlWriter WriteInlineElement(
			string ns,
			string elementName,
			string value)
		{
			_xmlTextWriter.WriteStartElement(elementName, ns);
			_xmlTextWriter.WriteString(value);
			_xmlTextWriter.WriteEndElement();
			return this;
		}
	}
}