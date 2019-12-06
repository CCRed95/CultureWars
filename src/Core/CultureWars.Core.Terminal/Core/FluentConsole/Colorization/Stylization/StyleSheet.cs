using System.Collections.Generic;
using System.Drawing;
using CultureWars.Core.FluentConsole.Patterning;
using JetBrains.Annotations;

namespace CultureWars.Core.FluentConsole.Colorization.Stylization
{
	/// <summary>
	///		Exposes a collection of style classifications which can be used to style text.
	/// </summary>
	public sealed class StyleSheet
	{
		/// <summary>
		///		The StyleSheet's collection of style classifications.
		/// </summary>
		public List<StyleClass<TextPattern>> Styles { get; }

		/// <summary>
		///		The color to be associated with unstyled text.
		/// </summary>
		public Color UnstyledColor { get; set; }


		/// <summary>
		///		Exposes a collection of style classifications which can be used to style text.
		/// </summary>
		/// <param name="defaultColor">
		///		The color to be associated with unstyled text.
		/// </param>
		public StyleSheet(
			Color defaultColor)
		{
			Styles = new List<StyleClass<TextPattern>>();
			UnstyledColor = defaultColor;
		}


		/// <summary>
		///		Adds a style classification to the StyleSheet.
		/// </summary>
		/// <param name="target">
		///		The string to be styled.
		/// </param>
		/// <param name="color">
		///		The color to be applied to the target.
		/// </param>
		/// <param name="matchHandler">
		///		A delegate instance which describes a transformation that can be applied to the target.
		/// </param>
		public void AddStyle(
			string target,
			Color color,
			MatchFound matchHandler)
		{
			var styler = new Stylizer(target, color, matchHandler);
			Styles.Add(styler);
		}

		/// <summary>
		///		Adds a style classification to the StyleSheet.
		/// </summary>
		/// <param name="pattern">
		///		The string to be styled.</param>
		/// <param name="color">
		///		The color to be applied to the target.
		///	</param>
		/// <param name="matchHandler">
		///		A delegate describing a simple transformation that will be applied to the target.
		/// </param>
		public void AddStyle(
			[RegexPattern] string pattern, 
			Color color, 
			MatchFoundLite matchHandler)
		{
			string Wrapper(string s, TextMatchDescriptor l, string m) => matchHandler.Invoke(m);
			var stylizer = new Stylizer(pattern, color, Wrapper);
			Styles.Add(stylizer);
		}

		/// <summary>
		///		Adds a style classification to the StyleSheet.
		/// </summary>
		/// <param name="target">
		///		The string to be styled.
		/// </param>
		/// <param name="color">
		///		The color to be applied to the target.
		/// </param>
		public void AddStyle(
			[RegexPattern] string target, 
			Color color)
		{
			string Handler(string s, TextMatchDescriptor l, string m) => m;
			var stylizer = new Stylizer(target, color, Handler);
			Styles.Add(stylizer);
		}
	}
}