using System;
using System.Collections.Generic;
using Ccr.Std.Core.Extensions;
using CultureWars.Data.Export.WordPress.Domain.ValueEnums;

namespace CultureWars.Data.Export.WordPress.Domain.Infrastructure.Builders
{
	public static class WPPostItemBuilderExtensions
	{
		public static WPPostItemBuilder WithPostID(
			this WPPostItemBuilder @this, 
			int postID)
		{
			return @this.As<IWPPostItemBuilder>()
			            .WithPostID(postID);
		}

		public static WPPostItemBuilder WithPostName(
			this WPPostItemBuilder @this,
			string postName)
		{
			return @this.As<IWPPostItemBuilder>()
			            .WithPostName(postName);
		}

		public static WPPostItemBuilder WithPostLink(
			this WPPostItemBuilder @this,
			string postLink)
		{
			return @this.As<IWPPostItemBuilder>()
			            .WithPostLink(postLink);
		}

		public static WPPostItemBuilder WithPostTitle(
			this WPPostItemBuilder @this,
			string postName)
		{
			return @this.As<IWPPostItemBuilder>()
			            .WithPostTitle(postName);
		}

		public static WPPostItemBuilder WithPostStatus(
			this WPPostItemBuilder @this,
			WPStatus postStatus)
		{
			return @this.As<IWPPostItemBuilder>()
			            .WithPostStatus(postStatus);
		}

		public static WPPostItemBuilder WithPublicationDate(
			this WPPostItemBuilder @this,
			DateTime publicationDate)
		{
			return @this.As<IWPPostItemBuilder>()
			            .WithPublicationDate(publicationDate);
		}

		public static WPPostItemBuilder WithAuthor(
			this WPPostItemBuilder @this,
			WPAuthor author)
		{
			return @this.As<IWPPostItemBuilder>()
			            .WithAuthor(author);
		}

		public static WPPostItemBuilder WithPostDate(
			this WPPostItemBuilder @this,
			DateTime postDate)
		{
			return @this.As<IWPPostItemBuilder>()
			            .WithPostDate(postDate);
		}

		public static WPPostItemBuilder WithPostDateGmt(
			this WPPostItemBuilder @this,
			DateTime postDateGmt)
		{
			return @this.As<IWPPostItemBuilder>()
			            .WithPostDateGmt(postDateGmt);
		}

		public static WPPostItemBuilder WithCategories(
			this WPPostItemBuilder @this,
			params WPCategory[] categories)
		{
			return @this.As<IWPPostItemBuilder>()
			            .WithCategories(categories);
		}

		public static WPPostItemBuilder WithTerms(
			this WPPostItemBuilder @this,
			params WPTerm[] terms)
		{
			return @this.As<IWPPostItemBuilder>()
			            .WithTerms(terms);
		}

		public static WPPostItemBuilder WithPostComments(
			this WPPostItemBuilder @this,
			IEnumerable<WPPostComment> postComments)
		{
			return @this.As<IWPPostItemBuilder>()
			            .WithPostComments(postComments);
		}

		public static WPPostItemBuilder WithPostContent(
			this WPPostItemBuilder @this,
			string postContent)
		{
			return @this.As<IWPPostItemBuilder>()
			            .WithPostContent(postContent);
		}

		public static WPPostItemBuilder WithPostExcerpt(
			this WPPostItemBuilder @this,
			string postExcerpt)
		{
			return @this.As<IWPPostItemBuilder>()
			            .WithPostExcerpt(postExcerpt);
		}

		public static WPPostItemBuilder WithPostThumbnailId(
			this WPPostItemBuilder @this,
			int thumbnailId)
		{
			return @this.As<IWPPostItemBuilder>()
			            .WithPostThumbnailId(thumbnailId);
		}
	}
}