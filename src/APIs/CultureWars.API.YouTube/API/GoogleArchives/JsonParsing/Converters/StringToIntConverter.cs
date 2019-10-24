using System;
using Ccr.Std.Core.Extensions;
using Newtonsoft.Json;

namespace CultureWars.API.GoogleArchives.JsonParsing.Converters
{
	public class StringToIntConverter
		: JsonConverter<int>
	{
		/// <inheritdoc />
		public override void WriteJson(
			JsonWriter writer,
			int value,
			JsonSerializer serializer)
		{
			writer.WriteValue(value.ToString());
		}

		/// <inheritdoc />
		public override int ReadJson(
			JsonReader reader,
			Type objectType,
			int existingValue,
			bool hasExistingValue,
			JsonSerializer serializer)
		{
			var strValue = reader.Value.ToString();

			if (!int.TryParse(strValue, out var intValue))
				throw new FormatException(
					$"The value {strValue.Quote()} is not convertable to {typeof(int).FormatName().SQuote()}.");

			return intValue;
		}
	}
}