using Ccr.Std.Core.Extensions;
using JetBrains.Annotations;

namespace CultureWars.API.CultureWars.Domain
{
	public class CWMIssue
	{
		public int VolumeNumber { get; }

		public int IssueNumber { get; }

		[NotNull]
		public Magazine Magazine { get; }

		[NotNull]
		public string IssuePageAbsoluteUrl { get; }

		[NotNull]
		public CWMVolume OwnerVolume { get; }



		public CWMIssue(
			int volumeNumber,
			int issueNumber,
			[NotNull] Magazine magazine,
			[NotNull] string issuePageAbsoluteUrl,
			[NotNull] CWMVolume ownerVolume)
		{
			magazine.IsNotNull(nameof(magazine));
			ownerVolume.IsNotNull(nameof(ownerVolume));
			issuePageAbsoluteUrl.IsNotNull(nameof(issuePageAbsoluteUrl));

			Magazine = magazine;
			VolumeNumber = volumeNumber;
			IssueNumber = issueNumber;
			OwnerVolume = ownerVolume;
			IssuePageAbsoluteUrl = issuePageAbsoluteUrl;
		}
	}
}