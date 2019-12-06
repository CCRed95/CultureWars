namespace CultureWars.Core.Terminal.Range
{
	public interface ITextRangePointer
		: ITextPointer
	{
		uint EndIndex { get; }

		uint Length { get; }

		string LiteralText { get; }
	}
}