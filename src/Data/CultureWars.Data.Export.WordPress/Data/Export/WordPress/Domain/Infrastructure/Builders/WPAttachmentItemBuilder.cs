using System;
using Ccr.Std.Core.Extensions;
using CultureWars.Core.Builders;
using JetBrains.Annotations;

namespace CultureWars.Data.Export.WordPress.Domain.Infrastructure.Builders
{
	public class WPAttachmentItemBuilder
		: IWPAttachmentItemBuilder, 
			IFluentBuilder<WPAttachmentItem>
	{
		[CanBeNull] private int? _postID;
		[CanBeNull] private string _postName;
		[CanBeNull] private string _postTitle;
		[CanBeNull] private string _postLink;
		[CanBeNull] private string _attachmentUrl;
		[CanBeNull] private string _status;
		[CanBeNull] private WPAuthor _author;
		[CanBeNull] private DateTime? _publicationDate;
		[CanBeNull] private DateTime? _postDate;
		[CanBeNull] private DateTime? _postDateGMT;
		[CanBeNull] private string _postContent;
		[CanBeNull] private string _postExcerpt;


		public WPAttachmentItem Build()
		{
			if (!_postID.HasValue)
				throw new NullReferenceException(
					$"The {nameof(_postID).SQuote()} fluent property is not configured.");

			if (_postName == null)
				throw new NullReferenceException(
					$"The {nameof(_postName).SQuote()} fluent property is not configured.");

			if (_postTitle == null)
				throw new NullReferenceException(
					$"The {nameof(_postTitle).SQuote()} fluent property is not configured.");

			if (_postLink == null)
				throw new NullReferenceException(
					$"The {nameof(_postLink).SQuote()} fluent property is not configured.");

			if (_status == null)
				throw new NullReferenceException(
					$"The {nameof(_status).SQuote()} fluent property is not configured.");

			if (_author == null)
				throw new NullReferenceException(
					$"The {nameof(_author).SQuote()} fluent property is not configured.");

			if (!_publicationDate.HasValue)
				throw new NullReferenceException(
					$"The {nameof(_publicationDate).SQuote()} fluent property is not configured.");

			if (!_postDate.HasValue)
				throw new NullReferenceException(
					$"The {nameof(_postDate).SQuote()} fluent property is not configured.");

			if (!_postDateGMT.HasValue)
				throw new NullReferenceException(
					$"The {nameof(_postDateGMT).SQuote()} fluent property is not configured.");

			if (_attachmentUrl == null)
				throw new NullReferenceException(
					$"The {nameof(_attachmentUrl).SQuote()} fluent property is not configured.");

			if (_postContent == null)
				throw new NullReferenceException(
					$"The {nameof(_postContent).SQuote()} fluent property is not configured.");

			if (_postExcerpt == null)
				throw new NullReferenceException(
					$"The {nameof(_postExcerpt).SQuote()} fluent property is not configured.");
			

			return new WPAttachmentItem(
				_postID.Value,
				_postName,
				_postTitle,
				_postLink,
				_attachmentUrl,
				_status,
				_author,
				_publicationDate.Value,
				_postDate.Value,
				_postDateGMT.Value,
				_postContent,
				_postExcerpt);
		}


		/// <inheritdoc />
		WPAttachmentItemBuilder IWPAttachmentItemBuilder.WithPostID(
			int postID)
		{
			_postID = postID;
			return this;
		}

		/// <inheritdoc />
		WPAttachmentItemBuilder IWPAttachmentItemBuilder.WithPostName(
			string postName)
		{
			_postName = postName;
			return this;
		}

		/// <inheritdoc />
		WPAttachmentItemBuilder IWPAttachmentItemBuilder.WithPostTitle(
			string postTitle)
		{
			_postTitle = postTitle;
			return this;
		}

		/// <inheritdoc />
		WPAttachmentItemBuilder IWPAttachmentItemBuilder.WithPostLink(
			string postLink)
		{
			_postLink = postLink;
			return this;
		}

		/// <inheritdoc />
		WPAttachmentItemBuilder IWPAttachmentItemBuilder.WithAttachmentUrl(
			string attachmentUrl)
		{
			_attachmentUrl = attachmentUrl;
			return this;
		}

		/// <inheritdoc />
		WPAttachmentItemBuilder IWPAttachmentItemBuilder.WithStatus(
			string status)
		{
			_status = status;
			return this;
		}

		/// <inheritdoc />
		WPAttachmentItemBuilder IWPAttachmentItemBuilder.WithPublicationDate(
			DateTime publicationDate)
		{
			_publicationDate = publicationDate;
			return this;
		}

		/// <inheritdoc />
		WPAttachmentItemBuilder IWPAttachmentItemBuilder.WithAuthor(
			WPAuthor author)
		{
			_author = author;
			return this;
		}

		/// <inheritdoc />
		WPAttachmentItemBuilder IWPAttachmentItemBuilder.WithPostDate(
			DateTime postDate)
		{
			_postDate = postDate;
			return this;
		}

		/// <inheritdoc />
		WPAttachmentItemBuilder IWPAttachmentItemBuilder.WithPostDateGmt(
			DateTime postDateGmt)
		{
			_postDateGMT = postDateGmt;
			return this;
		}
		
		/// <inheritdoc />
		WPAttachmentItemBuilder IWPAttachmentItemBuilder.WithPostContent(
			string postContent)
		{
			_postContent = postContent;
			return this;
		}

		/// <inheritdoc />
		WPAttachmentItemBuilder IWPAttachmentItemBuilder.WithPostExcerpt(
			string postExcerpt)
		{
			_postExcerpt = postExcerpt;
			return this;
		}
	}
}