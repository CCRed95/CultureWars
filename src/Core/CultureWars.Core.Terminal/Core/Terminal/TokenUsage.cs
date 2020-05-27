using System;
using CultureWars.Core.Terminal.Range;

namespace CultureWars.Core.Terminal
{
	public class TokenUsage<TToken>
		: TokenUsageBase
			where TToken
				: TokenBase
	{
		protected readonly TToken _token;


		public override TokenBase TokenBase
		{
			get => _token;
		}

		public TToken Token
		{
			get => _token;
		}

		public override Type TokenType
		{
			get => typeof(TToken);
		}


		public TokenUsage(
			ITextRangePointer textPointer,
			TToken token) 
				: base(
					textPointer)
		{
			_token = token;
		}
	}
}