using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using CultureWars.Core.Builders;

namespace CultureWars.Core.Terminal
{
	public class TerminalBuilder
	{
		private List<CommandDeclaration> _registeredCommands = new List<CommandDeclaration>();
//		private Expression<Func<CommandBuilder, CommandBuilder>> _commandBuilder;

		public TerminalBuilder WithCommand(
			Expression<Func<CommandBuilder, CommandBuilder>> commandBuilder)
		{
			var builder = CommandDeclaration.Builder;
			var s = commandBuilder.Compile(); 
			var builder2 = s.Invoke(builder);
			var command = builder2.Build();
			_registeredCommands.Add(command);
			
			return this;
		}

		public TerminalApplication Build()
		{
			return new TerminalApplication();
		}

	}


	public class CommandBuilder
		: IFluentBuilder<CommandDeclaration>
	{
		private string _commandName;
		private List<CommandOptionDeclaration> _commandOptionDeclarations
			 = new List<CommandOptionDeclaration>();


		public CommandBuilder WithName(
			string commandName)
		{
			_commandName = commandName;
			return this;
		}

		public CommandBuilder HasOption(
			Func<CommandOptionDeclaration, CommandOptionDeclaration> optionBuilder)
		{
			//var builder = CommandOption

			return this;
		}

		/// <inheritdoc />
		public CommandDeclaration Build()
		{
			throw new NotImplementedException();
		}
	}

	public class TerminalApplication
	{
		private List<CommandDeclaration> _registeredCommands;

		public static TerminalBuilder Builder
		{
			get => new TerminalBuilder();
		}


	}

	public abstract class OptionBuilder
	{
		private string _optionName;
		private string _description;

		public string OptionName
		{
			get => _optionName;
		}

		public string Description
		{
			get => _description;
		}


		public OptionBuilder WithName(
			string optionName)
		{
			_optionName = optionName;
			return this;
		}

		public OptionBuilder WithDescription(
			string description)
		{
			_description = description;
			return this;
		}


		public OptionBuilder<TValueType> OfValueType<TValueType>()
		{
			return new OptionBuilder<TValueType>(this);
		}
	}


	public class OptionBuilder<TValueType>
		: IFluentBuilder<CommandOptionDeclaration<TValueType>>
	{
		private readonly string _optionName;
		private readonly string _description;


		internal OptionBuilder(
			OptionBuilder baseOptionBuilder)
		{
			_optionName = baseOptionBuilder.OptionName;
			_description = baseOptionBuilder.Description;
		}


		/// <inheritdoc />
		public CommandOptionDeclaration<TValueType> Build()
		{
			return new CommandOptionDeclaration<TValueType>(
				_optionName,
				_description);
		}

		public static implicit operator CommandOptionDeclaration<TValueType> (
			OptionBuilder<TValueType> @this)
		{
			return @this.Build();
		}
	}

	public abstract class CommandOptionDeclaration
	{
		public string OptionName { get; }

		public abstract Type OptionValueType { get; }

		public string OptionDescription { get; }


		protected CommandOptionDeclaration(
			string optionName,
			string optionDescription)
		{
			OptionName = optionName;
			OptionDescription = optionDescription;
		}
	}

	public class CommandOptionDeclaration<TValue>
		: CommandOptionDeclaration
	{
		/// <inheritdoc />
		public override Type OptionValueType
		{
			get => typeof(TValue);
		}


		/// <inheritdoc />
		public CommandOptionDeclaration(
			string optionName,
			string optionDescription)
				: base(
					optionName,
					optionDescription)
		{
		}
	}


	public abstract class CommandOption
	{
		public string OptionName { get; }

		public object OptionValueBase { get; }

		public abstract Type ValueType { get; }

		public string OptionDescription { get; }


		protected CommandOption(
			string optionName,
			object optionValueBase)
		{
			OptionName = optionName;
			OptionValueBase = optionValueBase;
		}
	}

	public class CommandOption<TValue>
		: CommandOption
	{
		/// <inheritdoc />
		public override Type ValueType
		{
			get => typeof(TValue);
		}

		public TValue OptionValue { get; }


		/// <inheritdoc />
		public CommandOption(
			string optionName,
			TValue optionValue)
				: base(
					optionName,
					optionValue)
		{
			OptionValue = optionValue;
		}
	}

	public class CommandDeclaration
	{
		public string CommandName { get; }

		public CommandOption[] CommandOptions { get; }


		public static CommandBuilder Builder
		{
			get => new CommandBuilder();
		}

		public CommandDeclaration(
			string commandName,
			params CommandOption[] _commandOptions)
		{
			CommandName = commandName;
			CommandOptions = _commandOptions;
		}
	}
}

//	/// <summary>
//	/// Provides settings for <see cref="Parser"/>. Once consumed cannot be reused.
//	/// </summary>
//	public class ParserSettings : IDisposable
//	{
//		private const int DefaultMaximumLength = 80; // default console width

//		private bool disposed;
//		private bool caseSensitive;
//		private bool caseInsensitiveEnumValues;
//		private TextWriter helpWriter;
//		private bool ignoreUnknownArguments;
//		private bool autoHelp;
//		private bool autoVersion;
//		private CultureInfo parsingCulture;
//		private bool enableDashDash;
//		private int maximumDisplayWidth;

//		/// <summary>
//		/// Initializes a new instance of the <see cref="ParserSettings"/> class.
//		/// </summary>
//		public ParserSettings()
//		{
//			caseSensitive = true;
//			caseInsensitiveEnumValues = false;
//			autoHelp = true;
//			autoVersion = true;
//			parsingCulture = CultureInfo.InvariantCulture;
//			try
//			{
//				maximumDisplayWidth = Console.WindowWidth;
//				if (maximumDisplayWidth < 1)
//				{
//					maximumDisplayWidth = DefaultMaximumLength;
//				}
//			}
//			catch (IOException)
//			{
//				maximumDisplayWidth = DefaultMaximumLength;
//			}
//		}

//		/// <summary>
//		/// Finalizes an instance of the <see cref="ParserSettings"/> class.
//		/// </summary>
//		~ParserSettings()
//		{
//			Dispose(false);
//		}

//		/// <summary>
//		/// Gets or sets a value indicating whether perform case sensitive comparisons.
//		/// Note that case insensitivity only applies to <i>parameters</i>, not the values
//		/// assigned to them (for example, enum parsing).
//		/// </summary>
//		public bool CaseSensitive
//		{
//			get { return caseSensitive; }
//			set { PopsicleSetter.Set(Consumed, ref caseSensitive, value); }
//		}

//		/// <summary>
//		/// Gets or sets a value indicating whether perform case sensitive comparisons of <i>values</i>.
//		/// Note that case insensitivity only applies to <i>values</i>, not the parameters.
//		/// </summary>
//		public bool CaseInsensitiveEnumValues
//		{
//			get { return caseInsensitiveEnumValues; }
//			set { PopsicleSetter.Set(Consumed, ref caseInsensitiveEnumValues, value); }
//		}

//		/// <summary>
//		/// Gets or sets the culture used when parsing arguments to typed properties.
//		/// </summary>
//		/// <remarks>
//		/// Default is invariant culture, <see cref="System.Globalization.CultureInfo.InvariantCulture"/>.
//		/// </remarks>
//		public CultureInfo ParsingCulture
//		{
//			get { return parsingCulture; }
//			set
//			{
//				if (value == null) throw new ArgumentNullException("value");

//				PopsicleSetter.Set(Consumed, ref parsingCulture, value);
//			}
//		}

//		/// <summary>
//		/// Gets or sets the <see cref="System.IO.TextWriter"/> used for help method output.
//		/// Setting this property to null, will disable help screen.
//		/// </summary>
//		/// <remarks>
//		/// It is the caller's responsibility to dispose or close the <see cref="TextWriter"/>.
//		/// </remarks>
//		public TextWriter HelpWriter
//		{
//			get { return helpWriter; }
//			set { PopsicleSetter.Set(Consumed, ref helpWriter, value); }
//		}

//		/// <summary>
//		/// Gets or sets a value indicating whether the parser shall move on to the next argument and ignore the given argument if it
//		/// encounter an unknown arguments
//		/// </summary>
//		/// <value>
//		/// <c>true</c> to allow parsing the arguments with different class options that do not have all the arguments.
//		/// </value>
//		/// <remarks>
//		/// This allows fragmented version class parsing, useful for project with add-on where add-ons also requires command line arguments but
//		/// when these are unknown by the main program at build time.
//		/// </remarks>
//		public bool IgnoreUnknownArguments
//		{
//			get { return ignoreUnknownArguments; }
//			set { PopsicleSetter.Set(Consumed, ref ignoreUnknownArguments, value); }
//		}

//		/// <summary>
//		/// Gets or sets a value indicating whether implicit option or verb 'help' should be supported.
//		/// </summary>
//		public bool AutoHelp
//		{
//			get { return autoHelp; }
//			set { PopsicleSetter.Set(Consumed, ref autoHelp, value); }
//		}

//		/// <summary>
//		/// Gets or sets a value indicating whether implicit option or verb 'version' should be supported.
//		/// </summary>
//		public bool AutoVersion
//		{
//			get { return autoVersion; }
//			set { PopsicleSetter.Set(Consumed, ref autoVersion, value); }
//		}

//		/// <summary>
//		/// Gets or sets a value indicating whether enable double dash '--' syntax,
//		/// that forces parsing of all subsequent tokens as values.
//		/// </summary>
//		public bool EnableDashDash
//		{
//			get { return enableDashDash; }
//			set { PopsicleSetter.Set(Consumed, ref enableDashDash, value); }
//		}

//		/// <summary>
//		/// Gets or sets the maximum width of the display.  This determines word wrap when displaying the text.
//		/// </summary>
//		public int MaximumDisplayWidth
//		{
//			get { return maximumDisplayWidth; }
//			set { maximumDisplayWidth = value; }
//		}

//		internal StringComparer NameComparer
//		{
//			get
//			{
//				return CaseSensitive
//						? StringComparer.Ordinal
//						: StringComparer.OrdinalIgnoreCase;
//			}
//		}

//		internal bool Consumed { get; set; }

//		/// <summary>
//		/// Frees resources owned by the instance.
//		/// </summary>
//		public void Dispose()
//		{
//			Dispose(true);

//			GC.SuppressFinalize(this);
//		}

//		private void Dispose(bool disposing)
//		{
//			if (disposed)
//			{
//				return;
//			}

//			if (disposing)
//			{
//				// Do not dispose HelpWriter. It is the caller's responsibility.

//				disposed = true;
//			}
//		}
//	}

//	/// <summary>
//	/// Provides methods to parse command line arguments.
//	/// </summary>
//	public class Parser : IDisposable
//	{
//		private bool disposed;
//		private readonly ParserSettings settings;
//		private static readonly Lazy<Parser> DefaultParser = new Lazy<Parser>(
//				() => new Parser(new ParserSettings { HelpWriter = Console.Error }));

//		/// <summary>
//		/// Initializes a new instance of the <see cref="Parser"/> class.
//		/// </summary>
//		public Parser()
//		{
//			settings = new ParserSettings { Consumed = true };
//		}

//		/// <summary>
//		/// Initializes a new instance of the <see cref="Parser"/> class,
//		/// configurable with <see cref="ParserSettings"/> using a delegate.
//		/// </summary>
//		/// <param name="configuration">The <see cref="Action&lt;ParserSettings&gt;"/> delegate used to configure
//		/// aspects and behaviors of the parser.</param>
//		public Parser(Action<ParserSettings> configuration)
//		{
//			if (configuration == null) throw new ArgumentNullException("configuration");

//			settings = new ParserSettings();
//			configuration(settings);
//			settings.Consumed = true;
//		}

//		internal Parser(ParserSettings settings)
//		{
//			this.settings = settings;
//		}

//		/// <summary>
//		/// Finalizes an instance of the <see cref="Parser"/> class.
//		/// </summary>
//		~Parser()
//		{
//			Dispose(false);
//		}

//		/// <summary>
//		/// Gets the singleton instance created with basic defaults.
//		/// </summary>
//		public static Parser Default
//		{
//			get { return DefaultParser.Value; }
//		}

//		/// <summary>
//		/// Gets the instance that implements <see cref="ParserSettings"/> in use.
//		/// </summary>
//		public ParserSettings Settings
//		{
//			get { return settings; }
//		}

//		/// <summary>
//		/// Parses a string array of command line arguments constructing values in an instance of type <typeparamref name="T"/>.
//		/// Grammar rules are defined decorating public properties with appropriate attributes.
//		/// </summary>
//		/// <typeparam name="T">Type of the target instance built with parsed value.</typeparam>
//		/// <param name="args">A <see cref="System.String"/> array of command line arguments, normally supplied by application entry point.</param>
//		/// <returns>A <see cref="CommandLine.ParserResult{T}"/> containing an instance of type <typeparamref name="T"/> with parsed values
//		/// and a sequence of <see cref="CommandLine.Error"/>.</returns>
//		/// <exception cref="System.ArgumentNullException">Thrown if one or more arguments are null.</exception>
//		public ParserResult<T> ParseArguments<T>(IEnumerable<string> args)
//		{
//			if (args == null) throw new ArgumentNullException("args");

//			var factory = typeof(T).IsMutable()
//					? Maybe.Just<Func<T>>(Activator.CreateInstance<T>)
//					: Maybe.Nothing<Func<T>>();

//			return MakeParserResult(
//					InstanceBuilder.Build(
//							factory,
//							(arguments, optionSpecs) => Tokenize(arguments, optionSpecs, settings),
//							args,
//							settings.NameComparer,
//							settings.CaseInsensitiveEnumValues,
//							settings.ParsingCulture,
//							settings.AutoHelp,
//							settings.AutoVersion,
//							HandleUnknownArguments(settings.IgnoreUnknownArguments)),
//					settings);
//		}

//		/// <summary>
//		/// Parses a string array of command line arguments constructing values in an instance of type <typeparamref name="T"/>.
//		/// Grammar rules are defined decorating public properties with appropriate attributes.
//		/// </summary>
//		/// <typeparam name="T">Type of the target instance built with parsed value.</typeparam>
//		/// <param name="factory">A <see cref="System.Func{T}"/> delegate used to initialize the target instance.</param>
//		/// <param name="args">A <see cref="System.String"/> array of command line arguments, normally supplied by application entry point.</param>
//		/// <returns>A <see cref="CommandLine.ParserResult{T}"/> containing an instance of type <typeparamref name="T"/> with parsed values
//		/// and a sequence of <see cref="CommandLine.Error"/>.</returns>
//		/// <exception cref="System.ArgumentNullException">Thrown if one or more arguments are null.</exception>
//		public ParserResult<T> ParseArguments<T>(Func<T> factory, IEnumerable<string> args)
//				where T : new()
//		{
//			if (factory == null) throw new ArgumentNullException("factory");
//			if (!typeof(T).IsMutable()) throw new ArgumentException("factory");
//			if (args == null) throw new ArgumentNullException("args");

//			return MakeParserResult(
//					InstanceBuilder.Build(
//							Maybe.Just(factory),
//							(arguments, optionSpecs) => Tokenize(arguments, optionSpecs, settings),
//							args,
//							settings.NameComparer,
//							settings.CaseInsensitiveEnumValues,
//							settings.ParsingCulture,
//							settings.AutoHelp,
//							settings.AutoVersion,
//							HandleUnknownArguments(settings.IgnoreUnknownArguments)),
//					settings);
//		}

//		/// <summary>
//		/// Parses a string array of command line arguments for verb commands scenario, constructing the proper instance from the array of types supplied by <paramref name="types"/>.
//		/// Grammar rules are defined decorating public properties with appropriate attributes.
//		/// The <see cref="CommandLine.VerbAttribute"/> must be applied to types in the array.
//		/// </summary>
//		/// <param name="args">A <see cref="System.String"/> array of command line arguments, normally supplied by application entry point.</param>
//		/// <param name="types">A <see cref="System.Type"/> array used to supply verb alternatives.</param>
//		/// <returns>A <see cref="CommandLine.ParserResult{T}"/> containing the appropriate instance with parsed values as a <see cref="System.Object"/>
//		/// and a sequence of <see cref="CommandLine.Error"/>.</returns>
//		/// <exception cref="System.ArgumentNullException">Thrown if one or more arguments are null.</exception>
//		/// <exception cref="System.ArgumentOutOfRangeException">Thrown if <paramref name="types"/> array is empty.</exception>
//		/// <remarks>All types must expose a parameterless constructor. It's strongly recommended to use a generic overload.</remarks>
//		public ParserResult<object> ParseArguments(IEnumerable<string> args, params Type[] types)
//		{
//			if (args == null) throw new ArgumentNullException("args");
//			if (types == null) throw new ArgumentNullException("types");
//			if (types.Length == 0) throw new ArgumentOutOfRangeException("types");

//			return MakeParserResult(
//					InstanceChooser.Choose(
//							(arguments, optionSpecs) => Tokenize(arguments, optionSpecs, settings),
//							types,
//							args,
//							settings.NameComparer,
//							settings.CaseInsensitiveEnumValues,
//							settings.ParsingCulture,
//							settings.AutoHelp,
//							settings.AutoVersion,
//							HandleUnknownArguments(settings.IgnoreUnknownArguments)),
//					settings);
//		}

//		/// <summary>
//		/// Frees resources owned by the instance.
//		/// </summary>
//		public void Dispose()
//		{
//			Dispose(true);

//			GC.SuppressFinalize(this);
//		}

//		private static Result<IEnumerable<Token>, Error> Tokenize(
//						IEnumerable<string> arguments,
//						IEnumerable<OptionSpecification> optionSpecs,
//						ParserSettings settings)
//		{
//			return
//					Tokenizer.ConfigureTokenizer(
//							settings.NameComparer,
//							settings.IgnoreUnknownArguments,
//							settings.EnableDashDash)(arguments, optionSpecs);
//		}

//		private static ParserResult<T> MakeParserResult<T>(ParserResult<T> parserResult, ParserSettings settings)
//		{
//			return DisplayHelp(
//					parserResult,
//					settings.HelpWriter,
//					settings.MaximumDisplayWidth);
//		}

//		private static ParserResult<T> DisplayHelp<T>(ParserResult<T> parserResult, TextWriter helpWriter, int maxDisplayWidth)
//		{
//			parserResult.WithNotParsed(
//					errors =>
//							Maybe.Merge(errors.ToMaybe(), helpWriter.ToMaybe())
//									.Do((_, writer) => writer.Write(HelpText.AutoBuild(parserResult, maxDisplayWidth)))
//					);

//			return parserResult;
//		}

//		private static IEnumerable<ErrorType> HandleUnknownArguments(bool ignoreUnknownArguments)
//		{
//			return ignoreUnknownArguments
//					? Enumerable.Empty<ErrorType>().Concat(ErrorType.UnknownOptionError)
//					: Enumerable.Empty<ErrorType>();
//		}

//		private void Dispose(bool disposing)
//		{
//			if (disposed) return;

//			if (disposing)
//			{
//				if (settings != null)
//					settings.Dispose();

//				disposed = true;
//			}
//		}
//	}

//}
