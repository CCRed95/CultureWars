using System;
using Ccr.Std.Core.Extensions;
using JetBrains.Annotations;

namespace CultureWars.API.CultureWars.Domain
{
	public class Magazine
	{
		public int MagazineID { get; }

		[NotNull]
		public string MagazinePrefix { get; }

		[NotNull]
		public string MagazineName { get; }


		private Magazine(
			int magazineID,
			[NotNull] string magazinePrefix,
			[NotNull] string magazineName)
		{
			magazinePrefix.IsNotNull(nameof(magazinePrefix));
			magazineName.IsNotNull(nameof(magazineName));

			MagazineID = magazineID;
			MagazinePrefix = magazinePrefix;
			MagazineName = magazineName;
		}


		public static readonly Magazine FidelityMagazine = new Magazine(
			1, "f", "Fidelity Magazine");

		public static readonly Magazine CultureWarsMagazine = new Magazine(
			2, "cw", "Culture Wars Magazine");


		public static Magazine GetMagazineFromPrefix(
			[NotNull] string magazinePrefix)
		{
			magazinePrefix.IsNotNull(nameof(magazinePrefix));

			var formattedPrefix = magazinePrefix.ToLower();

			switch (formattedPrefix)
			{
				case "f":
				case "fidelity":
					return FidelityMagazine;

				case "cw":
					return CultureWarsMagazine;

				default:
					throw new NotSupportedException(
						$"Cannot find magazine with the prefix {magazinePrefix.SQuote()}");
			}
		}
	}
}