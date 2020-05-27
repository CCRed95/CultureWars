using Newtonsoft.Json;

namespace CultureWars.API.Smmry.Domain
{
	public class SmmryError
	{
		[JsonProperty("sm_api_error")]
		public int Code { get; private set; }

		[JsonProperty("sm_api_message")]
		public string Message { get; private set; }
	}
}