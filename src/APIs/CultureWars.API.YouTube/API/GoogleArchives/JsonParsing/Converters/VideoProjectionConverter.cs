using System;
using System.ComponentModel;
using Ccr.Std.Core.Extensions;
using CultureWars.API.GoogleArchives.JsonParsing.Domain;
using Newtonsoft.Json;

namespace CultureWars.API.GoogleArchives.JsonParsing.Converters
{
	public class VideoProjectionConverter
		: JsonConverter<VideoProjection>
	{
		/// <inheritdoc />
		public override void WriteJson(
			JsonWriter writer,
			VideoProjection value,
			JsonSerializer serializer)
		{
			switch (value)
			{
				case VideoProjection.Rectangular:
					writer.WriteValue("rectangular");

					break;

				case VideoProjection.ThreeDimensional360:
					writer.WriteValue("360");

					break;

				default:

					throw new InvalidEnumArgumentException(
						$"The value {value.ToString().SQuote()} is not valid for the enum type " +
						$"{nameof(VideoProjection).SQuote()}.");
			}
		}

		/// <inheritdoc />
		public override VideoProjection ReadJson(
			JsonReader reader,
			Type objectType,
			VideoProjection existingValue,
			bool hasExistingValue,
			JsonSerializer serializer)
		{
			if (objectType == typeof(VideoProjection))
			{
				var strValue = reader.Value.ToString()
					.ToLower();

				switch (strValue)
				{
					case "rectangular":

						return VideoProjection.Rectangular;
					case "360":

						return VideoProjection.ThreeDimensional360;
					default:

						throw new FormatException(
							$"The value {strValue.Quote()} is not valid for conversion to the enum type " +
							$"{nameof(VideoProjection).SQuote()}.");
				}
			}

			throw new FormatException(
				$"The objectType {objectType.FormatName().SQuote()} is not valid for conversion to the " +
				$"enum type {nameof(VideoProjection).SQuote()}.");
		}
	}
}