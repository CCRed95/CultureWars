using System;
using System.Drawing;
using System.Linq;
using CultureWars.Core.FluentConsole.Extensions;

namespace CultureWars.Core.FluentConsole.Colorization.Complex
{
	/// <summary>
	/// Exposes methods and properties used for alternating over a set of colors according to
	/// frequency of use.
	/// </summary>
	public sealed class FrequencyBasedColorAlternator
		: ColorAlternator,
			IPrototypable<FrequencyBasedColorAlternator>
	{
		private readonly int _alternationFrequency;
		private int _writeCount;


		/// <summary>
		/// Exposes methods and properties used for alternating over a set of colors according to
		/// frequency of use.
		/// </summary>
		/// <param name="alternationFrequency">
		/// The number of times GetNextColor must be called in order for the color to alternate.
		/// </param>
		/// <param name="colors">
		/// The set of colors over which to alternate.
		/// </param>
		public FrequencyBasedColorAlternator(
			int alternationFrequency, 
			params Color[] colors)
				: base(colors)
		{
			_alternationFrequency = alternationFrequency;
		}


		public new FrequencyBasedColorAlternator Prototype()
		{
			return new FrequencyBasedColorAlternator(
				_alternationFrequency,
				Colors.DeepCopy()
					.ToArray());
		}
		
		protected override ColorAlternator PrototypeCore()
		{
			return Prototype();
		}

		/// <summary>
		/// Alternates colors based on the number of times <see cref="GetNextColor"/> has been called.
		/// </summary>
		/// <param name="input">
		/// The string to be styled.
		/// </param>
		/// <returns>
		/// The current color of the <see cref="ColorAlternator"/>.
		/// </returns>
		public override Color GetNextColor(
			string input)
		{
			if (Colors.Length == 0)
				throw new InvalidOperationException(
					"No colors have been supplied over which to alternate.");
			
			var nextColor = Colors[_nextColorIndex];
			TryIncrementColorIndex();

			return nextColor;
		}

		protected override void TryIncrementColorIndex()
		{
			if (_writeCount >= Colors.Length * _alternationFrequency - 1)
			{
				_nextColorIndex = 0;
				_writeCount = 0;
			}
			else
			{
				_writeCount++;
				_nextColorIndex = (int) Math.Floor(_writeCount / (double) _alternationFrequency);
			}
		}
	}
}