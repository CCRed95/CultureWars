using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using CultureWars.Core.FluentConsole.Colorization.Stylization;
using CultureWars.Core.FluentConsole.Patterning;

namespace CultureWars.Core.FluentConsole.Annotating
{
	/// <summary>
	/// Exposes methods and properties used in batch styling of text.
	/// </summary>
	public sealed class TextAnnotator
	{
		private readonly StyleSheet _styleSheet;
		private readonly Dictionary<StyleClass<TextPattern>, MatchFound> _matchFoundHandlers =
			new Dictionary<StyleClass<TextPattern>, MatchFound>();


		/// <summary>
		///	Exposes methods and properties used in batch styling of text.
		/// </summary>
		/// <param name="styleSheet">
		///	The StyleSheet instance that defines the way in which text should be styled.
		/// </param>
		public TextAnnotator(
			StyleSheet styleSheet)
		{
			_styleSheet = styleSheet;

			foreach (var styleClass in styleSheet.Styles)
			{
				_matchFoundHandlers.Add(
					styleClass, 
					(styleClass as Stylizer)?.MatchFoundHandler);
			}
		}


		/// <summary>
		///	Partitions the input text into styled and unstyled pieces.
		/// </summary>
		/// <param name="input">
		///	The text to be styled.
		/// </param>
		/// <returns>
		///	Returns a map relating pieces of text to their corresponding styles.
		/// </returns>
		public List<(string, Color)> GetAnnotationMap(
			string input)
		{
			var targets = GetStyleTargets(input);
			return GenerateStyleMap(targets, input);
		}

		private IEnumerable<(StyleClass<TextPattern>, TextMatchDescriptor)> GetStyleTargets(
			string input)
		{
			var matches = new List<(StyleClass<TextPattern>, TextMatchDescriptor)>();
			var locations = new List<TextMatchDescriptor>();

			foreach (var pattern in _styleSheet.Styles)
			{
				foreach (var location in pattern.Target.GetMatchLocations(input))
				{
					if (locations.Contains(location))
					{
						var index = locations.IndexOf(location);

						matches.RemoveAt(index);
						locations.RemoveAt(index);
					}
					matches.Add((pattern, location));
					locations.Add(location);
				}
			}

			matches = matches.OrderBy(match => match.Item2).ToList();
			return matches;
		}

		private List<(string, Color)> GenerateStyleMap(
			IEnumerable<(StyleClass<TextPattern>, TextMatchDescriptor)> targets,
			string input)
		{
			var styleMap = new List<(string, Color)>();

			var previousLocation = new TextMatchDescriptor(0, 0, input);
			var chocolateEnd = 0;

			foreach (var styledLocation in targets)
			{
				var currentLocation = styledLocation.Item2;
				if (previousLocation.EndIndex > currentLocation.Index)
				{
					previousLocation = new TextMatchDescriptor(0, 0, input);
				}

				var vanillaStart = previousLocation.EndIndex;
				var vanillaEnd = currentLocation.Index;
				var chocolateStart = vanillaEnd;

				chocolateEnd = currentLocation.EndIndex;

				var vanilla = input.Substring(vanillaStart, vanillaEnd - vanillaStart);

				var chocolate = _matchFoundHandlers[styledLocation.Item1]
					.Invoke(
						input, 
						styledLocation.Item2,
						input.Substring(
							chocolateStart,
							chocolateEnd - chocolateStart));

				if (vanilla != "")
					styleMap.Add((vanilla, _styleSheet.UnstyledColor));

				if (chocolate != "")
					styleMap.Add((chocolate, styledLocation.Item1.Color));
				
				previousLocation = currentLocation.Prototype();
			}

			if (chocolateEnd < input.Length)
			{
				var vanilla = input.Substring(chocolateEnd, input.Length - chocolateEnd);
				styleMap.Add((vanilla, _styleSheet.UnstyledColor));
			}

			return styleMap;
		}
	}
}