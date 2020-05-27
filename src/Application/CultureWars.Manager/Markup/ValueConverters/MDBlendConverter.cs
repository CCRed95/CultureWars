using System;
using System.Windows.Media;
using Ccr.Core.Extensions;
using Ccr.Xaml.Markup.Converters.Infrastructure;

namespace CultureWars.Manager.Markup.ValueConverters
{
	public class MDBlendConverter
		: XamlConverter<
			SolidColorBrush, 
			SolidColorBrush, 
			ConverterParam<double>, 
			SolidColorBrush>
	{
		/// <inheritdoc/>
		public override SolidColorBrush Convert(
			SolidColorBrush initialBrush,
			SolidColorBrush mixedBrush,
			ConverterParam<double> lightenOpacityParam)
		{
			if (initialBrush == null || mixedBrush == null)
				return null;

			var lightenOpacity = lightenOpacityParam.Value;

			if (lightenOpacity <= 0 || lightenOpacity >= 1)
				throw new ArgumentOutOfRangeException(
					nameof(lightenOpacity),
					lightenOpacity,
					@"The mixin opacity value must be >= 0 and <= 1");

			return BrushExtensions.Blend(
				initialBrush,
				mixedBrush,
				lightenOpacity);
		}

		public class MDLightenConverter
			: XamlConverter<
				SolidColorBrush,
				ConverterParam<double>,
				SolidColorBrush>
		{
			/// <inheritdoc/>
			public override SolidColorBrush Convert(
				SolidColorBrush initialBrush,
				ConverterParam<double> lightenOpacityParam)
			{
				var lightenOpacity = lightenOpacityParam.Value;

				if (lightenOpacity <= 0 || lightenOpacity >= 1)
					throw new ArgumentOutOfRangeException(
						nameof(lightenOpacity),
						lightenOpacity,
						@"The darken opacity value must be >= 0 and <= 1");

				return BrushExtensions.Darken(
					initialBrush,
					lightenOpacity);
			}
		}

		public class MDDarkerConverter
			: XamlConverter<
				SolidColorBrush,
				ConverterParam<double>,
				SolidColorBrush>
		{
			/// <inheritdoc/>
			public override SolidColorBrush Convert(
				SolidColorBrush initialBrush,
				ConverterParam<double> darkenOpacityParam)
			{
				var darkenOpacity = darkenOpacityParam.Value;

				if (darkenOpacity <= 0 || darkenOpacity >= 1)
					throw new ArgumentOutOfRangeException(
						nameof(darkenOpacity),
						darkenOpacity,
						@"The darken opacity value must be >= 0 and <= 1");

				return BrushExtensions.Darken(
					initialBrush,
					darkenOpacity);
			}
		}
	}
}