using System;
using System.Collections.Generic;
using System.Linq;
using Ccr.Std.Core.Extensions;
using CultureWars.Data.Export.WordPress.Domain.ValueEnums;
using JetBrains.Annotations;

namespace CultureWars.Data.Export.WordPress.Domain.Infrastructure.Builders
{
	/*		[CanBeNull] private string _channelTitle;
		[CanBeNull] private string _channelLink;
		[CanBeNull] private DateTime? _publicationDate;
		[CanBeNull] private string _channelDescription;
		[CanBeNull] private string _languageCode;
		[CanBeNull] private double? _xmlVersion;
		[CanBeNull] private double? _wxrVersion;
		[CanBeNull] private IEnumerable<WPAuthor> _authors;
		[CanBeNull] private IEnumerable<WPTerm> _terms;
		[CanBeNull] private IEnumerable<WPCategory> _categories;
		[CanBeNull] private IEnumerable<WPPostItem> _posts;
		[CanBeNull] private IEnumerable<WPAttachmentItem> _attachments;*/


	public class WPPostItemBuilder
		: IWPPostItemBuilder
	{
		[CanBeNull] private int? _postID;
		[CanBeNull] private string _postName;
		[CanBeNull] private string _postTitle;
		[CanBeNull] private string _postLink;
		[CanBeNull] private WPStatus? _postStatus;
		[CanBeNull] private WPAuthor _author;
		[CanBeNull] private DateTime? _publicationDate;
		[CanBeNull] private DateTime? _postDate;
		[CanBeNull] private DateTime? _postDateGMT;
		[CanBeNull] private IReadOnlyList<WPCategory> _categories;
		[CanBeNull] private IReadOnlyList<WPTerm> _terms;
		[CanBeNull] private IReadOnlyList<WPPostComment> _postComments;
		[CanBeNull] private string _postContent;
		[CanBeNull] private string _postExcerpt;
		[CanBeNull] private int? _thumbnailId;


		public WPPostItem Build()
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

			if (!_postStatus.HasValue)
				throw new NullReferenceException(
					$"The {nameof(_postStatus).SQuote()} fluent property is not configured.");

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

			if (_categories == null)
				throw new NullReferenceException(
					$"The {nameof(_categories).SQuote()} fluent property is not configured.");

			if (_terms == null)
				throw new NullReferenceException(
					$"The {nameof(_terms).SQuote()} fluent property is not configured.");

			if (_postComments == null)
				throw new NullReferenceException(
					$"The {nameof(_postComments).SQuote()} fluent property is not configured.");

			if (_postContent == null)
				throw new NullReferenceException(
					$"The {nameof(_postContent).SQuote()} fluent property is not configured.");

			if (_postExcerpt == null)
				throw new NullReferenceException(
					$"The {nameof(_postExcerpt).SQuote()} fluent property is not configured.");

			if (!_thumbnailId.HasValue)
				throw new NullReferenceException(
					$"The {nameof(_thumbnailId).SQuote()} fluent property is not configured.");


			return new WPPostItem(
				_postID.Value,
				_postName,
				_postTitle,
				_postLink,
				_postStatus.Value,
				_author,
				_publicationDate.Value,
				_postDate.Value,
				_postDateGMT.Value,
				_categories,
				_terms,
				_postComments,
				_postContent,
				_postExcerpt,
				_thumbnailId.Value);
		}


		/// <inheritdoc />
		WPPostItemBuilder IWPPostItemBuilder.WithPostID(
			int postID)
		{
			_postID = postID;
			return this;
		}

		/// <inheritdoc />
		WPPostItemBuilder IWPPostItemBuilder.WithPostName(
			string postName)
		{
			_postName = postName;
			return this;
		}

		/// <inheritdoc />
		WPPostItemBuilder IWPPostItemBuilder.WithPostLink(
			string postLink)
		{
			_postLink = postLink;
			return this;
		}

		/// <inheritdoc />
		WPPostItemBuilder IWPPostItemBuilder.WithPostTitle(
			string postTitle)
		{
			_postTitle = postTitle;
			return this;
		}


		/// <inheritdoc />
		WPPostItemBuilder IWPPostItemBuilder.WithPostStatus(
			WPStatus postStatus)
		{
			_postStatus = postStatus;
			return this;
		}

		/// <inheritdoc />
		WPPostItemBuilder IWPPostItemBuilder.WithPublicationDate(
			DateTime publicationDate)
		{
			_publicationDate = publicationDate;
			return this;
		}

		/// <inheritdoc />
		WPPostItemBuilder IWPPostItemBuilder.WithAuthor(
			WPAuthor author)
		{
			_author = author;
			return this;
		}

		/// <inheritdoc />
		WPPostItemBuilder IWPPostItemBuilder.WithPostDate(
			DateTime postDate)
		{
			_postDate = postDate;
			return this;
		}

		/// <inheritdoc />
		WPPostItemBuilder IWPPostItemBuilder.WithPostDateGmt(
			DateTime postDateGmt)
		{
			_postDateGMT = postDateGmt;
			return this;
		}

		/// <inheritdoc />
		WPPostItemBuilder IWPPostItemBuilder.WithCategories(
			IEnumerable<WPCategory> categories)
		{
			_categories = categories.ToList();
			return this;
		}

		/// <inheritdoc />
		WPPostItemBuilder IWPPostItemBuilder.WithTerms(
			IEnumerable<WPTerm> terms)
		{
			_terms = terms.ToList();
			return this;
		}

		/// <inheritdoc />
		WPPostItemBuilder IWPPostItemBuilder.WithPostComments(
			IEnumerable<WPPostComment> postComments)
		{
			_postComments = postComments.ToList();
			return this;
		}

		/// <inheritdoc />
		WPPostItemBuilder IWPPostItemBuilder.WithPostContent(
			string postContent)
		{
			_postContent = postContent;
			return this;
		}

		/// <inheritdoc />
		WPPostItemBuilder IWPPostItemBuilder.WithPostExcerpt(
			string postExcerpt)
		{
			_postExcerpt = postExcerpt;
			return this;
		}

		/// <inheritdoc />
		WPPostItemBuilder IWPPostItemBuilder.WithPostThumbnailId(
			int thumbnailId)
		{
			_thumbnailId = thumbnailId;
			return this;
		}
	}
}