using System;
using System.Collections.Generic;
using System.Linq;
using Ccr.Std.Core.Extensions;
using CultureWars.Core.Extensions;
using static CultureWars.Data.Export.WordPress.Domain.WPXmlns;
using CultureWars.Data.Export.WordPress.Domain.Infrastructure;
using CultureWars.Data.Export.WordPress.Domain.Infrastructure.Builders;
using CultureWars.Data.Export.WordPress.XmlWriter;

namespace CultureWars.Data.Export.WordPress.Domain
{
	public static class WPXmlns
	{
		private const string _excerptNs = "http://wordpress.org/export/1.2/excerpt/";
		private const string _contentNs = "http://purl.org/rss/1.0/modules/content/";
		private const string _wfwNs = "http://wellformedweb.org/CommentAPI/";
		private const string _dcNs = "http://purl.org/dc/elements/1.1/";
		private const string _wpNs = "http://wordpress.org/export/1.2/";

		public static readonly FXNamespace excerptNs = ("excerpt", _excerptNs);
		public static readonly FXNamespace contentNs = ("content", _contentNs);
		public static readonly FXNamespace wfwNs = ("wfw", _wfwNs);
		public static readonly FXNamespace dcNs = ("dc", _dcNs);
		public static readonly FXNamespace wpNs = ("wp", _wpNs);
	}

	public class WPChannelDocument
		: IWPXmlStreamWritable
	{
		public string ChannelTitle { get; }

		public string ChannelLink { get; }

		public DateTime PublicationDate { get; }

		public string ChannelDescription { get; }

		public string LanguageCode { get; }

		public double XmlVersion { get; }

		public double WXRVersion { get; }

		public IReadOnlyList<WPAuthor> Authors { get; }

		public IReadOnlyList<WPTerm> Terms { get; }

		public IReadOnlyList<WPCategory> Categories { get; }

		public IReadOnlyList<WPPostItem> Posts { get; }

		public IReadOnlyList<WPAttachmentItem> Attachments { get; }



		public WPChannelDocument(
			string channelTitle,
			string channelLink,
			DateTime publicationDate,
			string channelDescription,
			string languageCode,
			double xmlVersion,
			double wxrVersion,
			IEnumerable<WPAuthor> authors,
			IEnumerable<WPTerm> terms,
			IEnumerable<WPCategory> categories,
			IEnumerable<WPPostItem> posts,
			IEnumerable<WPAttachmentItem> attachments)
		{
			ChannelTitle = channelTitle.EnforceNotNull(nameof(channelTitle));
			ChannelLink = channelLink.EnforceNotNull(nameof(channelLink));
			PublicationDate = publicationDate;
			ChannelDescription = channelDescription.EnforceNotNull(nameof(channelDescription));
			LanguageCode = languageCode.EnforceNotNull(nameof(languageCode));
			XmlVersion = xmlVersion;
			WXRVersion = wxrVersion;
			Authors = authors.EnforceNotNull(nameof(authors)).ToList();
			Terms = terms.EnforceNotNull(nameof(terms)).ToList();
			Categories = categories.EnforceNotNull(nameof(categories)).ToList();
			Posts = posts.EnforceNotNull(nameof(posts)).ToList();
			Attachments = attachments.EnforceNotNull(nameof(attachments)).ToList();
		}


		public static WPChannelDocumentBuilder Builder
		{
			get => new WPChannelDocumentBuilder();
		}


		/// <inheritdoc />
		public void WriteToXmlStream(
			XmlStreamWriter writer)
		{
			writer.WithDeclaration(FXDeclaration.Get(XmlVersion, "utf-8"))
			      .WithNamespace(excerptNs)
			      .WithNamespace(contentNs)
			      .WithNamespace(wfwNs)
			      .WithNamespace(dcNs)
			      .WithNamespace(wpNs);

			writer.WriteStartElement("rss")
			      .WriteStartElement("channel")
			      .WriteInlineElement("title", "Culture Wars")
			      .WriteInlineElement(wpNs, "wxr_version", $"{WXRVersion:0.0}");

			foreach (var author in Authors)
			{
				author.WriteToXmlStream(writer);
			}
			foreach (var term in Terms)
			{
				term.WriteToXmlStream(writer);
			}
			foreach (var category in Categories)
			{
				category.WriteToXmlStream(writer);
			}
			foreach (var post in Posts)
			{
				post.WriteToXmlStream(writer);
			}
			foreach (var attachments in Attachments)
			{
				attachments.WriteToXmlStream(writer);
			}

			writer.WriteEndElement()
			      .WriteEndElement();
		}
	}
}