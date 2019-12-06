using System.Drawing;
using CultureWars.Core.FluentConsole.Builders;
using CultureWars.Core.FluentConsole.Colorization.Stylization;
using JetBrains.Annotations;

namespace CultureWars.Core.FluentConsole.Extensions
{
	public static class StyleSheetBuilderExtensions
	{
		public static StyleSheetBuilder WithUnstyledColor(
			this StyleSheetBuilder @this,
			Color color)
		{
			@this._styleSheet.UnstyledColor = color;
			return @this;
		}

		public static StyleSheetBuilder WithStyle(
			this StyleSheetBuilder @this,
			[RegexPattern] string pattern,
			Color color)
		{
			@this._styleSheet.AddStyle(pattern, color);
			return @this;
		}

		public static StyleSheetBuilder WithStyle(
			this StyleSheetBuilder @this,
			[RegexPattern] string pattern,
			Color color,
			MatchFound matchTransformer)
		{
			@this._styleSheet.AddStyle(pattern, color, matchTransformer);
			return @this;
		}

		public static StyleSheetBuilder WithStyle(
			this StyleSheetBuilder @this,
			[RegexPattern] string pattern,
			Color color,
			MatchFoundLite matchTransformer)
		{
			@this._styleSheet.AddStyle(pattern, color, matchTransformer);
			return @this;
		}
	}
}
