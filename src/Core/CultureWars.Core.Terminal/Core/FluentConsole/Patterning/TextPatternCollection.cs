using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;

namespace CultureWars.Core.FluentConsole.Patterning
{
	/// <summary>
	/// Represents a collection of TextPattern objects.
	/// </summary>
	public class TextPatternCollection
		: IPrototypable<TextPatternCollection>
	{
		protected readonly List<TextPattern> _patterns = new List<TextPattern>();


		/// <summary>
		///	Represents a collection of TextPattern objects.
		/// </summary>
		/// <param name="patterns">
		///	Other patterns to be added to the collection.
		/// </param>
		public TextPatternCollection(
			//[RegexPattern] string firstPattern,
			[RegexPattern] params string[] patterns)
		{
			_patterns.AddRange(
				patterns.Select(t => new TextPattern(t)).ToArray());
		}
		
		
		TextPatternCollection IPrototypable<TextPatternCollection>.Prototype()
		{
			return new TextPatternCollection(
				_patterns.Select(pattern => pattern.OriginalPattern).ToArray());
		}

		/// <summary>
		/// Attempts to match any of the TextPatternCollection's member TextPatterns against a string input.
		/// </summary>
		/// <param name="input">
		/// The input against which Patterns will potentially be matched.
		/// </param>
		/// <returns>
		/// Returns 'true' if any of the TextPatternCollection's member TextPatterns matches against the
		/// input string.
		/// </returns>
		public bool HasAnyMatches(string input)
		{
			return _patterns.Any(t => t.GetMatchLocations(input).Any());
		}

		public bool HasAllMatches(string input)
		{
			return _patterns.All(t => !t.GetMatchLocations(input).Any());
		}
	}
}
