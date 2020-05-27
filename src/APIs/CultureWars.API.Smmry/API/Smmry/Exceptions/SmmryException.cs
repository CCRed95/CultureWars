using System;

namespace CultureWars.API.Smmry.Exceptions
{
	public class SmmryException 
		: Exception
	{
		public SmmryException()
		{
		}

		public SmmryException(
			string message)
				: base(message)
		{
		}

		public SmmryException(
			string message,
			Exception innerException)
				: base(message, innerException)
		{
		}
	}
}
