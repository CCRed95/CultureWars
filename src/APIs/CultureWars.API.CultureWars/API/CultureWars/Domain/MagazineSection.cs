using System.Runtime.CompilerServices;
using Ccr.Std.Core.Collections;
using JetBrains.Annotations;

namespace CultureWars.API.CultureWars.Domain
{
	public sealed class MagazineSection 
		: ValueEnum<string>
	{
		public static readonly MagazineSection Editorial = new MagazineSection("Editorials");

		public static readonly MagazineSection Commentary = new MagazineSection("Commentary");

		public static readonly MagazineSection Features = new MagazineSection("Features");

		public static readonly MagazineSection Reflections = new MagazineSection("Reflections");

		public static readonly MagazineSection Reviews = new MagazineSection("Reviews");
		
		public static readonly MagazineSection InternationalProLifeReport = new MagazineSection("International Pro-Life Report");
		
		public static readonly MagazineSection LettersToTheReaders = new MagazineSection("Letters To The Readers");
		
		public static readonly MagazineSection Huswifery = new MagazineSection("Huswifery");
		
		public static readonly MagazineSection PointCounterpoint = new MagazineSection("Point/Counterpoint");
		
		public static readonly MagazineSection TheFinalWord = new MagazineSection("The Final Word");
		
		public static readonly MagazineSection TheLastWord = new MagazineSection("The Last Word");
		
		public static readonly MagazineSection CultureofDeathWatch = new MagazineSection("Culture of Death Watch");
		
		public static readonly MagazineSection FaithandCulture = new MagazineSection("Faith and Culture");
		
		public static readonly MagazineSection Interviews = new MagazineSection("Interviews");


		private MagazineSection(
			[NotNull] string value,
			[CallerMemberName] string memberName = "",
			[CallerLineNumber] int lineNumber = 0) : base(
				value,
				memberName,
				lineNumber)
		{
		}
	}
}