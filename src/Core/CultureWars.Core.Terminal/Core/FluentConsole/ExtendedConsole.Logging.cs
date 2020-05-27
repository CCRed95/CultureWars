using System;
using CultureWars.Core.FluentConsole.Geometry;

namespace CultureWars.Core.FluentConsole
{
	public partial class ExtendedConsole
	{
		//public IExtendedConsoleSession WriteTable(
		//	params IEnumerable @this)
		//{
		//	var array = @this.Cast<object>().ToArray();
		//	var commonColumnWidth = (double)WindowWidth / array.Length;
		//	//var columnOffset = (int)commonColumnWidth.Floor();
		//	var currentOffsetExact = 0d;

		//	foreach (var item in array)
		//	{
		//		currentOffsetExact += commonColumnWidth;
		//		var rounded = (int) currentOffsetExact.Round();
		//		WriteTableElement(item, out var _cursorPosition);
		//	}

		//	return this;
		//}
		public TableGeometry GetTableGeometry()
		{
			return new TableGeometry(
				WindowWidth,
				WindowHeight,
				CursorTop,
				CursorLeft);
		}

		public ITableWriter<T0> BeginWriteTableHeaders<T0>(
			string h0)
		{
			var writer = new TableWriter<T0>(this);
			writer.WriteRecordHeaders(h0);
			return writer;
		}

		public ITableWriter<T0, T1> BeginWriteTableHeaders<T0, T1>(
			string h0,
			string h1)
		{
			var writer = new TableWriter<T0, T1>(this);
			writer.WriteRecordHeaders(h0, h1);
			return writer;
		}

		public ITableWriter<T0, T1, T2> BeginWriteTableHeaders<T0, T1, T2>(
			string h0,
			string h1,
			string h2)
		{
			var writer = new TableWriter<T0, T1, T2>(this);
			writer.WriteRecordHeaders(h0, h1, h2);
			return writer;
		}

		public ITableWriter<T0, T1, T2, T3> BeginWriteTableHeaders<T0, T1, T2, T3>(
			string h0,
			string h1,
			string h2,
			string h3)
		{
			var writer = new TableWriter<T0, T1, T2, T3>(this);
			writer.WriteRecordHeaders(h0, h1, h2, h3);
			return writer;
		}

		public ITableWriter<T0, T1, T2, T3, T4> BeginWriteTableHeaders<T0, T1, T2, T3, T4>(
			string h0,
			string h1,
			string h2,
			string h3,
			string h4)
		{
			var writer = new TableWriter<T0, T1, T2, T3, T4>(this);
			writer.WriteRecordHeaders(h0, h1, h2, h3, h4);
			return writer;
		}

		public ITableWriter<T0, T1, T2, T3, T4, T5> BeginWriteTableHeaders<T0, T1, T2, T3, T4, T5>(
			string h0,
			string h1,
			string h2,
			string h3,
			string h4,
			string h5)
		{
			var writer = new TableWriter<T0, T1, T2, T3, T4, T5>(this);
			writer.WriteRecordHeaders(h0, h1, h2, h3, h4, h5);
			return writer;
		}

		public ITableWriter<T0, T1, T2, T3, T4, T5, T6> BeginWriteTableHeaders
			<T0, T1, T2, T3, T4, T5, T6>(
				string h0,
				string h1,
				string h2,
				string h3,
				string h4,
				string h5,
				string h6)
			{
				var writer = new TableWriter<T0, T1, T2, T3, T4, T5, T6>(this);
				writer.WriteRecordHeaders(h0, h1, h2, h3, h4, h5, h6);
				return writer;
			}

		public ITableWriter<T0, T1, T2, T3, T4, T5, T6, T7> BeginWriteTableHeaders
			<T0, T1, T2, T3, T4, T5, T6, T7>(
				string h0,
				string h1,
				string h2,
				string h3,
				string h4,
				string h5,
				string h6,
				string h7)
			{
				var writer = new TableWriter<T0, T1, T2, T3, T4, T5, T6, T7>(this);
				writer.WriteRecordHeaders(h0, h1, h2, h3, h4, h5, h6, h7);
				return writer;
			}

		public ITableWriter<T0, T1, T2, T3, T4, T5, T6, T7, T8> BeginWriteTableHeaders
			<T0, T1, T2, T3, T4, T5, T6, T7, T8>(
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
				var writer = new TableWriter<T0, T1, T2, T3, T4, T5, T6, T7, T8>(this);
				writer.WriteRecordHeaders(h0, h1, h2, h3, h4, h5, h6, h7, h8);
				return writer;
			}

		public ITableWriter<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9> BeginWriteTableHeaders
			<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9>(
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
				var writer = new TableWriter<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9>(this);
				writer.WriteRecordHeaders(h0, h1, h2, h3, h4, h5, h6, h7, h8, h9);
				return writer;
			}


		public ITableWriter<T0> BeginWriteTable<T0>(
			T0 arg0)
		{
			var writer = new TableWriter<T0>(this);
			writer.WriteRecord(arg0);
			return writer;
		}

		public ITableWriter<T0, T1> BeginWriteTable<T0, T1>(
			T0 arg0,
			T1 arg1)
		{
			var writer = new TableWriter<T0, T1>(this);
			writer.WriteRecord(arg0, arg1);
			return writer;
		}

		public ITableWriter<T0, T1, T2> BeginWriteTable<T0, T1, T2>(
			T0 arg0,
			T1 arg1,
			T2 arg2)
		{
			var writer = new TableWriter<T0, T1, T2>(this);
			writer.WriteRecord(arg0, arg1, arg2);
			return writer;
		}

		public ITableWriter<T0, T1, T2, T3> BeginWriteTable<T0, T1, T2, T3>(
			T0 arg0,
			T1 arg1,
			T2 arg2,
			T3 arg3)
		{
			var writer = new TableWriter<T0, T1, T2, T3>(this);
			writer.WriteRecord(arg0, arg1, arg2, arg3);
			return writer;
		}

		public ITableWriter<T0, T1, T2, T3, T4> BeginWriteTable<T0, T1, T2, T3, T4>(
			T0 arg0,
			T1 arg1,
			T2 arg2,
			T3 arg3,
			T4 arg4)
		{
			var writer = new TableWriter<T0, T1, T2, T3, T4>(this);
			writer.WriteRecord(arg0, arg1, arg2, arg3, arg4);
			return writer;
		}

		public ITableWriter<T0, T1, T2, T3, T4, T5> BeginWriteTable<T0, T1, T2, T3, T4, T5>(
			T0 arg0,
			T1 arg1,
			T2 arg2,
			T3 arg3,
			T4 arg4,
			T5 arg5)
		{
			var writer = new TableWriter<T0, T1, T2, T3, T4, T5>(this);
			writer.WriteRecord(arg0, arg1, arg2, arg3, arg4, arg5);
			return writer;
		}

		public ITableWriter<T0, T1, T2, T3, T4, T5, T6> BeginWriteTable<T0, T1, T2, T3, T4, T5, T6>(
			T0 arg0,
			T1 arg1,
			T2 arg2,
			T3 arg3,
			T4 arg4,
			T5 arg5,
			T6 arg6)
		{
			var writer = new TableWriter<T0, T1, T2, T3, T4, T5, T6>(this);
			writer.WriteRecord(arg0, arg1, arg2, arg3, arg4, arg5, arg6);
			return writer;
		}

		public ITableWriter<T0, T1, T2, T3, T4, T5, T6, T7> BeginWriteTable
			<T0, T1, T2, T3, T4, T5, T6, T7>(
				T0 arg0,
				T1 arg1,
				T2 arg2,
				T3 arg3,
				T4 arg4,
				T5 arg5,
				T6 arg6,
				T7 arg7)
			{
				var writer = new TableWriter<T0, T1, T2, T3, T4, T5, T6, T7>(this);
				writer.WriteRecord(arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7);
				return writer;
			}

		public ITableWriter<T0, T1, T2, T3, T4, T5, T6, T7, T8> BeginWriteTable
			<T0, T1, T2, T3, T4, T5, T6, T7, T8>(
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
				var writer = new TableWriter<T0, T1, T2, T3, T4, T5, T6, T7, T8>(this);
				writer.WriteRecord(arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8);
				return writer;
			}

		public ITableWriter<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9> BeginWriteTable
			<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9>(
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
				var writer = new TableWriter<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9>(this);
				writer.WriteRecord(arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9);
				return writer;

				//var commonColumnWidth = (double)WindowWidth / array.Length;
				////var columnOffset = (int)commonColumnWidth.Floor();
				//var currentOffsetExact = 0d;

				//foreach (var item in array)
				//{
				//	currentOffsetExact += commonColumnWidth;
				//	var rounded = (int)currentOffsetExact.Round();
				//	WriteTableElement(item, out var _cursorPosition);
				//}

				//return this;
			}


		//public IExtendedConsoleSession WriteTableElement(
		//	object element,
		//	out int cursorPosition)
		//{
		//	var commonColumnWidth = (double)WindowWidth / array.Length;
		//	//var columnOffset = (int)commonColumnWidth.Floor();
		//	var currentOffsetExact = 0d;

		//	foreach (var item in array)
		//	{
		//		currentOffsetExact += commonColumnWidth;
		//		var rounded = (int)currentOffsetExact.Round();
		//	}

		//	return this;
		//}

		public IExtendedConsoleSession WriteTable(
			Action<ITableWriter> @this)
		{
			throw new NotImplementedException();
		}

		public IExtendedConsoleSession WriteTable<T0>(
			Action<ITableWriter<T0>> @this)
		{
			@this.Invoke(new TableWriter<T0>(this));
			return this;
		}

		public IExtendedConsoleSession WriteTable<T0, T1>(
			Action<ITableWriter<T0, T1>> @this)
		{
			@this.Invoke(new TableWriter<T0, T1>(this));
			return this;
		}

		public IExtendedConsoleSession WriteTable<T0, T1, T2>(
			Action<ITableWriter<T0, T1, T2>> @this)
		{
			@this.Invoke(new TableWriter<T0, T1, T2>(this));
			return this;
		}

		public IExtendedConsoleSession WriteTable<T0, T1, T2, T3>(
			Action<ITableWriter<T0, T1, T2, T3>> @this)
		{
			@this.Invoke(new TableWriter<T0, T1, T2, T3>(this));
			return this;
		}

		public IExtendedConsoleSession WriteTable<T0, T1, T2, T3, T4>(
			Action<ITableWriter<T0, T1, T2, T3, T4>> @this)
		{
			@this.Invoke(new TableWriter<T0, T1, T2, T3, T4>(this));
			return this;
		}

		public IExtendedConsoleSession WriteTable<T0, T1, T2, T3, T4, T5>(
			Action<ITableWriter<T0, T1, T2, T3, T4, T5>> @this)
		{
			@this.Invoke(new TableWriter<T0, T1, T2, T3, T4, T5>(this));
			return this;
		}

		public IExtendedConsoleSession WriteTable<T0, T1, T2, T3, T4, T5, T6>(
			Action<ITableWriter<T0, T1, T2, T3, T4, T5, T6>> @this)
		{
			@this.Invoke(new TableWriter<T0, T1, T2, T3, T4, T5, T6>(this));
			return this;
		}

		public IExtendedConsoleSession WriteTable<T0, T1, T2, T3, T4, T5, T6, T7>(
			Action<ITableWriter<T0, T1, T2, T3, T4, T5, T6, T7>> @this)
		{
			@this.Invoke(new TableWriter<T0, T1, T2, T3, T4, T5, T6, T7>(this));
			return this;
		}

		public IExtendedConsoleSession WriteTable<T0, T1, T2, T3, T4, T5, T6, T7, T8>(
			Action<ITableWriter<T0, T1, T2, T3, T4, T5, T6, T7, T8>> @this)
		{
			@this.Invoke(new TableWriter<T0, T1, T2, T3, T4, T5, T6, T7, T8>(this));
			return this;
		}

		public IExtendedConsoleSession WriteTable<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9>(
			Action<ITableWriter<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9>> @this)
		{
			@this.Invoke(new TableWriter<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9>(this));
			return this;
		}
	}
}
