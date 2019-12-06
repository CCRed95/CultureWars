using System.Drawing;
using System.Linq;
using Ccr.Std.Core.Extensions;
using CultureWars.Core.Extensions;
using JetBrains.Annotations;
using ContentAlignment = CultureWars.Core.FluentConsole.Geometry.ContentAlignment;

namespace CultureWars.Core.FluentConsole
{
	public abstract class TableWriter
		: ITableWriter
	{
		public IExtendedConsoleSession Session { get; }


		protected TableWriter(
			[NotNull] IExtendedConsoleSession session)
		{
			session.IsNotNull(nameof(session));
			Session = session;
		}


		protected ITableWriter WriteRecordBase(
			params object[] args)
		{
			var tableGeometry = Session.GetTableGeometry();
			var cells = tableGeometry
				.GetCells(args.Length, Session.WindowWidth)
				.ToArray();

			Session.WriteLine();

			var index = 0;

			foreach (var arg in args)
			{
				var targetCell = cells[index];
				targetCell.WriteToCell(arg, Color.DarkTurquoise, ContentAlignment.Center);
				//Session.CursorTop = targetCell.Row;
				//Session.CursorLeft = targetCell.Column;
				//Session.Write(arg, Color.DarkTurquoise);
				index++;
			}
			Session.WriteLine();
			return this;
		}

		protected ITableWriter WriteRecordHeadersBase(
			params string[] headers)
		{
			var tableGeometry = Session.GetTableGeometry();
			var cells = tableGeometry
				.GetCells(headers.Length, Session.WindowWidth)
				.ToArray();

			var index = 0;

			foreach (var header in headers)
			{
				var targetCell = cells[index];

				targetCell.WriteToCell(header, Color.MediumSlateBlue, ContentAlignment.Center);
				//Session.CursorTop = targetCell.Row;
				//Session.CursorLeft = targetCell.Column;

				//Session.Write(header, Color.MediumSlateBlue);
				index++;
			}
			Session
				.WriteLine()
				.WriteLine("-".RepeatFill(Session.BufferWidth), Color.DodgerBlue);
			return this;
		}
	}

	public class TableWriter<T0>
		: TableWriter,
			ITableWriter<T0>
	{
		public TableWriter(
			[NotNull] IExtendedConsoleSession session)
				: base(
					session)
		{
		}


		public ITableWriter<T0> WriteRecord(
			T0 arg0)
		{
			WriteRecordBase(arg0);
			return this;
		}

		public ITableWriter<T0> WriteRecordHeaders(
			string h0)
		{
			WriteRecordHeadersBase(h0);
			return this;
		}
	}

	public class TableWriter<T0, T1>
		: TableWriter,
			ITableWriter<T0, T1>
	{
		public TableWriter(
			[NotNull] IExtendedConsoleSession session)
			: base(
				session)
		{
		}

		public ITableWriter<T0, T1> WriteRecord(
			T0 arg0,
			T1 arg1)
		{
			WriteRecordBase(arg0, arg1);
			return this;
		}

		public ITableWriter<T0, T1> WriteRecordHeaders(
			string h0,
			string h1)
		{
			WriteRecordHeadersBase(h0, h1);
			return this;
		}
	}

	public class TableWriter<T0, T1, T2>
		: TableWriter,
			ITableWriter<T0, T1, T2>
	{
		public TableWriter(
			[NotNull] IExtendedConsoleSession session)
			: base(
				session)
		{
		}

		public ITableWriter<T0, T1, T2> WriteRecord(
			T0 arg0,
			T1 arg1,
			T2 arg2)
		{
			WriteRecordBase(arg0, arg1, arg2);
			return this;
		}

		public ITableWriter<T0, T1, T2> WriteRecordHeaders(
			string h0,
			string h1,
			string h2)
		{
			WriteRecordHeadersBase(h0, h1, h2);
			return this;
		}
	}

	public class TableWriter<T0, T1, T2, T3>
		: TableWriter,
			ITableWriter<T0, T1, T2, T3>
	{
		public TableWriter(
			[NotNull] IExtendedConsoleSession session)
			: base(
				session)
		{
		}

		public ITableWriter<T0, T1, T2, T3> WriteRecord(
			T0 arg0,
			T1 arg1,
			T2 arg2,
			T3 arg3)
		{
			WriteRecordBase(arg0, arg1, arg2, arg3);
			return this;
		}

		public ITableWriter<T0, T1, T2, T3> WriteRecordHeaders(
			string h0,
			string h1,
			string h2,
			string h3)
		{
			WriteRecordHeadersBase(h0, h1, h2, h3);
			return this;
		}
	}

	public class TableWriter<T0, T1, T2, T3, T4>
		: TableWriter,
			ITableWriter<T0, T1, T2, T3, T4>
	{
		public TableWriter(
			[NotNull] IExtendedConsoleSession session)
			: base(
				session)
		{
		}

		public ITableWriter<T0, T1, T2, T3, T4> WriteRecord(
			T0 arg0,
			T1 arg1,
			T2 arg2,
			T3 arg3,
			T4 arg4)
		{
			WriteRecordBase(arg0, arg1, arg2, arg3, arg4);
			return this;
		}

		public ITableWriter<T0, T1, T2, T3, T4> WriteRecordHeaders(
			string h0,
			string h1,
			string h2,
			string h3,
			string h4)
		{
			WriteRecordHeadersBase(h0, h1, h2, h3, h4);
			return this;
		}
	}

	public class TableWriter<T0, T1, T2, T3, T4, T5>
		: TableWriter,
			ITableWriter<T0, T1, T2, T3, T4, T5>
	{
		public TableWriter(
			[NotNull] IExtendedConsoleSession session)
			: base(
				session)
		{
		}

		public ITableWriter<T0, T1, T2, T3, T4, T5> WriteRecord(
			T0 arg0,
			T1 arg1,
			T2 arg2,
			T3 arg3,
			T4 arg4,
			T5 arg5)
		{
			WriteRecordBase(arg0, arg1, arg2, arg3, arg4, arg5);
			return this;
		}

		public ITableWriter<T0, T1, T2, T3, T4, T5> WriteRecordHeaders(
			string h0,
			string h1,
			string h2,
			string h3,
			string h4,
			string h5)
		{
			WriteRecordHeadersBase(h0, h1, h2, h3, h4, h5);
			return this;
		}
	}

	public class TableWriter<T0, T1, T2, T3, T4, T5, T6>
		: TableWriter,
			ITableWriter<T0, T1, T2, T3, T4, T5, T6>
	{
		public TableWriter(
			[NotNull] IExtendedConsoleSession session)
			: base(
				session)
		{
		}

		public ITableWriter<T0, T1, T2, T3, T4, T5, T6> WriteRecord(
			T0 arg0,
			T1 arg1,
			T2 arg2,
			T3 arg3,
			T4 arg4,
			T5 arg5,
			T6 arg6)
		{
			WriteRecordBase(arg0, arg1, arg2, arg3, arg4, arg5, arg6);
			return this;
		}

		public ITableWriter<T0, T1, T2, T3, T4, T5, T6> WriteRecordHeaders(
			string h0,
			string h1,
			string h2,
			string h3,
			string h4,
			string h5,
			string h6)
		{
			WriteRecordHeadersBase(h0, h1, h2, h3, h4, h5, h6);
			return this;
		}
	}

	public class TableWriter<T0, T1, T2, T3, T4, T5, T6, T7>
		: TableWriter,
			ITableWriter<T0, T1, T2, T3, T4, T5, T6, T7>
	{
		public TableWriter(
			[NotNull] IExtendedConsoleSession session)
			: base(
				session)
		{
		}

		public ITableWriter<T0, T1, T2, T3, T4, T5, T6, T7> WriteRecord(
			T0 arg0,
			T1 arg1,
			T2 arg2,
			T3 arg3,
			T4 arg4,
			T5 arg5,
			T6 arg6,
			T7 arg7)
		{
			WriteRecordBase(arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7);
			return this;
		}

		public ITableWriter<T0, T1, T2, T3, T4, T5, T6, T7> WriteRecordHeaders(
			string h0,
			string h1,
			string h2,
			string h3,
			string h4,
			string h5,
			string h6,
			string h7)
		{
			WriteRecordHeadersBase(h0, h1, h2, h3, h4, h5, h6, h7);
			return this;
		}
	}

	public class TableWriter<T0, T1, T2, T3, T4, T5, T6, T7, T8>
		: TableWriter,
			ITableWriter<T0, T1, T2, T3, T4, T5, T6, T7, T8>
	{
		public TableWriter(
			[NotNull] IExtendedConsoleSession session)
			: base(
				session)
		{
		}

		public ITableWriter<T0, T1, T2, T3, T4, T5, T6, T7, T8> WriteRecord(
			T0 arg0,
			T1 arg1,
			T2 arg2,
			T3 arg3,
			T4 arg4,
			T5 arg5,
			T6 arg6,
			T7 arg7,
			T8 arg8)
		{
			WriteRecordBase(arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8);
			return this;
		}

		public ITableWriter<T0, T1, T2, T3, T4, T5, T6, T7, T8> WriteRecordHeaders(
			string h0,
			string h1,
			string h2,
			string h3,
			string h4,
			string h5,
			string h6,
			string h7,
			string h8)
		{
			WriteRecordHeadersBase(h0, h1, h2, h3, h4, h5, h6, h7, h8);
			return this;
		}
	}

	public class TableWriter<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9>
		: TableWriter,
			ITableWriter<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9>
	{
		public TableWriter(
			[NotNull] IExtendedConsoleSession session)
			: base(
				session)
		{
		}

		public ITableWriter<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9> WriteRecord(
			T0 arg0,
			T1 arg1,
			T2 arg2,
			T3 arg3,
			T4 arg4,
			T5 arg5,
			T6 arg6,
			T7 arg7,
			T8 arg8,
			T9 arg9)
		{
			WriteRecordBase(arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8);
			return this;
		}

		public ITableWriter<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9> WriteRecordHeaders(
			string h0,
			string h1,
			string h2,
			string h3,
			string h4,
			string h5,
			string h6,
			string h7,
			string h8,
			string h9)
		{
			WriteRecordHeadersBase(h0, h1, h2, h3, h4, h5, h6, h7, h8, h9);
			return this;
		}
	}
}