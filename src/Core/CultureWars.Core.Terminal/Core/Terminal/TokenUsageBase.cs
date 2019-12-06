using System;
using CultureWars.Core.Terminal.Range;

namespace CultureWars.Core.Terminal
{
	public abstract class TokenUsageBase
		: ITextRangePointer
	{
		public ITextRangePointer TextPointer { get; }
		
		public abstract TokenBase TokenBase { get; }

		public abstract Type TokenType { get; }


		public uint StartIndex
		{
			get => TextPointer.StartIndex;
		}

		public uint EndIndex
		{
			get => TextPointer.EndIndex;
		}

		public uint Length
		{
			get => TextPointer.Length;
		}

		public string LiteralText
		{
			get => TextPointer.LiteralText;
		}
		

		protected TokenUsageBase(
			ITextRangePointer textPointer)
		{
			TextPointer = textPointer;
		}
	}
}