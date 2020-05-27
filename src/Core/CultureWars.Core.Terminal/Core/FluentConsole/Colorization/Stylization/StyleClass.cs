using System;
using System.Drawing;

namespace CultureWars.Core.FluentConsole.Colorization.Stylization
{
	/// <summary>
	///	Exposes methods and properties that represent a style classification.
	/// </summary>
	/// <typeparam name="TValue">
	///	The type of the <see cref="Target"/>.
	/// </typeparam>
	public class StyleClass<TValue>
		: IEquatable<StyleClass<TValue>>
	{
		/// <summary>
		///	The object to be styled.
		/// </summary>
		public TValue Target { get; set; }

		/// <summary>
		///	The color to be applied to the target.
		/// </summary>
		public Color Color { get; set; }


		/// <summary>
		///	Exposes methods and properties that represent a style classification.
		/// </summary>
		public StyleClass()
		{
		}

		/// <summary>
		///	Exposes methods and properties that represent a style classification.
		/// </summary>
		/// <param name="target">
		///	The object to be styled.
		/// </param>
		/// <param name="color">
		///	The color to be applied to the target.
		/// </param>
		public StyleClass(
			TValue target,
			Color color)
		{
			Target = target;
			Color = color;
		}


		public bool Equals(
			StyleClass<TValue> other)
		{
			if (other == null)
				return false;
			
			return Target.Equals(other.Target)
				&& Color == other.Color;
		}

		public override bool Equals(object obj)
		{
			return Equals(obj as StyleClass<TValue>);
		}

		public override int GetHashCode()
		{
			var hash = 163;

			hash *= 79 + Target.GetHashCode();
			hash *= 79 + Color.GetHashCode();

			return hash;
		}
	}
}