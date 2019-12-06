using System;
using System.Collections.Generic;
using Ccr.Std.Core.Extensions;
using CultureWars.Core.Builders;
using JetBrains.Annotations;

namespace CultureWars.Data.Export.WordPress.Domain.Infrastructure.Builders
{
	public class WPChannelDocumentBuilder
		: IWPChannelDocumentBuilder,
			IFluentBuilder<WPChannelDocument>
	{
		[CanBeNull] private string _channelTitle;
		[CanBeNull] private string _channelLink;
		[CanBeNull] private DateTime? _publicationDate;
		[CanBeNull] private string _channelDescription;
		[CanBeNull] private string _postTitle;
		[CanBeNull] private string _languageCode;
		[CanBeNull] private double? _xmlVersion;
		[CanBeNull] private double? _wxrVersion;
		[CanBeNull] private IEnumerable<WPAuthor> _authors;
		[CanBeNull] private IEnumerable<WPTerm> _terms;
		[CanBeNull] private IEnumerable<WPCategory> _categories;
		[CanBeNull] private IEnumerable<WPPostItem> _posts;
		[CanBeNull] private IEnumerable<WPAttachmentItem> _attachments;


		/// <inheritdoc />
		public WPChannelDocument Build()
		{
			if (_channelTitle == null)
				throw new NullReferenceException(
					$"The {nameof(_channelTitle).SQuote()} fluent property is not configured.");

			if (_channelLink == null)
				throw new NullReferenceException(
					$"The {nameof(_channelLink).SQuote()} fluent property is not configured.");

			if (!_publicationDate.HasValue)
				throw new NullReferenceException(
					$"The {nameof(_publicationDate).SQuote()} fluent property is not configured.");

			if (_channelDescription == null)
				throw new NullReferenceException(
					$"The {nameof(_channelDescription).SQuote()} fluent property is not configured.");

			if (_postTitle == null)
				throw new NullReferenceException(
					$"The {nameof(_postTitle).SQuote()} fluent property is not configured.");

			if (_languageCode == null)
				throw new NullReferenceException(
					$"The {nameof(_languageCode).SQuote()} fluent property is not configured.");

			if (!_xmlVersion.HasValue)
				throw new NullReferenceException(
					$"The {nameof(_xmlVersion).SQuote()} fluent property is not configured.");

			if (!_wxrVersion.HasValue)
				throw new NullReferenceException(
					$"The {nameof(_wxrVersion).SQuote()} fluent property is not configured.");

			if (_authors == null)
				throw new NullReferenceException(
					$"The {nameof(_authors).SQuote()} fluent property is not configured.");

			if (_terms == null)
				throw new NullReferenceException(
					$"The {nameof(_terms).SQuote()} fluent property is not configured.");

			if (_categories == null)
				throw new NullReferenceException(
					$"The {nameof(_categories).SQuote()} fluent property is not configured.");

			if (_posts == null)
				throw new NullReferenceException(
					$"The {nameof(_posts).SQuote()} fluent property is not configured.");

			if (_attachments == null)
				throw new NullReferenceException(
					$"The {nameof(_attachments).SQuote()} fluent property is not configured.");


			return new WPChannelDocument(
				_channelTitle,
				_channelLink,
				_publicationDate.Value,
				_channelDescription,
				_languageCode,
				_xmlVersion.Value,
				_wxrVersion.Value,
				_authors,
				_terms,
				_categories,
				_posts,
				_attachments);
		}


		/// <inheritdoc />
		WPChannelDocumentBuilder IWPChannelDocumentBuilder.WithChannelTitle(
			string channelTitle)
		{
			_channelTitle = channelTitle;
			return this;
		}

		/// <inheritdoc />
		WPChannelDocumentBuilder IWPChannelDocumentBuilder.WithChannelLink(
			string channelLink)
		{
			_channelLink = channelLink;
			return this;
		}

		/// <inheritdoc />
		WPChannelDocumentBuilder IWPChannelDocumentBuilder.WithPublicationDate(
			DateTime publicationDate)
		{
			_publicationDate = publicationDate;
			return this;
		}

		/// <inheritdoc />
		WPChannelDocumentBuilder IWPChannelDocumentBuilder.WithChannelDescription(
			string channelDescription)
		{
			_channelDescription = channelDescription;
			return this;
		}

		/// <inheritdoc />
		WPChannelDocumentBuilder IWPChannelDocumentBuilder.WithPostTitle(
			string postTitle)
		{
			_postTitle = postTitle;
			return this;
		}

		/// <inheritdoc />
		WPChannelDocumentBuilder IWPChannelDocumentBuilder.WithLanguageCode(
			string languageCode)
		{
			_languageCode = languageCode;
			return this;
		}

		/// <inheritdoc />
		WPChannelDocumentBuilder IWPChannelDocumentBuilder.WithXmlVersion(
			double xmlVersion)
		{
			_xmlVersion = xmlVersion;
			return this;
		}

		/// <inheritdoc />
		WPChannelDocumentBuilder IWPChannelDocumentBuilder.WithWXRVersion(
			double wxrVersion)
		{
			_wxrVersion = wxrVersion;
			return this;
		}

		/// <inheritdoc />
		WPChannelDocumentBuilder IWPChannelDocumentBuilder.WithAuthors(
			params WPAuthor[] authors)
		{
			_authors = authors;
			return this;
		}

		/// <inheritdoc />
		WPChannelDocumentBuilder IWPChannelDocumentBuilder.WithTerms(
			params WPTerm[] terms)
		{
			_terms = terms;
			return this;
		}

		/// <inheritdoc />
		WPChannelDocumentBuilder IWPChannelDocumentBuilder.WithCategories(
			params WPCategory[] categories)
		{
			_categories = categories;
			return this;
		}

		/// <inheritdoc />
		WPChannelDocumentBuilder IWPChannelDocumentBuilder.WithPosts(
			params WPPostItem[] posts)
		{
			_posts = posts;
			return this;
		}

		/// <inheritdoc />
		WPChannelDocumentBuilder IWPChannelDocumentBuilder.WithAttachments(
			params WPAttachmentItem[] attachments)
		{
			_attachments = attachments;
			return this;
		}
	}
}