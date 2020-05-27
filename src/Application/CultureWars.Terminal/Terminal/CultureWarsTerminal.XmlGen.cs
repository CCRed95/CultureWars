using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Ccr.Std.Core.Extensions;
using CultureWars.API.GoogleArchives.JsonParsing;
using CultureWars.API.InternetArchive;
using CultureWars.API.InternetArchive.Domain;
using CultureWars.API.InternetArchive.Query;
using CultureWars.API.Web;
using CultureWars.Data.Export.WordPress;
using CultureWars.Data.Export.WordPress.Domain;
using CultureWars.Data.Export.WordPress.Domain.Infrastructure.Builders;
using CultureWars.Data.Export.WordPress.Domain.ValueEnums;
using CultureWars.Data.Export.WordPress.XmlWriter;
using CultureWars.Terminal.Utilities;

namespace CultureWars.Terminal
{
	public partial class CultureWarsTerminal
	{
		private const string _excerptNs = "http://wordpress.org/export/1.2/excerpt/";
		private const string _contentNs = "http://purl.org/rss/1.0/modules/content/";
		private const string _wfwNs = "http://wellformedweb.org/CommentAPI/";
		private const string _dcNs = "http://purl.org/dc/elements/1.1/";
		private const string _wpNs = "http://wordpress.org/export/1.2/";

		private static readonly FXNamespace excerptNs = ("excerpt", _excerptNs);
		private static readonly FXNamespace contentNs = ("content", _contentNs);
		private static readonly FXNamespace wfwNs = ("wfw", _wfwNs);
		private static readonly FXNamespace dcNs = ("dc", _dcNs);
		private static readonly FXNamespace wpNs = ("wp", _wpNs);

		private const double _xmlVersion = 1.0;
		private const double _wxrVersion = 1.2;



		public static void XmlGen()
		{
			var outputFilePath = new DirectoryInfo(
					Environment.GetFolderPath(Environment.SpecialFolder.Desktop)).FullName +
				$@"\XmlGenTest6.xml";

			var outputFileInfo = new FileInfo(outputFilePath);

			using var consoleStreamWriter = new ConsoleStreamWriter(
				outputFileInfo.FullName, false);

			var writer = new XmlStreamWriter(consoleStreamWriter);
			writer.WithDeclaration(FXDeclaration.Get(_xmlVersion, "utf-8"))
				.WithNamespace(excerptNs)
				.WithNamespace(contentNs)
				.WithNamespace(wfwNs)
				.WithNamespace(dcNs)
				.WithNamespace(wpNs);

			writer.WriteStartElement("rss")
				.WriteStartElement("channel")
				.WriteInlineElement(wpNs, "wxr_version", $"{_wxrVersion:0.0}");
			
			foreach (var tag in WPTerm.AllTags)
			{
				tag.WriteToXmlStream(writer);
			}

			foreach (var category in WPCategory.AllCategories)
			{
				category.WriteToXmlStream(writer);
			}

			foreach (var category in WPCategory.AllCategories)
			{
				writer.WriteStartElement(wpNs, "category")
					.WriteInlineCDataElement(wpNs, "cat_name", category.CategoryName)
					.WriteInlineCDataElement(wpNs, "category_nicename", category.CategoryNiceName)
					.WriteInlineElement(wpNs, "category_parent", "0")
					.WriteEndElement();
			}

			writer.WriteEndElement()
				.WriteEndElement();
		}

		public static IEnumerable<WPPostItem> GetWPPosts()
		{
			Console.WriteLine($"Generating WordPress Posts...");
			Console.WriteLine();

			var archiveApi = new InternetArchiveAPI();
			var queryBuilder = InternetArchiveQueryBuilder
				.Builder
				.WithUploader("Dr. E. Michael Jones")
				.WithSort(
					IAQueryFields.Title,
					IASortDirection.Ascending)
				.WithFields(
					IAQueryFields.Creator,
					IAQueryFields.Date,
					IAQueryFields.Description,
					IAQueryFields.Identifier,
					IAQueryFields.MediaType,
					IAQueryFields.Title)
				.WithRows(5000)
				.WithOutputKind(APIDataOutputKind.JSON)
				.WithCallback("callback")
				.WithShouldSave(true);

			//var addedTags = new List<WPTerm>();
			//addedTags.AddRange(WPTerm.AllTags);

			//foreach (var cwTag in WPTerm.AllTags)
			//{
			//	var wpTerm = new Term(
			//		(ulong)cwTag.CultureWarsTagID,
			//		cwTag.TagFriendlyName,
			//		cwTag.HtmlEncodedTagName);

			//	Console.WriteLine($"  <term ID=\"{cwTag.CultureWarsTagID}\" Name={cwTag.TagName.Quote()} FriendlyName={cwTag.TagFriendlyName.Quote()} HtmlEncoded={cwTag.HtmlEncodedTagName.Quote()}\\>");

			//	//	context.Terms.Add(wpTerm);
			//}

			//var index = 87ul;
			var thumbnailIndex = 1;
			foreach (var archiveItem in archiveApi.Query(queryBuilder))
			{
				foreach (var file in archiveItem.GetItemFiles())
				{
					if (!file.FileName.EndsWith(".mp4"))
						continue;

					var uploadIndex = archiveItem.Identifier.Replace("emj-archive-", "");

					var decodedFileName = file.FileName.UrlDecode();

					var localJsonFilePath = JsonMetadataFileAssociator
						.GetAssociatedJsonFile(uploadIndex, file.FileName, out var matchedDistance);

					var jsonResultMetadata = JsonYouTubeMetadataParser
						.ParseJsonYouTubeMetadata(localJsonFilePath);

					var flattenedMetadata = new YouTubeVideoFlattenedMetadata(jsonResultMetadata);
					var encodedFileNameCharArray = file
						.Title
						.ToLower()
						.Replace(" ", "-")
						.Where(t => t.IsLetterOrDigit() || t == '-')
						.ToArray();

					var videoThumbnail =
						AlternativeThumbnailAssociator.GetPrimaryAssociatedThumbnailFileUrl(file);

					var thumbnailUrl = videoThumbnail;

					var encodedFileName = new string(encodedFileNameCharArray);

					var linkPath = $@"/videos/{encodedFileName}";

					var terms = new List<WPTerm>();

					if (flattenedMetadata.TagList != null)
					{
						foreach (var tag in flattenedMetadata.TagList)
						{
							var titleCase = tag.ToTitleCase();
							var term = WPTerm.FromFriendlyNameOrNull(titleCase);

							if (term != null)
								terms.Add(term);
							else
							{

							}
						}
					}

					var postItem = WPPostItem
						.Builder
						.WithPostID(thumbnailIndex)
						.WithPostName(encodedFileName)
						.WithPostTitle(flattenedMetadata.Title)
						.WithPostLink(linkPath)
						.WithPostStatus(WPStatus.Publish)
						.WithAuthor(WPAuthor.EMichaelJones)
						.WithPublicationDate(flattenedMetadata.VideoPublishedDate)
						.WithPostDate(flattenedMetadata.VideoPublishedDate)
						.WithPostDateGmt(flattenedMetadata.VideoPublishedDate)
						.WithCategories(
							WPCategory.CensoredVideos)
						.WithTerms(terms.ToArray())
						.WithPostComments(new List<WPPostComment>())
						.WithPostContent(
							$@"<iframe src=""https://archive.org/download/{archiveItem.Identifier}/{file.FileName}"" width=""640"" height=""480"" frameborder=""0"" webkitallowfullscreen=""true"" mozallowfullscreen=""true"" allowfullscreen=""""></iframe>")
						.WithPostExcerpt(flattenedMetadata.Description)
						.WithPostThumbnailId(thumbnailIndex + 1)
						.Build();

					var thumbnailItem = WPAttachmentItem
						.Builder
						.WithPostID(thumbnailIndex + 1)
						.WithPostName($"")
						.WithPostLink($"")
						.WithPostTitle($"")
						.WithAttachmentUrl($"{videoThumbnail}")
						.WithStatus("inherit")
						.WithAuthor(WPAuthor.EMichaelJones)
						.WithPublicationDate(flattenedMetadata.VideoPublishedDate)
						.WithPostDate(flattenedMetadata.VideoPublishedDate)
						.WithPostDateGmt(flattenedMetadata.VideoPublishedDate)
						.WithPostContent($"")
						.WithPostExcerpt($"")
						.Build();

					yield return postItem;
					thumbnailIndex += 2;



					//if (flattenedMetadata.TagList != null)
					//{
					//	foreach (var tagText in flattenedMetadata.TagList)
					//	{
					//		var titleCase = tagText.ToTitleCase();
					//		var existing = addedTags.Any(t => t.TagFriendlyName == titleCase);
					//		if (!existing)
					//		{
					//			var cwTag = new WPTerm((int)index, titleCase);

					//			var wpTerm = new Term(
					//				index,
					//				cwTag.TagFriendlyName,
					//				cwTag.HtmlEncodedTagName);

					//			context.Terms.Add(wpTerm);

					//			Console.WriteLine($"  <term ID=\"{cwTag.CultureWarsTagID}\" Name={cwTag.TagName.Quote()} FriendlyName={cwTag.TagFriendlyName.Quote()} HtmlEncoded={cwTag.HtmlEncodedTagName.Quote()}\\>");
					//			index++;
					//		}
					//	}
					//}
				}


				Console.WriteLine($"Generation complete.");
				//	context.SaveChanges();
				Console.WriteLine($"saved to sql");
			}
		}
	}
}
