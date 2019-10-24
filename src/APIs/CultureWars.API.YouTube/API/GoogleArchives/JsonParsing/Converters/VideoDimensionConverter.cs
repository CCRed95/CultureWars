using System;
using System.ComponentModel;
using Ccr.Std.Core.Extensions;
using CultureWars.API.GoogleArchives.JsonParsing.Domain;
using Newtonsoft.Json;

namespace CultureWars.API.GoogleArchives.JsonParsing.Converters
{
	public class VideoDimensionConverter
		: JsonConverter<VideoDimension>
	{
		/// <inheritdoc />
		public override void WriteJson(
			JsonWriter writer,
			VideoDimension value,
			JsonSerializer serializer)
		{
			switch (value)
			{
				case VideoDimension.TwoDimensional:
					writer.WriteValue("2d");

					break;

				case VideoDimension.ThreeDimensional:
					writer.WriteValue("3d");

					break;

				default:

					throw new InvalidEnumArgumentException(
						$"The value {value.ToString().SQuote()} is not valid for the enum type " +
						$"{nameof(VideoDimension).SQuote()}.");
			}
		}

		/// <inheritdoc />
		public override VideoDimension ReadJson(
			JsonReader reader,
			Type objectType,
			VideoDimension existingValue,
			bool hasExistingValue,
			JsonSerializer serializer)
		{
			if (objectType == typeof(VideoDimension))
			{
				var strValue = reader.Value.ToString()
					.ToLower();

				switch (strValue)
				{
					case "2d":

						return VideoDimension.TwoDimensional;
					case "3d":

						return VideoDimension.ThreeDimensional;
					default:

						throw new FormatException(
							$"The value {strValue.Quote()} is not valid for conversion to the enum type " +
							$"{nameof(VideoDimension).SQuote()}.");
				}
			}

			throw new FormatException(
				$"The objectType {objectType.FormatName().SQuote()} is not valid for conversion to the " +
				$"enum type {nameof(VideoDimension).SQuote()}.");
		}
	}
}