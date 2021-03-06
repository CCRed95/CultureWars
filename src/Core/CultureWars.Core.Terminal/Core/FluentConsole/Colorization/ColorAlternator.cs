﻿using System.Drawing;

namespace CultureWars.Core.FluentConsole.Colorization
{
	/// <summary>
	/// Exposes methods and properties used for alternating over a set of colors.
	/// </summary>
	public abstract class ColorAlternator
		: IPrototypable<ColorAlternator>
	{
		protected int _nextColorIndex = 0;


		/// <summary>
		/// The set of colors over which to alternate.
		/// </summary>
		public Color[] Colors { get; set; }



		/// <summary>
		/// Exposes methods and properties used for alternating over a set of colors.
		/// </summary>
		protected ColorAlternator()
		{
			Colors = new Color[] { };
		}

		/// <summary>
		/// Exposes methods and properties used for alternating over a set of colors.
		/// </summary>
		protected ColorAlternator(
			params Color[] colors)
		{
			Colors = colors;
		}


		public ColorAlternator Prototype()
		{
			return PrototypeCore();
		}

		protected abstract ColorAlternator PrototypeCore();

		/// <summary>
		/// Alternates colors based on the state of the ColorAlternator instance.
		/// </summary>
		/// <param name="input">
		/// The string to be styled.
		/// </param>
		/// <returns>
		/// The current color of the ColorAlternator.
		/// </returns>
		public abstract Color GetNextColor(string input);

		protected abstract void TryIncrementColorIndex();
	}
}