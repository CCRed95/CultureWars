using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using CultureWars.Core.FluentConsole.Colorization;
using CultureWars.Core.FluentConsole.Colorization.Factories;
using CultureWars.Core.FluentConsole.Colorization.Stylization;
using CultureWars.Core.FluentConsole.Exceptions;
using Formatter = CultureWars.Core.FluentConsole.Formatting.Formatter;

namespace CultureWars.Core.FluentConsole
{
	//public enum CodeKind
	//{
	//	DefaultText,

	//	Brace,
	//	Comment,

	//	NonStaticClassIdentifier,
	//	StaticClassIdentifier,

	//	ConstIdentifier,
	//	DelegateIdentifier,

	//	//EnumIdentifierBase
	//	EnumIdentifier,
	//	EnumMemberIdentifier,

	//	EventIdentifier,
	//	ExtensionMethodIdentifier,
	//	FieldIdentifier,
	//	InterfaceIdentifier,

	//	//MethodIdentifierBase,
	//	LocalMethodIdentifier,
	//	MethodIdentifier,

	//	NamespaceIdentifier,
	//	ParameterIdentifier,

	//	PathIdentifier,
	//	PropertyIdentifier,
	//	StructIdentifier,
	//	TypeParameterIdentifier,
	//	ValueTupleComponentIdentifier,

	//	//VariableIdentifierBase,
	//	VariableLocalImmutableIdentifier,
	//	VariableLocalMutableIdentifier,

	//	Keyword,
	//	Number,
	//	Operator,

	//	//RegexBase,
	//	Regex,
	//	RegexComment,
	//	RegexGroup,
	//	RegexIdentifier,
	//	RegexMatchBase,
	//	RegexMatchedSelection,
	//	RegexMatchedValueBase,
	//	RegexSet,

	//	//StringBase
	//	String,
	//	StringFormatItem,
	//	StringLiteral,
	//	StringEscapeCharacter,

	//	TodoItem
	//}


	public partial class ExtendedConsole
		: IExtendedConsoleSession
	{
		public static readonly ExtendedConsole XConsole = new ExtendedConsole();


		private static int _indentLevel = 0;
		private static int _tabWidthSpaces = 2;
		private static bool _hasCurrentTabChanged = true;
		private static string _prefixCached = "";


		public int IndentLevel
		{
			get => _indentLevel;
			set
			{
				_indentLevel = value;
				_hasCurrentTabChanged = true;
			}
		}

		internal static string getPrefix()
		{
			if (!_hasCurrentTabChanged)
				return _prefixCached;

			var sb = new StringBuilder();

			var total = _indentLevel * _tabWidthSpaces;

			for (var i = 0; i < total; i++)
			{
				sb.Append(" ");
			}

			_prefixCached = sb.ToString();

			return _prefixCached;
		}

		protected IExtendedConsoleSession Prefix()
		{
			Console.Write(getPrefix());
			return this;
		}

		public IExtendedConsoleSession Indent()
		{
			_indentLevel++;
			return this;
		}

		public IExtendedConsoleSession Outdent()
		{
			_indentLevel--;
			return this;
		}

		public IExtendedConsoleSession ClearIndent()
		{
			_indentLevel = 0;
			return this;
		}


		public IExtendedConsoleSession InScope(
			Action<IExtendedConsoleSession> @this)
		{
			_indentLevel++;
			@this.Invoke(this);
			_indentLevel--;
                      
			return this;
		}

		public Color BackgroundColor
		{
			get => colorManager.GetColor(Console.BackgroundColor);
			set => Console.BackgroundColor = colorManager.GetConsoleColor(value);
		}

		public int BufferHeight
		{
			get => Console.BufferHeight;
			set => Console.BufferHeight = value;
		}

		public int BufferWidth
		{
			get => Console.BufferWidth;
			set => Console.BufferWidth = value;
		}

		public bool CapsLock
		{
			get => Console.CapsLock; 
		}

		public int CursorLeft
		{
			get => Console.CursorLeft;
			set => Console.CursorLeft = value;
		}

		public int CursorSize
		{
			get => Console.CursorSize;
			set => Console.CursorSize = value;
		}

		public int CursorTop
		{
			get => Console.CursorTop;
			set => Console.CursorTop = value;
		}

		public bool CursorVisible
		{
			get => Console.CursorVisible;
			set => Console.CursorVisible = value;
		}

		public TextWriter Error
		{
			get => Console.Error;
		}

		public Color ForegroundColor
		{
			get => colorManager.GetColor(Console.ForegroundColor);
			set => Console.ForegroundColor = colorManager.GetConsoleColor(value);
		}

		public TextReader In
		{
			get => Console.In;
		}

		public Encoding InputEncoding
		{
			get => Console.InputEncoding;
			set => Console.InputEncoding = value;
		}

		#if !NET40
		public bool IsErrorRedirected
		{
			get => Console.IsErrorRedirected;
		}

		public bool IsInputRedirected
		{
			get => Console.IsInputRedirected;
		}

		public bool IsOutputRedirected
		{
			get => Console.IsOutputRedirected;
		}

		#endif

		public bool KeyAvailable
		{
			get => Console.KeyAvailable;
		}

		public int LargestWindowHeight
		{
			get => Console.LargestWindowHeight;
		}

		public int LargestWindowWidth
		{
			get => Console.LargestWindowWidth;
		}

		public bool NumberLock
		{
			get => Console.NumberLock;
		}

		public TextWriter Out
		{
			get => Console.Out;
		}

		public Encoding OutputEncoding
		{
			get => Console.OutputEncoding;
			set => Console.OutputEncoding = value;
		}

		public string Title
		{
			get => Console.Title;
			set => Console.Title = value;
		}

		public bool TreatControlCAsInput
		{
			get => Console.TreatControlCAsInput;
			set => Console.TreatControlCAsInput = value;
		}

		public int WindowHeight
		{
			get => Console.WindowHeight;
			set => Console.WindowHeight = value;
		}

		public int WindowLeft
		{
			get => Console.WindowLeft;
			set => Console.WindowLeft = value;
		}

		public int WindowTop
		{
			get => Console.WindowTop;
			set => Console.WindowTop = value;
		}

		public int WindowWidth
		{
			get => Console.WindowWidth;
			set => Console.WindowWidth = value;
		}

		public event ConsoleCancelEventHandler CancelKeyPress = delegate { };



		protected ExtendedConsole()
		{
			isInCompatibilityMode = false;
			isWindows = ColorManager.IsWindows();

			try
			{
				if (isWindows)
					defaultColorMap = new ColorMapper().GetBufferColors();
			}
			catch (ConsoleAccessException)
			{
				isInCompatibilityMode = true;
			}
			ReplaceAllColorsWithDefaults();

			Console.CancelKeyPress += Console_CancelKeyPress;
		}

		
		public IExtendedConsoleSession Write(
			bool value)
		{
			Prefix();
			Console.Write(value);
			return this;
		}

		public IExtendedConsoleSession Write(
			bool value,
			Color color)
		{
			WriteInColor(
				Console.Write,
				value,
				color);

			return this;
		}

		public IExtendedConsoleSession WriteAlternating(
			bool value,
			ColorAlternator alternator)
		{
			WriteInColorAlternating(
				Console.Write,
				value,
				alternator);

			return this;
		}

		public IExtendedConsoleSession WriteStyled(
			bool value,
			StyleSheet styleSheet)
		{
			WriteInColorStyled(
				WRITE_TRAILER,
				value,
				styleSheet);

			return this;
		}

		public IExtendedConsoleSession Write(
			char value)
		{
			Prefix();
			Console.Write(value);
			return this;
		}

		public IExtendedConsoleSession Write(
			char value, 
			Color color)
		{
			WriteInColor(Console.Write, value, color);
			return this;
		}

		public IExtendedConsoleSession WriteAlternating(
			char value, 
			ColorAlternator alternator)
		{
			WriteInColorAlternating(Console.Write, value, alternator);
			return this;
		}

		public IExtendedConsoleSession WriteStyled(
			char value, 
			StyleSheet styleSheet)
		{
			WriteInColorStyled(WRITE_TRAILER, value, styleSheet);
			return this;
		}

		public IExtendedConsoleSession Write(
			char[] value)
		{
			Console.Write(value);
			return this;
		}

		public IExtendedConsoleSession Write(
			char[] value, 
			Color color)
		{
			WriteInColor(Console.Write, value, color);
			return this;
		}

		public IExtendedConsoleSession WriteAlternating(
			char[] value, 
			ColorAlternator alternator)
		{
			WriteInColorAlternating(Console.Write, value, alternator);
			return this;
		}

		public IExtendedConsoleSession WriteStyled(
			char[] value, 
			StyleSheet styleSheet)
		{
			WriteInColorStyled(WRITE_TRAILER, value, styleSheet);
			return this;
		}

		public IExtendedConsoleSession Write(
			decimal value)
		{
			Console.Write(value);
			return this;
		}

		public IExtendedConsoleSession Write(
			decimal value, 
			Color color)
		{
			WriteInColor(Console.Write, value, color);
			return this;
		}

		public IExtendedConsoleSession WriteAlternating(
			decimal value,
			ColorAlternator alternator)
		{
			WriteInColorAlternating(Console.Write, value, alternator);
			return this;
		}

		public IExtendedConsoleSession WriteStyled(
			decimal value, 
			StyleSheet styleSheet)
		{
			WriteInColorStyled(WRITE_TRAILER, value, styleSheet);
			return this;
		}

		public IExtendedConsoleSession Write(
			double value)
		{
			Console.Write(value);
			return this;
		}

		public IExtendedConsoleSession Write(
			double value, 
			Color color)
		{
			WriteInColor(Console.Write, value, color);
			return this;
		}

		public IExtendedConsoleSession WriteAlternating(
			double value, 
			ColorAlternator alternator)
		{
			WriteInColorAlternating(Console.Write, value, alternator);
			return this;
		}

		public IExtendedConsoleSession WriteStyled(
			double value, 
			StyleSheet styleSheet)
		{
			WriteInColorStyled(WRITE_TRAILER, value, styleSheet);
			return this;
		}

		public IExtendedConsoleSession Write(
			float value)
		{
			Console.Write(value);
			return this;
		}

		public IExtendedConsoleSession Write(
			float value, 
			Color color)
		{
			WriteInColor(Console.Write, value, color);
			return this;
		}

		public IExtendedConsoleSession WriteAlternating(
			float value, 
			ColorAlternator alternator)
		{
			WriteInColorAlternating(Console.Write, value, alternator);
			return this;
		}

		public IExtendedConsoleSession WriteStyled(
			float value, 
			StyleSheet styleSheet)
		{
			WriteInColorStyled(WRITE_TRAILER, value, styleSheet);
			return this;
		}

		public IExtendedConsoleSession Write(
			int value)
		{
			Console.Write(value);
			return this;
		}

		public IExtendedConsoleSession Write(
			int value,
			Color color)
		{
			WriteInColor(Console.Write, value, color);
			return this;
		}

		public IExtendedConsoleSession WriteAlternating(
			int value, 
			ColorAlternator alternator)
		{
			WriteInColorAlternating(Console.Write, value, alternator);
			return this;
		}

		public IExtendedConsoleSession WriteStyled(
			int value, 
			StyleSheet styleSheet)
		{
			Console.Write(getPrefix());
			WriteInColorStyled(WRITE_TRAILER, value, styleSheet);
			return this;
		}

		public IExtendedConsoleSession Write(
			long value)
		{
			Console.Write(getPrefix());
			Console.Write(value);
			return this;
		}

		public IExtendedConsoleSession Write(
			long value, 
			Color color)
		{
			WriteInColor(Console.Write, value, color);
			return this;
		}

		public IExtendedConsoleSession WriteAlternating(
			long value, 
			ColorAlternator alternator)
		{
			WriteInColorAlternating(Console.Write, value, alternator);
			return this;
		}

		public IExtendedConsoleSession WriteStyled(
			long value, 
			StyleSheet styleSheet)
		{
			WriteInColorStyled(WRITE_TRAILER, value, styleSheet);
			return this;
		}

		public IExtendedConsoleSession Write(
			object value)
		{
			Console.Write(value);
			return this;
		}

		public IExtendedConsoleSession Write(
			object value,
			Color color)
		{
			WriteInColor(Console.Write, value, color);
			return this;
		}

		public IExtendedConsoleSession WriteAlternating(
			object value, 
			ColorAlternator alternator)
		{
			WriteInColorAlternating(Console.Write, value, alternator);
			return this;
		}

		public IExtendedConsoleSession WriteStyled(
			object value,
			StyleSheet styleSheet)
		{
			WriteInColorStyled(WRITE_TRAILER, value, styleSheet);
			return this;
		}

		public IExtendedConsoleSession Write(
			string value)
		{
			Console.Write(value);
			return this;
		}

		public IExtendedConsoleSession Write(
			string value, 
			Color color)
		{
			WriteInColor(Console.Write, value, color);
			return this;
		}

		public IExtendedConsoleSession WriteAlternating(
			string value, 
			ColorAlternator alternator)
		{
			WriteInColorAlternating(Console.Write, value, alternator);
			return this;
		}

		public IExtendedConsoleSession WriteStyled(
			string value,
			StyleSheet styleSheet)
		{
			WriteInColorStyled(WRITE_TRAILER, value, styleSheet);
			return this;
		}

		public IExtendedConsoleSession Write(
			uint value)
		{
			Console.Write(value);
			return this;
		}

		public IExtendedConsoleSession Write(
			uint value, 
			Color color)
		{
			WriteInColor(Console.Write, value, color);
			return this;
		}

		public IExtendedConsoleSession WriteAlternating(
			uint value,
			ColorAlternator alternator)
		{
			WriteInColorAlternating(Console.Write, value, alternator);
			return this;
		}

		public IExtendedConsoleSession WriteStyled(
			uint value, 
			StyleSheet styleSheet)
		{
			WriteInColorStyled(WRITE_TRAILER, value, styleSheet);
			return this;
		}

		public IExtendedConsoleSession Write(
			ulong value)
		{
			Console.Write(value);
			return this;
		}

		public IExtendedConsoleSession Write(
			ulong value,
			Color color)
		{
			WriteInColor(Console.Write, value, color);
			return this;
		}

		public IExtendedConsoleSession WriteAlternating(
			ulong value, 
			ColorAlternator alternator)
		{
			WriteInColorAlternating(Console.Write, value, alternator);
			return this;
		}

		public IExtendedConsoleSession WriteStyled(
			ulong value,
			StyleSheet styleSheet)
		{
			WriteInColorStyled(WRITE_TRAILER, value, styleSheet);
			return this;
		}

		public IExtendedConsoleSession Write(
			string format, 
			object arg0)
		{
			Console.Write(format, arg0);
			return this;
		}

		public IExtendedConsoleSession Write(
			string format,
			object arg0, 
			Color color)
		{
			WriteInColor(Console.Write, format, arg0, color);
			return this;
		}

		public IExtendedConsoleSession WriteAlternating(
			string format,
			object arg0,
			ColorAlternator alternator)
		{
			WriteInColorAlternating(Console.Write, format, arg0, alternator);
			return this;
		}

		public IExtendedConsoleSession WriteStyled(
			string format,
			object arg0,
			StyleSheet styleSheet)
		{
			WriteInColorStyled(WRITE_TRAILER, format, arg0, styleSheet);
			return this;
		}

		public IExtendedConsoleSession WriteFormatted(
			string format,
			object arg0,
			Color styledColor,
			Color defaultColor)
		{
			WriteInColorFormatted(WRITE_TRAILER, format, arg0, styledColor, defaultColor);
			return this;
		}

		public IExtendedConsoleSession WriteFormatted(
			string format,
			Formatter arg0, 
			Color defaultColor)
		{
			WriteInColorFormatted(WRITE_TRAILER, format, arg0, defaultColor);
			return this;
		}

		public IExtendedConsoleSession Write(
			string format, 
			params object[] args)
		{
			Console.Write(format, args);
			return this;
		}

		public IExtendedConsoleSession Write(
			string format, 
			Color color, 
			params object[] args)
		{
			WriteInColor(Console.Write, format, args, color);
			return this;
		}

		public IExtendedConsoleSession WriteAlternating(
			string format,
			ColorAlternator alternator,
			params object[] args)
		{
			WriteInColorAlternating(Console.Write, format, args, alternator);
			return this;
		}

		public IExtendedConsoleSession WriteStyled(
			StyleSheet styleSheet,
			string format,
			params object[] args)
		{
			WriteInColorStyled(WRITE_TRAILER, format, args, styleSheet);
			return this;
		}

		public IExtendedConsoleSession WriteFormatted(
			string format,
			Color styledColor,
			Color defaultColor,
			params object[] args)
		{
			WriteInColorFormatted(WRITE_TRAILER, format, args, styledColor, defaultColor);
			return this;
		}

		public IExtendedConsoleSession WriteFormatted(
			string format,
			Color defaultColor,
			params Formatter[] args)
		{
			WriteInColorFormatted(WRITE_TRAILER, format, args, defaultColor);
			return this;
		}

		public IExtendedConsoleSession Write(
			char[] buffer,
			int index, 
			int count)
		{
			Console.Write(buffer, index, count);
			return this;
		}

		public IExtendedConsoleSession Write(
			char[] buffer, 
			int index, 
			int count,
			Color color)
		{
			WriteChunkInColor(Console.Write, buffer, index, count, color);
			return this;
		}

		public IExtendedConsoleSession WriteAlternating(
			char[] buffer,
			int index,
			int count,
			ColorAlternator alternator)
		{
			WriteChunkInColorAlternating(Console.Write, buffer, index, count, alternator);
			return this;
		}

		public IExtendedConsoleSession WriteStyled(
			char[] buffer,
			int index,
			int count,
			StyleSheet styleSheet)
		{
			WriteChunkInColorStyled(WRITE_TRAILER, buffer, index, count, styleSheet);
			return this;
		}

		public IExtendedConsoleSession Write(
			string format, 
			object arg0, 
			object arg1)
		{
			Console.Write(format, arg0, arg1);
			return this;
		}

		public IExtendedConsoleSession Write(
			string format,
			object arg0, 
			object arg1,
			Color color)
		{
			WriteInColor(Console.Write, format, arg0, arg1, color);
			return this;
		}

		public IExtendedConsoleSession WriteAlternating(
			string format,
			object arg0,
			object arg1,
			ColorAlternator alternator)
		{
			WriteInColorAlternating(Console.Write, format, arg0, arg1, alternator);
			return this;
		}

		public IExtendedConsoleSession WriteStyled(
			string format,
			object arg0,
			object arg1,
			StyleSheet styleSheet)
		{
			WriteInColorStyled(WRITE_TRAILER, format, arg0, arg1, styleSheet);
			return this;
		}

		public IExtendedConsoleSession WriteFormatted(
			string format,
			object arg0,
			object arg1,
			Color styledColor,
			Color defaultColor)
		{
			WriteInColorFormatted(WRITE_TRAILER, format, arg0, arg1, styledColor, defaultColor);
			return this;
		}

		public IExtendedConsoleSession WriteFormatted(
			string format,
			Formatter arg0,
			Formatter arg1,
			Color defaultColor)
		{
			WriteInColorFormatted(WRITE_TRAILER, format, arg0, arg1, defaultColor);
			return this;
		}

		public IExtendedConsoleSession Write(
			string format, 
			object arg0, 
			object arg1, 
			object arg2)
		{
			Console.Write(format, arg0, arg1, arg2);
			return this;
		}

		public IExtendedConsoleSession Write(
			string format,
			object arg0,
			object arg1,
			object arg2,
			Color color)
		{
			WriteInColor(Console.Write, format, arg0, arg1, arg2, color);
			return this;
		}

		public IExtendedConsoleSession WriteAlternating(
			string format,
			object arg0,
			object arg1,
			object arg2,
			ColorAlternator alternator)
		{
			WriteInColorAlternating(Console.Write, format, arg0, arg1, arg2, alternator);
			return this;
		}

		public IExtendedConsoleSession WriteStyled(
			string format,
			object arg0,
			object arg1,
			object arg2,
			StyleSheet styleSheet)
		{
			WriteInColorStyled(WRITE_TRAILER, format, arg0, arg1, arg2, styleSheet);
			return this;
		}

		public IExtendedConsoleSession WriteFormatted(
			string format,
			object arg0,
			object arg1,
			object arg2,
			Color styledColor,
			Color defaultColor)
		{
			WriteInColorFormatted(WRITE_TRAILER, format, arg0, arg1, arg2, styledColor, defaultColor);
			return this;
		}

		public IExtendedConsoleSession WriteFormatted(
			string format,
			Formatter arg0,
			Formatter arg1,
			Formatter arg2,
			Color defaultColor)
		{
			WriteInColorFormatted(WRITE_TRAILER, format, arg0, arg1, arg2, defaultColor);
			return this;
		}

		public IExtendedConsoleSession Write(
			string format,
			object arg0,
			object arg1,
			object arg2,
			object arg3)
		{
			Console.Write(format, arg0, arg1, arg2, arg3);
			return this;
		}

		public IExtendedConsoleSession Write(
			string format,
			object arg0,
			object arg1,
			object arg2,
			object arg3,
			Color color)
		{
			// NOTE: The Intellisense for this overload of System.Console.Write is misleading, as the C# compiler
			//       actually resolves this overload to System.Console.Write(string format, object[] args)!

			WriteInColor(
				Console.Write,
				format,
				new[]
				{
					arg0,
					arg1,
					arg2,
					arg3
				},
				color);

			return this;
		}

		public IExtendedConsoleSession WriteAlternating(
			string format,
			object arg0,
			object arg1,
			object arg2,
			object arg3,
			ColorAlternator alternator)
		{
			WriteInColorAlternating(
				Console.Write,
				format,
				new[]
				{
					arg0,
					arg1,
					arg2,
					arg3
				},
				alternator);

			return this;
		}

		public IExtendedConsoleSession WriteStyled(
			string format,
			object arg0,
			object arg1,
			object arg2,
			object arg3,
			StyleSheet styleSheet)
		{
			WriteInColorStyled(
				WRITE_TRAILER,
				format,
				new[]
				{
					arg0,
					arg1,
					arg2,
					arg3
				},
				styleSheet);

			return this;
		}

		public IExtendedConsoleSession WriteFormatted(
			string format,
			object arg0,
			object arg1,
			object arg2,
			object arg3,
			Color styledColor,
			Color defaultColor)
		{
			WriteInColorFormatted(
				WRITE_TRAILER,
				format,
				new[]
				{
					arg0,
					arg1,
					arg2,
					arg3
				},
				styledColor,
				defaultColor);

			return this;
		}

		public IExtendedConsoleSession WriteFormatted(
			string format,
			Formatter arg0,
			Formatter arg1,
			Formatter arg2,
			Formatter arg3,
			Color defaultColor)
		{
			WriteInColorFormatted(
				WRITE_TRAILER,
				format,
				new[]
				{
					arg0,
					arg1,
					arg2,
					arg3
				},
				defaultColor);

			return this;
		}

		public IExtendedConsoleSession WriteLine()
		{
			Console.WriteLine();
			return this;
		}

		public IExtendedConsoleSession WriteLineAlternating(
			ColorAlternator alternator)
		{
			WriteInColorAlternating(Console.Write, WRITELINE_TRAILER, alternator);
			return this;
		}

		public IExtendedConsoleSession WriteLineStyled(
			StyleSheet styleSheet)
		{
			WriteInColorStyled(WRITE_TRAILER, WRITELINE_TRAILER, styleSheet);
			return this;
		}

		public IExtendedConsoleSession WriteLine(
			bool value)
		{
			Console.WriteLine(value);
			return this;
		}

		public IExtendedConsoleSession WriteLine(
			bool value,
			Color color)
		{
			WriteInColor(Console.WriteLine, value, color);
			return this;
		}

		public IExtendedConsoleSession WriteLineAlternating(
			bool value, 
			ColorAlternator alternator)
		{
			WriteInColorAlternating(Console.WriteLine, value, alternator);
			return this;
		}

		public IExtendedConsoleSession WriteLineStyled(
			bool value, 
			StyleSheet styleSheet)
		{
			WriteInColorStyled(WRITELINE_TRAILER, value, styleSheet);
			return this;
		}

		public IExtendedConsoleSession WriteLine(
			char value)
		{
			Console.WriteLine(value);
			return this;
		}

		public IExtendedConsoleSession WriteLine(
			char value,
			Color color)
		{
			WriteInColor(Console.WriteLine, value, color);
			return this;
		}

		public IExtendedConsoleSession WriteLineAlternating(
			char value, 
			ColorAlternator alternator)
		{
			WriteInColorAlternating(Console.WriteLine, value, alternator);
			return this;
		}

		public IExtendedConsoleSession WriteLineStyled(
			char value, 
			StyleSheet styleSheet)
		{
			WriteInColorStyled(WRITELINE_TRAILER, value, styleSheet);
			return this;
		}

		public IExtendedConsoleSession WriteLine(
			char[] value)
		{
			Console.WriteLine(value);
			return this;
		}

		public IExtendedConsoleSession WriteLine(
			char[] value, 
			Color color)
		{
			WriteInColor(Console.WriteLine, value, color);
			return this;
		}

		public IExtendedConsoleSession WriteLineAlternating(
			char[] value, 
			ColorAlternator alternator)
		{
			WriteInColorAlternating(Console.WriteLine, value, alternator);
			return this;
		}

		public IExtendedConsoleSession WriteLineStyled(
			char[] value, 
			StyleSheet styleSheet)
		{
			WriteInColorStyled(WRITELINE_TRAILER, value, styleSheet);
			return this;
		}

		public IExtendedConsoleSession WriteLine(
			decimal value)
		{
			Console.WriteLine(value);
			return this;
		}

		public IExtendedConsoleSession WriteLine(
			decimal value,
			Color color)
		{
			WriteInColor(Console.WriteLine, value, color);
			return this;
		}

		public IExtendedConsoleSession WriteLineAlternating(
			decimal value, 
			ColorAlternator alternator)
		{
			WriteInColorAlternating(Console.WriteLine, value, alternator);
			return this;
		}

		public IExtendedConsoleSession WriteLineStyled(
			decimal value, 
			StyleSheet styleSheet)
		{
			WriteInColorStyled(WRITELINE_TRAILER, value, styleSheet);
			return this;
		}

		public IExtendedConsoleSession WriteLine(
			double value)
		{
			Console.WriteLine(value);
			return this;
		}

		public IExtendedConsoleSession WriteLine(
			double value, 
			Color color)
		{
			WriteInColor(Console.WriteLine, value, color);
			return this;
		}

		public IExtendedConsoleSession WriteLineAlternating(
			double value, 
			ColorAlternator alternator)
		{
			WriteInColorAlternating(Console.WriteLine, value, alternator);
			return this;
		}

		public IExtendedConsoleSession WriteLineStyled(
			double value,
			StyleSheet styleSheet)
		{
			WriteInColorStyled(WRITELINE_TRAILER, value, styleSheet);
			return this;
		}

		public IExtendedConsoleSession WriteLine(
			float value)
		{
			Console.WriteLine(value);
			return this;
		}

		public IExtendedConsoleSession WriteLine(
			float value,
			Color color)
		{
			WriteInColor(Console.WriteLine, value, color);
			return this;
		}

		public IExtendedConsoleSession WriteLineAlternating(
			float value,
			ColorAlternator alternator)
		{
			WriteInColorAlternating(Console.WriteLine, value, alternator);
			return this;
		}

		public IExtendedConsoleSession WriteLineStyled(
			float value, 
			StyleSheet styleSheet)
		{
			WriteInColorStyled(WRITELINE_TRAILER, value, styleSheet);
			return this;
		}

		public IExtendedConsoleSession WriteLine(
			int value)
		{
			Console.WriteLine(value);
			return this;
		}

		public IExtendedConsoleSession WriteLine(
			int value, 
			Color color)
		{
			WriteInColor(Console.WriteLine, value, color);
			return this;
		}

		public IExtendedConsoleSession WriteLineAlternating(
			int value,
			ColorAlternator alternator)
		{
			WriteInColorAlternating(Console.WriteLine, value, alternator);
			return this;
		}

		public IExtendedConsoleSession WriteLineStyled(
			int value, 
			StyleSheet styleSheet)
		{
			WriteInColorStyled(WRITELINE_TRAILER, value, styleSheet);
			return this;
		}

		public IExtendedConsoleSession WriteLine(
			long value)
		{
			Console.WriteLine(value);
			return this;
		}

		public IExtendedConsoleSession WriteLine(
			long value, 
			Color color)
		{
			WriteInColor(Console.WriteLine, value, color);
			return this;
		}

		public IExtendedConsoleSession WriteLineAlternating(
			long value,
			ColorAlternator alternator)
		{
			WriteInColorAlternating(Console.WriteLine, value, alternator);
			return this;
		}

		public IExtendedConsoleSession WriteLineStyled(
			long value, 
			StyleSheet styleSheet)
		{
			WriteInColorStyled(WRITELINE_TRAILER, value, styleSheet);
			return this;
		}

		public IExtendedConsoleSession WriteLine(
			object value)
		{
			Console.WriteLine(value);
			return this;
		}

		public IExtendedConsoleSession WriteLine(
			object value,
			Color color)
		{
			WriteInColor(Console.WriteLine, value, color);
			return this;
		}

		public IExtendedConsoleSession WriteLineAlternating(
			object value,
			ColorAlternator alternator)
		{
			WriteInColorAlternating(Console.WriteLine, value, alternator);
			return this;
		}

		//public IExtendedConsoleSession WriteLineStyled(StyledString value, StyleSheet styleSheet)
		//{
		//	WriteAsciiInColorStyled(WRITELINE_TRAILER, value, styleSheet);

		//	return this;
		//}

		public IExtendedConsoleSession WriteLine(
			string value)
		{
			Console.WriteLine(value);
			return this;
		}

		public IExtendedConsoleSession WriteLine(
			string value, 
			Color color)
		{
			WriteInColor(Console.WriteLine, value, color);
			return this;
		}
		
		public IExtendedConsoleSession WriteLineAlternating(
			string value, 
			ColorAlternator alternator)
		{
			WriteInColorAlternating(Console.WriteLine, value, alternator);
			return this;
		}

		public IExtendedConsoleSession WriteLineStyled(
			string value,
			StyleSheet styleSheet)
		{
			WriteInColorStyled(WRITELINE_TRAILER, value, styleSheet);
			return this;
		}

		public IExtendedConsoleSession WriteLine(
			uint value)
		{
			Console.WriteLine(value);
			return this;
		}

		public IExtendedConsoleSession WriteLine(
			uint value,
			Color color)
		{
			WriteInColor(Console.WriteLine, value, color);
			return this;
		}

		public IExtendedConsoleSession WriteLineAlternating(
			uint value, 
			ColorAlternator alternator)
		{
			WriteInColorAlternating(Console.WriteLine, value, alternator);
			return this;
		}

		public IExtendedConsoleSession WriteLineStyled(
			uint value,
			StyleSheet styleSheet)
		{
			WriteInColorStyled(WRITELINE_TRAILER, value, styleSheet);
			return this;
		}

		public IExtendedConsoleSession WriteLine(
			ulong value)
		{
			Console.WriteLine(value);
			return this;
		}

		public IExtendedConsoleSession WriteLine(
			ulong value, 
			Color color)
		{
			WriteInColor(Console.WriteLine, value, color);
			return this;
		}

		public IExtendedConsoleSession WriteLineAlternating(
			ulong value, 
			ColorAlternator alternator)
		{
			WriteInColorAlternating(Console.WriteLine, value, alternator);
			return this;
		}

		public IExtendedConsoleSession WriteLineStyled(
			ulong value,
			StyleSheet styleSheet)
		{
			WriteInColorStyled(WRITELINE_TRAILER, value, styleSheet);
			return this;
		}

		public IExtendedConsoleSession WriteLine(
			string format, 
			object arg0)
		{
			Console.WriteLine(format, arg0);
			return this;
		}

		public IExtendedConsoleSession WriteLine(
			string format,
			object arg0, 
			Color color)
		{
			WriteInColor(Console.WriteLine, format, arg0, color);
			return this;
		}

		public IExtendedConsoleSession WriteLineAlternating(
			string format,
			object arg0,
			ColorAlternator alternator)
		{
			WriteInColorAlternating(Console.WriteLine, format, arg0, alternator);
			return this;
		}

		public IExtendedConsoleSession WriteLineStyled(
			string format,
			object arg0,
			StyleSheet styleSheet)
		{
			WriteInColorStyled(WRITELINE_TRAILER, format, arg0, styleSheet);
			return this;
		}

		public IExtendedConsoleSession WriteLineFormatted(
			string format,
			object arg0,
			Color styledColor,
			Color defaultColor)
		{
			WriteInColorFormatted(WRITELINE_TRAILER, format, arg0, styledColor, defaultColor);
			return this;
		}

		public IExtendedConsoleSession WriteLineFormatted(
			string format,
			Formatter arg0,
			Color defaultColor)
		{
			WriteInColorFormatted(WRITELINE_TRAILER, format, arg0, defaultColor);
			return this;
		}

		public IExtendedConsoleSession WriteLine(
			string format, 
			params object[] args)
		{
			Console.WriteLine(format, args);
			return this;
		}

		public IExtendedConsoleSession WriteLine(
			string format, 
			Color color, 
			params object[] args)
		{
			WriteInColor(Console.WriteLine, format, args, color);
			return this;
		}

		public IExtendedConsoleSession WriteLineAlternating(
			string format,
			ColorAlternator alternator,
			params object[] args)
		{
			WriteInColorAlternating(Console.WriteLine, format, args, alternator);
			return this;
		}

		public IExtendedConsoleSession WriteLineStyled(
			StyleSheet styleSheet,
			string format,
			params object[] args)
		{
			WriteInColorStyled(WRITELINE_TRAILER, format, args, styleSheet);
			return this;
		}

		public IExtendedConsoleSession WriteLineFormatted(
			string format,
			Color styledColor,
			Color defaultColor,
			params object[] args)
		{
			WriteInColorFormatted(WRITELINE_TRAILER, format, args, styledColor, defaultColor);
			return this;
		}

		public IExtendedConsoleSession WriteLineFormatted(
			string format,
			Color styledColor,
			Color defaultColor,
			IEnumerable<object> args)
		{
			WriteInColorFormatted(WRITELINE_TRAILER, format, args.ToArray(), styledColor, defaultColor);
			return this;
		}

		public IExtendedConsoleSession WriteLineFormatted(
			string format,
			Color defaultColor,
			params Formatter[] args)
		{
			WriteInColorFormatted(WRITELINE_TRAILER, format, args, defaultColor);
			return this;
		}

		public IExtendedConsoleSession WriteLine(
			char[] buffer, 
			int index,
			int count)
		{
			Console.WriteLine(buffer, index, count);
			return this;
		}

		public IExtendedConsoleSession WriteLine(
			char[] buffer, 
			int index,
			int count, 
			Color color)
		{
			WriteChunkInColor(Console.WriteLine, buffer, index, count, color);
			return this;
		}

		public IExtendedConsoleSession WriteLineAlternating(
			char[] buffer,
			int index,
			int count,
			ColorAlternator alternator)
		{
			WriteChunkInColorAlternating(Console.WriteLine, buffer, index, count, alternator);
			return this;
		}

		public IExtendedConsoleSession WriteLineStyled(
			char[] buffer,
			int index,
			int count,
			StyleSheet styleSheet)
		{
			WriteChunkInColorStyled(WRITELINE_TRAILER, buffer, index, count, styleSheet);
			return this;
		}

		public IExtendedConsoleSession WriteLine(
			string format, 
			object arg0, 
			object arg1)
		{
			Console.WriteLine(format, arg0, arg1);
			return this;
		}

		public IExtendedConsoleSession WriteLine(
			string format,
			object arg0, 
			object arg1,
			Color color)
		{
			WriteInColor(Console.WriteLine, format, arg0, arg1, color);
			return this;
		}

		public IExtendedConsoleSession WriteLineAlternating(
			string format,
			object arg0,
			object arg1,
			ColorAlternator alternator)
		{
			WriteInColorAlternating(Console.WriteLine, format, arg0, arg1, alternator);
			return this;
		}

		public IExtendedConsoleSession WriteLineStyled(
			string format,
			object arg0,
			object arg1,
			StyleSheet styleSheet)
		{
			WriteInColorStyled(WRITELINE_TRAILER, format, arg0, arg1, styleSheet);
			return this;
		}

		public IExtendedConsoleSession WriteLineFormatted(
			string format,
			object arg0,
			object arg1,
			Color styledColor,
			Color defaultColor)
		{
			WriteInColorFormatted(WRITELINE_TRAILER, format, arg0, arg1, styledColor, defaultColor);
			return this;
		}

		public IExtendedConsoleSession WriteLineFormatted(
			string format,
			Formatter arg0,
			Formatter arg1,
			Color defaultColor)
		{
			WriteInColorFormatted(WRITELINE_TRAILER, format, arg0, arg1, defaultColor);
			return this;
		}

		public IExtendedConsoleSession WriteLine(
			string format, 
			object arg0, 
			object arg1, 
			object arg2)
		{
			Console.WriteLine(format, arg0, arg1, arg2);
			return this;
		}

		public IExtendedConsoleSession WriteLine(
			string format,
			object arg0,
			object arg1,
			object arg2,
			Color color)
		{
			WriteInColor(Console.WriteLine, format, arg0, arg1, arg2, color);
			return this;
		}

		public IExtendedConsoleSession WriteLineAlternating(
			string format,
			object arg0,
			object arg1,
			object arg2,
			ColorAlternator alternator)
		{
			WriteInColorAlternating(Console.WriteLine, format, arg0, arg1, arg2, alternator);
			return this;
		}

		public IExtendedConsoleSession WriteLineStyled(
			string format,
			object arg0,
			object arg1,
			object arg2,
			StyleSheet styleSheet)
		{
			WriteInColorStyled(WRITELINE_TRAILER, format, arg0, arg1, arg2, styleSheet);
			return this;
		}

		public IExtendedConsoleSession WriteLineFormatted(
			string format,
			object arg0,
			object arg1,
			object arg2,
			Color styledColor,
			Color defaultColor)
		{
			WriteInColorFormatted(WRITELINE_TRAILER, format, arg0, arg1, arg2, styledColor, defaultColor);
			return this;
		}

		public IExtendedConsoleSession WriteLineFormatted(
			string format,
			Formatter arg0,
			Formatter arg1,
			Formatter arg2,
			Color defaultColor)
		{
			WriteInColorFormatted(WRITELINE_TRAILER, format, arg0, arg1, arg2, defaultColor);
			return this;
		}

		public IExtendedConsoleSession WriteLine(
			string format,
			object arg0,
			object arg1,
			object arg2,
			object arg3)
		{
			Console.WriteLine(format, arg0, arg1, arg2, arg3);
			return this;
		}

		public IExtendedConsoleSession WriteLine(
			string format,
			object arg0,
			object arg1,
			object arg2,
			object arg3,
			Color color)
		{
			// NOTE: The Intellisense for this overload of System.Console.WriteLine is misleading, as the C# compiler
			//       actually resolves this overload to System.Console.WriteLine(string format, object[] args)!

			WriteInColor(
				Console.WriteLine,
				format,
				new[]
				{
					arg0,
					arg1,
					arg2,
					arg3
				},
				color);

			return this;
		}

		public IExtendedConsoleSession WriteLineAlternating(
			string format,
			object arg0,
			object arg1,
			object arg2,
			object arg3,
			ColorAlternator alternator)
		{
			WriteInColorAlternating(
				Console.WriteLine,
				format,
				new[]
				{
					arg0,
					arg1,
					arg2,
					arg3
				},
				alternator);

			return this;
		}

		public IExtendedConsoleSession WriteLineStyled(
			string format,
			object arg0,
			object arg1,
			object arg2,
			object arg3,
			StyleSheet styleSheet)
		{
			WriteInColorStyled(
				WRITELINE_TRAILER,
				format,
				new[]
				{
					arg0,
					arg1,
					arg2,
					arg3
				},
				styleSheet);

			return this;
		}

		public IExtendedConsoleSession WriteLineFormatted(
			string format,
			object arg0,
			object arg1,
			object arg2,
			object arg3,
			Color styledColor,
			Color defaultColor)
		{
			WriteInColorFormatted(
				WRITELINE_TRAILER,
				format,
				new[]
				{
					arg0,
					arg1,
					arg2,
					arg3
				},
				styledColor,
				defaultColor);

			return this;
		}

		public IExtendedConsoleSession WriteLineFormatted(
			string format,
			Formatter arg0,
			Formatter arg1,
			Formatter arg2,
			Formatter arg3,
			Color defaultColor)
		{
			WriteInColorFormatted(
				WRITELINE_TRAILER,
				format,
				new[]
				{
					arg0,
					arg1,
					arg2,
					arg3
				},
				defaultColor);

			return this;
		}

		//public IExtendedConsoleSession WriteAscii(string value)
		//{
		//	WriteAscii(value, null);
		//	return this;
		//}

		//public IExtendedConsoleSession WriteAscii(string value, FigletFont font)
		//{
		//	WriteLine(GetFiglet(font).ToAscii(value).ConcreteValue);
		//	return this;
		//}

		//public IExtendedConsoleSession WriteAscii(string value, Color color)
		//{
		//	WriteAscii(value, null, color);
		//	return this;
		//}

		//public IExtendedConsoleSession WriteAscii(string value, FigletFont font, Color color)
		//{
		//	WriteLine(GetFiglet(font).ToAscii(value).ConcreteValue, color);
		//	return this;
		//}

		//public IExtendedConsoleSession WriteAsciiAlternating(string value, ColorAlternator alternator)
		//{
		//	WriteAsciiAlternating(value, null, alternator);
		//	return this;
		//}

		//public IExtendedConsoleSession WriteAsciiAlternating(string value, FigletFont font, ColorAlternator alternator)
		//{
		//	foreach (var line in GetFiglet(font).ToAscii(value).ConcreteValue.Split('\n'))
		//		WriteLineAlternating(line, alternator);

		//	return this;
		//}

		//public IExtendedConsoleSession WriteAsciiStyled(string value, StyleSheet styleSheet)
		//{
		//	WriteAsciiStyled(value, null, styleSheet);
		//	return this;
		//}

		//public IExtendedConsoleSession WriteAsciiStyled(string value, FigletFont font, StyleSheet styleSheet)
		//{
		//	WriteLineStyled(GetFiglet(font).ToAscii(value), styleSheet);
		//	return this;
		//}

		public IExtendedConsoleSession WriteWithGradient<T>(
			IEnumerable<T> input,
			Color startColor,
			Color endColor,
			int maxColorsInGradient = MAX_COLOR_CHANGES)
		{
			DoWithGradient(Write, input, startColor, endColor, maxColorsInGradient);
			return this;
		}

		public IExtendedConsoleSession WriteLineWithGradient<T>(
			IEnumerable<T> input,
			Color startColor,
			Color endColor,
			int maxColorsInGradient = MAX_COLOR_CHANGES)
		{
			DoWithGradient(WriteLine, input, startColor, endColor, maxColorsInGradient);
			return this;
		}

		public int Read()
		{
			return Console.Read();
		}

		public ConsoleKeyInfo ReadKey()
		{
			return Console.ReadKey();
		}

		public ConsoleKeyInfo ReadKey(bool intercept)
		{
			return Console.ReadKey(intercept);
		}

		public string ReadLine()
		{
			return Console.ReadLine();
		}

		public IExtendedConsoleSession ResetColor()
		{
			Console.ResetColor();
			return this;
		}

		public IExtendedConsoleSession SetBufferSize(
			int width, 
			int height)
		{
			Console.SetBufferSize(width, height);
			return this;
		}

		public IExtendedConsoleSession SetCursorPosition(
			int left,
			int top)
		{
			Console.SetCursorPosition(left, top);
			return this;
		}

		public IExtendedConsoleSession SetError(
			TextWriter newError)
		{
			Console.SetError(newError);
			return this;
		}

		public IExtendedConsoleSession SetIn(
			TextReader newIn)
		{
			Console.SetIn(newIn);
			return this;
		}

		public IExtendedConsoleSession SetOut(
			TextWriter newOut)
		{
			Console.SetOut(newOut);
			return this;
		}

		public IExtendedConsoleSession SetWindowPosition(
			int left,
			int top)
		{
			Console.SetWindowPosition(left, top);
			return this;
		}

		public IExtendedConsoleSession SetWindowSize(
			int width,
			int height)
		{
			Console.SetWindowSize(width, height);
			return this;
		}

		public Stream OpenStandardError()
		{
			return Console.OpenStandardError();
		}

		#if !NETSTANDARD2_0
		public Stream OpenStandardError(
			int bufferSize)
		{
			return Console.OpenStandardError(bufferSize);
		}
		#endif

		public Stream OpenStandardInput()
		{
			return Console.OpenStandardInput();
		}

		#if !NETSTANDARD2_0
		public Stream OpenStandardInput(
			int bufferSize)
		{
			return Console.OpenStandardInput(bufferSize);
		}
		#endif

		public Stream OpenStandardOutput()
		{
			return Console.OpenStandardOutput();
		}

		#if !NETSTANDARD2_0
		public Stream OpenStandardOutput(
			int bufferSize)
		{
			return Console.OpenStandardOutput(bufferSize);
		}
		#endif

		public IExtendedConsoleSession MoveBufferArea(
			int sourceLeft,
			int sourceTop,
			int sourceWidth,
			int sourceHeight,
			int targetLeft,
			int targetTop)
		{
			Console.MoveBufferArea(
				sourceLeft,
				sourceTop,
				sourceWidth,
				sourceHeight,
				targetLeft,
				targetTop);

			return this;
		}

		public IExtendedConsoleSession MoveBufferArea(
			int sourceLeft,
			int sourceTop,
			int sourceWidth,
			int sourceHeight,
			int targetLeft,
			int targetTop,
			char sourceChar,
			ConsoleColor sourceForeColor,
			ConsoleColor sourceBackColor)
		{
			Console.MoveBufferArea(
				sourceLeft,
				sourceTop,
				sourceWidth,
				sourceHeight,
				targetLeft,
				targetTop,
				sourceChar,
				sourceForeColor,
				sourceBackColor);

			return this;
		}

		public IExtendedConsoleSession Clear()
		{
			Console.Clear();
			return this;
		}

		public IExtendedConsoleSession ReplaceColor(
			Color oldColor,
			Color newColor)
		{
			colorManager.ReplaceColor(oldColor, newColor);
			return this;
		}

		public IExtendedConsoleSession ReplaceAllColorsWithDefaults()
		{
			colorStore = GetColorStore();
			colorManagerFactory = new ColorManagerFactory();
			colorManager = colorManagerFactory.GetManager(
				colorStore,
				MAX_COLOR_CHANGES,
				INITIAL_COLOR_CHANGE_COUNT_VALUE,
				isInCompatibilityMode);

			// There's no need to do this if in compatibility mode (or if not on Windows), as more than 16
			// colors won't be used, anyway.
			if (!colorManager.IsInCompatibilityMode && isWindows)
				new ColorMapper().SetBatchBufferColors(defaultColorMap);

			return this;
		}

		public IExtendedConsoleSession Beep(
			int frequency, 
			int duration)
		{
			Console.Beep(frequency, duration);
			return this;
		}

		private void Console_CancelKeyPress(
			object sender, 
			ConsoleCancelEventArgs e)
		{
			CancelKeyPress.Invoke(sender, e);
		}
	}
}