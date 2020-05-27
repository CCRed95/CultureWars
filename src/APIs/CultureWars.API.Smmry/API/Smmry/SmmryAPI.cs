using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using CultureWars.API.Smmry.Domain;
using CultureWars.API.Smmry.Exceptions;
using CultureWars.API.Web;
using Newtonsoft.Json;

namespace CultureWars.API.Smmry
{
	public class SmmryAPI
		: IDisposable
	{
		private const string _domain = "https://api.smmry.com/";
		private static readonly HttpClient _client = new HttpClient();
		

		public async Task<SmmryResponse> QueryAsync(
			SmmryParameters smmryParameters)
		{
			var json = await GetJsonAsync(smmryParameters);

			var smmryResponse = JsonConvert
				.DeserializeObject<SmmryResponse>(json);

			var isErrorResponse = smmryResponse.GetType()
				.GetProperties()
				.All(x => x.GetValue(smmryResponse) == null);

			if (!isErrorResponse)
				return smmryResponse;

			var error = JsonConvert.DeserializeObject<SmmryError>(json);

			throw new SmmryException(
				$"{error.Code}: {error.Message}");
		}

		public SmmryResponse Query(
			SmmryParameters smmryParameters)
		{
			var json = GetJson(smmryParameters);

			var smmryResponse = JsonConvert
				.DeserializeObject<SmmryResponse>(json);

			var isErrorResponse = smmryResponse.GetType()
				.GetProperties()
				.All(x => x.GetValue(smmryResponse) == null);

			if (!isErrorResponse)
				return smmryResponse;

			var error = JsonConvert.DeserializeObject<SmmryError>(json);

			throw new SmmryException(
				$"{error.Code}: {error.Message}");
		}


		public static async Task<string> GetJsonAsync(
			Dictionary<string, object> smmryParameters)
		{
			var url = _domain + smmryParameters;
			
			using var responsemessage = await _client.GetAsync(url);
			using var content = responsemessage.Content;

			return await content.ReadAsStringAsync();
		}

		private static string GetJson(
			Dictionary<string, object> smmryParameters)
		{
			var url = _domain + smmryParameters;

			using var _clientWrapper = new HttpClientWrapper();

			var responseMessage = _clientWrapper
				.GetContentAsync(url)
				.GetAwaiter()
				.GetResult();

			return responseMessage;
		}

		/// <inheritdoc />
		public void Dispose()
		{
			_client?.Dispose();
		}
	}
}





/*		public static SmmryResponse Query(
			SmmryParameters smmryParameters)
		{
			var json = GetJsonAsync(smmryParameters)
				.GetAwaiter()
				.GetResult();

			var smmryResponse = JsonConvert
				.DeserializeObject<SmmryResponse>(json);

			var isErrorResponse = smmryResponse.GetType()
				.GetProperties()
				.All(x => x.GetValue(smmryResponse) == null);

			if (!isErrorResponse)
				return smmryResponse;

			var error = JsonConvert.DeserializeObject<SmmryError>(json);

			throw new SmmryException(
				$"{error.Code}: {error.Message}");
		}


		private static async Task<string> GetJsonAsync(
			Dictionary<string, object> smmryParameters)
		{
			var url = _domain + smmryParameters;

			using var responsemessage = await _client.GetAsync(url);
			using var content = responsemessage.Content;
			
			return await content.ReadAsStringAsync();
		}*/
