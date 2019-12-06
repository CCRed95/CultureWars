using System.Drawing;
using CultureWars.Core.FluentConsole.Colorization.Stylization;

namespace CultureWars.Core.FluentConsole.Builders
{
	public class StyleSheetBuilder
	{
		private static readonly Color _defaultColor = Color.Aqua;
		internal readonly StyleSheet _styleSheet;


		public static StyleSheetBuilder Build
		{
			get => new StyleSheetBuilder();
		}


		private StyleSheetBuilder()
			: this(
				new StyleSheet(
					_defaultColor))
		{
		}

		protected StyleSheetBuilder(
			StyleSheet baseStyleSheet)
		{
			_styleSheet = baseStyleSheet;
		}


		public static implicit operator StyleSheet(
			StyleSheetBuilder @this)
		{
			return @this._styleSheet;
		}

		#region old

		//public void AddStyle(
		//	string target,
		//	Color color,
		//	Styler.MatchFound matchHandler)
		//{
		//	_styleSheet
		//		.Styles.Add((StyleClass<TextPattern>)new Styler(target, color, matchHandler));
		//}

		//public void AddStyle(string target, Color color, Styler.MatchFoundLite matchHandler)
		//{
		//	this.Styles.Add((StyleClass<TextPattern>)new Styler(target, color, (Styler.MatchFound)((s, l, m) => matchHandler(m))));
		//}

		//public void AddStyle(string target, Color color)
		//{
		//	this.Styles.Add((StyleClass<TextPattern>)new Styler(target, color, (Styler.MatchFound)((s, l, m) => m)));
		//}

		#endregion

	}
}