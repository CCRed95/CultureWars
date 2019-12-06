using System;

namespace CultureWars.Data.Export.WordPress.Domain.Infrastructure.Builders
{
	internal interface IWPAttachmentItemBuilder
	{
		WPAttachmentItemBuilder WithPostID(
			int postID);

		WPAttachmentItemBuilder WithPostName(
			string postName);

		WPAttachmentItemBuilder WithPostLink(
			string postLink);

		WPAttachmentItemBuilder WithPostTitle(
			string postName);
		
		WPAttachmentItemBuilder WithAttachmentUrl(
			string attachmentUrl);

		WPAttachmentItemBuilder WithStatus(
			string status);

		WPAttachmentItemBuilder WithPublicationDate(
			DateTime publicationDate);

		WPAttachmentItemBuilder WithAuthor(
			WPAuthor author);

		WPAttachmentItemBuilder WithPostDate(
			DateTime postDate);

		WPAttachmentItemBuilder WithPostDateGmt(
			DateTime postDateGmt);
		
		WPAttachmentItemBuilder WithPostContent(
			string postContent);

		WPAttachmentItemBuilder WithPostExcerpt(
			string postExcerpt);
	}
}