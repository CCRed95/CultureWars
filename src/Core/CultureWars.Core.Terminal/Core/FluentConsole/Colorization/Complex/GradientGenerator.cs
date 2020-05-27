using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using CultureWars.Core.FluentConsole.Colorization.Stylization;

namespace CultureWars.Core.FluentConsole.Colorization.Complex
{
	public sealed class GradientGenerator
	{
		public List<StyleClass<TValue>> GenerateGradient<TValue>(
			IEnumerable<TValue> input,
			Color startColor,
			Color endColor,
			int maxColorsInGradient)
		{
			var inputAsList = input.ToList();
			var numberOfGrades = inputAsList.Count / maxColorsInGradient;
			var numberOfGradesRemainder = inputAsList.Count % maxColorsInGradient;

			var gradients = new List<StyleClass<TValue>>();
			var previousColor = Color.Empty;
			var previousItem = default(TValue);

			// An attempt to make the gradient symmetric in the event that maxColorsInGradient does not
			// divide input.Count evenly.
			int SetProgressSymmetrically(int remainder)
			{
				return remainder > 1 ? -1 : 0;
			}

			// An attempt to make the gradient symmetric in the event that maxColorsInGradient does not
			// divide input.Count evenly.
			int ResetProgressSymmetrically(int progress)
			{
				return progress == 0 ? -1 : 0;
			}

			bool IsFirstRun(int index)
			{
				return index == 0;
			}

			bool ShouldChangeColor(
				int index,
				int progress, 
				TValue current, 
				TValue previous)
			{
				return progress > numberOfGrades - 1 
					&& !current.Equals(previous) || IsFirstRun(index);
			}

			bool CanChangeColor(int changeCount)
			{
				return changeCount < maxColorsInGradient;
			}

			
			var colorChangeProgress = SetProgressSymmetrically(numberOfGradesRemainder);
			var colorChangeCount = 0;

			for (var i = 0; i < inputAsList.Count; i++)
			{
				var currentItem = inputAsList[i];
				colorChangeProgress++;

				if (ShouldChangeColor(i, colorChangeProgress, currentItem, previousItem)
					&& CanChangeColor(colorChangeCount))
				{
					previousColor = GetGradientColor(i, startColor, endColor, inputAsList.Count);
					previousItem = currentItem;
					colorChangeProgress = ResetProgressSymmetrically(colorChangeProgress);

					colorChangeCount++;
				}
				gradients.Add(
					new StyleClass<TValue>(currentItem, previousColor));
			}

			return gradients;
		}

		private Color GetGradientColor(
			int index, 
			Color startColor, 
			Color endColor, 
			int numberOfGrades)
		{
			var numberOfGradesAdjusted = numberOfGrades - 1;

			var rDistance = startColor.R - endColor.R;
			var gDistance = startColor.G - endColor.G;
			var bDistance = startColor.B - endColor.B;

			var r = startColor.R + -rDistance * ((double) index / numberOfGradesAdjusted);
			var g = startColor.G + -gDistance * ((double) index / numberOfGradesAdjusted);
			var b = startColor.B + -bDistance * ((double) index / numberOfGradesAdjusted);

			var graded = Color.FromArgb((int) r, (int) g, (int) b);

			return graded;
		}
	}
}