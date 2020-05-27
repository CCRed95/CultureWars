using System;

namespace CultureWars.Core.FluentConsole
{
	/// <summary>
	///	Class that represents a token in a string.
	/// </summary>
	public class TextMatchDescriptor
		: IEquatable<TextMatchDescriptor>,
			IComparable<TextMatchDescriptor>,
			IPrototypable<TextMatchDescriptor>
	{
		/// <summary>
		///	Reference to the original document source string.
		/// </summary>
		private readonly string _sourceString;

		private string _literalTextCache;


		/// <summary>
		///	Gets the position in the template string this token starts at.
		/// </summary>
		public int Index { get; }

		/// <summary>
		///	The length, in number of characters, of the token.
		/// </summary>
		public int Length { get; }

		/// <summary>
		///	Gets the position in the template string this token ends at.
		/// </summary>
		public int EndIndex
		{
			get => Index + Length;
		}


		/// <param name="index">
		/// Gets the position in the template string this token starts at.
		/// </param>
		/// <param name="length">
		/// The length, in number of characters, of the token.
		/// </param>
		/// <param name="sourceString">
		/// Reference to the original document source string.
		/// </param>
		public TextMatchDescriptor(
			int index,
			int length,
			string sourceString)
		{
			Index = index;
			Length = length;
			_sourceString = sourceString;
		}


		public string GetText()
		{
			return _literalTextCache ??
				(_literalTextCache = _sourceString.Substring(Index, Length));
		}
		
		public TextMatchDescriptor Prototype()
		{
			return new TextMatchDescriptor(
				Index,
				Length,
				_sourceString);
		}
		
		public bool Equals(TextMatchDescriptor other)
		{
			if (other == null)
				return false;
			
			return Index == other.Index
				&& EndIndex == other.EndIndex;
		}

		public override bool Equals(object obj)
		{
			return Equals(obj as TextMatchDescriptor);
		}

		public override int GetHashCode()
		{
			var hash = 163;

			hash *= 79 + Index.GetHashCode();
			hash *= 79 + EndIndex.GetHashCode();

			return hash;
		}

		public int CompareTo(TextMatchDescriptor other)
		{
			return Index.CompareTo(other.Index);
		}

		public override string ToString()
		{
			return $"[{Index} -> {EndIndex}] " +
				$"(Length: {Length})";
		}
	}
}