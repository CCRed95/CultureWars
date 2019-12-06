using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.IO;
using System.Reflection;
using CultureWars.Core.FluentConsole.Colorization;
using CultureWars.Core.FluentConsole.Colorization.Stylization;
using CultureWars.Core.FluentConsole.Formatting;
using CultureWars.Core.FluentConsole.Geometry;
using JetBrains.Annotations;

namespace CultureWars.Core.FluentConsole
{
	//class IFluentConsole
	//{
	//}

	public interface IExtendedConsoleAppearance
	{
		/// <inheritdoc cref="Console.BackgroundColor"/>
		Color BackgroundColor { get; set; }

		/// <inheritdoc cref="Console.ForegroundColor"/>
		Color ForegroundColor { get; set; }
	}

	public interface IExtendedConsoleCursorInfo
	{
		/// <inheritdoc cref="Console.CursorLeft"/>
		int CursorLeft { get; set; }

		/// <inheritdoc cref="Console.CursorSize"/>
		int CursorSize { get; set; }

		/// <inheritdoc cref="System.Console.CursorVisible"/>
		int CursorTop { get; set; }

		/// <inheritdoc cref="System.Console.CursorVisible"/>
		bool CursorVisible { get; set; }


		/// <inheritdoc cref="System.Console.SetCursorPosition(int,int)"/>
		/// <summary>
		///		Fluent API call to set the cursor position.
		/// </summary>
		IExtendedConsoleSession SetCursorPosition(
			int left,
			int top);
	}

	public interface IExtendedConsoleBufferInfo
	{
		/// <inheritdoc cref="Console.BufferHeight"/>
		int BufferHeight { get; set; }

		/// <inheritdoc cref="Console.BufferWidth"/>
		int BufferWidth { get; set; }

		/// <inheritdoc cref="Console.MoveBufferArea(int,int,int,int,int,int)"/>
		/// <summary>
		///		Fluent API call to move the buffer.
		/// </summary>
		IExtendedConsoleSession MoveBufferArea(
			int sourceLeft,
			int sourceTop,
			int sourceWidth,
			int sourceHeight,
			int targetLeft,
			int targetTop);

		/// <inheritdoc cref="Console.MoveBufferArea(int,int,int,int,int,int,char,ConsoleColor,ConsoleColor)"/>
		/// <summary>
		///		Fluent API call to move the buffer.
		/// </summary>
		IExtendedConsoleSession MoveBufferArea(
			int sourceLeft,
			int sourceTop,
			int sourceWidth,
			int sourceHeight,
			int targetLeft,
			int targetTop,
			char sourceChar,
			ConsoleColor sourceForeColor,
			ConsoleColor sourceBackColor);

		/// <inheritdoc cref="Console.SetBufferSize(int,int)"/>
		/// <summary>
		///		Fluent API call to set the buffer size.
		/// </summary>
		IExtendedConsoleSession SetBufferSize(
			int width,
			int height);
	}

	public interface IExtendedConsoleInfrastructureInfo
	{
		/// <inheritdoc cref="Console.OpenStandardError()"/>
		Stream OpenStandardError();

		/// <inheritdoc cref="Console.OpenStandardInput()"/>
		Stream OpenStandardInput();

		/// <inheritdoc cref="Console.OpenStandardOutput()"/>
		Stream OpenStandardOutput();


		/// <inheritdoc cref="Console.In"/>
		TextReader In { get; }

		/// <inheritdoc cref="Console.Out"/>
		TextWriter Out { get; }

		/// <inheritdoc cref="Console.Error"/>
		TextWriter Error { get; }


		/// <inheritdoc cref="Console.InputEncoding"/>
		Encoding InputEncoding { get; set; }

		/// <inheritdoc cref="Console.OutputEncoding"/>
		Encoding OutputEncoding { get; set; }


		/// <inheritdoc cref="Console.IsErrorRedirected"/>
		bool IsErrorRedirected { get; }

		/// <inheritdoc cref="Console.IsInputRedirected"/>
		bool IsInputRedirected { get; }

		/// <inheritdoc cref="Console.IsOutputRedirected"/>
		bool IsOutputRedirected { get; }


		/// <inheritdoc cref="Console.SetError(TextWriter)"/>
		IExtendedConsoleSession SetError(TextWriter newError);

		/// <inheritdoc cref="Console.SetIn(TextReader)"/>
		IExtendedConsoleSession SetIn(TextReader newIn);

		/// <inheritdoc cref="Console.SetOut(TextWriter)"/>
		IExtendedConsoleSession SetOut(TextWriter newOut);
	}

	public interface IExtendedConsoleGeometryInfo
	{
		/// <inheritdoc cref="Console.WindowHeight"/>
		int WindowHeight { get; set; }

		/// <inheritdoc cref="Console.WindowLeft"/>
		int WindowLeft { get; set; }

		/// <inheritdoc cref="Console.WindowTop"/>
		int WindowTop { get; set; }

		/// <inheritdoc cref="Console.WindowWidth"/>
		int WindowWidth { get; set; }


		/// <inheritdoc cref="Console.LargestWindowHeight"/>
		int LargestWindowHeight { get; }

		/// <inheritdoc cref="Console.LargestWindowWidth"/>
		int LargestWindowWidth { get; }


		/// <inheritdoc cref="Console.SetWindowPosition(int,int)"/>
		IExtendedConsoleSession SetWindowPosition(int left, int top);

		/// <inheritdoc cref="Console.SetWindowSize(int,int)"/>
		IExtendedConsoleSession SetWindowSize(int width, int height);
	}

	public interface IExtendedConsoleStateInfo
	{
		/// <inheritdoc cref="Console.LargestWindowWidth"/>
		bool KeyAvailable { get; }

		/// <inheritdoc cref="Console.CapsLock"/>
		bool CapsLock { get; }

		/// <inheritdoc cref="Console.NumberLock"/>
		bool NumberLock { get; }
	}

	public interface IExtendedConsoleGeneralInfo
	{
		/// <inheritdoc cref="Console.Read()"/>
		int Read();

		/// <inheritdoc cref="Console.ReadLine()"/>
		string ReadLine();

		/// <inheritdoc cref="Console.ReadLine()"/>
		ConsoleKeyInfo ReadKey();

		/// <inheritdoc cref="Console.ReadKey(bool)"/>
		ConsoleKeyInfo ReadKey(bool intercept);

		/// <inheritdoc cref="Console.Title"/>
		string Title { get; set; }

		/// <inheritdoc cref="Console.TreatControlCAsInput"/>
		bool TreatControlCAsInput { get; set; }

		/// <inheritdoc cref="Console.CancelKeyPress"/>
		event ConsoleCancelEventHandler CancelKeyPress;

		/// <inheritdoc cref="Console.Beep(int,int)"/>
		/// <summary>
		///		Fluent API call to trigger a console beep.
		/// </summary>
		IExtendedConsoleSession Beep(int frequency, int duration);

		/// <inheritdoc cref="Console.Clear()"/>
		/// <summary>
		///		Fluent API call to trigger a console beep.
		/// </summary>
		IExtendedConsoleSession Clear();
	}

	public interface IExtendedConsoleCodeWriter
	{
		/// <summary>
		///		Writes a number as a <see cref="string"/> to the console.
		/// </summary>
		/// <param name="number">
		///		The number, as a <see cref="string"/>.
		///	</param>
		/// <returns>
		///		Returns a <see cref="IExtendedConsoleSession"/> instance for fluent, chained calls.
		/// </returns>
		IExtendedConsoleSession WriteNumber(string number);

		/// <summary>
		///		Writes a <see cref="Type"/> to the console.
		/// </summary>
		/// <param name="type">
		///		The <see cref="Type"/> to write to the console.
		///	</param>
		/// <param name="fullyQualified">
		///		Optional <see cref="bool"/> parameter indicating whether or not the type should be
		///		represented in a fully-qualified format.
		///	</param>
		/// <returns>
		///		Returns a <see cref="IExtendedConsoleSession"/> instance for fluent, chained calls.
		/// </returns>
		IExtendedConsoleSession WriteType(Type type, bool fullyQualified = false);

		/// <summary>
		///		Writes a <see cref="MethodInfo"/> to the console.
		/// </summary>
		/// <param name="methodInfo">
		///		The <see cref="MethodInfo"/> to write to the console.
		///	</param>
		/// <returns>
		///		Returns a <see cref="IExtendedConsoleSession"/> instance for fluent, chained calls.
		/// </returns>
		IExtendedConsoleSession WriteMethod(MethodInfo methodInfo);

		/// <summary>
		///		Writes a <see cref="FieldInfo"/> to the console.
		/// </summary>
		/// <param name="fieldInfo">
		///		The <see cref="FieldInfo"/> to write to the console.
		///	</param>
		/// <returns>
		///		Returns a <see cref="IExtendedConsoleSession"/> instance for fluent, chained calls.
		/// </returns>
		IExtendedConsoleSession WriteField(FieldInfo fieldInfo);

		/// <summary>
		///		Writes a <see cref="PropertyInfo"/> to the console.
		/// </summary>
		/// <param name="propertyInfo">
		///		The <see cref="PropertyInfo"/> to write to the console.
		///	</param>
		/// <returns>
		///		Returns a <see cref="IExtendedConsoleSession"/> instance for fluent, chained calls.
		/// </returns>
		IExtendedConsoleSession WriteProperty(PropertyInfo propertyInfo);

		/// <summary>
		///		Writes a <see cref="Attribute"/> to the console.
		/// </summary>
		/// <param name="attribute">
		///		The <see cref="Attribute"/> to write to the console.
		///	</param>
		/// <returns>
		///		Returns a <see cref="IExtendedConsoleSession"/> instance for fluent, chained calls.
		/// </returns>
		IExtendedConsoleSession WriteAttribute(Attribute attribute);

		/// <summary>
		///		Writes a <see cref="ConstructorInfo"/> to the console.
		/// </summary>
		/// <param name="constructorInfo">
		///		The <see cref="ConstructorInfo"/> to write to the console.
		///	</param>
		/// <returns>
		///		Returns a <see cref="IExtendedConsoleSession"/> instance for fluent, chained calls.
		/// </returns>
		IExtendedConsoleSession WriteConstructor(ConstructorInfo constructorInfo);

		/// <summary>
		///		Writes a <see cref="EventInfo"/> to the console.
		/// </summary>
		/// <param name="eventInfo">
		///		The <see cref="EventInfo"/> to write to the console.
		///	</param>
		/// <returns>
		///		Returns a <see cref="IExtendedConsoleSession"/> instance for fluent, chained calls.
		/// </returns>
		IExtendedConsoleSession WriteEvent(EventInfo eventInfo);

		/// <summary>
		///		Writes a <see cref="ParameterInfo"/> to the console.
		/// </summary>
		/// <param name="parameterInfo">
		///		The <see cref="ParameterInfo"/> to write to the console.
		///	</param>
		/// <returns>
		///		Returns a <see cref="IExtendedConsoleSession"/> instance for fluent, chained calls.
		/// </returns>
		IExtendedConsoleSession WriteParameter(ParameterInfo parameterInfo);
	}

	//public interface IExtendedConsoleLog
	//{
	//	IExtendedConsoleSession WriteLog(EventLogEntry eventLogEntry);

	//	IExtendedConsoleSession WriteLog(string message, Severity severity = Severity.Unknown);


	//	IExtendedConsoleSession WriteLogVerbose(string message);

	//	IExtendedConsoleSession WriteLogInformation(string message);

	//	IExtendedConsoleSession WriteLogWarning(string message);

	//	IExtendedConsoleSession WriteLogError(string message);

	//	IExtendedConsoleSession WriteLogFatal(string message);
	//}


	public interface IExtendedConsoleTableWriter
	{
		//IExtendedConsoleSession WriteTable(
		//	Action<ITableWriter> @this);

		IExtendedConsoleSession WriteTable<T0>(
			Action<ITableWriter<T0>> @this);

		IExtendedConsoleSession WriteTable<T0, T1>(
			Action<ITableWriter<T0, T1>> @this);

		IExtendedConsoleSession WriteTable<T0, T1, T2>(
			Action<ITableWriter<T0, T1, T2>> @this);

		IExtendedConsoleSession WriteTable<T0, T1, T2, T3>(
			Action<ITableWriter<T0, T1, T2, T3>> @this);

		IExtendedConsoleSession WriteTable<T0, T1, T2, T3, T4>(
			Action<ITableWriter<T0, T1, T2, T3, T4>> @this);

		IExtendedConsoleSession WriteTable<T0, T1, T2, T3, T4, T5>(
			Action<ITableWriter<T0, T1, T2, T3, T4, T5>> @this);

		IExtendedConsoleSession WriteTable<T0, T1, T2, T3, T4, T5, T6>(
			Action<ITableWriter<T0, T1, T2, T3, T4, T5, T6>> @this);

		IExtendedConsoleSession WriteTable<T0, T1, T2, T3, T4, T5, T6, T7>(
			Action<ITableWriter<T0, T1, T2, T3, T4, T5, T6, T7>> @this);

		IExtendedConsoleSession WriteTable<T0, T1, T2, T3, T4, T5, T6, T7, T8>(
			Action<ITableWriter<T0, T1, T2, T3, T4, T5, T6, T7, T8>> @this);

		IExtendedConsoleSession WriteTable<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9>(
			Action<ITableWriter<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9>> @this);

	}


	public interface IExtendedConsoleSession
		: IExtendedConsoleAppearance,
			IExtendedConsoleCursorInfo,
			IExtendedConsoleBufferInfo,
			IExtendedConsoleInfrastructureInfo,
			IExtendedConsoleGeometryInfo,
			IExtendedConsoleStateInfo,
			IExtendedConsoleGeneralInfo,
			IExtendedConsoleCodeWriter,
			//IExtendedConsoleLog,
			IExtendedConsoleTableWriter
	{
		IExtendedConsoleSession ReplaceAllColorsWithDefaults();
		IExtendedConsoleSession ReplaceColor(Color oldColor, Color newColor);
		IExtendedConsoleSession ResetColor();

		TableGeometry GetTableGeometry();

		//IExtendedConsoleSession SetWindowPosition(int left, int top);
		//IExtendedConsoleSession SetWindowSize(int width, int height);

		//IExtendedConsoleSession SetError(TextWriter newError);
		//IExtendedConsoleSession SetIn(TextReader newIn);
		//IExtendedConsoleSession SetOut(TextWriter newOut);


		/// <summary>
		///		Writes the provided <see cref="bool"/> <paramref name="value"/> parameter to the Console,
		///		without any colorization or stylization.
		/// </summary>
		/// <param name="value">
		///		A <see cref="bool"/> value to write to the console.
		/// </param>
		/// <returns>
		///		Returns a <see cref="IExtendedConsoleSession"/> instance for fluent, chained calls.
		/// </returns>
		IExtendedConsoleSession Write(bool value);

		/// <inheritdoc cref="Console.Write(char)"/>
		/// <summary>
		///		Writes the provided <see cref="char"/> <paramref name="value"/> parameter to the Console,
		///		without any colorization or stylization.
		/// </summary>
		/// <param name="value">
		///		A <see cref="char"/> value to write to the console.
		/// </param>
		/// <returns>
		///		Returns a <see cref="IExtendedConsoleSession"/> instance for fluent, chained calls.
		/// </returns>
		IExtendedConsoleSession Write(char value);

		/// <inheritdoc cref="Console.Write(char[])"/>
		/// <returns>
		///		Returns a <see cref="IExtendedConsoleSession"/> instance for fluent, chained calls.
		/// </returns>
		IExtendedConsoleSession Write(char[] value);

		/// <inheritdoc cref="Console.Write(char[],int,int)"/>
		/// <returns>
		///		Returns a <see cref="IExtendedConsoleSession"/> instance for fluent, chained calls.
		/// </returns>
		IExtendedConsoleSession Write(char[] buffer, int index, int count);

		/// <inheritdoc cref="Console.Write(decimal)"/>
		/// <returns>
		///		Returns a <see cref="IExtendedConsoleSession"/> instance for fluent, chained calls.
		/// </returns>
		IExtendedConsoleSession Write(decimal value);

		/// <inheritdoc cref="Console.Write(double)"/>
		/// <returns>
		///		Returns a <see cref="IExtendedConsoleSession"/> instance for fluent, chained calls.
		/// </returns>
		IExtendedConsoleSession Write(double value);

		/// <inheritdoc cref="Console.Write(float)"/>
		/// <returns>
		///		Returns a <see cref="IExtendedConsoleSession"/> instance for fluent, chained calls.
		/// </returns>
		IExtendedConsoleSession Write(float value);

		/// <inheritdoc cref="Console.Write(int)"/>
		/// <returns>
		///		Returns a <see cref="IExtendedConsoleSession"/> instance for fluent, chained calls.
		/// </returns>
		IExtendedConsoleSession Write(int value);

		/// <inheritdoc cref="Console.Write(long)"/>
		/// <returns>
		///		Returns a <see cref="IExtendedConsoleSession"/> instance for fluent, chained calls.
		/// </returns>
		IExtendedConsoleSession Write(long value);

		/// <inheritdoc cref="Console.Write(object)"/>
		/// <returns>
		///		Returns a <see cref="IExtendedConsoleSession"/> instance for fluent, chained calls.
		/// </returns>
		IExtendedConsoleSession Write(object value);

		/// <inheritdoc cref="Console.Write(string)"/>
		/// <returns>
		///		Returns a <see cref="IExtendedConsoleSession"/> instance for fluent, chained calls.
		/// </returns>
		IExtendedConsoleSession Write(string value);

		/// <inheritdoc cref="Console.Write(bool)"/>
		/// <summary>
		///		Writes the specified <see cref="bool"/> value to the standard output stream in the
		///		specified <see cref="Color"/>.
		/// </summary>
		/// <param name="value">
		///		The value to write.
		/// </param>
		/// <param name="color">
		///		The <see cref="Color"/> foreground in which to write in.
		/// </param>
		/// <returns>
		///		Returns a <see cref="IExtendedConsoleSession"/> instance for fluent, chained calls.
		/// </returns>
		IExtendedConsoleSession Write(bool value, Color color);

		/// <inheritdoc cref="Console.Write(char)"/>
		/// <summary>
		///		Writes the specified <see cref="char"/> value to the standard output stream in the
		///		specified <see cref="Color"/>.
		/// </summary>
		/// <param name="value">
		///		The value to write.
		/// </param>
		/// <param name="color">
		///		The <see cref="Color"/> foreground in which to write in.
		/// </param>
		/// <returns>
		///		Returns a <see cref="IExtendedConsoleSession"/> instance for fluent, chained calls.
		/// </returns>
		IExtendedConsoleSession Write(char value, Color color);

		/// <inheritdoc cref="Console.Write(char[])"/>
		/// <summary>
		///		Writes the specified <see cref="T:char[]"/> value to the standard output stream in the
		///		specified <see cref="Color"/>.
		/// </summary>
		/// <param name="value">
		///		The value to write.
		/// </param>
		/// <param name="color">
		///		The <see cref="Color"/> foreground in which to write in.
		/// </param>
		/// <returns>
		///		Returns a <see cref="IExtendedConsoleSession"/> instance for fluent, chained calls.
		/// </returns>
		IExtendedConsoleSession Write(char[] value, Color color);

		/// <inheritdoc cref="Console.Write(char[],int,int)"/>
		/// <summary>
		///		Writes the specified subarray of Unicode character value to the standard output stream in the
		///		specified <see cref="Color"/>.
		/// </summary>
		/// <param name="buffer">
		///		An array of Unicode characters.
		/// </param>
		/// <param name="index">
		///		The starting position in <paramref name="buffer"/>.
		/// </param>
		/// <param name="count">
		///		The number of characters to write.
		/// </param>
		/// <param name="color">
		///		The <see cref="Color"/> foreground in which to write in.
		/// </param>
		/// <returns>
		///		Returns a <see cref="IExtendedConsoleSession"/> instance for fluent, chained calls.
		/// </returns>
		IExtendedConsoleSession Write(char[] buffer, int index, int count, Color color);

		/// <inheritdoc cref="Console.Write(decimal)"/>
		/// <summary>
		///		Writes the specified <see cref="decimal"/> value to the standard output stream in the
		///		specified <see cref="Color"/>.
		/// </summary>
		/// <param name="value">
		///		The value to write.
		/// </param>
		/// <param name="color">
		///		The <see cref="Color"/> foreground in which to write in.
		/// </param>
		/// <returns>
		///		Returns a <see cref="IExtendedConsoleSession"/> instance for fluent, chained calls.
		/// </returns>
		IExtendedConsoleSession Write(decimal value, Color color);

		/// <inheritdoc cref="Console.Write(double)"/>
		/// <summary>
		///		Writes the specified <see cref="double"/> value to the standard output stream in the
		///		specified <see cref="Color"/>.
		/// </summary>
		/// <param name="value">
		///		The value to write.
		/// </param>
		/// <param name="color">
		///		The <see cref="Color"/> foreground in which to write in.
		/// </param>
		/// <returns>
		///		Returns a <see cref="IExtendedConsoleSession"/> instance for fluent, chained calls.
		/// </returns>
		IExtendedConsoleSession Write(double value, Color color);

		/// <inheritdoc cref="Console.Write(float)"/>
		/// <summary>
		///		Writes the specified <see cref="float"/> value to the standard output stream in the
		///		specified <see cref="Color"/>.
		/// </summary>
		/// <param name="value">
		///		The value to write.
		/// </param>
		/// <param name="color">
		///		The <see cref="Color"/> foreground in which to write in.
		/// </param>
		/// <returns>
		///		Returns a <see cref="IExtendedConsoleSession"/> instance for fluent, chained calls.
		/// </returns>
		IExtendedConsoleSession Write(float value, Color color);

		/// <inheritdoc cref="Console.Write(int)"/>
		/// <summary>
		///		Writes the specified <see cref="int"/> value to the standard output stream in the
		///		specified <see cref="Color"/>.
		/// </summary>
		/// <param name="value">
		///		The value to write.
		/// </param>
		/// <param name="color">
		///		The <see cref="Color"/> foreground in which to write in.
		/// </param>
		/// <returns>
		///		Returns a <see cref="IExtendedConsoleSession"/> instance for fluent, chained calls.
		/// </returns>
		IExtendedConsoleSession Write(int value, Color color);

		/// <inheritdoc cref="Console.Write(long)"/>
		/// <summary>
		///		Writes the specified <see cref="long"/> value to the standard output stream in the
		///		specified <see cref="Color"/>.
		/// </summary>
		/// <param name="value">
		///		The value to write.
		/// </param>
		/// <param name="color">
		///		The <see cref="Color"/> foreground in which to write in.
		/// </param>
		/// <returns>
		///		Returns a <see cref="IExtendedConsoleSession"/> instance for fluent, chained calls.
		/// </returns>
		IExtendedConsoleSession Write(long value, Color color);

		/// <inheritdoc cref="Console.Write(object)"/>
		/// <summary>
		///		Writes the specified <see cref="object"/> value to the standard output stream in the
		///		specified <see cref="Color"/>.
		/// </summary>
		/// <param name="value">
		///		The value to write.
		/// </param>
		/// <param name="color">
		///		The <see cref="Color"/> foreground in which to write in.
		/// </param>
		/// <returns>
		///		Returns a <see cref="IExtendedConsoleSession"/> instance for fluent, chained calls.
		/// </returns>
		IExtendedConsoleSession Write(object value, Color color);

		/// <inheritdoc cref="Console.Write(string)"/>
		/// <summary>
		///		Writes the specified <see cref="string"/> value to the standard output stream in the
		///		specified <see cref="Color"/>.
		/// </summary>
		/// <param name="value">
		///		The value to write.
		/// </param>
		/// <param name="color">
		///		The <see cref="Color"/> foreground in which to write in.
		/// </param>
		/// <returns>
		///		Returns a <see cref="IExtendedConsoleSession"/> instance for fluent, chained calls.
		/// </returns>
		IExtendedConsoleSession Write(string value, Color color);


		[StringFormatMethod("format")]
		IExtendedConsoleSession Write(string format, Color color, params object[] args);
		[StringFormatMethod("format")]
		IExtendedConsoleSession Write(string format, object arg0);
		[StringFormatMethod("format")]
		IExtendedConsoleSession Write(string format, object arg0, Color color);
		[StringFormatMethod("format")]
		IExtendedConsoleSession Write(string format, object arg0, object arg1);
		[StringFormatMethod("format")]
		IExtendedConsoleSession Write(string format, object arg0, object arg1, Color color);
		[StringFormatMethod("format")]
		IExtendedConsoleSession Write(string format, object arg0, object arg1, object arg2);
		[StringFormatMethod("format")]
		IExtendedConsoleSession Write(string format, object arg0, object arg1, object arg2, Color color);
		[StringFormatMethod("format")]
		IExtendedConsoleSession Write(string format, object arg0, object arg1, object arg2, object arg3);
		[StringFormatMethod("format")]
		IExtendedConsoleSession Write(string format, object arg0, object arg1, object arg2, object arg3, Color color);
		[StringFormatMethod("format")]
		IExtendedConsoleSession Write(string format, params object[] args);

		IExtendedConsoleSession Write(uint value);
		IExtendedConsoleSession Write(uint value, Color color);
		IExtendedConsoleSession Write(ulong value);
		IExtendedConsoleSession Write(ulong value, Color color);

		IExtendedConsoleSession WriteAlternating(bool value, ColorAlternator alternator);
		IExtendedConsoleSession WriteAlternating(char value, ColorAlternator alternator);
		IExtendedConsoleSession WriteAlternating(char[] value, ColorAlternator alternator);
		IExtendedConsoleSession WriteAlternating(char[] buffer, int index, int count, ColorAlternator alternator);
		IExtendedConsoleSession WriteAlternating(decimal value, ColorAlternator alternator);
		IExtendedConsoleSession WriteAlternating(double value, ColorAlternator alternator);
		IExtendedConsoleSession WriteAlternating(float value, ColorAlternator alternator);
		IExtendedConsoleSession WriteAlternating(int value, ColorAlternator alternator);
		IExtendedConsoleSession WriteAlternating(long value, ColorAlternator alternator);
		IExtendedConsoleSession WriteAlternating(object value, ColorAlternator alternator);
		IExtendedConsoleSession WriteAlternating(string value, ColorAlternator alternator);

		[StringFormatMethod("format")]
		IExtendedConsoleSession WriteAlternating(string format, ColorAlternator alternator, params object[] args);
		[StringFormatMethod("format")]
		IExtendedConsoleSession WriteAlternating(string format, object arg0, ColorAlternator alternator);
		[StringFormatMethod("format")]
		IExtendedConsoleSession WriteAlternating(string format, object arg0, object arg1, ColorAlternator alternator);
		[StringFormatMethod("format")]
		IExtendedConsoleSession WriteAlternating(string format, object arg0, object arg1, object arg2, ColorAlternator alternator);
		[StringFormatMethod("format")]
		IExtendedConsoleSession WriteAlternating(string format, object arg0, object arg1, object arg2, object arg3, ColorAlternator alternator);

		IExtendedConsoleSession WriteAlternating(uint value, ColorAlternator alternator);
		IExtendedConsoleSession WriteAlternating(ulong value, ColorAlternator alternator);

		//IExtendedConsoleSession WriteAscii(string value);
		//IExtendedConsoleSession WriteAscii(string value, Color color);
		//IExtendedConsoleSession WriteAscii(string value, FigletFont font);
		//IExtendedConsoleSession WriteAscii(string value, FigletFont font, Color color);
		//IExtendedConsoleSession WriteAsciiAlternating(string value, ColorAlternator alternator);
		//IExtendedConsoleSession WriteAsciiAlternating(string value, FigletFont font, ColorAlternator alternator);
		//IExtendedConsoleSession WriteAsciiStyled(string value, FigletFont font, StyleSheet styleSheet);
		//IExtendedConsoleSession WriteAsciiStyled(string value, StyleSheet styleSheet);

		[StringFormatMethod("format")]
		IExtendedConsoleSession WriteFormatted(string format, Color styledColor, Color defaultColor, params object[] args);
		[StringFormatMethod("format")]
		IExtendedConsoleSession WriteFormatted(string format, Color defaultColor, params Formatter[] args);
		[StringFormatMethod("format")]
		IExtendedConsoleSession WriteFormatted(string format, Formatter arg0, Color defaultColor);
		[StringFormatMethod("format")]
		IExtendedConsoleSession WriteFormatted(string format, Formatter arg0, Formatter arg1, Color defaultColor);
		[StringFormatMethod("format")]
		IExtendedConsoleSession WriteFormatted(string format, Formatter arg0, Formatter arg1, Formatter arg2, Color defaultColor);
		[StringFormatMethod("format")]
		IExtendedConsoleSession WriteFormatted(string format, Formatter arg0, Formatter arg1, Formatter arg2, Formatter arg3, Color defaultColor);
		[StringFormatMethod("format")]
		IExtendedConsoleSession WriteFormatted(string format, object arg0, Color styledColor, Color defaultColor);
		[StringFormatMethod("format")]
		IExtendedConsoleSession WriteFormatted(string format, object arg0, object arg1, Color styledColor, Color defaultColor);
		[StringFormatMethod("format")]
		IExtendedConsoleSession WriteFormatted(string format, object arg0, object arg1, object arg2, Color styledColor, Color defaultColor);
		[StringFormatMethod("format")]
		IExtendedConsoleSession WriteFormatted(string format, object arg0, object arg1, object arg2, object arg3, Color styledColor, Color defaultColor);

		IExtendedConsoleSession WriteLine();

		IExtendedConsoleSession WriteLine(bool value);
		IExtendedConsoleSession WriteLine(char value);
		IExtendedConsoleSession WriteLine(char[] value);
		IExtendedConsoleSession WriteLine(char[] buffer, int index, int count);
		IExtendedConsoleSession WriteLine(decimal value);
		IExtendedConsoleSession WriteLine(double value);
		IExtendedConsoleSession WriteLine(float value);
		IExtendedConsoleSession WriteLine(int value);
		IExtendedConsoleSession WriteLine(long value);
		IExtendedConsoleSession WriteLine(object value);
		IExtendedConsoleSession WriteLine(string value);

		IExtendedConsoleSession WriteLine(bool value, Color color);
		IExtendedConsoleSession WriteLine(char value, Color color);
		IExtendedConsoleSession WriteLine(char[] value, Color color);
		IExtendedConsoleSession WriteLine(char[] buffer, int index, int count, Color color);
		IExtendedConsoleSession WriteLine(decimal value, Color color);
		IExtendedConsoleSession WriteLine(double value, Color color);
		IExtendedConsoleSession WriteLine(float value, Color color);
		IExtendedConsoleSession WriteLine(int value, Color color);
		IExtendedConsoleSession WriteLine(long value, Color color);
		IExtendedConsoleSession WriteLine(object value, Color color);
		IExtendedConsoleSession WriteLine(string value, Color color);

		//IExtendedConsoleSession WriteCode(bool value);
		//IExtendedConsoleSession WriteCode(char value);
		//IExtendedConsoleSession WriteCode(char[] value);
		//IExtendedConsoleSession WriteCode(char[] buffer, int index, int count);
		//IExtendedConsoleSession WriteCode(decimal value);
		//IExtendedConsoleSession WriteCode(double value);
		//IExtendedConsoleSession WriteCode(float value);
		//IExtendedConsoleSession WriteCode(int value);
		//IExtendedConsoleSession WriteCode(long value);
		//IExtendedConsoleSession WriteCode(string value);
		//IExtendedConsoleSession WriteCodeIdentifier(string value);
		//IExtendedConsoleSession WriteCode(Type value);
		//IExtendedConsoleSession WriteCodeMethod(string value);
		//IExtendedConsoleSession WriteCodeKeyword(string value);


		IExtendedConsoleSession WriteCode(string lexeme, SimpleCodeKind codeKind);




		[StringFormatMethod("format")]
		IExtendedConsoleSession WriteLine(string format, Color color, params object[] args);
		[StringFormatMethod("format")]
		IExtendedConsoleSession WriteLine(string format, object arg0);
		[StringFormatMethod("format")]
		IExtendedConsoleSession WriteLine(string format, object arg0, Color color);
		[StringFormatMethod("format")]
		IExtendedConsoleSession WriteLine(string format, object arg0, object arg1);
		[StringFormatMethod("format")]
		IExtendedConsoleSession WriteLine(string format, object arg0, object arg1, Color color);
		[StringFormatMethod("format")]
		IExtendedConsoleSession WriteLine(string format, object arg0, object arg1, object arg2);
		[StringFormatMethod("format")]
		IExtendedConsoleSession WriteLine(string format, object arg0, object arg1, object arg2, Color color);
		[StringFormatMethod("format")]
		IExtendedConsoleSession WriteLine(string format, object arg0, object arg1, object arg2, object arg3);
		[StringFormatMethod("format")]
		IExtendedConsoleSession WriteLine(string format, object arg0, object arg1, object arg2, object arg3, Color color);
		[StringFormatMethod("format")]
		IExtendedConsoleSession WriteLine(string format, params object[] args);

		IExtendedConsoleSession WriteLine(uint value);
		IExtendedConsoleSession WriteLine(ulong value);

		IExtendedConsoleSession WriteLine(uint value, Color color);
		IExtendedConsoleSession WriteLine(ulong value, Color color);

		IExtendedConsoleSession WriteLineAlternating(bool value, ColorAlternator alternator);
		IExtendedConsoleSession WriteLineAlternating(char value, ColorAlternator alternator);
		IExtendedConsoleSession WriteLineAlternating(char[] value, ColorAlternator alternator);
		IExtendedConsoleSession WriteLineAlternating(char[] buffer, int index, int count, ColorAlternator alternator);
		IExtendedConsoleSession WriteLineAlternating(ColorAlternator alternator);
		IExtendedConsoleSession WriteLineAlternating(decimal value, ColorAlternator alternator);
		IExtendedConsoleSession WriteLineAlternating(double value, ColorAlternator alternator);
		IExtendedConsoleSession WriteLineAlternating(float value, ColorAlternator alternator);
		IExtendedConsoleSession WriteLineAlternating(int value, ColorAlternator alternator);
		IExtendedConsoleSession WriteLineAlternating(long value, ColorAlternator alternator);
		IExtendedConsoleSession WriteLineAlternating(object value, ColorAlternator alternator);
		IExtendedConsoleSession WriteLineAlternating(string value, ColorAlternator alternator);


		[StringFormatMethod("format")]
		IExtendedConsoleSession WriteLineAlternating(string format, ColorAlternator alternator, params object[] args);
		[StringFormatMethod("format")]
		IExtendedConsoleSession WriteLineAlternating(string format, object arg0, ColorAlternator alternator);
		[StringFormatMethod("format")]
		IExtendedConsoleSession WriteLineAlternating(string format, object arg0, object arg1, ColorAlternator alternator);
		[StringFormatMethod("format")]
		IExtendedConsoleSession WriteLineAlternating(string format, object arg0, object arg1, object arg2, ColorAlternator alternator);
		[StringFormatMethod("format")]
		IExtendedConsoleSession WriteLineAlternating(string format, object arg0, object arg1, object arg2, object arg3, ColorAlternator alternator);

		IExtendedConsoleSession WriteLineAlternating(uint value, ColorAlternator alternator);
		IExtendedConsoleSession WriteLineAlternating(ulong value, ColorAlternator alternator);

		[StringFormatMethod("format")]
		IExtendedConsoleSession WriteLineFormatted(string format, Color styledColor, Color defaultColor, IEnumerable<object> args);
		[StringFormatMethod("format")]
		IExtendedConsoleSession WriteLineFormatted(string format, Color styledColor, Color defaultColor, params object[] args);
		[StringFormatMethod("format")]
		IExtendedConsoleSession WriteLineFormatted(string format, Color defaultColor, params Formatter[] args);
		[StringFormatMethod("format")]
		IExtendedConsoleSession WriteLineFormatted(string format, Formatter arg0, Color defaultColor);
		[StringFormatMethod("format")]
		IExtendedConsoleSession WriteLineFormatted(string format, Formatter arg0, Formatter arg1, Color defaultColor);
		[StringFormatMethod("format")]
		IExtendedConsoleSession WriteLineFormatted(string format, Formatter arg0, Formatter arg1, Formatter arg2, Color defaultColor);
		[StringFormatMethod("format")]
		IExtendedConsoleSession WriteLineFormatted(string format, Formatter arg0, Formatter arg1, Formatter arg2, Formatter arg3, Color defaultColor);
		[StringFormatMethod("format")]
		IExtendedConsoleSession WriteLineFormatted(string format, object arg0, Color styledColor, Color defaultColor);
		[StringFormatMethod("format")]
		IExtendedConsoleSession WriteLineFormatted(string format, object arg0, object arg1, Color styledColor, Color defaultColor);
		[StringFormatMethod("format")]
		IExtendedConsoleSession WriteLineFormatted(string format, object arg0, object arg1, object arg2, Color styledColor, Color defaultColor);
		[StringFormatMethod("format")]
		IExtendedConsoleSession WriteLineFormatted(string format, object arg0, object arg1, object arg2, object arg3, Color styledColor, Color defaultColor);


		IExtendedConsoleSession WriteLineStyled(bool value, StyleSheet styleSheet);
		IExtendedConsoleSession WriteLineStyled(char value, StyleSheet styleSheet);
		IExtendedConsoleSession WriteLineStyled(char[] buffer, int index, int count, StyleSheet styleSheet);
		IExtendedConsoleSession WriteLineStyled(char[] value, StyleSheet styleSheet);
		IExtendedConsoleSession WriteLineStyled(decimal value, StyleSheet styleSheet);
		IExtendedConsoleSession WriteLineStyled(double value, StyleSheet styleSheet);
		IExtendedConsoleSession WriteLineStyled(float value, StyleSheet styleSheet);
		IExtendedConsoleSession WriteLineStyled(int value, StyleSheet styleSheet);
		IExtendedConsoleSession WriteLineStyled(long value, StyleSheet styleSheet);


		[StringFormatMethod("format")]
		IExtendedConsoleSession WriteLineStyled(string format, object arg0, object arg1, object arg2, object arg3, StyleSheet styleSheet);
		[StringFormatMethod("format")]
		IExtendedConsoleSession WriteLineStyled(string format, object arg0, object arg1, object arg2, StyleSheet styleSheet);
		[StringFormatMethod("format")]
		IExtendedConsoleSession WriteLineStyled(string format, object arg0, object arg1, StyleSheet styleSheet);
		[StringFormatMethod("format")]
		IExtendedConsoleSession WriteLineStyled(string format, object arg0, StyleSheet styleSheet);


		IExtendedConsoleSession WriteLineStyled(string value, StyleSheet styleSheet);
		//IExtendedConsoleSession WriteLineStyled(StyledString value, StyleSheet styleSheet);
		IExtendedConsoleSession WriteLineStyled(StyleSheet styleSheet);
		IExtendedConsoleSession WriteLineStyled(StyleSheet styleSheet, string format, params object[] args);
		IExtendedConsoleSession WriteLineStyled(uint value, StyleSheet styleSheet);
		IExtendedConsoleSession WriteLineStyled(ulong value, StyleSheet styleSheet);
		IExtendedConsoleSession WriteLineWithGradient<T>(IEnumerable<T> input, Color startColor, Color endColor, int maxColorsInGradient = 16);
		IExtendedConsoleSession WriteStyled(bool value, StyleSheet styleSheet);
		IExtendedConsoleSession WriteStyled(char value, StyleSheet styleSheet);
		IExtendedConsoleSession WriteStyled(char[] buffer, int index, int count, StyleSheet styleSheet);
		IExtendedConsoleSession WriteStyled(char[] value, StyleSheet styleSheet);
		IExtendedConsoleSession WriteStyled(decimal value, StyleSheet styleSheet);
		IExtendedConsoleSession WriteStyled(double value, StyleSheet styleSheet);
		IExtendedConsoleSession WriteStyled(float value, StyleSheet styleSheet);
		IExtendedConsoleSession WriteStyled(int value, StyleSheet styleSheet);
		IExtendedConsoleSession WriteStyled(long value, StyleSheet styleSheet);
		IExtendedConsoleSession WriteStyled(object value, StyleSheet styleSheet);


		[StringFormatMethod("format")]
		IExtendedConsoleSession WriteStyled(string format, object arg0, object arg1, object arg2, object arg3, StyleSheet styleSheet);
		[StringFormatMethod("format")]
		IExtendedConsoleSession WriteStyled(string format, object arg0, object arg1, object arg2, StyleSheet styleSheet);
		[StringFormatMethod("format")]
		IExtendedConsoleSession WriteStyled(string format, object arg0, object arg1, StyleSheet styleSheet);
		[StringFormatMethod("format")]
		IExtendedConsoleSession WriteStyled(string format, object arg0, StyleSheet styleSheet);


		IExtendedConsoleSession WriteStyled(string value, StyleSheet styleSheet);
		[StringFormatMethod("format")]
		IExtendedConsoleSession WriteStyled(StyleSheet styleSheet, string format, params object[] args);
		IExtendedConsoleSession WriteStyled(uint value, StyleSheet styleSheet);
		IExtendedConsoleSession WriteStyled(ulong value, StyleSheet styleSheet);

		IExtendedConsoleSession WriteWithGradient<T>(IEnumerable<T> input, Color startColor, Color endColor, int maxColorsInGradient = 16);
	}
}
