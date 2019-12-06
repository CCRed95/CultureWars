using System.Drawing;
using CultureWars.Core.FluentConsole.Colorization.Complex;
using CultureWars.Core.FluentConsole.Patterning;
using CultureWars.Core.FluentConsole.Patterning.Complex;

namespace CultureWars.Core.FluentConsole.Colorization.Factories
{
	public sealed class ColorAlternatorFactory
	{
		public ColorAlternator GetAlternator(
			string[] patterns, 
			params Color[] colors)
		{
			return new PatternBasedColorAlternator(
				new TextPatternCollection(patterns), 
				colors);
		}

		public ColorAlternator GetAlternator(
			int frequency, 
			params Color[] colors)
		{
			return new FrequencyBasedColorAlternator(
				frequency, 
				colors);
		}
	}
}