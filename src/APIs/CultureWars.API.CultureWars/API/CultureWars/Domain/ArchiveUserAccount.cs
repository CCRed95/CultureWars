//using System;
//using AngleSharp;
//using Ccr.Std.Core.Extensions;
//using CultureWars.API.Infrastructure;

//namespace CultureWars.API.CultureWars.Domain
//{
//	public class InternetArchiveClient
//	{
//		public InternetArchiveChannel GetChannel(
//			string channelName)
//		{
//			channelName.IsNotNull(nameof(channelName));

//			var _requestBuilder = new DomainFragment("https://archive.org");

//			var url = _requestBuilder
//				.Builder
//				.WithPath("details")
//				.WithPath($"@{channelName}")
//				.Build();

//			var context = BrowsingContext.New(
//				Configuration.Default.WithDefaultLoader());

//			using (var document = context
//				.OpenAsync(url)
//				.GetAwaiter()
//				.GetResult())
//			{
//				var s = document.QuerySelector(
//					"html > body > #wrap > #naWrap > #naWrap > div > ul > li > #user-menu > .img.avatar");

//				return new InternetArchiveChannel(
//					"",
//					"");
//			}

//			//using (var httpClient = new HttpClientWrapper())
//			//{
//			//	var response = httpClient
//			//		.GetContentAsync(url)
//			//		.GetAwaiter()
//			//		.GetResult();

//			//	var formattedResponse = response;
//			//	if (formattedResponse.StartsWith("callback("))
//			//	{
//			//		formattedResponse = formattedResponse
//			//			.Substring("callback(".Length)
//			//			.TrimEnd(')');
//			//	}

//			//	var archiveResponse = JsonConvert
//			//		.DeserializeObject<RootObject>(
//			//			formattedResponse);

//			//	var archiveAlbums = archiveResponse
//			//		.Response
//			//		.Docs
//			//		.Select(
//			//			ArchiveAlbumInterpreter.CreateArchiveAlbum);

//			//	return archiveAlbums;
//			//}
//		}
//	}
//}