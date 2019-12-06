using System;

namespace CultureWars.Data.Export.WordPress.Domain.Infrastructure.Builders
{
	internal interface IWPChannelDocumentBuilder
	{
		WPChannelDocumentBuilder WithChannelTitle(
			string channelTitle);

		WPChannelDocumentBuilder WithChannelLink(
			string channelLink);

		WPChannelDocumentBuilder WithPublicationDate(
			DateTime publicationDate);
		
		WPChannelDocumentBuilder WithChannelDescription(
			string channelDescription);

		WPChannelDocumentBuilder WithPostTitle(
			string postName);

		WPChannelDocumentBuilder WithLanguageCode(
			string languageCode);
		
		WPChannelDocumentBuilder WithXmlVersion(
			double xmlVersion);

		WPChannelDocumentBuilder WithWXRVersion(
			double wxrVersion);

		WPChannelDocumentBuilder WithAuthors(
			params WPAuthor[] authors);

		WPChannelDocumentBuilder WithTerms(
			params WPTerm[] terms);

		WPChannelDocumentBuilder WithCategories(
			params WPCategory[] categories);

		WPChannelDocumentBuilder WithPosts(
			params WPPostItem[] posts);

		WPChannelDocumentBuilder WithAttachments(
			params WPAttachmentItem[] attachments);
	}
}