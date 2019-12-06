using System;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace CultureWars.Core.Extensions
{
	public static class StringExtensions
	{
		public static string RemoveFromEnd(
			this string @this,
			int length)
		{
			return @this.Substring(@this.Length - length);
		}

		public static string RemoveFileExtension(
			this string @this)
		{
			var strLength = @this.LastIndexOf('.') - 1;
			return @this.Substring(0, strLength);
		}


		public static string Repeat(
			this string @this,
			int count)
		{
			var sb = new StringBuilder();
			for (var x = 0; x < count; x++)
			{
				sb.Append(@this);
			}
			return sb.ToString();
		}

		public static string RepeatFill(
			this string @this,
			int totalColumns)
		{
			var sb = new StringBuilder();
			var stringLength = @this.Length;
			var count = (double)totalColumns / stringLength;
			var countInt = (int)Math.Floor(count);

			for (var x = 0; x < countInt; x++)
			{
				sb.Append(@this);
			}
			if (totalColumns - countInt > 0)
			{
				var repeatEndChars = totalColumns - countInt;
				sb.Append(@this.Substring(repeatEndChars));
			}
			return sb.ToString();
		}

		//public static string ToTitleCase(
		//	this string @this)
		//{
		//	var result = @this;
		//	var matches = Regex.Matches(@this, @"(\w|[^\u0000-\u007F])+'?\w*");

		//	foreach (Match word in matches)
		//	{
		//		if (!allCapitals(word.Value))
		//		{
		//			result = replaceWithTitleCase(word, result);
		//		}
		//	}

		//	return result;
		//}

		private static bool allCapitals(
			string input)
		{
			return input.ToCharArray()
				.All(char.IsUpper);
		}

		private static string replaceWithTitleCase(
			Match word, 
			string source)
		{
			var wordToConvert = word.Value;
			var replacement = char.ToUpper(
				wordToConvert[0]) + wordToConvert.Remove(0, 1).ToLower();

			return source.Substring(0, word.Index)
				+ replacement 
				+ source.Substring(
					word.Index + word.Length);
		}
	}
}
