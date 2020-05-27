using System;
using System.Collections.Generic;
using Ccr.Parsing.Tokenizer;
using Ccr.Parsing.Tokenizer.Tokens.OLD;
using Ccr.Std.Core.Extensions;

namespace CultureWars.Core.Terminal
{
	public class OptionToken
		: TokenBase
	{
		public string OptionText { get; }

		public string ValueAssignment { get; }


		public OptionToken(
			string optionText)
				: base(optionText)
		{
			OptionText = optionText;
		}

		public OptionToken(
			string optionText,
			string valueAssignment)
				: base($"{optionText}={valueAssignment}")
		{
			OptionText = optionText;
			ValueAssignment = valueAssignment;
		}
	}

	public class ParsedCommand
	{
		public IdentiferToken CommandIdentifier { get; }

		public OptionToken[] CommandOptions { get; }


		public ParsedCommand(
			IdentiferToken commandIdentifier,
			params OptionToken[] commandOptions)
		{
			CommandIdentifier = commandIdentifier;
			CommandOptions = commandOptions;
		}
	}

	public class CommandParser
	{
		private readonly TokenizerOld _tokenizer;


		public CommandParser(
			string commandText)
		{
			_tokenizer = new TokenizerOld(commandText);
		}


		public ParsedCommand Parse()
		{
			_tokenizer.SkipWhiteSpace();

			var commandToken = scanIdentifer();
			
			var options = new List<OptionToken>();

			_tokenizer.SkipWhiteSpace();

			while (_tokenizer.HasMore())
			{
				var option = scanOption();
				options.Add(option);

				_tokenizer.SkipWhiteSpace();
			}

			return new ParsedCommand(
				commandToken,
				options.ToArray());
		}

		
		private IdentiferToken scanIdentifer()
		{
			var text = "";
			var isFirst = true;
			char c;

			while (_tokenizer.TryRead(out c))
			{
				if (isFirst)
				{
					isFirst = false;

					if (char.IsDigit(c))
						throw new FormatException(
							$"The character {c.SQuote()} is not valid. An identifier must start with a letter.");

					if (char.IsLetter(c))
						text += c;

					else
						throw new FormatException(
							$"The character {c.SQuote()} is not valid. An identifier must start with a letter.");
				}
				else
				{
					if (char.IsLetterOrDigit(c) || c == '-')
						text += c;

					else
						break;
				}
			}

			if (string.IsNullOrWhiteSpace(text))
				throw new FormatException(
					$"The text \"{text}\" is not a valid identifier because it is null or whitespace.");

			_tokenizer.Step(-1);

			return new IdentiferToken(text);
		}

		private OptionToken scanOption()
		{
			var text = "";
			var valueAssignment = "";
			var isFirst = true;
			var hasValueAssignment = false;
			char c;

			while (_tokenizer.TryRead(out c))
			{
				if (isFirst)
				{
					isFirst = false;

					if (char.IsDigit(c))
						throw new FormatException(
							$"The character {c.SQuote()} is not valid. An identifier must start with a letter.");

					if (char.IsLetter(c))
						text += c;

					else
						throw new FormatException(
							$"The character {c.SQuote()} is not valid. An identifier must start with a letter.");
				}
				else
				{
					if (char.IsLetterOrDigit(c) || c == '-')
						text += c;

					else if (c == '=')
					{
						hasValueAssignment = true;
						break;
					}
					else
						break;
				}
			}

			if (string.IsNullOrWhiteSpace(text))
				throw new FormatException(
					$"The text \"{text}\" is not a valid identifier because it is null or whitespace.");

			if (!hasValueAssignment)
			{
				_tokenizer.Step(-1);
				return new OptionToken(text);
			}

			while (_tokenizer.TryRead(out c))
			{
				if (!char.IsWhiteSpace(c))
					valueAssignment += c;
			}

			return new OptionToken(text, valueAssignment);
		}
	}
}