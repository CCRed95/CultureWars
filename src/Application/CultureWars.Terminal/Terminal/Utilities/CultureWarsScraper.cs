//using System;
//using System.Collections.Generic;
//using System.Globalization;
//using System.Linq;
//using System.Text;
//using AngleSharp;
//using Ccr.Std.Core.Extensions;
//using CultureWars.API.InternetArchive.Domain;
//using CultureWars.Data.Domain;

//namespace CultureWars.Terminal.Utilities
//{
//	public class CultureWarsScraper
//	{
//		public static IEnumerable<CultureWarsMagazineVolume> ScrapeCultureWarsMagazineVolumes(
//			InternetArchiveItem internetArchiveItem)
//		{
//			var context = BrowsingContext.New(
//				Configuration.Default.WithDefaultLoader());

//			var downloadPageUrl = internetArchiveItem.GetItemDownloadPageUrl();

//			using (var document = context
//				.OpenAsync(downloadPageUrl)
//				.GetAwaiter()
//				.GetResult())
//			{
//				var maincontent = document
//					.GetElementById("maincontent");

//				var container = maincontent
//					.GetElementsByClassName("container-ia")
//					.First();

//				var directoryListing = container
//					.GetElementsByClassName("download-directory-listing")
//					.First();

//				var tbody = directoryListing
//					.GetElementsByTagName("tbody")
//					.First();

//				var fileNodeList = tbody
//					.GetElementsByTagName("tr")
//					.Skip(1);

//				var itemIndex = 0;

//				foreach (var fileNode in fileNodeList)
//				{
//					var fileLinkElement = fileNode
//						.GetElementsByTagName("td")
//						.First()
//						.GetElementsByTagName("a")
//						.First();

//					var fileLinkPath = fileLinkElement
//						.GetAttribute("href");

//					var fileTitle = fileLinkElement
//						.TextContent;

//					var fileDate = fileNode
//						.GetElementsByTagName("td")
//						.Skip(1)
//						.First()
//						.TextContent;

//					var fileSize = fileNode
//						.GetElementsByTagName("td")
//						.Skip(2)
//						.First()
//						.TextContent;

//					var fileKind = DetermineIAFileKind(fileTitle);

//					//var airDate = DetermineArchiveFileAirDate(
//					//  postShowStr)
//					// .GetValueOrDefault();

//					var approximateBytes = DetermineArchiveFileSizeBytes(fileSize);

//					if (!DateTime.TryParseExact(
//							fileDate,
//							"dd-MMM-yyyy ss:mm",
//							DateTimeFormatInfo.CurrentInfo,
//							DateTimeStyles.None,
//							out var lastModifiedDate))
//						throw new FormatException(
//							$"Cannot parse dateTime from string {fileDate.Quote()}.");

//					yield return new InternetArchiveFile(
//						internetArchiveItem,
//						fileLinkPath,
//						fileKind,
//						fileTitle,
//						lastModifiedDate,
//						approximateBytes,
//						itemIndex);

//					itemIndex++;
//				}
//			}
//		}

//		public static IEnumerable<InternetArchiveFile> ScrapeArchiveThumbnailFiles(
//			InternetArchiveItem internetArchiveItem)
//		{
//			var context = BrowsingContext.New(
//				Configuration.Default.WithDefaultLoader());

//			var downloadPageUrl = internetArchiveItem.GetItemThumbnailDownloadPageUrl();

//			using (var document = context
//				.OpenAsync(downloadPageUrl)
//				.GetAwaiter()
//				.GetResult())
//			{
//				var maincontent = document
//					.GetElementById("maincontent");

//				var container = maincontent
//					.GetElementsByClassName("container-ia")
//					.First();

//				var directoryListing = container
//					.GetElementsByClassName("download-directory-listing")
//					.First();

//				var tbody = directoryListing
//					.GetElementsByTagName("tbody")
//					.First();

//				var fileNodeList = tbody
//					.GetElementsByTagName("tr")
//					.Skip(1);

//				var itemIndex = 0;

//				foreach (var fileNode in fileNodeList)
//				{
//					var fileLinkElement = fileNode
//						.GetElementsByTagName("td")
//						.First()
//						.GetElementsByTagName("a")
//						.First();

//					var fileLinkPath = fileLinkElement
//						.GetAttribute("href");

//					var fileTitle = fileLinkElement
//						.TextContent;

//					var fileDate = fileNode
//						.GetElementsByTagName("td")
//						.Skip(1)
//						.First()
//						.TextContent;

//					var fileSize = fileNode
//						.GetElementsByTagName("td")
//						.Skip(2)
//						.First()
//						.TextContent;

//					var fileKind = DetermineIAFileKind(fileTitle);

//					//var airDate = DetermineArchiveFileAirDate(
//					//  postShowStr)
//					// .GetValueOrDefault();

//					var approximateBytes = DetermineArchiveFileSizeBytes(fileSize);

//					if (!DateTime.TryParseExact(
//							fileDate,
//							"dd-MMM-yyyy ss:mm",
//							DateTimeFormatInfo.CurrentInfo,
//							DateTimeStyles.None,
//							out var lastModifiedDate))
//						throw new FormatException(
//							$"Cannot parse dateTime from string {fileDate.Quote()}.");

//					yield return new InternetArchiveFile(
//						internetArchiveItem,
//						fileLinkPath,
//						fileKind,
//						fileTitle,
//						lastModifiedDate,
//						approximateBytes,
//						itemIndex,
//						true);

//					itemIndex++;
//				}
//			}
//		}

//	}
//}
