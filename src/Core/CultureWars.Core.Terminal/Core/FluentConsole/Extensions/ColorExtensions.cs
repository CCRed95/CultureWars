﻿using System;
using System.Drawing;

namespace CultureWars.Core.FluentConsole.Extensions
{
	/// <summary>
	/// <see cref="Color"/> extension methods.
	/// </summary>
	public static class ColorExtensions
	{
		/// <summary>
		/// Converts the specified <see cref="Color"/> to it's nearest <see cref="ConsoleColor"/> equivalent.
		/// </summary>
		/// <remarks>
		///	Code taken from Glenn Slayden at https://stackoverflow.com/questions/1988833/converting-color-to-consolecolor
		/// </remarks>
		public static ConsoleColor ToNearestConsoleColor(
			this Color color)
		{
			ConsoleColor closestConsoleColor = 0;
			var delta = double.MaxValue;

			foreach (ConsoleColor consoleColor in Enum.GetValues(typeof(ConsoleColor)))
			{
				var consoleColorName = Enum.GetName(typeof(ConsoleColor), consoleColor);

				consoleColorName = string.Equals(
					consoleColorName, 
					nameof(ConsoleColor.DarkYellow), 
					StringComparison.Ordinal)
						? nameof(Color.Orange)
						: consoleColorName;

				var rgbColor = Color.FromName(consoleColorName);
				var sum = Math.Pow(rgbColor.R - color.R, 2.0)
					+ Math.Pow(rgbColor.G - color.G, 2.0) 
					+ Math.Pow(rgbColor.B - color.B, 2.0);

				var epsilon = 0.001;

				if (sum < epsilon)
					return consoleColor;

				if (sum < delta)
				{
					delta = sum;
					closestConsoleColor = consoleColor;
				}
			}
			return closestConsoleColor;
		}
	}
}