namespace CultureWars.Core.FluentConsole.Geometry
{
	public class CursorLocation
	{
		public int Column { get; }

		public int Row { get; }


		public CursorLocation(
			int column,
			int row)
		{
			Column = column;
			Row = row;
		}
	}
}