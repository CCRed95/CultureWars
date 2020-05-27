using System;
using System.Windows.Markup;
using System.Windows.Media;
using Ccr.Core.Extensions;

namespace CultureWars.Manager.Markup.ValueConverters
{
	public class MDDarkenExtension
		: MarkupExtension
	{
		private readonly SolidColorBrush _initialBrush;
		private readonly double _darkenOpacity;


		public MDDarkenExtension(
			SolidColorBrush initialSwatch,
			double darkenOpacity)
		{
			_initialBrush = initialSwatch;

			if (darkenOpacity <= 0 || darkenOpacity >= 1)
				throw new ArgumentOutOfRangeException(
					nameof(darkenOpacity),
					darkenOpacity,
					@"The darken opacity value must be >= 0 and <= 1");

			_darkenOpacity = darkenOpacity;
		}


		public override object ProvideValue(
			IServiceProvider serviceProvider)
		{
			return BrushExtensions.Darken(
				_initialBrush,
				_darkenOpacity);
		}
	}
}