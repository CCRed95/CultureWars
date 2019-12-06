using System;
using JetBrains.Annotations;

namespace CultureWars.Core.Terminal
{
	public abstract class SymbolToken
		: TokenBase
	{
		protected SymbolToken(
			[RegexPattern] string regexPattern) 
			: base(
				regexPattern)
		{
		}

		protected SymbolToken(
			Func<string, bool> matchPredicate)
			: base(
				matchPredicate)
		{
		}
	}


	public class AtSignToken
	: SymbolToken
	{
		public AtSignToken() 
			: base("@")
		{
		}
	}

	public class PoundSignToken
		: SymbolToken
	{
		public PoundSignToken() 
			: base("#")
		{
		}
	}

	public class DollarSignToken
		: SymbolToken
	{
		public DollarSignToken() 
			: base("$")
		{
		}
	}

	public class PercentSignToken
		: SymbolToken
	{
		public PercentSignToken()
			: base("%")
		{
		}
	}

	public class CaretToken
		: SymbolToken
	{
		public CaretToken()
			: base("^")
		{
		}
	}


	public class AmpresandToken
		: SymbolToken
	{
		public AmpresandToken() : base(
			"&")
		{
		}
	}

	//public class AsteriskToken
	//	: SymbolToken
	//{
	//	public AsteriskToken() : base(
	//		"*")
	//	{
	//	}
	//}
	//[TokenQualifier(@"(\()")]
	//public class OpenParenthesisToken
	//	: SymbolToken
	//{
	//	public OpenParenthesisToken() : base(
	//		"(")
	//	{
	//	}
	//}
	//[TokenQualifier(@"(\))")]
	//public class CloseParenthesisToken
	//	: SymbolToken
	//{
	//	public CloseParenthesisToken() : base(
	//		")")
	//	{
	//	}
	//}
	//[TokenQualifier(@"(_)")]
	//public class UserscoreToken
	//	: SymbolToken
	//{
	//	public UserscoreToken() : base(
	//		"_")
	//	{
	//	}
	//}
	//[TokenQualifier(@"(\+)")]
	//public class AddToken
	//	: SymbolToken
	//{
	//	public AddToken() : base(
	//		"+")
	//	{
	//	}
	//}
	//[TokenQualifier(@"(\-)")]
	//public class SubtractToken
	//	: SymbolToken
	//{
	//	public SubtractToken() : base(
	//		"-")
	//	{
	//	}
	//}
	//[TokenQualifier(@"(\=)")]
	//public class EqualsToken
	//	: SymbolToken
	//{
	//	public EqualsToken() : base(
	//		"=")
	//	{
	//	}
	//}
	//[TokenQualifier(@"(\[)")]
	//public class OpenSquareBraceToken
	//	: SymbolToken
	//{
	//	public OpenSquareBraceToken() : base(
	//		"[")
	//	{
	//	}
	//}
	//[TokenQualifier(@"(\[)")]
	//public class CloseSquareBraceToken
	//	: SymbolToken
	//{
	//	public CloseSquareBraceToken() : base(
	//		"]")
	//	{
	//	}
	//}
	//[TokenQualifier(@"(\{)")]
	//public class OpenCurlyBraceToken
	//	: SymbolToken
	//{
	//	public OpenCurlyBraceToken() : base(
	//		"{")
	//	{
	//	}
	//}
	//[TokenQualifier(@"(\})")]
	//public class CloseCurlyBraceToken
	//	: SymbolToken
	//{
	//	public CloseCurlyBraceToken() : base(
	//		"}")
	//	{
	//	}
	//}
	//[TokenQualifier(@"(\|)")]
	//public class VerticalPipeToken
	//	: SymbolToken
	//{
	//	public VerticalPipeToken() : base(
	//		"|")
	//	{
	//	}
	//}
	//[TokenQualifier(@"(\\)")]
	//public class BlackSlashToken
	//	: SymbolToken
	//{
	//	public BlackSlashToken() : base(
	//		"\\")
	//	{
	//	}
	//}
	//[TokenQualifier(@"(;)")]
	//public class SemicolonToken
	//	: SymbolToken
	//{
	//	public SemicolonToken() : base(
	//		";")
	//	{
	//	}
	//}
	//[TokenQualifier(@"(:)")]
	//public class ColonToken
	//	: SymbolToken
	//{
	//	public ColonToken() : base(
	//		":")
	//	{
	//	}
	//}
	//[TokenQualifier(@"(\')")]
	//public class SingleQuoteToken
	//	: SymbolToken
	//{
	//	public SingleQuoteToken() : base(
	//		"'")
	//	{
	//	}
	//}
	//[TokenQualifier("(\")")]
	//public class DoubleQuoteToken
	//	: SymbolToken
	//{
	//	public DoubleQuoteToken() : base(
	//		"\"")
	//	{
	//	}
	//}
	//[TokenQualifier(@"(\<)")]
	//public class LessThanToken
	//	: SymbolToken
	//{
	//	public LessThanToken() : base(
	//		"<")
	//	{
	//	}
	//}
	//[TokenQualifier(@"(,)")]
	//public class CommaToken
	//	: SymbolToken
	//{
	//	public CommaToken() : base(
	//		",")
	//	{
	//	}
	//}
	//[TokenQualifier(@"(\>)")]
	//public class GreaterThanToken
	//	: SymbolToken
	//{
	//	public GreaterThanToken() : base(
	//		">")
	//	{
	//	}
	//}
	//[TokenQualifier(@"(.)")]
	//public class PeriodToken
	//	: SymbolToken
	//{
	//	public PeriodToken() : base(
	//		".")
	//	{
	//	}
	//}
	//[TokenQualifier(@"(/)")]
	//public class ForwardSlashToken
	//	: SymbolToken
	//{
	//	public ForwardSlashToken() : base(
	//		"/")
	//	{
	//	}
	//}
	//[TokenQualifier(@"(\?)")]
	//public class QuestionMarkToken
	//	: SymbolToken
	//{
	//	public QuestionMarkToken() : base(
	//		"?")
	//	{
	//	}
	//}
	//[TokenQualifier(@"(~)")]
	//public class TildeToken
	//	: SymbolToken
	//{
	//	public TildeToken() : base(
	//		"~")
	//	{
	//	}
	//}
	//[TokenQualifier(@"(`)")]
	//public class BacktickToken : SymbolToken
	//{
	//	public BacktickToken() : base(
	//		"`")
	//	{
	//	}
	//}

}