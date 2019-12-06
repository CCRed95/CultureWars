using System;

namespace CultureWars.Core.FluentConsole
{
	/// <summary>
	///		Class that represents a token in a string.
	/// </summary>
	public class TextMatchDescriptor
		: IEquatable<TextMatchDescriptor>,
		  IComparable<TextMatchDescriptor>,
		  IPrototypable<TextMatchDescriptor>
	{
		/// <summary>
		///		Gets the position in the template string this token starts at.
		/// </summary>
		public int Index { get; }

		/// <summary>
		///		The length, in number of characters, of the token.
		/// </summary>
		public int Length { get; }


		/// <summary>
		///		Gets the position in the template string this token ends at.
		/// </summary>
		public int EndIndex
		{
			get => Index + Length;
		}

		/// <summary>
		///		Reference to the original document source string.
		/// </summary>
		private readonly string _sourceString;

		private string _literalTextCache;


		/// <param name="index">
		/// 		Gets the position in the template string this token starts at.
		///  </param>
		///  <param name="length">
		/// 		The length, in number of characters, of the token.
		///  </param>
		///  <param name="sourceString">
		/// 		Reference to the original document source string.
		///  </param>
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



	///// <summary>
	/////		Describes the location of a match of a particular Pattern within a source.
	///// </summary>
	//public class MatchLocation
	//	: IEquatable<MatchLocation>,
	//	  IComparable<MatchLocation>,
	//	  IPrototypable<MatchLocation>
	//{
	//	/// <summary>
	//	///		The index of the beginning of the pattern match.
	//	/// </summary>
	//	public int StartIndex { get; }

	//	/// <summary>
	//	///		The index of the end of the pattern match.
	//	/// </summary>
	//	public int EndIndex { get; }


	//	/// <summary>
	//	///		Exposes properties describing the indices of the beginning and end of a pattern match.
	//	/// </summary>
	//	/// <param name="startIndex">
	//	///		The index of the beginning of the pattern match.
	//	/// </param>
	//	/// <param name="endIndex">
	//	///		The index of the end of the pattern match.
	//	/// </param>
	//	public MatchLocation(
	//		int startIndex, 
	//		int endIndex)
	//	{
	//		StartIndex = startIndex;
	//		EndIndex = endIndex;
	//	}


	//	public MatchLocation Prototype()
	//	{
	//		return new MatchLocation(StartIndex, EndIndex);
	//	}


	//	public bool Equals(MatchLocation other)
	//	{
	//		if (other == null)
	//			return false;
			
	//		return StartIndex == other.StartIndex 
	//		       && EndIndex == other.EndIndex;
	//	}

	//	public override bool Equals(object obj)
	//	{
	//		return Equals(obj as MatchLocation);
	//	}

	//	public override int GetHashCode()
	//	{
	//		var hash = 163;

	//		hash *= 79 + StartIndex.GetHashCode();
	//		hash *= 79 + EndIndex.GetHashCode();

	//		return hash;
	//	}

	//	public int CompareTo(MatchLocation other)
	//	{
	//		return StartIndex.CompareTo(other.StartIndex);
	//	}

	//	public override string ToString()
	//	{
	//		return $"[{StartIndex} -> {EndIndex}] " +
	//		       $"(Length:{EndIndex - StartIndex - 1}";
	//	}
	//}
}