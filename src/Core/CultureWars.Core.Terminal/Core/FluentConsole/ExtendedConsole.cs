using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using Ccr.Std.Core.Extensions;
using Ccr.Std.Core.TypeSystemInfo;
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
	{
		//private static IList<Action<>>
		public static readonly ExtendedConsole console = new ExtendedConsole();


		//private static readonly ColorToEnumMap<SimpleCodeKind> CodeColors
		//	= new ColorToEnumMap<SimpleCodeKind>();

		private static readonly ConcurrentDictionary<SimpleCodeKind, Color> _codeKindToConsoleColorMap =
			new ConcurrentDictionary<SimpleCodeKind, Color>
			{
				[SimpleCodeKind.BracesAndOperators] = Color.WhiteSmoke,
				[SimpleCodeKind.DefaultText] = Color.Cyan,
				[SimpleCodeKind.BracesAndOperators] = Color.DarkTurquoise,
				[SimpleCodeKind.ClassIdentifier] = Color.MediumSeaGreen,
				[SimpleCodeKind.DelegateIdentifier] = Color.GhostWhite,
				[SimpleCodeKind.EnumIdentifier] = Color.MediumAquamarine,
				[SimpleCodeKind.EventIdentifier] = Color.OrangeRed,
				[SimpleCodeKind.FieldIdentifier] = Color.MediumPurple,
				[SimpleCodeKind.InterfaceIdentifier] = Color.DarkOrange,
				[SimpleCodeKind.MethodIdentifier] = Color.DodgerBlue,
				//NamespaceIdentifier = 10,
				[SimpleCodeKind.ParameterIdentifier] = Color.Turquoise,
				[SimpleCodeKind.PropertyIdentifier] = Color.SkyBlue,
				//StructIdentifier = 13,
				[SimpleCodeKind.TypeParameterIdentifier] = Color.Yellow,
				[SimpleCodeKind.VariableIdentifier] = Color.LightSkyBlue,
				[SimpleCodeKind.Keyword] = Color.Magenta,
				[SimpleCodeKind.Number] = Color.White,
				[SimpleCodeKind.String] = Color.Red
			};

		

		public IExtendedConsoleSession WriteCode(
			bool value)
		{
			return WriteCode($"{value}", SimpleCodeKind.Keyword);
		}

		public IExtendedConsoleSession WriteCode(
			char value)
		{
			return WriteCode($"'{value}'", SimpleCodeKind.String);
		}

		public IExtendedConsoleSession WriteCode(
			char[] value)
		{
			return WriteCode($"{value}", SimpleCodeKind.String);
		}

		public IExtendedConsoleSession WriteCode(
			char[] buffer,
			int index,
			int count)
		{
			throw new NotSupportedException();
		}

		public IExtendedConsoleSession WriteCode(
			decimal value)
		{
			return WriteCode($"{value}", SimpleCodeKind.Number);
		}

		public IExtendedConsoleSession WriteCode(
			double value)
		{
			return WriteCode($"{value}", SimpleCodeKind.Number);
		}

		public IExtendedConsoleSession WriteCode(
			float value)
		{
			return WriteCode($"{value}", SimpleCodeKind.Number);
		}

		public IExtendedConsoleSession WriteCode(
			int value)
		{
			return WriteCode($"{value}", SimpleCodeKind.Number);
		}

		public IExtendedConsoleSession WriteCode(
			long value)
		{
			return WriteCode($"{value}", SimpleCodeKind.Number);
		}

		public IExtendedConsoleSession WriteCode(
			string value)
		{
			return WriteCode($"\"{value}\"", SimpleCodeKind.String);
		}

		public IExtendedConsoleSession WriteCodeIdentifier(
			string value)
		{
			return WriteCode(value, SimpleCodeKind.ClassIdentifier);
		}

		//public IExtendedConsoleSession WriteCode(Type value)
		//{
		//	return this.PrintType(value);
		//}

		//public IExtendedConsoleSession WriteCodeMethod(string value)
		//{
		//	return this;
		//}

		//public IExtendedConsoleSession WriteCodeKeyword(string value)
		//{
		//	return this;
		//}

		public IExtendedConsoleSession WriteCodeWord(
			string lexeme)
		{
			//if (lexeme.IsNumber())
			//{
			//	WriteCode(lexeme, SimpleCodeKind.Number);
			//}
			//else if (lexeme.IsStringLiteral())
			//{
			//	WriteCode(lexeme, SimpleCodeKind.String);
			//}
			//else if (lexeme.IsCSharpKeyword())
			//{
			//	WriteCode(lexeme, SimpleCodeKind.Keyword);
			//}
			 if (lexeme.IsValidCSharpIdentifier())
			{
				WriteCode(lexeme, SimpleCodeKind.VariableIdentifier);
			}
			else
			{
				WriteCode(lexeme, SimpleCodeKind.DefaultText);
			}

			return this;
		}

		public IExtendedConsoleSession WriteCode(
			string lexeme,
			SimpleCodeKind codeKind)
		{
			var color = _codeKindToConsoleColorMap[codeKind];

			return Write(lexeme, color);

			//var oldSystemColor = _log.ForegroundColor;

			//Console.ForegroundColor = colorManager.GetConsoleColor(color);
			//Console.Write(lexeme, color);
			//Console.ForegroundColor = oldSystemColor;
		}

		public IExtendedConsoleSession WriteType(
			Type type,
			bool fullyQualified = false)
		{
			if (fullyQualified)
			{
				if (type.Namespace != null)
				{
					var namespaceParts = type.Namespace.Split('.');

					foreach (var namespacePart in namespaceParts)
					{
						WriteCode(namespacePart, SimpleCodeKind.Keyword) // TODO namespace not keyword!
							.WriteCode(".", SimpleCodeKind.BracesAndOperators);
					}
				}
			}

			if (type.IsClass)
			{
				if (type.IsPrimitive)
				{
				}

				if (type.ContainsGenericParameters)
				{
					var genericTypeDefinition = type.GetGenericTypeDefinition();

					var genericTypeCount = genericTypeDefinition.GenericTypeArguments.Length;
					var currentTypeIndex = 0;

					WriteCode("<", SimpleCodeKind.BracesAndOperators);

					foreach (var typeArgument in genericTypeDefinition.GenericTypeArguments)
					{
						WriteSimpleType(typeArgument);
						if (currentTypeIndex <= genericTypeCount - 2)
							WriteCode(", ", SimpleCodeKind.BracesAndOperators);

						currentTypeIndex++;
					}

					WriteCode(">", SimpleCodeKind.BracesAndOperators);
				}
				else
				{
					WriteSimpleType(type);
				}
			}
			else if (type.IsInterface)
			{
				WriteCode(type.Name, SimpleCodeKind.InterfaceIdentifier);
			}
			else
			{
				WriteCode(type.Name, SimpleCodeKind.ClassIdentifier); //TODO struct
			}

			return this;
		}

		private IExtendedConsoleSession WriteSimpleType(
			Type type)
		{
			if (!type.IsClass || type.ContainsGenericParameters || type.IsInterface)
				throw new NotSupportedException();

			var isBuiltInType = TypeReference.BuiltInTypes.Contains(type);

			switch (type.Name)
			{
				case "Void":
				case "object":
					isBuiltInType = true;

					break;
			}

			var codeStyle = isBuiltInType
				? SimpleCodeKind.Keyword
				: SimpleCodeKind.ClassIdentifier;

			WriteCode(type.FormatName(), codeStyle);

			return this;
		}

		public IExtendedConsoleSession WriteNumber(
			string number)
		{
			if (number.Contains('.'))
			{
				if (!decimal.TryParse(number, out var value))
					throw new FormatException();

				WriteCode($"{value}", SimpleCodeKind.Number);
			}
			else
			{
				if (!long.TryParse(number, out var value))
					throw new FormatException();

				WriteCode($"{value}", SimpleCodeKind.Number);
			}

			return this;
		}

		public IExtendedConsoleSession WriteMethod(
			MethodInfo methodInfo)
		{
			if (methodInfo.IsPublic)
				WriteCode("public ", SimpleCodeKind.Keyword);

			if (methodInfo.IsPrivate)
				WriteCode("private ", SimpleCodeKind.Keyword);

			if (methodInfo.IsFamily)
				WriteCode("protected ", SimpleCodeKind.Keyword);

			if (methodInfo.IsAssembly)
				WriteCode("internal ", SimpleCodeKind.Keyword);

			if (methodInfo.IsStatic)
				WriteCode("static ", SimpleCodeKind.Keyword);

			if (methodInfo.IsAbstract)
				WriteCode("abstract ", SimpleCodeKind.Keyword);

			if (methodInfo.IsFinal)
				WriteCode("sealed ", SimpleCodeKind.Keyword);

			WriteType(methodInfo.ReturnType)
				.WriteCode(" ", SimpleCodeKind.DefaultText)
				.WriteCode(methodInfo.Name, SimpleCodeKind.MethodIdentifier)
				.WriteCode("(", SimpleCodeKind.BracesAndOperators);

			var parametersCount = methodInfo.GetParameters()
				.Length;
			var currentParameterIndex = 0;

			foreach (var parameterInfo in methodInfo.GetParameters())
			{
				WriteParameter(parameterInfo);
				//WriteType(parameter.ParameterType)
				//	.WriteCode(" ", SimpleCodeKind.DefaultText)
				//	.WriteCode(parameter.Name, SimpleCodeKind.ParameterIdentifier);

				//if (parameter.HasDefaultValue)
				//{
				//	WriteCode(" ", SimpleCodeKind.DefaultText)
				//		.WriteCode("=", SimpleCodeKind.BracesAndOperators)
				//		.WriteCode(" ", SimpleCodeKind.DefaultText)
				//		.WriteCode(parameter.RawDefaultValue.ToString(), SimpleCodeKind.DefaultText);
				//}

				if (currentParameterIndex <= parametersCount - 2)
					WriteCode(", ", SimpleCodeKind.BracesAndOperators);

				currentParameterIndex++;
			}

			WriteCode(");", SimpleCodeKind.BracesAndOperators);

			return this;
		}

		public IExtendedConsoleSession WriteField(
			FieldInfo fieldInfo)
		{
			if (fieldInfo.IsPublic)
				WriteCode("public ", SimpleCodeKind.Keyword);

			if (fieldInfo.IsPrivate)
				WriteCode("private ", SimpleCodeKind.Keyword);

			if (fieldInfo.IsFamily)
				WriteCode("protected ", SimpleCodeKind.Keyword);

			if (fieldInfo.IsAssembly)
				WriteCode("internal ", SimpleCodeKind.Keyword);

			if (fieldInfo.IsStatic)
				WriteCode("static ", SimpleCodeKind.Keyword);

			if (fieldInfo.IsLiteral)
				WriteCode("const ", SimpleCodeKind.Keyword);

			if (fieldInfo.IsInitOnly)
				WriteCode("readonly ", SimpleCodeKind.Keyword);

			//var defaultValue = fieldInfo.GetRawConstantValue();

			WriteType(fieldInfo.FieldType)
				.WriteCode(" ", SimpleCodeKind.DefaultText)
				.WriteCode(fieldInfo.Name, SimpleCodeKind.FieldIdentifier)
				.WriteCode(";", SimpleCodeKind.BracesAndOperators);

			return this;
		}

		public IExtendedConsoleSession WriteProperty(
			PropertyInfo propertyInfo)
		{
			var getMethodBase = propertyInfo.GetMethod;
			var setMethodBase = propertyInfo.SetMethod;

			WriteMethodBaseModifiers(getMethodBase);

			//var defaultValue = fieldInfo.GetRawConstantValue();
			//propertyInfo.
			WriteType(propertyInfo.PropertyType)
				.WriteCode(" ", SimpleCodeKind.DefaultText)
				.WriteCode(propertyInfo.Name, SimpleCodeKind.FieldIdentifier)
				.WriteCode(" { ", SimpleCodeKind.BracesAndOperators);

			if (propertyInfo.CanRead)
				WriteCode("get", SimpleCodeKind.Keyword)
					.WriteCode("; ", SimpleCodeKind.BracesAndOperators);

			if (propertyInfo.CanWrite)
				WriteCode("set", SimpleCodeKind.Keyword)
					.WriteCode("; ", SimpleCodeKind.BracesAndOperators);

			WriteCode("}", SimpleCodeKind.BracesAndOperators);

			return this;
		}

		public IExtendedConsoleSession WriteMethodBaseModifiers(
			MethodBase methodBase)
		{
			if (methodBase.IsPublic)
				WriteCode("public ", SimpleCodeKind.Keyword);

			if (methodBase.IsPrivate)
				WriteCode("private ", SimpleCodeKind.Keyword);

			if (methodBase.IsFamily)
				WriteCode("protected ", SimpleCodeKind.Keyword);

			if (methodBase.IsAssembly)
				WriteCode("internal ", SimpleCodeKind.Keyword);

			if (methodBase.IsStatic)
				WriteCode("static ", SimpleCodeKind.Keyword);

			return this;
		}

		public IExtendedConsoleSession WriteAttribute(
			Attribute attribute)
		{
			throw new NotImplementedException();
		}

		public IExtendedConsoleSession WriteConstructor(
			ConstructorInfo constructorInfo)
		{
			throw new NotImplementedException();
		}

		public IExtendedConsoleSession WriteEvent(
			EventInfo eventInfo)
		{
			throw new NotImplementedException();
		}

		public IExtendedConsoleSession WriteParameter(
			ParameterInfo parameterInfo)
		{
			WriteType(parameterInfo.ParameterType)
				.WriteCode(" ", SimpleCodeKind.DefaultText)
				.WriteCode(parameterInfo.Name, SimpleCodeKind.ParameterIdentifier);

			if (parameterInfo.HasDefaultValue)
			{
				WriteCode(" ", SimpleCodeKind.DefaultText)
					.WriteCode("=", SimpleCodeKind.BracesAndOperators)
					.WriteCode(" ", SimpleCodeKind.DefaultText)
					.WriteCode(parameterInfo.RawDefaultValue.ToString(), SimpleCodeKind.DefaultText);
			}

			return this;
		}
	}

	public partial class ExtendedConsole
		: IExtendedConsoleSession
	{
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
			System.Console.Write(getPrefix());
			return console;
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
			get => colorManager.GetColor(System.Console.BackgroundColor);
			set => System.Console.BackgroundColor = colorManager.GetConsoleColor(value);
		}

		public int BufferHeight
		{
			get => System.Console.BufferHeight;
			set => System.Console.BufferHeight = value;
		}

		public int BufferWidth
		{
			get => System.Console.BufferWidth;
			set => System.Console.BufferWidth = value;
		}

		public bool CapsLock => System.Console.CapsLock;

		public int CursorLeft
		{
			get => System.Console.CursorLeft;
			set => System.Console.CursorLeft = value;
		}

		public int CursorSize
		{
			get => System.Console.CursorSize;
			set => System.Console.CursorSize = value;
		}

		public int CursorTop
		{
			get => System.Console.CursorTop;
			set => System.Console.CursorTop = value;
		}

		public bool CursorVisible
		{
			get => System.Console.CursorVisible;
			set => System.Console.CursorVisible = value;
		}

		public TextWriter Error => System.Console.Error;

		public Color ForegroundColor
		{
			get => colorManager.GetColor(System.Console.ForegroundColor);
			set => System.Console.ForegroundColor = colorManager.GetConsoleColor(value);
		}

		public TextReader In => System.Console.In;

		public Encoding InputEncoding
		{
			get => System.Console.InputEncoding;
			set => System.Console.InputEncoding = value;
		}

		#if !NET40
		public bool IsErrorRedirected => System.Console.IsErrorRedirected;

		public bool IsInputRedirected => System.Console.IsInputRedirected;

		public bool IsOutputRedirected => System.Console.IsOutputRedirected;
		#endif

		public bool KeyAvailable => System.Console.KeyAvailable;

		public int LargestWindowHeight => System.Console.LargestWindowHeight;

		public int LargestWindowWidth => System.Console.LargestWindowWidth;

		public bool NumberLock => System.Console.NumberLock;

		public TextWriter Out => System.Console.Out;

		public Encoding OutputEncoding
		{
			get => System.Console.OutputEncoding;
			set => System.Console.OutputEncoding = value;
		}

		public string Title
		{
			get => System.Console.Title;
			set => System.Console.Title = value;
		}

		public bool TreatControlCAsInput
		{
			get => System.Console.TreatControlCAsInput;
			set => System.Console.TreatControlCAsInput = value;
		}

		public int WindowHeight
		{
			get => System.Console.WindowHeight;
			set => System.Console.WindowHeight = value;
		}

		public int WindowLeft
		{
			get => System.Console.WindowLeft;
			set => System.Console.WindowLeft = value;
		}

		public int WindowTop
		{
			get => System.Console.WindowTop;
			set => System.Console.WindowTop = value;
		}

		public int WindowWidth
		{
			get => System.Console.WindowWidth;
			set => System.Console.WindowWidth = value;
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
			System.Console.CancelKeyPress += Console_CancelKeyPress;
		}


		public IExtendedConsoleSession Write(
			bool value)
		{
			Prefix();
			System.Console.Write(value);
			return this;
		}

		public IExtendedConsoleSession Write(
			bool value,
			Color color)
		{
			WriteInColor(
				System.Console.Write,
				value,
				color);
			return this;
		}

		public IExtendedConsoleSession WriteAlternating(
			bool value,
			ColorAlternator alternator)
		{
			WriteInColorAlternating(
				System.Console.Write,
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
			System.Console.Write(value);
			return this;
		}

		public IExtendedConsoleSession Write(
			char value, 
			Color color)
		{
			WriteInColor(System.Console.Write, value, color);
			return this;
		}

		public IExtendedConsoleSession WriteAlternating(
			char value, 
			ColorAlternator alternator)
		{
			WriteInColorAlternating(System.Console.Write, value, alternator);
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
			System.Console.Write(value);
			return this;
		}

		public IExtendedConsoleSession Write(
			char[] value, 
			Color color)
		{
			WriteInColor(System.Console.Write, value, color);
			return this;
		}

		public IExtendedConsoleSession WriteAlternating(
			char[] value, 
			ColorAlternator alternator)
		{
			WriteInColorAlternating(System.Console.Write, value, alternator);
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
			System.Console.Write(value);
			return this;
		}

		public IExtendedConsoleSession Write(
			decimal value, 
			Color color)
		{
			WriteInColor(System.Console.Write, value, color);
			return this;
		}

		public IExtendedConsoleSession WriteAlternating(
			decimal value,
			ColorAlternator alternator)
		{
			WriteInColorAlternating(System.Console.Write, value, alternator);
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
			System.Console.Write(value);
			return this;
		}

		public IExtendedConsoleSession Write(
			double value, 
			Color color)
		{
			WriteInColor(System.Console.Write, value, color);
			return this;
		}

		public IExtendedConsoleSession WriteAlternating(
			double value, 
			ColorAlternator alternator)
		{
			WriteInColorAlternating(System.Console.Write, value, alternator);
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
			System.Console.Write(value);
			return this;
		}

		public IExtendedConsoleSession Write(
			float value, 
			Color color)
		{
			WriteInColor(System.Console.Write, value, color);
			return this;
		}

		public IExtendedConsoleSession WriteAlternating(
			float value, 
			ColorAlternator alternator)
		{
			WriteInColorAlternating(System.Console.Write, value, alternator);
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
			System.Console.Write(value);
			return this;
		}

		public IExtendedConsoleSession Write(
			int value,
			Color color)
		{
			WriteInColor(System.Console.Write, value, color);
			return this;
		}

		public IExtendedConsoleSession WriteAlternating(
			int value, 
			ColorAlternator alternator)
		{
			WriteInColorAlternating(System.Console.Write, value, alternator);
			return this;
		}

		public IExtendedConsoleSession WriteStyled(
			int value, 
			StyleSheet styleSheet)
		{
			System.Console.Write(getPrefix());
			WriteInColorStyled(WRITE_TRAILER, value, styleSheet);
			return this;
		}

		public IExtendedConsoleSession Write(
			long value)
		{
			System.Console.Write(getPrefix());
			System.Console.Write(value);
			return this;
		}

		public IExtendedConsoleSession Write(
			long value, 
			Color color)
		{
			WriteInColor(System.Console.Write, value, color);
			return this;
		}

		public IExtendedConsoleSession WriteAlternating(
			long value, 
			ColorAlternator alternator)
		{
			WriteInColorAlternating(System.Console.Write, value, alternator);
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
			System.Console.Write(value);
			return this;
		}

		public IExtendedConsoleSession Write(
			object value,
			Color color)
		{
			WriteInColor(System.Console.Write, value, color);
			return this;
		}

		public IExtendedConsoleSession WriteAlternating(
			object value, 
			ColorAlternator alternator)
		{
			WriteInColorAlternating(System.Console.Write, value, alternator);
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
			System.Console.Write(value);
			return this;
		}

		public IExtendedConsoleSession Write(
			string value, 
			Color color)
		{
			WriteInColor(System.Console.Write, value, color);
			return this;
		}

		public IExtendedConsoleSession WriteAlternating(
			string value, 
			ColorAlternator alternator)
		{
			WriteInColorAlternating(System.Console.Write, value, alternator);
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
			System.Console.Write(value);
			return this;
		}

		public IExtendedConsoleSession Write(
			uint value, 
			Color color)
		{
			WriteInColor(System.Console.Write, value, color);
			return this;
		}

		public IExtendedConsoleSession WriteAlternating(
			uint value,
			ColorAlternator alternator)
		{
			WriteInColorAlternating(System.Console.Write, value, alternator);
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
			System.Console.Write(value);
			return this;
		}

		public IExtendedConsoleSession Write(
			ulong value,
			Color color)
		{
			WriteInColor(System.Console.Write, value, color);
			return this;
		}

		public IExtendedConsoleSession WriteAlternating(
			ulong value, 
			ColorAlternator alternator)
		{
			WriteInColorAlternating(System.Console.Write, value, alternator);
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
			System.Console.Write(format, arg0);
			return this;
		}

		public IExtendedConsoleSession Write(
			string format,
			object arg0, 
			Color color)
		{
			WriteInColor(System.Console.Write, format, arg0, color);
			return this;
		}

		public IExtendedConsoleSession WriteAlternating(
			string format,
			object arg0,
			ColorAlternator alternator)
		{
			WriteInColorAlternating(System.Console.Write, format, arg0, alternator);
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
			System.Console.Write(format, args);

			return this;
		}

		public IExtendedConsoleSession Write(
			string format, 
			Color color, 
			params object[] args)
		{
			WriteInColor(System.Console.Write, format, args, color);
			return this;
		}

		public IExtendedConsoleSession WriteAlternating(
			string format,
			ColorAlternator alternator,
			params object[] args)
		{
			WriteInColorAlternating(System.Console.Write, format, args, alternator);
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
			System.Console.Write(buffer, index, count);
			return this;
		}

		public IExtendedConsoleSession Write(
			char[] buffer, 
			int index, 
			int count,
			Color color)
		{
			WriteChunkInColor(System.Console.Write, buffer, index, count, color);
			return this;
		}

		public IExtendedConsoleSession WriteAlternating(
			char[] buffer,
			int index,
			int count,
			ColorAlternator alternator)
		{
			WriteChunkInColorAlternating(System.Console.Write, buffer, index, count, alternator);
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
			System.Console.Write(format, arg0, arg1);
			return this;
		}

		public IExtendedConsoleSession Write(
			string format,
			object arg0, 
			object arg1,
			Color color)
		{
			WriteInColor(System.Console.Write, format, arg0, arg1, color);
			return this;
		}

		public IExtendedConsoleSession WriteAlternating(
			string format,
			object arg0,
			object arg1,
			ColorAlternator alternator)
		{
			WriteInColorAlternating(System.Console.Write, format, arg0, arg1, alternator);
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
			System.Console.Write(format, arg0, arg1, arg2);
			return this;
		}

		public IExtendedConsoleSession Write(
			string format,
			object arg0,
			object arg1,
			object arg2,
			Color color)
		{
			WriteInColor(System.Console.Write, format, arg0, arg1, arg2, color);
			return this;
		}

		public IExtendedConsoleSession WriteAlternating(
			string format,
			object arg0,
			object arg1,
			object arg2,
			ColorAlternator alternator)
		{
			WriteInColorAlternating(System.Console.Write, format, arg0, arg1, arg2, alternator);
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
			System.Console.Write(format, arg0, arg1, arg2, arg3);
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
				System.Console.Write,
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
				System.Console.Write,
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
			System.Console.WriteLine();
			return this;
		}

		public IExtendedConsoleSession WriteLineAlternating(
			ColorAlternator alternator)
		{
			WriteInColorAlternating(System.Console.Write, WRITELINE_TRAILER, alternator);
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
			System.Console.WriteLine(value);
			return this;
		}

		public IExtendedConsoleSession WriteLine(
			bool value,
			Color color)
		{
			WriteInColor(System.Console.WriteLine, value, color);
			return this;
		}

		public IExtendedConsoleSession WriteLineAlternating(
			bool value, 
			ColorAlternator alternator)
		{
			WriteInColorAlternating(System.Console.WriteLine, value, alternator);
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
			System.Console.WriteLine(value);
			return this;
		}

		public IExtendedConsoleSession WriteLine(
			char value,
			Color color)
		{
			WriteInColor(System.Console.WriteLine, value, color);
			return this;
		}

		public IExtendedConsoleSession WriteLineAlternating(
			char value, 
			ColorAlternator alternator)
		{
			WriteInColorAlternating(System.Console.WriteLine, value, alternator);
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
			System.Console.WriteLine(value);
			return this;
		}

		public IExtendedConsoleSession WriteLine(
			char[] value, 
			Color color)
		{
			WriteInColor(System.Console.WriteLine, value, color);
			return this;
		}

		public IExtendedConsoleSession WriteLineAlternating(
			char[] value, 
			ColorAlternator alternator)
		{
			WriteInColorAlternating(System.Console.WriteLine, value, alternator);
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
			System.Console.WriteLine(value);
			return this;
		}

		public IExtendedConsoleSession WriteLine(
			decimal value,
			Color color)
		{
			WriteInColor(System.Console.WriteLine, value, color);
			return this;
		}

		public IExtendedConsoleSession WriteLineAlternating(
			decimal value, 
			ColorAlternator alternator)
		{
			WriteInColorAlternating(System.Console.WriteLine, value, alternator);
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
			System.Console.WriteLine(value);
			return this;
		}

		public IExtendedConsoleSession WriteLine(
			double value, 
			Color color)
		{
			WriteInColor(System.Console.WriteLine, value, color);
			return this;
		}

		public IExtendedConsoleSession WriteLineAlternating(
			double value, 
			ColorAlternator alternator)
		{
			WriteInColorAlternating(System.Console.WriteLine, value, alternator);
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
			System.Console.WriteLine(value);
			return this;
		}

		public IExtendedConsoleSession WriteLine(
			float value,
			Color color)
		{
			WriteInColor(System.Console.WriteLine, value, color);
			return this;
		}

		public IExtendedConsoleSession WriteLineAlternating(
			float value,
			ColorAlternator alternator)
		{
			WriteInColorAlternating(System.Console.WriteLine, value, alternator);
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
			System.Console.WriteLine(value);
			return this;
		}

		public IExtendedConsoleSession WriteLine(
			int value, 
			Color color)
		{
			WriteInColor(System.Console.WriteLine, value, color);
			return this;
		}

		public IExtendedConsoleSession WriteLineAlternating(
			int value,
			ColorAlternator alternator)
		{
			WriteInColorAlternating(System.Console.WriteLine, value, alternator);
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
			System.Console.WriteLine(value);
			return this;
		}

		public IExtendedConsoleSession WriteLine(
			long value, 
			Color color)
		{
			WriteInColor(System.Console.WriteLine, value, color);

			return this;
		}

		public IExtendedConsoleSession WriteLineAlternating(
			long value,
			ColorAlternator alternator)
		{
			WriteInColorAlternating(System.Console.WriteLine, value, alternator);
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
			System.Console.WriteLine(value);
			return this;
		}

		public IExtendedConsoleSession WriteLine(
			object value,
			Color color)
		{
			WriteInColor(System.Console.WriteLine, value, color);
			return this;
		}

		public IExtendedConsoleSession WriteLineAlternating(
			object value,
			ColorAlternator alternator)
		{
			WriteInColorAlternating(System.Console.WriteLine, value, alternator);
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
			System.Console.WriteLine(value);
			return this;
		}

		public IExtendedConsoleSession WriteLine(
			string value, 
			Color color)
		{
			WriteInColor(System.Console.WriteLine, value, color);
			return this;
		}
		
		public IExtendedConsoleSession WriteLineAlternating(
			string value, 
			ColorAlternator alternator)
		{
			WriteInColorAlternating(System.Console.WriteLine, value, alternator);
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
			System.Console.WriteLine(value);
			return this;
		}

		public IExtendedConsoleSession WriteLine(
			uint value,
			Color color)
		{
			WriteInColor(System.Console.WriteLine, value, color);
			return this;
		}

		public IExtendedConsoleSession WriteLineAlternating(
			uint value, 
			ColorAlternator alternator)
		{
			WriteInColorAlternating(System.Console.WriteLine, value, alternator);
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
			System.Console.WriteLine(value);
			return this;
		}

		public IExtendedConsoleSession WriteLine(
			ulong value, 
			Color color)
		{
			WriteInColor(System.Console.WriteLine, value, color);
			return this;
		}

		public IExtendedConsoleSession WriteLineAlternating(
			ulong value, 
			ColorAlternator alternator)
		{
			WriteInColorAlternating(System.Console.WriteLine, value, alternator);
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
			System.Console.WriteLine(format, arg0);
			return this;
		}

		public IExtendedConsoleSession WriteLine(
			string format,
			object arg0, 
			Color color)
		{
			WriteInColor(System.Console.WriteLine, format, arg0, color);
			return this;
		}

		public IExtendedConsoleSession WriteLineAlternating(
			string format,
			object arg0,
			ColorAlternator alternator)
		{
			WriteInColorAlternating(System.Console.WriteLine, format, arg0, alternator);
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
			System.Console.WriteLine(format, args);
			return this;
		}

		public IExtendedConsoleSession WriteLine(
			string format, 
			Color color, 
			params object[] args)
		{
			WriteInColor(System.Console.WriteLine, format, args, color);
			return this;
		}

		public IExtendedConsoleSession WriteLineAlternating(
			string format,
			ColorAlternator alternator,
			params object[] args)
		{
			WriteInColorAlternating(System.Console.WriteLine, format, args, alternator);
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
			System.Console.WriteLine(buffer, index, count);
			return this;
		}

		public IExtendedConsoleSession WriteLine(
			char[] buffer, 
			int index,
			int count, 
			Color color)
		{
			WriteChunkInColor(System.Console.WriteLine, buffer, index, count, color);
			return this;
		}

		public IExtendedConsoleSession WriteLineAlternating(
			char[] buffer,
			int index,
			int count,
			ColorAlternator alternator)
		{
			WriteChunkInColorAlternating(System.Console.WriteLine, buffer, index, count, alternator);
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
			System.Console.WriteLine(format, arg0, arg1);
			return this;
		}

		public IExtendedConsoleSession WriteLine(
			string format,
			object arg0, 
			object arg1,
			Color color)
		{
			WriteInColor(System.Console.WriteLine, format, arg0, arg1, color);
			return this;
		}

		public IExtendedConsoleSession WriteLineAlternating(
			string format,
			object arg0,
			object arg1,
			ColorAlternator alternator)
		{
			WriteInColorAlternating(System.Console.WriteLine, format, arg0, arg1, alternator);
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
			System.Console.WriteLine(format, arg0, arg1, arg2);
			return this;
		}

		public IExtendedConsoleSession WriteLine(
			string format,
			object arg0,
			object arg1,
			object arg2,
			Color color)
		{
			WriteInColor(System.Console.WriteLine, format, arg0, arg1, arg2, color);
			return this;
		}

		public IExtendedConsoleSession WriteLineAlternating(
			string format,
			object arg0,
			object arg1,
			object arg2,
			ColorAlternator alternator)
		{
			WriteInColorAlternating(System.Console.WriteLine, format, arg0, arg1, arg2, alternator);
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
			System.Console.WriteLine(format, arg0, arg1, arg2, arg3);
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
				System.Console.WriteLine,
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
				System.Console.WriteLine,
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
			return System.Console.Read();
		}

		public ConsoleKeyInfo ReadKey()
		{
			return System.Console.ReadKey();
		}

		public ConsoleKeyInfo ReadKey(bool intercept)
		{
			return System.Console.ReadKey(intercept);
		}

		public string ReadLine()
		{
			return System.Console.ReadLine();
		}

		public IExtendedConsoleSession ResetColor()
		{
			System.Console.ResetColor();
			return this;
		}

		public IExtendedConsoleSession SetBufferSize(
			int width, 
			int height)
		{
			System.Console.SetBufferSize(width, height);
			return this;
		}

		public IExtendedConsoleSession SetCursorPosition(
			int left,
			int top)
		{
			System.Console.SetCursorPosition(left, top);
			return this;
		}

		public IExtendedConsoleSession SetError(
			TextWriter newError)
		{
			System.Console.SetError(newError);
			return this;
		}

		public IExtendedConsoleSession SetIn(
			TextReader newIn)
		{
			System.Console.SetIn(newIn);
			return this;
		}

		public IExtendedConsoleSession SetOut(
			TextWriter newOut)
		{
			System.Console.SetOut(newOut);
			return this;
		}

		public IExtendedConsoleSession SetWindowPosition(
			int left,
			int top)
		{
			System.Console.SetWindowPosition(left, top);
			return this;
		}

		public IExtendedConsoleSession SetWindowSize(
			int width,
			int height)
		{
			System.Console.SetWindowSize(width, height);
			return this;
		}

		public Stream OpenStandardError()
		{
			return System.Console.OpenStandardError();
		}

		#if !NETSTANDARD2_0
		public Stream OpenStandardError(
			int bufferSize)
		{
			return System.Console.OpenStandardError(bufferSize);
		}
		#endif

		public Stream OpenStandardInput()
		{
			return System.Console.OpenStandardInput();
		}

		#if !NETSTANDARD2_0
		public Stream OpenStandardInput(
			int bufferSize)
		{
			return System.Console.OpenStandardInput(bufferSize);
		}
		#endif

		public Stream OpenStandardOutput()
		{
			return System.Console.OpenStandardOutput();
		}

		#if !NETSTANDARD2_0
		public Stream OpenStandardOutput(
			int bufferSize)
		{
			return System.Console.OpenStandardOutput(bufferSize);
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
			System.Console.MoveBufferArea(
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
			System.Console.MoveBufferArea(
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
			System.Console.Clear();
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
			System.Console.Beep(frequency, duration);
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