using System;
using System.Drawing;
using System.Linq;
using CultureWars.Core.FluentConsole.Colorization;
using CultureWars.Core.FluentConsole.Extensions;

namespace CultureWars.Core.FluentConsole.Patterning.Complex
{
	/// <summary>
	/// Exposes methods and properties used for alternating over a set of colors according to the
	/// occurrences of patterns.
	/// </summary>
	public sealed class PatternBasedColorAlternator
		: ColorAlternator,
			IPrototypable<PatternBasedColorAlternator>
	{
		private readonly TextPatternCollection _patternMatcher;
		private bool _isFirstRun = true;


		/// <summary>
		///	Exposes methods and properties used for alternating over a set of colors according to	the
		/// occurrences of patterns.
		/// </summary>
		/// <param name="patternMatcher">
		///	The PatternMatcher instance which will dictate what will need to happen in order for the
		///	color to alternate.
		/// </param>
		/// <param name="colors">
		///	The set of colors over which to alternate.
		/// </param>
		public PatternBasedColorAlternator(
			TextPatternCollection patternMatcher,
			params Color[] colors)
				: base(
					colors)
		{
			_patternMatcher = patternMatcher;
		}


		public new PatternBasedColorAlternator Prototype()
		{
			return new PatternBasedColorAlternator(
				_patternMatcher.Proto(),
				Colors.DeepCopy().ToArray());
		}

		protected override ColorAlternator PrototypeCore()
		{
			return Prototype();
		}

		/// <summary>
		///	Alternates colors based on patterns matched in the input string.
		/// </summary>
		/// <param name="input">
		///	The string to be styled.
		/// </param>
		/// <returns>
		///	The current color of the ColorAlternator.
		/// </returns>
		public override Color GetNextColor(
			string input)
		{
			if (Colors.Length == 0)
				throw new InvalidOperationException(
					"No colors have been supplied over which to alternate!");
			
			if (_isFirstRun)
			{
				_isFirstRun = false;
				return Colors[_nextColorIndex];
			}
			if (_patternMatcher.HasAnyMatches(input))
			{
				TryIncrementColorIndex();
			}
			var nextColor = Colors[_nextColorIndex];
			return nextColor;
		}

		protected override void TryIncrementColorIndex()
		{
			if (_nextColorIndex >= Colors.Length - 1)
				_nextColorIndex = 0;
			else
				_nextColorIndex++;
		}
	}
}