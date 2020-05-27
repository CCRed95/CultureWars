using System.Collections.Generic;
using Ccr.Std.Core.Extensions;
using static CultureWars.Core.FluentConsole.ExtendedConsole;

namespace CultureWars.Core.FluentConsole.Geometry
{
	public class TableGeometry
	{
		public int Width { get; }

		public int Height { get; }

		public CursorLocation CursorLocation { get; }


		public TableGeometry(
			int width,
			int height,
			int column,
			int row)
		{
			Width = width;
			Height = height;
			CursorLocation = new CursorLocation(
				column,
				row);
		}
		

		public IEnumerable<TableCellGeometry> GetCells(
			int cellCount,
			int windowWidth)
		{
			(int left, int width) getColumnSpanAtIndex(int columnIndex)
			{
				var commonColumnWidthExact = (double)windowWidth / cellCount;

				var columnOffset = (int)(commonColumnWidthExact * columnIndex).Round();
				var columnOffsetNext = (int)(commonColumnWidthExact * (columnIndex + 1)).Round();
				var effectiveColumnWidth = columnOffsetNext - columnOffset;

				return (left: columnOffset, width: effectiveColumnWidth);
			}

			var currentTop = XConsole.CursorTop;

			for (var i = 0; i < cellCount; i++)
			{
				var columnSpan = getColumnSpanAtIndex(i);

				yield return new TableCellGeometry(
					columnSpan.left,
					currentTop,
					columnSpan.width,
					1);
			}
		}
	}
}
