using System.Collections.Generic;
using System.Linq;

namespace CultureWars.Core.FluentConsole.Extensions
{
	internal static class ExtensionMethods
	{
		internal static T Proto<T>(
			this T @this)
				where T
				: IPrototypable<T>
		{
			return @this.Prototype();
		}

		internal static IEnumerable<T> Proto<T>(
			this IEnumerable<T> @this)
				where T 
					: IPrototypable<T>
		{
			return @this.Select(
				t => t.Prototype());
		}

		internal static IEnumerable<T> DeepCopy<T>(
			this IEnumerable<T> @this)
				where T 
					: struct
		{
			foreach (var item in @this)
			{
				yield return item;
			}
		}

		/// <summary>
		/// Convenience wrapper around the String.Join method.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="input"></param>
		/// <returns></returns>
		internal static string AsString<T>(
			this T input)
		{
			// Cast to dynamic due to type inference shortcomings.  If input is an array,
			// and we pass it to the String.Join method (which takes arguments of type 'string'
			// and 'params object[]'), the compiler will think that String.Join is
			// being passed an array of arrays.  This is not necessarily incorrect, but I think
			// that in most cases, if we're passing an array into String.Join, the intention is
			// for it to be "unrolled" into a collection of the array's elements, rather than the
			// default behavior described previously.
			return string.Join(string.Empty, (dynamic) input);
		}

		// TODO: NO DYNAMIC IN .NET CORE
		/// <summary>
		/// Takes a single object (which could be a 1-dimensional array) and returns it (or, potentially,
		/// all of its elements) as an element of an array of the corresponding type.
		/// </summary>
		/// <typeparam name="T">
		///
		/// </typeparam>
		/// <param name="input">
		/// The object which will be transformed into an array.
		/// </param>
		/// <returns>
		/// An array of a certain type, as dynamic.
		/// </returns>
		internal static dynamic Normalize<T>(
			this T input)
		{
			// See the AsString<T> method for a comment relating to part of the dynamic return type
			// of this method.

			var output = new List<dynamic>();
			var inputAsArray = input as dynamic[];

			if (inputAsArray != null)
			{
				output.AddRange(inputAsArray);
			}
			else
			{
				output.Add((dynamic) input);
			}

			return output.ToArray();
		}
	}
}