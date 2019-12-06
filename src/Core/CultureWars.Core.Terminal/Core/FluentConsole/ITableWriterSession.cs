namespace CultureWars.Core.FluentConsole
{
	public interface ITableWriterSession
	{
		IExtendedConsoleSession Session { get; }


		ITableWriter<T0> BeginWriteTableHeaders<T0>(
			string h0);

		ITableWriter<T0, T1> BeginWriteTableHeaders<T0, T1>(
			string h0,
			string h1);

		ITableWriter<T0, T1, T2> BeginWriteTableHeaders<T0, T1, T2>(
			string h0,
			string h1,
			string h2);

		ITableWriter<T0, T1, T2, T3> BeginWriteTableHeaders<T0, T1, T2, T3>(
			string h0,
			string h1,
			string h2,
			string h3);

		ITableWriter<T0, T1, T2, T3, T4> BeginWriteTableHeaders<T0, T1, T2, T3, T4>(
			string h0,
			string h1,
			string h2,
			string h3,
			string h4);

		ITableWriter<T0, T1, T2, T3, T4, T5> BeginWriteTableHeaders<T0, T1, T2, T3, T4, T5>(
			string h0,
			string h1,
			string h2,
			string h3,
			string h4,
			string h5);

		ITableWriter<T0, T1, T2, T3, T4, T5, T6> BeginWriteTableHeaders<T0, T1, T2, T3, T4, T5, T6>(
			string h0,
			string h1,
			string h2,
			string h3,
			string h4,
			string h5,
			string h6);

		ITableWriter<T0, T1, T2, T3, T4, T5, T6, T7> BeginWriteTableHeaders<T0, T1, T2, T3, T4, T5, T6, T7>(
			string h0,
			string h1,
			string h2,
			string h3,
			string h4,
			string h5,
			string h6,
			string h7);

		ITableWriter<T0, T1, T2, T3, T4, T5, T6, T7, T8> BeginWriteTableHeaders<T0, T1, T2, T3, T4, T5, T6, T7, T8>(
			string h0,
			string h1,
			string h2,
			string h3,
			string h4,
			string h5,
			string h6,
			string h7,
			string h8);

		ITableWriter<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9> BeginWriteTableHeaders<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9>(
			string h0,
			string h1,
			string h2,
			string h3,
			string h4,
			string h5,
			string h6,
			string h7,
			string h8,
			string h9);


		ITableWriter<T0> BeginWriteTable<T0>(
			T0 arg0);

		ITableWriter<T0, T1> BeginWriteTable<T0, T1>(
			T0 arg0,
			T1 arg1);

		ITableWriter<T0, T1, T2> BeginWriteTable<T0, T1, T2>(
			T0 arg0,
			T1 arg1,
			T2 arg2);

		ITableWriter<T0, T1, T2, T3> BeginWriteTable<T0, T1, T2, T3>(
			T0 arg0,
			T1 arg1,
			T2 arg2,
			T3 arg3);

		ITableWriter<T0, T1, T2, T3, T4> BeginWriteTable<T0, T1, T2, T3, T4>(
			T0 arg0,
			T1 arg1,
			T2 arg2,
			T3 arg3,
			T4 arg4);

		ITableWriter<T0, T1, T2, T3, T4, T5> BeginWriteTable<T0, T1, T2, T3, T4, T5>(
			T0 arg0,
			T1 arg1,
			T2 arg2,
			T3 arg3,
			T4 arg4,
			T5 arg5);

		ITableWriter<T0, T1, T2, T3, T4, T5, T6> BeginWriteTable<T0, T1, T2, T3, T4, T5, T6>(
			T0 arg0,
			T1 arg1,
			T2 arg2,
			T3 arg3,
			T4 arg4,
			T5 arg5,
			T6 arg6);

		ITableWriter<T0, T1, T2, T3, T4, T5, T6, T7> BeginWriteTable<T0, T1, T2, T3, T4, T5, T6, T7>(
			T0 arg0,
			T1 arg1,
			T2 arg2,
			T3 arg3,
			T4 arg4,
			T5 arg5,
			T6 arg6,
			T7 arg7);

		ITableWriter<T0, T1, T2, T3, T4, T5, T6, T7, T8> BeginWriteTable<T0, T1, T2, T3, T4, T5, T6, T7, T8>(
			T0 arg0,
			T1 arg1,
			T2 arg2,
			T3 arg3,
			T4 arg4,
			T5 arg5,
			T6 arg6,
			T7 arg7,
			T8 arg8);

		ITableWriter<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9> BeginWriteTable<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9>(
			T0 arg0,
			T1 arg1,
			T2 arg2,
			T3 arg3,
			T4 arg4,
			T5 arg5,
			T6 arg6,
			T7 arg7,
			T8 arg8,
			T9 arg9);
	}
}