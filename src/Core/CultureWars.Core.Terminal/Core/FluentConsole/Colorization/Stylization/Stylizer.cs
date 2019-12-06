using System;
using System.Drawing;
using CultureWars.Core.FluentConsole.Patterning;
using JetBrains.Annotations;

namespace CultureWars.Core.FluentConsole.Colorization.Stylization
{
	/// <summary>
	///		A StyleClass instance that exposes a delegate instance which can be used for more
	///		customized styling.
	/// </summary>
	public class Stylizer
		: StyleClass<TextPattern>,
			IEquatable<Stylizer>
	{
		/// <summary>
		///		A delegate instance which can be used for custom styling.
		/// </summary>
		public MatchFound MatchFoundHandler { get; }

		/// <summary>
		///		Creates an instance of the <see cref="Stylizer"/> class.
		/// </summary>
		/// <param name="pattern">
		///		The string to be styled.
		/// </param>
		/// <param name="color">
		///		The color to be applied to the target.
		/// </param>
		/// <param name="matchHandler">
		///		A delegate instance which describes a transformation that can be applied to the target.
		/// </param>
		public Stylizer(
			[RegexPattern] string pattern,
			Color color,
			MatchFound matchHandler)
		{
			Target = new TextPattern(pattern);
			Color = color;
			MatchFoundHandler = matchHandler;
		}

		public bool Equals(Stylizer other)
		{
			if (other == null)
				return false;

			return base.Equals(other)
						 && MatchFoundHandler == other.MatchFoundHandler;
		}

		public override bool Equals(object obj) => Equals(obj as Stylizer);

		public override int GetHashCode()
		{
			int hash = base.GetHashCode();

			hash *= 79 + MatchFoundHandler.GetHashCode();

			return hash;
		}
	}
}