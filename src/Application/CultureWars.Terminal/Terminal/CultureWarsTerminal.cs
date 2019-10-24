using Ccr.Std.Core.Extensions;
using System;
using CultureWars.API.InternetArchive;
using CultureWars.API.InternetArchive.Domain;
using CultureWars.API.InternetArchive.Query;

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

