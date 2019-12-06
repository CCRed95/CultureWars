using System.Collections.Generic;
using System.Text.RegularExpressions;
using JetBrains.Annotations;

namespace CultureWars.Core.FluentConsole.Patterning
{
	/// <summary>
	///		Represents a Pattern within a <see cref="string"/> text source.
	/// </summary>
	public sealed class TextPattern
	{
		private readonly Regex _regexPattern;
		internal string OriginalPattern { get; }

		/// <summary>
		///		Exposes methods and properties representing a text pattern.
		/// </summary>
		/// <param name="pattern">
		///		A Regex pattern in which to use as the <see cref="TextPattern"/>'s match qualifier. 
		/// </param>
		public TextPattern(
			[RegexPattern] string pattern)
		{
			_regexPattern = new Regex(pattern);
			OriginalPattern = pattern;
		}


		/// <summary>
		///		Finds all match locations of this TextPattern in the input <see cref="string"/> text source.
		/// </summary>
		/// <param name="input">
		///		The source text string.
		/// </param>
		/// <returns>
		///		Returns all match locations of this TextPattern in the specified input string.
		/// </returns>
		public IEnumerable<TextMatchDescriptor> GetMatchLocations(
			string input)
		{
			var matches = _regexPattern.Matches(input);

			if (matches.Count == 0)
			{
				yield break;
			}
			foreach (Match match in matches)
			{
				yield return new TextMatchDescriptor(
					match.Index,
					match.Length,
					input);
			}
		}

		/// <summary>
		///		Finds all matches of this TextPattern in the input <see cref="string"/> text source.
		/// </summary>
		/// <param name="input">
		///		The source text string.
		/// </param>
		/// <returns>
		///		Returns all match values locations of this TextPattern in the specified input string.
		/// </returns>
		public IEnumerable<string> GetMatchesLiteral(
			string input)
		{
			var matches = _regexPattern.Matches(input);
			if (matches.Count == 0)
			{
				yield break;
			}
			foreach (Match match in matches)
			{
				yield return match.Value;
			}
		}
	}
}