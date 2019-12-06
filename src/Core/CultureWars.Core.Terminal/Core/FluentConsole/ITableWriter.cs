namespace CultureWars.Core.FluentConsole
{
	public interface ITableWriter
	{
		IExtendedConsoleSession Session { get; }

		//ITableWriter WriteRecordBase(params object[] args);
		//ITableWriter WriteRecordHeadersBase(params string[] headers);
	}

	public interface ITableWriter<in T0>
		: ITableWriter
	{
		ITableWriter<T0> WriteRecord(
			T0 arg0);

		ITableWriter<T0> WriteRecordHeaders(
			string h0);
	}

	public interface ITableWriter<in T0, in T1>
		: ITableWriter
	{
		ITableWriter<T0, T1> WriteRecord(
			T0 arg0,
			T1 arg1);

		ITableWriter<T0, T1> WriteRecordHeaders(
			string h0,
			string h1);
	}

	public interface ITableWriter<in T0, in T1, in T2>
		: ITableWriter
	{
		ITableWriter<T0, T1, T2> WriteRecord(
			T0 arg0,
			T1 arg1,
			T2 arg2);

		ITableWriter<T0, T1, T2> WriteRecordHeaders(
			string h0,
			string h1,
			string h2);
	}

	public interface ITableWriter<in T0, in T1, in T2, in T3>
		: ITableWriter
	{
		ITableWriter<T0, T1, T2, T3> WriteRecord(
			T0 arg0,
			T1 arg1,
			T2 arg2,
			T3 arg3);

		ITableWriter<T0, T1, T2, T3> WriteRecordHeaders(
			string h0,
			string h1,
			string h2,
			string h3);
	}

	public interface ITableWriter<in T0, in T1, in T2, in T3, in T4>
		: ITableWriter
	{
		ITableWriter<T0, T1, T2, T3, T4> WriteRecord(
			T0 arg0,
			T1 arg1,
			T2 arg2,
			T3 arg3,
			T4 arg4);

		ITableWriter<T0, T1, T2, T3, T4> WriteRecordHeaders(
			string h0,
			string h1,
			string h2,
			string h3,
			string h4);
	}

	public interface ITableWriter<in T0, in T1, in T2, in T3, in T4, in T5>
		: ITableWriter
	{
		ITableWriter<T0, T1, T2, T3, T4, T5> WriteRecord(
			T0 arg0,
			T1 arg1,
			T2 arg2,
			T3 arg3,
			T4 arg4,
			T5 arg5);

		ITableWriter<T0, T1, T2, T3, T4, T5> WriteRecordHeaders(
			string h0,
			string h1,
			string h2,
			string h3,
			string h4,
			string h5);
	}

	public interface ITableWriter<in T0, in T1, in T2, in T3, in T4, in T5, in T6>
		: ITableWriter
	{
		ITableWriter<T0, T1, T2, T3, T4, T5, T6> WriteRecord(
			T0 arg0,
			T1 arg1,
			T2 arg2,
			T3 arg3,
			T4 arg4,
			T5 arg5,
			T6 arg6);

		ITableWriter<T0, T1, T2, T3, T4, T5, T6> WriteRecordHeaders(
			string h0,
			string h1,
			string h2,
			string h3,
			string h4,
			string h5,
			string h6);
	}

	public interface ITableWriter<in T0, in T1, in T2, in T3, in T4, in T5, in T6, in T7>
		: ITableWriter
	{
		ITableWriter<T0, T1, T2, T3, T4, T5, T6, T7> WriteRecord(
			T0 arg0,
			T1 arg1,
			T2 arg2,
			T3 arg3,
			T4 arg4,
			T5 arg5,
			T6 arg6,
			T7 arg7);

		ITableWriter<T0, T1, T2, T3, T4, T5, T6, T7> WriteRecordHeaders(
			string h0,
			string h1,
			string h2,
			string h3,
			string h4,
			string h5,
			string h6,
			string h7);
	}

	public interface ITableWriter<in T0, in T1, in T2, in T3, in T4, in T5, in T6, in T7, in T8>
		: ITableWriter
	{
		ITableWriter<T0, T1, T2, T3, T4, T5, T6, T7, T8> WriteRecord(
			T0 arg0,
			T1 arg1,
			T2 arg2,
			T3 arg3,
			T4 arg4,
			T5 arg5,
			T6 arg6,
			T7 arg7,
			T8 arg8);

		ITableWriter<T0, T1, T2, T3, T4, T5, T6, T7, T8> WriteRecordHeaders(
			string h0,
			string h1,
			string h2,
			string h3,
			string h4,
			string h5,
			string h6,
			string h7,
			string h8);
	}

	public interface ITableWriter<in T0, in T1, in T2, in T3, in T4, in T5, in T6, in T7, in T8, in T9>
		: ITableWriter
	{
		ITableWriter<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9> WriteRecord(
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

		ITableWriter<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9> WriteRecordHeaders(
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
	}
}