using System;
using Ccr.Std.Core.Extensions;

namespace CultureWars.Data.Export.WordPress.Domain.Infrastructure.Builders
{
	public static class WPAttachmentItemBuilderExtensions
	{
		public static WPAttachmentItemBuilder WithPostID(
			this WPAttachmentItemBuilder @this,
			int postID)
		{
			return @this.As<IWPAttachmentItemBuilder>()
			            .WithPostID(postID);
		}

		public static WPAttachmentItemBuilder WithPostName(
			this WPAttachmentItemBuilder @this,
			string postName)
		{
			return @this.As<IWPAttachmentItemBuilder>()
			            .WithPostName(postName);
		}

		public static WPAttachmentItemBuilder WithPostLink(
			this WPAttachmentItemBuilder @this,
			string postLink)
		{
			return @this.As<IWPAttachmentItemBuilder>()
			            .WithPostLink(postLink);
		}

		public static WPAttachmentItemBuilder WithPostTitle(
			this WPAttachmentItemBuilder @this,
			string postName)
		{
			return @this.As<IWPAttachmentItemBuilder>()
			            .WithPostTitle(postName);
		}

		public static WPAttachmentItemBuilder WithAttachmentUrl(
			this WPAttachmentItemBuilder @this,
			string attachmentUrl)
		{
			return @this.As<IWPAttachmentItemBuilder>()
			            .WithAttachmentUrl(attachmentUrl);
		}

		public static WPAttachmentItemBuilder WithStatus(
			this WPAttachmentItemBuilder @this,
			string status)
		{
			return @this.As<IWPAttachmentItemBuilder>()
			            .WithStatus(status);
		}

		public static WPAttachmentItemBuilder WithPublicationDate(
			this WPAttachmentItemBuilder @this,
			DateTime publicationDate)
		{
			return @this.As<IWPAttachmentItemBuilder>()
			            .WithPublicationDate(publicationDate);
		}

		public static WPAttachmentItemBuilder WithAuthor(
			this WPAttachmentItemBuilder @this,
			WPAuthor author)
		{
			return @this.As<IWPAttachmentItemBuilder>()
			            .WithAuthor(author);
		}

		public static WPAttachmentItemBuilder WithPostDate(
			this WPAttachmentItemBuilder @this,
			DateTime postDate)
		{
			return @this.As<IWPAttachmentItemBuilder>()
			            .WithPostDate(postDate);
		}

		public static WPAttachmentItemBuilder WithPostDateGmt(
			this WPAttachmentItemBuilder @this,
			DateTime postDateGmt)
		{
			return @this.As<IWPAttachmentItemBuilder>()
			            .WithPostDateGmt(postDateGmt);
		}
		
		public static WPAttachmentItemBuilder WithPostContent(
			this WPAttachmentItemBuilder @this,
			string postContent)
		{
			return @this.As<IWPAttachmentItemBuilder>()
			            .WithPostContent(postContent);
		}

		public static WPAttachmentItemBuilder WithPostExcerpt(
			this WPAttachmentItemBuilder @this,
			string postExcerpt)
		{
			return @this.As<IWPAttachmentItemBuilder>()
			            .WithPostExcerpt(postExcerpt);
		}
	}
}