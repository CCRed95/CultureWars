using Ccr.Std.Core.Extensions;
using JetBrains.Annotations;

namespace CultureWars.API.CultureWars.Domain
{
	public class CWMVolume
	{
		public int Year { get; }
		
		public int VolumeNumber { get; }

		[NotNull]
		public string VolumePageAbsoluteUrl { get; }
		

		public CWMVolume(
			int year,
			int volumeNumber,
			[NotNull] string volumePageAbsoluteUrl)
		{
			volumePageAbsoluteUrl.IsNotNull(nameof(volumePageAbsoluteUrl));

			Year = year;
			VolumeNumber = volumeNumber;
			VolumePageAbsoluteUrl = volumePageAbsoluteUrl;
		}
	}
}