using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using CultureWars.Core.FluentConsole.Patterning;

namespace CultureWars.Core.FluentConsole.Formatting
{
	/// <summary>
	/// Exposes methods and properties used in batch styling of text.  In contrast to the TextAnnotator
	/// class, this class is meant to be used in the styling of *formatted* strings, i.e. strings that
	/// follow the "{0}, {1}...{n}" pattern.
	/// </summary>
	public sealed class TextFormatter
	{
		// NOTE: I still feel that there's too much overlap between this class and the TextAnnotator class.
		private Color _defaultColor;
		private TextPattern _textPattern;
		private readonly string _defaultFormatToken = "{[0-9][^}]*}";


		/// <summary>
		/// Exposes methods and properties used in batch styling of text.  In contrast to the TextAnnotator
		/// class, this class is meant to be used in the styling of *formatted* strings, i.e. strings that
		/// follow the "{0}, {1}...{n}" pattern.
		/// </summary>
		/// <param name="defaultColor">
		/// The color to be associated with unstyled text.
		/// </param>
		public TextFormatter(
			Color defaultColor)
		{
			_defaultColor = defaultColor;
			_textPattern = new TextPattern(_defaultFormatToken);
		}

		/// <summary>
		/// Exposes methods and properties used in batch styling of text.  In contrast to the TextAnnotator
		/// class, this class is meant to be used in the styling of *formatted* strings, i.e. strings that
		/// follow the "{0}, {1}...{n}" pattern.
		/// </summary>
		/// <param name="defaultColor">
		/// The color to be associated with unstyled text.
		/// </param>
		/// <param name="formatToken">
		/// A regular expression representing the format token. By default, the TextFormatter will use a
		/// regular expression that matches the "{0}, {1}...{n}" pattern.
		/// </param>
		public TextFormatter(
			Color defaultColor,
			string formatToken)
		{
			_defaultColor = defaultColor;
			_textPattern = new TextPattern(formatToken);
		}


		/// <summary>
		/// Partitions the input text into styled and unstyled pieces.
		/// </summary>
		/// <param name="input">
		/// The text to be styled.
		/// </param>
		/// <param name="args">
		/// A collection of objects that will replace the format tokens in the input string.
		/// </param>
		/// <param name="colors">
		/// </param>
		/// <returns>
		/// Returns a map relating pieces of text to their corresponding styles.
		/// </returns>
		public List<(string, Color)> GetFormatMap(
			string input,
			object[] args,
			Color[] colors)
		{
			var formatMap = new List<(string, Color)>();
			var locations = _textPattern.GetMatchLocations(input).ToList();
			var indices = _textPattern.GetMatchesLiteral(input).ToList();

			TryExtendColors(ref args, ref colors);

			var chocolateEnd = 0;

			for (var i = 0; i < locations.Count; i++)
			{
				var styledIndex = int.Parse(indices[i].TrimStart('{').TrimEnd('}'));

				var vanillaStart = 0;
				if (i > 0)
				{
					vanillaStart = locations[i - 1].EndIndex;
				}

				var vanillaEnd = locations[i].Index;
				chocolateEnd = locations[i].EndIndex;

				var vanilla = input.Substring(vanillaStart, vanillaEnd - vanillaStart);
				var chocolate = args[styledIndex].ToString();

				formatMap.Add((vanilla, _defaultColor));
				formatMap.Add((chocolate, colors[styledIndex]));
			}

			if (chocolateEnd < input.Length)
			{
				var vanilla = input.Substring(chocolateEnd, input.Length - chocolateEnd);
				formatMap.Add((vanilla, _defaultColor));
			}

			return formatMap;
		}

		private void TryExtendColors(
			ref object[] args, 
			ref Color[] colors)
		{
			if (colors.Length < args.Length)
			{
				var styledColor = colors[0];
				colors = new Color[args.Length];

				for (var i = 0; i < args.Length; i++)
				{
					colors[i] = styledColor;
				}
			}
		}
	}
}