namespace CultureWars.API.InternetArchive.Domain
{
	public class ArchiveCredentials
	{
		public string AccessApiKey { get; }

		public string SecretApiKey { get; }


		public ArchiveCredentials(
			string accessApiKey,
			string secretApiKey)
		{
			AccessApiKey = accessApiKey;
			SecretApiKey = secretApiKey;
		}
	}
}
