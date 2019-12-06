using Ccr.Std.Core.Extensions;
using JetBrains.Annotations;

namespace CultureWars.Data.Export.WordPress.Domain
{
	public partial struct WPShowHost
	{
		[NotNull]
		public string ShowHostName { get; }

		[NotNull]
		public string OfficialShowName { get; }


		public WPShowHost(
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