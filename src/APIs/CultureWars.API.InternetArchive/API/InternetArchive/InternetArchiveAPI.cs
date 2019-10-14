using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using CultureWars.API.Common;
using CultureWars.API.Infrastructure;
using CultureWars.API.InternetArchive.Domain;
using CultureWars.API.InternetArchive.Domain.Responses;
using CultureWars.API.InternetArchive.Interpreters;
using CultureWars.API.InternetArchive.Query;
using CultureWars.API.Web;
using Newtonsoft.Json;

namespace CultureWars.API.InternetArchive.API.InternetArchive
{
	public class InternetArchiveAPI
		: APIBase<
			InternetArchiveItem,
			InternetArchiveQueryBuilder>

	{
		private const string _domain = "https://www.archive.org/";
		private const string _uploadDomain = "http://s3.us.archive.org/";

		private DomainFragment _requestBuilder;
		private DomainFragment _uploadRequestBuilder;
		

		protected override DomainFragment RequestBuilder
		{
			get => _requestBuilder
				?? (_requestBuilder = new DomainFragment(_domain));
		}

		protected DomainFragment UploadRequestBuilder
		{
			get => _uploadRequestBuilder
				?? (_uploadRequestBuilder = new DomainFragment(_uploadDomain));
		}

		

		public override IEnumerable<InternetArchiveItem> Query(
			InternetArchiveQueryBuilder queryBuilder)
		{
			var url = queryBuilder
				.BuilldRequestUrl(
					RequestBuilder);

			using (var httpClient = new HttpClientWrapper())
			{
				var response = httpClient
					.GetContentAsync(url)
					.GetAwaiter()
					.GetResult();

				var formattedResponse = response;
				if (formattedResponse.StartsWith("callback("))
				{
					formattedResponse = formattedResponse
						.Substring("callback(".Length)
						.TrimEnd(')');
				}

				var archiveResponse = JsonConvert
					.DeserializeObject<RootObject>(
						formattedResponse);

				var archiveItems = archiveResponse
					.Response
					.Docs
					.Select(
						ArchiveItemInterpreter.CreateArchiveItem);

				return archiveItems;
			}
		}

		public async Task<string> UploadFile(
			string collectionID,
			FileInfo fileInfo)
		{
			var urlBuilder = UploadRequestBuilder
				.Builder
				.WithPath(collectionID)
				.WithPath(fileInfo.Name);

			var url = urlBuilder.Build();

			using (var httpClient = new HttpClient())
			{
				using (var request = new HttpRequestMessage(
					new HttpMethod("PUT"), url))
				{
					request.Headers.TryAddWithoutValidation("Authorization", "LOW $accesskey:$secret");
					request.Headers.TryAddWithoutValidation("x-amz-auto-make-bucket", "1");
					request.Headers.TryAddWithoutValidation("x-archive-meta01-collection", "opensource_movies");
					request.Headers.TryAddWithoutValidation("x-archive-meta-mediatype", "movies");
					request.Headers.TryAddWithoutValidation("x-archive-meta-title", "Ben plays piano.");

					request.Content = new ByteArrayContent(File.ReadAllBytes("ben-2009-05-09.avi"));

					var response = await httpClient.SendAsync(request);

					return url;
				}
			}
		}
	}
}