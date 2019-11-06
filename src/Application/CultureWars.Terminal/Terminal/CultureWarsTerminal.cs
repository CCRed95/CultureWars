using Ccr.Std.Core.Extensions;
using System;
using System.Threading;
using CultureWars.API.GoogleArchives.JsonParsing;
using CultureWars.API.InternetArchive;
using CultureWars.API.InternetArchive.Domain;
using CultureWars.API.InternetArchive.Query;
using CultureWars.API.Web;
using CultureWars.Terminal.Utilities;

namespace CultureWars.Terminal
{
	public class CultureWarsTerminal
	{
		public static void Main(string[] args)
		{
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
				if (c == "generate-wp")
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

