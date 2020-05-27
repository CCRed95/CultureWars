using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using AngleSharp;
using Ccr.Algorithms;
using Ccr.Std.Core.Collections;
using Ccr.Std.Core.Extensions;
using CultureWars.API.CultureWars.Domain;

namespace CultureWars.API.CultureWars.Interpreters
{
	public class CWMInterpreter
	{
		private const string _domain = "https://culturewars.com/";
		private static readonly string _archiveDomain = $"{_domain}archive/";

		private static readonly Regex _volumeLinkTextRegex = new Regex(
			@"(?<year>[0-9]*) - Vol. (?<volumeNumber>[0-9]*)");

		private static readonly Regex _issueLinkTextRegex = new Regex(
			@"volumes-[0-9]*-[0-9]*/(?<magazine>f|fidelity|cw)-(?<volumeNumber>[0-9]*)-(?<issueNumber>[0-9]*)");



		public static IEnumerable<CWMVolume> ScrapeVolumes()
		{
			var context = BrowsingContext.New(
				Configuration.Default.WithDefaultLoader());

			var volumesListPageUrl = _archiveDomain;

			using (var document = context
				.OpenAsync(volumesListPageUrl)
				.GetAwaiter()
				.GetResult())
			{
				var content = document
					.GetElementById("canvas-wrapper")
					.QuerySelector("div#canvas")
					.QuerySelector("div#page-body-wrapper")
					.QuerySelector("div#page-body")
					.QuerySelector("div#content-wrapper")
					.QuerySelector("div#content");

				var mainContentDiv = content
					.Children[1];

				var mainContentRow = mainContentDiv
					.QuerySelector("div.sqs-layout")
					.QuerySelector("div.row.sqs-row");

				var unorderedLists = mainContentRow
					.QuerySelectorAll(
						"ul.archive-group-list")
					.ToArray();

				foreach (var unorderedList in unorderedLists)
				{
					var volumeListItemArchiveGroupEntries = unorderedList
						.QuerySelectorAll(
							"li.archive-group")
						.ToArray();

					foreach (var volumeListItemArchiveGroupEntry in volumeListItemArchiveGroupEntries)
					{
						var volumeLinkElement = volumeListItemArchiveGroupEntry
							.QuerySelector("a.archive-group-name-link");
						                                                                                                     
						var hrefRelativeVolumeLink = volumeLinkElement.GetAttribute("href");

						var hrefVolumeLinkRawText = volumeLinkElement.TextContent;

						var formattedVolumeText = hrefVolumeLinkRawText.Trim();
						var volumeTextMatch = _volumeLinkTextRegex.Match(formattedVolumeText);

						var volumeYearMatchText = volumeTextMatch.Groups["year"].Value;
						var volumeNumberMatchText = volumeTextMatch.Groups["volumeNumber"].Value;

						if (!int.TryParse(volumeYearMatchText, out var volumeYear))
							throw new FormatException(
								$"Cannot parse the 'year' from the text {volumeYearMatchText.Quote()}.");

						if (!int.TryParse(volumeNumberMatchText, out var volumeNumber))
							throw new FormatException(
								$"Cannot parse the 'volumeNumber' from the text {volumeNumberMatchText.Quote()}.");

						var volumePageAbsoluteUrl = $"{_domain.TrimEnd('/')}{hrefRelativeVolumeLink}";

						yield return new CWMVolume(
							volumeYear,
							volumeNumber,
							volumePageAbsoluteUrl);
					}
				}
			}
		}

		public static IEnumerable<CWMIssue> ScrapeVolumeIssues(
			CWMVolume cwmVolume)
		{
			var context = BrowsingContext.New(
				Configuration.Default.WithDefaultLoader());

			var downloadPageUrl = cwmVolume.VolumePageAbsoluteUrl;

			using (var document = context
				.OpenAsync(downloadPageUrl)
				.GetAwaiter()
				.GetResult())
			{
				var canvasWrapper = document
					.GetElementById("canvas-wrapper");

				var contentDiv = canvasWrapper
					.QuerySelector(
						"div#canvas > " +
						"div#page-body-wrapper > " +
						"div#page-body > " +
						"div#content-wrapper > " +
						"div#content");

				var mainContentDiv = contentDiv.Children[2];

				var mainProductList = mainContentDiv
					.QuerySelector("div#productList");

				var issueLinkElements = mainProductList
					.QuerySelectorAll(
						"a.product")
					.ToArray();

				foreach (var issueLinkElement in issueLinkElements)
				{
					var hrefRelativeIssueLink = issueLinkElement.GetAttribute("href");
					var formattedIssueText = hrefRelativeIssueLink.Trim();

					if (!_issueLinkTextRegex.IsMatch(formattedIssueText))
					{
						//Console.WriteLine($"Cannot parse IssueLinkText {formattedIssueText.Quote()}");
						continue;
					}
					var issueTextMatch = _issueLinkTextRegex.Match(formattedIssueText);

					var issueMagazineText = issueTextMatch.Groups["magazine"].Value;
					var volumeNumberMatchText = issueTextMatch.Groups["volumeNumber"].Value;
					var issueNumberMatchText = issueTextMatch.Groups["issueNumber"].Value;

					if (issueNumberMatchText.IsNullOrEmptyEx())
					{
						//Console.WriteLine($"Cannot parse IssueLinkText regex {formattedIssueText.Quote()}");
						continue;
					}
					var issueMagazine = Magazine.GetMagazineFromPrefix(issueMagazineText);

					if (!int.TryParse(volumeNumberMatchText, out var volumeNumber))
						throw new FormatException(
							$"Cannot parse the 'volumeNumber' from the text {volumeNumberMatchText.Quote()}.");

					if (!int.TryParse(issueNumberMatchText, out var issueNumber))
						throw new FormatException(
							$"Cannot parse the 'issueNumber' from the text {issueNumberMatchText.Quote()}.");


					var issuePageAbsoluteUrl = $"{_domain.TrimEnd('/')}{hrefRelativeIssueLink}";

					yield return new CWMIssue(
						volumeNumber,
						issueNumber,
						issueMagazine,
						issuePageAbsoluteUrl,
						cwmVolume);
				}
			}
		}

		public static IEnumerable<CWMArticle> ScrapeIssueArticles(
			CWMIssue cwmIssue)
		{
			var context = BrowsingContext.New(
				Configuration.Default.WithDefaultLoader());

			var downloadPageUrl = cwmIssue.IssuePageAbsoluteUrl;

			using (var document = context
				.OpenAsync(downloadPageUrl)
				.GetAwaiter()
				.GetResult())
			{
				var canvasWrapper = document
					.GetElementById("canvas-wrapper");

				var contentDiv = canvasWrapper
					.QuerySelector(
						"div#canvas > " +
						"div#page-body-wrapper > " +
						"div#page-body > " +
						"div#content-wrapper > " +
						"div#content");
				
				var mainContentDiv = contentDiv.Children[2]; 

				var productBlockContentElement = mainContentDiv
					.QuerySelector(
						"div#productWrapper > " +
						"div.product-description > " +
						"div.sqs-layout > " +
						"div.row.sqs-row > " +
						"div.col > " +
						"div.sqs-block.html-block > " +
						"div.sqs-block-content");

				var currentArticleCategory = "Unknown";

				var magazineSections = ValueEnum
					.EnumerateValues<MagazineSection, string>()
					.ToArray();

				foreach (var productBlockElement in productBlockContentElement.Children)
				{
					var articeInfoStr = productBlockElement
						.TextContent
						.Replace("&nbsp;", "")
						.Trim();

					var isMagazineSection = magazineSections
						.Contains(
							articeInfoStr, 
							new FuzzyStringMatchingComparer(2));

					if (isMagazineSection)
					{
						currentArticleCategory = articeInfoStr;
						continue;
					}

					var splitTerms = articeInfoStr.Split('-');

					if (splitTerms.Length == 2)
					{
						var articleName = splitTerms[0].Trim();
						var articleAuthor = splitTerms[1].Trim();

						yield return new CWMArticle(
							currentArticleCategory,
							articleName,
							articleAuthor,
							cwmIssue);
					}
					else
					{
						var articleName = articeInfoStr.Trim();

						yield return new CWMArticle(
							currentArticleCategory,
							articleName,
							"unknown",
							cwmIssue);
					}
				}
			}
		}
	}

	public class FuzzyStringMatchingComparer
		: IEqualityComparer<string>
	{
		public int DifferenceThreshold { get; }


		public FuzzyStringMatchingComparer(
			int differentThreshold)
		{
			DifferenceThreshold = differentThreshold;
		}


		/// <inheritdoc/>
		public bool Equals(string x, string y)
		{
			var difference = StringAlgorithms.LevenshteinDistance(x, y);
			return difference <= DifferenceThreshold;
		}

		/// <inheritdoc/>
		public int GetHashCode(string obj)
		{
			return base.GetHashCode() * DifferenceThreshold;
		}
	}
}


//if (productBlockElement.LocalName == "h1")
//{
//}
//if (productBlockElement.LocalName == "h2")
//{
//	var strongElement = productBlockElement.QuerySelector("strong");
//	var categoryName = strongElement
//		.TextContent
//		.Replace("&nbsp;", "")
//		.Trim();

//	currentArticleCategory = categoryName;
//}
//else if (productBlockElement.LocalName == "p")
//{
//	var articeInfoStr = productBlockElement
//		.TextContent
//		.Replace("&nbsp;", "")
//		.Trim();

//	var splitTerms = articeInfoStr.Split('-');

//	if (splitTerms.Length == 2)
//	{
//		var articleName = splitTerms[0];
//		var articleAuthor = splitTerms[0];

//		yield return new CWMArticle(
//			currentArticleCategory,
//			articleName,
//			articleAuthor,
//			cwmIssue);
//	}
//	else
//	{
//		throw new FormatException(
//			$"Cannot process text {articeInfoStr.Quote()} because there is not 1 hyphen.");
//	}
//}
//else
//{
//	throw new FormatException(
//		$"Cannot process article element '{productBlockElement}'.");
//}