using Ccr.Std.Core.Extensions;
using JetBrains.Annotations;

namespace CultureWars.Data.Domain
{
	public partial struct ShowHost
	{
		[NotNull]
		public string ShowHostName { get; }

		[NotNull]
		public string OfficialShowName { get; }


		public ShowHost(
			string showHostName,
			string officialShowName)
		{
			showHostName.IsNotNull(nameof(showHostName));
			officialShowName.IsNotNull(nameof(officialShowName));

			ShowHostName = showHostName;
			OfficialShowName = officialShowName;
		}
	}
}