using System;
using System.Collections.Concurrent;
using System.Drawing;
using System.Linq;

namespace CultureWars.Core.FluentConsole.Colorization
{
	/// <summary>
	///		Stores and manages the assignment of Color objects to ConsoleColor objects.
	/// </summary>
	public sealed class ColorStore
	{
		/// <summary>
		///		A map from Color to ConsoleColor.
		/// </summary>
		public ConcurrentDictionary<Color, ConsoleColor> Colors { get; }

		/// <summary>
		///		A map from ConsoleColor to Color.
		/// </summary>
		public ConcurrentDictionary<ConsoleColor, Color> ConsoleColors { get; }

		
		/// <summary>
		///		Manages the assignment of System.Drawing.Color objects to ConsoleColor objects.
		/// </summary>
		/// <param name="colorMap">
		///		The Dictionary the ColorStore should use to key Color objects to ConsoleColor objects.
		/// </param>
		/// <param name="consoleColorMap">
		///		The Dictionary the ColorStore should use to key ConsoleColor objects to Color objects.
		/// </param>
		public ColorStore(
			ConcurrentDictionary<Color, ConsoleColor> colorMap,
			ConcurrentDictionary<ConsoleColor, Color> consoleColorMap)
		{
			Colors = colorMap;
			ConsoleColors = consoleColorMap;
		}

		public ColorStore(
			params (ConsoleColor consoleColor, Color color)[] colorMappings)
		{
			Colors = new ConcurrentDictionary<Color, ConsoleColor>(
				colorMappings
					.ToDictionary(
						t => t.color,
						t => t.consoleColor));

			ConsoleColors = new ConcurrentDictionary<ConsoleColor, Color>(
				colorMappings
					.ToDictionary(
						t => t.consoleColor,
						t => t.color));
		}


		/// <summary>
		///		Adds a new Color to the ColorStore.
		/// </summary>
		/// <param name="oldColor">
		///		The ConsoleColor to be replaced by the new Color.
		/// </param>
		/// <param name="newColor">
		/// The Color to be added to the ColorStore.
		/// </param>
		public void Update(
			ConsoleColor oldColor, 
			Color newColor)
		{
			Colors.TryAdd(newColor, oldColor);
			ConsoleColors[oldColor] = newColor;
		}

		/// <summary>
		///		Replaces one Color in the ColorStore with another.
		/// </summary>
		/// <param name="oldColor">
		///		The color to be replaced.
		/// </param>
		/// <param name="newColor">
		///		The replacement color.
		/// </param>
		/// <returns>
		///		The ConsoleColor key of the Color object that was replaced in the ColorStore.
		/// </returns>
		public ConsoleColor Replace(
			Color oldColor, 
			Color newColor)
		{
			var oldColorExistedInColorStore = Colors.TryRemove(oldColor, out var consoleColorKey);

			if (!oldColorExistedInColorStore)
				throw new ArgumentException(
					"An attempt was made to replace a nonexistent color in the ColorStore!");
			
			Colors.TryAdd(newColor, consoleColorKey);
			ConsoleColors[consoleColorKey] = newColor;

			return consoleColorKey;
		}

		/// <summary>
		///		Notifies the caller whether or not the specified Color needs to be added to the ColorStore.
		/// </summary>
		/// <param name="color">
		///		The Color to be checked for membership.
		/// </param>
		/// <returns>
		///		Returns true if the ColorStore already contains the specified Color.
		/// </returns>
		public bool RequiresUpdate(Color color)
		{
			return !Colors.ContainsKey(color);
		}
	}
}