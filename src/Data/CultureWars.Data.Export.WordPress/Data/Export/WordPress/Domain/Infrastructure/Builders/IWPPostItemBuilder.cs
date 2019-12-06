using System;
using System.Collections.Generic;
using CultureWars.Data.Export.WordPress.Domain.ValueEnums;

namespace CultureWars.Data.Export.WordPress.Domain.Infrastructure.Builders
{
	internal interface IWPPostItemBuilder
	{
		WPPostItemBuilder WithPostID(
			int postID);

		WPPostItemBuilder WithPostName(
			string postName);

		WPPostItemBuilder WithPostLink(
			string postLink);

		WPPostItemBuilder WithPostTitle(
			string postName);

		WPPostItemBuilder WithPostStatus(
			WPStatus postStatus);

		WPPostItemBuilder WithPublicationDate(
			DateTime publicationDate);

		WPPostItemBuilder WithAuthor(
			WPAuthor author);

		WPPostItemBuilder WithPostDate(
			DateTime postDate);

		WPPostItemBuilder WithPostDateGmt(
			DateTime postDateGmt);
		
		WPPostItemBuilder WithCategories(
			IEnumerable<WPCategory> categories);

		WPPostItemBuilder WithTerms(
			IEnumerable<WPTerm> terms);

		WPPostItemBuilder WithPostComments(
			IEnumerable<WPPostComment> postComments);

		WPPostItemBuilder WithPostContent(
			string postContent);

		WPPostItemBuilder WithPostExcerpt(
			string postExcerpt);
		
		WPPostItemBuilder WithPostThumbnailId(
			int thumbnailId);
	}
}