using Ccr.Std.Core.Extensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using CultureWars.API.CultureWars;
using CultureWars.API.CultureWars.Extensions;
using CultureWars.API.GoogleArchives.JsonParsing;
using CultureWars.API.InternetArchive;
using CultureWars.API.InternetArchive.Domain;
using CultureWars.API.InternetArchive.Query;
using CultureWars.API.Web;
using CultureWars.Data.Export.WordPress;
using CultureWars.Data.Export.WordPress.Domain;
using CultureWars.Data.Export.WordPress.XmlWriter;
using CultureWars.Terminal.Utilities;
using static CultureWars.Core.FluentConsole.ExtendedConsole;

namespace CultureWars.Terminal
{
	public partial class CultureWarsTerminal
	{
		public static void GenerateCSTagDeclarationsFromXml()
		{
			var addedTags = new List<WPTerm>();
			addedTags.AddRange(WPTerm.AllTags);

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

			var index = addedTags.Count;

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

					if (flattenedMetadata.TagList != null)
					{
						foreach (var tagText in flattenedMetadata.TagList)
						{
							var wpTerm = new WPTerm(index, tagText);

							var existing = addedTags.Any(t => t.TagFriendlyName == wpTerm.TagFriendlyName);
							var existingCsharpID = addedTags.Any(t => t.TagName == wpTerm.TagName);

							if (!existing && !existingCsharpID)
							{
								addedTags.Add(wpTerm);
								Console.WriteLine($"public static readonly WPTerm {wpTerm.TagName} = new WPTerm({wpTerm.WPTermID}, {wpTerm.TagName.Quote()}, {wpTerm.TagFriendlyName.Quote()}, {wpTerm.HtmlEncodedTagName.Quote()};");
								Console.WriteLine();
								//								Console.WriteLine($"  <term ID=\"{wpTag.CultureWarsTagID}\" Name={wpTag.TagName.Quote()} FriendlyName={wpTag.TagFriendlyName.Quote()} HtmlEncoded={wpTag.HtmlEncodedTagName.Quote()}\\>");
								index++;
							}
							else
							{
								Console.WriteLine($"//public static readonly WPTerm {wpTerm.TagName} = new WPTerm(-1, {wpTerm.TagName.Quote()}, {wpTerm.TagFriendlyName.Quote()}, {wpTerm.HtmlEncodedTagName.Quote()};");
								Console.WriteLine();
							}
						}
					}
				}
			}
		}
	}

	public partial class CultureWarsTerminal
	{
		public static void QueryCultureWarsArticles()
		{
			var volumes = CultureWarsAPI.QueryVolumes();

			//Console.WriteLine($"Querying CultureWars Articles");
			//onsole.WriteLine($"======================================");

			XConsole.BeginWriteXmlElement("CultureWarsArchive")
				.EndWriteComplexXmlElement();

			XConsole.Indent();

			foreach (var volume in volumes)
			{
				XConsole
					.BeginWriteXmlElement("Volume")
					.WriteXmlInlineParameter("VolumeNumber", volume.VolumeNumber.ToString())
					.WriteXmlInlineParameter("Year", volume.Year.ToString())
					.EndWriteComplexXmlElement();

				//Console.WriteLine($"<Volume Vol=\"{volume.VolumeNumber}\" Year=\"{volume.Year}\">");

				XConsole.Indent();

				foreach (var issue in volume.GetVolumeIssues())
				{
					XConsole
						.BeginWriteXmlElement("Issue")
						.WriteXmlInlineParameter("VolumeNumber", issue.VolumeNumber.ToString())
						.WriteXmlInlineParameter("IssueNumber", issue.IssueNumber.ToString())
						.WriteXmlInlineParameter("Url", issue.IssuePageAbsoluteUrl)
						.EndWriteComplexXmlElement();

					//Console.WriteLine($"  <Issue " +
					//	$"Vol=\"{issue.VolumeNumber}\" " +
					//	$"Issue=\"{issue.IssueNumber}\" " +
					//	$"VolumeNumber=\"{issue.VolumeNumber}\" " +
					//	$"Magazine={issue.Magazine.MagazineName.Quote()} " +
					//	$"IssuePageAbsoluteUrl={issue.IssuePageAbsoluteUrl.Quote()}>");

					XConsole.Indent();

					foreach (var article in issue.GetIssueArticles())
					{
						XConsole
							.BeginWriteXmlElement("Article")
							.WriteXmlInlineParameter(
								"Name", 
								article
									.ArticleName
									.Replace("&", "&amp;")
									.Replace("\"", ""))
							.WriteXmlInlineParameter("Author", article.AuthorName)
							.WriteXmlInlineParameter("Category", article.CategoryName)
							.EndWriteXmlElement();

						//Console.WriteLine($"    <Article " +
						//	$"Vol={article.ArticleName.Quote()} " +
						//	$"Author={article.AuthorName.Quote()} " +
						//	$"CategoryName={article.CategoryName.Quote()}/>");
					}

					XConsole
						.Outdent()
						.WriteEndComplexXmlElement("Issue");

					//Console.WriteLine($"  </Issue>");
				}
				XConsole
					.Outdent()
					.WriteEndComplexXmlElement("Volume");

				//Console.WriteLine($"</Volume>");
				//Console.WriteLine($"======================================");
			}
			XConsole
				.Outdent()
				.WriteEndComplexXmlElement("CultureWarsArchive");
		}
	}

	public partial class CultureWarsTerminal
	{
		//private const string _excerptNs = "http://wordpress.org/export/1.2/excerpt/";
		//private const string _contentNs = "http://purl.org/rss/1.0/modules/content/";
		//private const string _wfwNs = "http://wellformedweb.org/CommentAPI/";
		//private const string _dcNs = "http://purl.org/dc/elements/1.1/";
		//private const string _wpNs = "http://wordpress.org/export/1.2/";

		//private static readonly FXNamespace excerptNs = ("excerpt", _excerptNs);
		//private static readonly FXNamespace contentNs = ("content", _contentNs);
		//private static readonly FXNamespace wfwNs = ("wfw", _wfwNs);
		//private static readonly FXNamespace dcNs = ("dc", _dcNs);
		//private static readonly FXNamespace wpNs = ("wp", _wpNs);

		//private const double _xmlVersion = 1.0;
		//private const double _wxrVersion = 1.2;


		public static void Main(string[] args)
		{
			//TerminalApplication
			//	.Builder
			//	.WithCommand(
			//		t => t.WithName("minify-thumb")
			//					.HasOption(t => t.))

			Console.WriteLine("culturewars terminal");

			var hasQuit = false;

			while (!hasQuit)
			{
				Console.Write("enter command: ");
				var c = Console.ReadLine();
				Console.WriteLine();

				if (c == "upload-archive")
				{

				}
				if (c == "cw-articles")
				{
					QueryCultureWarsArticles();
				}
				if (c == "gen-cs-terms")
				{
					GenerateCSTagDeclarationsFromXml();
				}
				if (c == "minify-thumb")
				{
					MinifyThumbnails();
				}
				if (c == "gen-thumb")
				{
					GenerateThumbnails();
				}
				if (c == "wp-text")
				{
					var archiveApi = new InternetArchiveAPI();

					var queryBuilder = InternetArchiveQueryBuilder.Builder
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

					foreach (var archiveItem in archiveApi.Query(queryBuilder))
					{
						//Console.WriteLine($"Archive Item - Identifier: {archiveItem.Identifier}");
						foreach (var file in archiveItem.GetItemFiles())
						{
							if (!file.FileName.EndsWith(".mp4"))
								continue;

							Console.WriteLine("===========================================================================================");
							Console.WriteLine("BEGIN WRITE");
							Console.WriteLine();

							var uploadIndex = archiveItem.Identifier.Replace("emj-archive-", "");

							Console.WriteLine($"UploadIndex:            {uploadIndex}");
							Console.WriteLine($"EncodedFileName:        {file.FileName}");
							Console.WriteLine();

							var decodedFileName = file.FileName.UrlDecode();

							Console.WriteLine($"DecodedFileName:        {decodedFileName}       @jsonFileMatch...");

							var localJsonFilePath = JsonMetadataFileAssociator
								.GetAssociatedJsonFile(uploadIndex, file.FileName, out var matchedDistance);

							Console.WriteLine($"LocalJson:              {localJsonFilePath.Name}      @dist: {matchedDistance}");

							Console.WriteLine();
							Console.WriteLine("FlattenedMetadata:");

							Console.WriteLine();
							var jsonResultMetadata = JsonYouTubeMetadataParser.ParseJsonYouTubeMetadata(localJsonFilePath);
							var flattenedMetadata = new YouTubeVideoFlattenedMetadata(jsonResultMetadata);

							var csvFlattened = flattenedMetadata.GetStrLineAsList();

							Console.WriteLine(csvFlattened);
							Console.WriteLine();

							var archiveIdScope = $"https://www.archive.org/download/{archiveItem.Identifier}/";
							var unencodedFileName = file.FileName.UrlDecode();

							Console.WriteLine($"ia:archiveItemID:       {archiveItem.Identifier}");
							Console.WriteLine($"  ia:IndexWithinItem:   {file.IndexWithinItem}");
							Console.WriteLine($"  archiveIdScope:       {archiveIdScope}");
							Console.WriteLine($"  ia:Title:             {file.Title}");
							Console.WriteLine($"  ia:PathUrl:           {file.FilePathUrl}");
							Console.WriteLine($"  decodedFileName:      {unencodedFileName}");
							Console.WriteLine($"  ia:LastModifiedDate:  {file.LastModifiedDate:G}");
							Console.WriteLine($"  ia:ApproxBytes:       {file.ApproximateBytes} bytes");
							Console.WriteLine();
						}
					}
				}
				if (c == "wp-xml-tags")
				{
					Console.WriteLine($"Generating tags...");
					Console.WriteLine();

					var archiveApi = new InternetArchiveAPI();

					var queryBuilder = InternetArchiveQueryBuilder.Builder
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


					var outputFilePath = new DirectoryInfo(
							Environment.GetFolderPath(Environment.SpecialFolder.Desktop)).FullName +
						$@"\InternetArchiveTags9.xml";

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

					foreach (var term in WPTerm.AllTags)
					{
						term.WriteToXmlStream(writer);
					}

					writer.WriteEndElement()
						.WriteEndElement();

					Console.WriteLine($"Generation complete.");
				}
				if (c == "wp-xml")
				{
					var archiveApi = new InternetArchiveAPI();

					var queryBuilder = InternetArchiveQueryBuilder.Builder
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

					var outputFilePath = new DirectoryInfo(
						Environment.GetFolderPath(Environment.SpecialFolder.Desktop)).FullName +
						$@"\InternetArchiveCultureWars.xml";

					var outputFileInfo = new FileInfo(outputFilePath);

					using (var fileStream = outputFileInfo.OpenText())
					{
						var xDocument = new XDocument(
							new XDeclaration("1.0", "UTF-8", "yes"));

						XNamespace excerptNs = "http://wordpress.org/export/1.2/excerpt/";
						XNamespace contentNs = "http://purl.org/rss/1.0/modules/content/";
						XNamespace wfwNs = "http://wellformedweb.org/CommentAPI/";
						XNamespace dcNs = "http://purl.org/dc/elements/1.1/";
						XNamespace wpNs = "http://wordpress.org/export/1.2/";

						var rssNode = new XElement(XName.Get("rss"),
							excerptNs,
							contentNs,
							wfwNs,
							dcNs,
							wpNs);

						xDocument.Add(rssNode);

						var channelNode = new XElement(XName.Get("channel"));
						rssNode.Add(channelNode);

						var wxrVersionName = XName.Get(
							"wxr_version",
							"http://wordpress.org/export/1.2/");

						var wxrVersionNode = new XElement(wxrVersionName)
						{
							Value = "1.2"
						};

						channelNode.Add(wxrVersionNode);

						var index = 0;

						foreach (var tag in WPTerm.AllTags)
						{
							var categoryNode = new XElement(
								XName.Get("tag", "http://wordpress.org/export/1.2/"));

							var termIdNode = new XElement(
								XName.Get("term_id", "http://wordpress.org/export/1.2/"))
							{
								Value = index.ToString()
							};

							var tagSlugNode = new XElement(
								XName.Get("tag_slug", "http://wordpress.org/export/1.2/"));

							tagSlugNode.Add(new XCData(tag.HtmlEncodedTagName));

							var tagNameNode = new XElement(
								XName.Get("tag_name", "http://wordpress.org/export/1.2/"));

							tagNameNode.Add(new XCData(tag.TagFriendlyName));

							categoryNode.Add(termIdNode);
							categoryNode.Add(tagSlugNode);
							categoryNode.Add(tagNameNode);

							channelNode.Add(categoryNode);

							index++;
						}

						var desktopDirectory = new DirectoryInfo(
							Environment.GetFolderPath(Environment.SpecialFolder.Desktop));

						var exportFile = new FileInfo(
							desktopDirectory.FullName + @"\CultureWarsTags2.xml");

						using (var textWriter = exportFile.CreateText())
						{
							using (var xmlWriter = XmlWriter.Create(textWriter))
							{
								xDocument.Save(xmlWriter);
							}
						}
					}
					foreach (var archiveItem in archiveApi.Query(queryBuilder))
					{
						//Console.WriteLine($"Archive Item - Identifier: {archiveItem.Identifier}");
						foreach (var file in archiveItem.GetItemFiles())
						{
							if (!file.FileName.EndsWith(".mp4"))
								continue;

							Console.WriteLine("===========================================================================================");
							Console.WriteLine("BEGIN WRITE");
							Console.WriteLine();

							var uploadIndex = archiveItem.Identifier.Replace("emj-archive-", "");

							Console.WriteLine($"UploadIndex:            {uploadIndex}");
							Console.WriteLine($"EncodedFileName:        {file.FileName}");
							Console.WriteLine();

							var decodedFileName = file.FileName.UrlDecode();

							Console.WriteLine($"DecodedFileName:        {decodedFileName}       @jsonFileMatch...");

							var localJsonFilePath = JsonMetadataFileAssociator
								.GetAssociatedJsonFile(uploadIndex, file.FileName, out var matchedDistance);

							Console.WriteLine($"LocalJson:              {localJsonFilePath.Name}      @dist: {matchedDistance}");

							Console.WriteLine();
							Console.WriteLine("FlattenedMetadata:");

							Console.WriteLine();
							var jsonResultMetadata = JsonYouTubeMetadataParser.ParseJsonYouTubeMetadata(localJsonFilePath);
							var flattenedMetadata = new YouTubeVideoFlattenedMetadata(jsonResultMetadata);

							var csvFlattened = flattenedMetadata.GetStrLineAsList();

							Console.WriteLine(csvFlattened);
							Console.WriteLine();

							var archiveIdScope = $"https://www.archive.org/download/{archiveItem.Identifier}/";
							var unencodedFileName = file.FileName.UrlDecode();

							Console.WriteLine($"ia:archiveItemID:       {archiveItem.Identifier}");
							Console.WriteLine($"  ia:IndexWithinItem:   {file.IndexWithinItem}");
							Console.WriteLine($"  archiveIdScope:       {archiveIdScope}");
							Console.WriteLine($"  ia:Title:             {file.Title}");
							Console.WriteLine($"  ia:PathUrl:           {file.FilePathUrl}");
							Console.WriteLine($"  decodedFileName:      {unencodedFileName}");
							Console.WriteLine($"  ia:LastModifiedDate:  {file.LastModifiedDate:G}");
							Console.WriteLine($"  ia:ApproxBytes:       {file.ApproximateBytes} bytes");
							Console.WriteLine();
						}
					}
				}

				if (c == "xml-gen")
				{
					XmlGen();

					var outputFilePath = new DirectoryInfo(
							Environment.GetFolderPath(Environment.SpecialFolder.Desktop)).FullName +
						$@"\XmlGenTest7.xml";

					var outputFileInfo = new FileInfo(outputFilePath);
					using var consoleStreamWriter = new ConsoleStreamWriter(outputFileInfo.FullName, false);

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

					foreach (var item in GetWPPosts())
					{
						var pubDateStr = item.PublicationDate.ToString("ddd, dd MMM yyyy HH:mm:ss") + " +0000";
						var postDateStr = item.PostDate.ToString("yyyy-MM-dd HH:mm:ss");
						var gmtPostDateStr = item.PostDateGMT.ToString("yyyy-MM-dd HH:mm:ss");

						writer.WriteStartElement("item")
							.WriteInlineElement("title", item.PostTitle)
							.WriteInlineElement("link", item.PostLink)
							.WriteInlineCDataElement(contentNs, "encoded", item.PostContent)
							.WriteInlineCDataElement(excerptNs, "encoded", item.PostExcerpt)
							.WriteInlineElement(wpNs, "post_name", item.PostName)
							.WriteInlineElement(wpNs, "post_type", item.PostType)
							.WriteInlineElement(wpNs, "post_id", item.PostID.ToString())
							.WriteInlineElement(wpNs, "status", item.PostStatus.StatusName)
							.WriteInlineElement("pubdate", pubDateStr)
							.WriteInlineElement(wpNs, "post_date", postDateStr)
							.WriteInlineElement(wpNs, "post_date_gmt", gmtPostDateStr)
							.WriteInlineElement(dcNs, "creator", item.Author.Login)
							.WriteInlineElement(wpNs, "comment_status", "open");

						foreach (var category in item.Categories)
						{
							writer.WriteStartElement(wpNs, "category")
								.WriteInlineCDataElement(wpNs, "cat_name", category.CategoryName)
								.WriteInlineCDataElement(wpNs, "category_nicename", category.CategoryNiceName)
								.WriteInlineElement(wpNs, "category_parent", "0")
								.WriteEndElement();
						}
						writer.WriteEndElement();
					}
					writer.WriteEndElement()
						.WriteEndElement();
				}

				if (c == "query-archive")
				{
					var archiveApi = new InternetArchiveAPI();

					var queryBuilder = InternetArchiveQueryBuilder.Builder
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

					foreach (var archiveItem in archiveApi.Query(queryBuilder))
					{
						Console.WriteLine($"Archive Item - Identifier: {archiveItem.Identifier}");

						foreach (var file in archiveItem.GetItemFiles())
						{
							Console.WriteLine(
								$"{archiveItem.Identifier.Quote()}," +
								$"{file.IndexWithinItem}," +
								$"{file.Title.Quote()}," +
								$"{file.FilePathUrl.Quote()}," +
								$"\"{file.LastModifiedDate:G}\"");
						}
					}
				}
				else if (c == "quit" || c == "exit")
				{
					Console.WriteLine($"Really quit? (Y/N): ");

					var result = Console.ReadLine();
					if (result.ToUpper() == "Y")
					{
						hasQuit = true;
					}
				}
				else
				{
					Console.WriteLine($"{c.SQuote()} is not a recognized command.");
				}
			}
		}
	}
}