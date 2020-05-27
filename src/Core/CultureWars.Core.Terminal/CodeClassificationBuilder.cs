using System;
using System.Drawing;
using Ccr.Std.Core.Extensions;
using CultureWars.Core.Builders;

namespace CultureWars
{
	public class CodeClassificationBuilder
		: IFluentBuilder<CodeClassification>,
			ICodeClassificationBuilder
	{
		private static readonly ColorConverter _colorConverter = new ColorConverter();


		private bool? _isBold;
		private Color? _foregroundColor;
		private Color? _backgroundColor;
		private ClassificationScope? _scope;
		private ClassificationLanguage? _language;


		CodeClassificationBuilder ICodeClassificationBuilder.WithForeground(
			Color foregroundColor)
		{
			_foregroundColor.EnsureNotAssigned(nameof(foregroundColor));
			_foregroundColor = foregroundColor;

			return this;
		}

		CodeClassificationBuilder ICodeClassificationBuilder.WithForeground(
			string foregroundColorHex)
		{
			var foregroundColor = parseInverseHex(foregroundColorHex);

			return this
				.As<ICodeClassificationBuilder>()
				.WithForeground(foregroundColor);
		}

		CodeClassificationBuilder ICodeClassificationBuilder.WithBackground(
			Color backgroundColor)
		{
			_backgroundColor.EnsureNotAssigned(nameof(backgroundColor));
			_backgroundColor = backgroundColor;

			return this;
		}

		CodeClassificationBuilder ICodeClassificationBuilder.WithBackground(
			string backgroundColorHex)
		{
			var backgroundColor = _colorConverter
				.ConvertFrom(backgroundColorHex)
				.As<Color>();

			return this.As<ICodeClassificationBuilder>()
				.WithBackground(backgroundColor);
		}

		CodeClassificationBuilder ICodeClassificationBuilder.IsBold(
			bool isBold)
		{
			_isBold.EnsureNotAssigned(nameof(isBold));
			_isBold = isBold;

			return this;
		}

		CodeClassificationBuilder ICodeClassificationBuilder.IsInClassificationScope(
			ClassificationScope scope)
		{
			_scope.EnsureNotAssigned(nameof(scope));
			_scope = scope;

			return this;
		}

		CodeClassificationBuilder ICodeClassificationBuilder.ClassifiesLanguage(
			ClassificationLanguage language)
		{
			_language.EnsureNotAssigned(nameof(language));
			_language = language;
			
			return this;
		}

		private static Color parseInverseHex(
			string inverseHexText)
		{
			if (inverseHexText.Length == 7)
			{
				if (!inverseHexText.StartsWith('#'))
					throw new FormatException(
						$"Invalid inverse hex {inverseHexText.Quote()} is not valid, as it must begin with a #.");

				var inverseHexValues = inverseHexText.Substring(1);

				var bHexValue = inverseHexValues.Substring(0, 2);
				var gHexValue = inverseHexValues.Substring(2, 2);
				var rHexValue = inverseHexValues.Substring(4, 2);

				var hexString = $"#{rHexValue}{gHexValue}{bHexValue}";

				var convertedColor = _colorConverter
					.ConvertFrom(hexString)
					.As<Color>();

				return convertedColor;
			}
			else if (inverseHexText.Length == 9)
			{
				throw new NotImplementedException();
			}
			else
			{
				throw new FormatException();
			}
		} 



		public static implicit operator CodeClassification(
			CodeClassificationBuilder @this)
		{
			return @this.Build();
		}

		
		/// <inheritdoc />
		public CodeClassification Build()
		{
			var foregroundColor = _foregroundColor.EnsureAssigned(nameof(_foregroundColor));
			var backgroundColor = _backgroundColor.EnsureAssigned(nameof(_backgroundColor));
			var isBold = _isBold.EnsureAssigned(nameof(_isBold)); 
			var scope = _scope.EnsureAssigned(nameof(_scope));
			var language = _language.EnsureAssigned(nameof(_language));

			return new CodeClassification(
				"",
				foregroundColor,
				backgroundColor, 
				isBold,
				scope,
				language);
		}
	}
}