using System.Text.RegularExpressions;
using JetBrains.Annotations;

namespace CultureWars.API.YouTube.Domain
{
	public class YouTubeChannel
	{
		private static readonly Regex _channelID = new Regex(@"");


		public int YouTubeChannelID { get; }

		public string DisplayName { get; }

		public string ChannelID { get; }

		public string ChannelUrl { get; }

		public string ProfileImageUrl { get; }

		public string ModeratorID { get; }

		public bool IsVerified { get; }



		public YouTubeChannel(
			int youTubeChannelID,
			[NotNull] string displayName,
			[NotNull] string channelID,
			[NotNull] string channelUrl,
			[NotNull] string profileImageUrl,
			[NotNull] string moderatorID,
			bool isVerified)
		{
			YouTubeChannelID = youTubeChannelID;
			DisplayName = displayName;
			ChannelID = channelID;
			ChannelUrl = channelUrl;
			ProfileImageUrl = profileImageUrl;
			ModeratorID = moderatorID;
			IsVerified = isVerified;
		}
	}

}