namespace CultureWars.Core.Terminal.Range
{
	public class TextRangePointer
		: ITextRangePointer
	{
		protected string _textSource;
		protected readonly uint _startIndex;
		protected readonly uint _length;
		protected string _literalText;


		public uint StartIndex
		{
			get => _startIndex;
		}

		public uint EndIndex
		{
			get => StartIndex + Length;
		}

		public uint Length
		{
			get => _length;
		}

		public string LiteralText
		{
			get => _literalText ??= _textSource.Substring((int)_startIndex, (int) _length);
		}


		public TextRangePointer(
			string textSource,
			uint startIndex,
			uint length)
		{
			_textSource = textSource;
			_startIndex = startIndex;
			_length = length;
		}
	}
}
