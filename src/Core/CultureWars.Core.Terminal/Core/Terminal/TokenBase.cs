using System;
using System.Text.RegularExpressions;
using JetBrains.Annotations;

namespace CultureWars.Core.Terminal
{
	public abstract class TokenBase
	{
		public Regex Regex { get; }

		public Func<string, bool> MatchPredicate { get; }


		protected TokenBase(
			[RegexPattern] string regexPattern)
		{
			Regex = new Regex(regexPattern);
		}

		protected TokenBase(
			Func<string, bool> matchPredicate)
		{
			MatchPredicate = matchPredicate;
		}


		public bool IsMatch(string text)
		{
			if (Regex != null)
				return Regex.IsMatch(text);

			if (MatchPredicate == null)
				throw new NotSupportedException();

			return MatchPredicate(text);

		}
	}
}