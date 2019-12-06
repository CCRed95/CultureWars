using System;
using System.ComponentModel;
using System.Drawing;

namespace CultureWars.Core.FluentConsole.Geometry
{
	public class TableCellGeometry
	{
		public int Column { get; }

		public int Row { get; }

		public int Width { get; }

		public int Height { get; }


		public Rectangle Rect
		{
			get => new Rectangle(
				new Point(Column, Row),
				new Size(Width, Height));
		}


		public TableCellGeometry(
			int column,
			int row,
			int width,
			int height)
		{
			Column = column;
			Row = row;
			Width = width;
			Height = height;
		}

		public IExtendedConsoleSession WriteToCell(
			object element,
			Color color,
			ContentAlignment alignment = ContentAlignment.Left)
		{
			var content = element == null 
				? "<null>" 
				: element.ToString();

			(int top, int left) getConsoleAlignment(
				ContentAlignment _alignment)
			{
				switch (_alignment)
				{
					case ContentAlignment.Left:
					{
						return (top: Row, left: Column);
					}
					case ContentAlignment.Center:
					{
						var contentLength = content.Length;
						var leftOffset = (int) Math.Floor(contentLength / 2d);
						return (top: Row, left: Column + leftOffset);
					}
					case ContentAlignment.Right:
					{
						var contentLength = content.Length;
						var leftOffset = Width - contentLength;
						return (top: Row, left: Column + leftOffset);
					}
					default:
						throw new InvalidEnumArgumentException();
				}
			}
			var position = getConsoleAlignment(alignment);

			ExtendedConsole.console.CursorTop = position.top;
			ExtendedConsole.console.CursorLeft = position.left;

			return ExtendedConsole.console.Write(content, color);
		}
	}
}