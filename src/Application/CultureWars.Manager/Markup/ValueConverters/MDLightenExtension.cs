using System;
using System.Windows.Markup;
using System.Windows.Media;
using Ccr.Core.Extensions;

namespace CultureWars.Manager.Markup.ValueConverters
{
	public class MDLightenExtension
		: MarkupExtension
	{
		private readonly SolidColorBrush _initialBrush;
		private readonly double _lightenOpacity;


		public MDLightenExtension(
			SolidColorBrush initialSwatch,
			double lightenOpacity)
		{
			_initialBrush = initialSwatch;

			if (lightenOpacity <= 0 || lightenOpacity >= 1)
				throw new ArgumentOutOfRangeException(
					nameof(lightenOpacity),
					lightenOpacity,
					@"The ligthen opacity value must be >= 0 and <= 1");

			_lightenOpacity = lightenOpacity;
		}


		public override object ProvideValue(
			IServiceProvider serviceProvider)
		{
			return BrushExtensions.Lighten(
				_initialBrush,
				_lightenOpacity);
		}
	}
}
