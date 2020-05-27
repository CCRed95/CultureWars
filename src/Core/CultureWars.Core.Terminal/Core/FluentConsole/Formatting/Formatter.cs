using System.Drawing;
using CultureWars.Core.FluentConsole.Colorization.Stylization;
using JetBrains.Annotations;

namespace CultureWars.Core.FluentConsole.Formatting
{
	/// <summary>
	///	Exposes properties representing an object and its color. This is a convenience wrapper
	///	around the StyleClass type, so you don't have to provide the type argument each time.
	/// </summary>
	public sealed class Formatter
	{
		private readonly StyleClass<string> _backingClass;


		/// <summary>
		///	The object to be styled.
		/// </summary>
		public object Target
		{
			get => _backingClass.Target;
		}

		/// <summary>
		///	The color to be applied to the target.
		/// </summary>
		public Color Color
		{
			get => _backingClass.Color;
		}
		

		/// <summary>
		///	Exposes properties representing an object and its color.  This is a convenience wrapper
		///	around the StyleClass type, so you don't have to provide the type argument each time.
		/// </summary>
		/// <param name="target">
		///	The object to be styled.
		/// </param>
		/// <param name="color">
		///	The color to be applied to the target.
		/// </param>
		public Formatter(
			[RegexPattern] string target, 
			Color color)
		{
			_backingClass = new StyleClass<string>(target, color);
		}
	}
}