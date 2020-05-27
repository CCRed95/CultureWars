using System.Collections.Generic;
using System.Linq;
using CultureWars.API.CultureWars.Domain;
using CultureWars.API.CultureWars.Interpreters;

namespace CultureWars.API.CultureWars.Extensions
{
	public static class CWMDataItemExtensions
	{
		public static IReadOnlyList<CWMIssue> GetVolumeIssues(
			this CWMVolume @this)
		{
			return CWMInterpreter.ScrapeVolumeIssues(@this)
				.ToArray();
		}

		public static IReadOnlyList<CWMArticle> GetIssueArticles(
			this CWMIssue @this)
		{
			return CWMInterpreter.ScrapeIssueArticles(@this)
				.ToArray();
		}


		//public static string GetItemDownloadPageUrl(
		//	this InternetArchiveItem @this)
		//{
		//	return $"https://www.archive.org/" +
		//		$"download/" +
		//		$"{@this.Identifier}/";
		//}

		//public static string GetItemThumbnailDownloadPageUrl(
		//	this InternetArchiveItem @this)
		//{
		//	return $"https://www.archive.org/" +
		//		$"download/" +
		//		$"{@this.Identifier}/" +
		//		$"{@this.Identifier}.thumbs/";
		//}

	}
}