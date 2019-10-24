using System;
using System.ComponentModel;
using Ccr.Std.Core.Extensions;
using CultureWars.API.GoogleArchives.JsonParsing.Domain;
using Newtonsoft.Json;

namespace CultureWars.API.GoogleArchives.JsonParsing.Converters
{
	public class VideoQualityConverter
		: JsonConverter<VideoQuality>
	{
		/// <inheritdoc />
		public override void WriteJson(
			JsonWriter writer,
			VideoQuality value,
			JsonSerializer serializer)
		{
			switch (value)
			{
				case VideoQuality.StandardDefinition:
					writer.WriteValue("sd");

					break;

				case VideoQuality.HighDefinition:
					writer.WriteValue("hd");

					break;

				default:

					throw new InvalidEnumArgumentException(
						$"The value {value.ToString().SQuote()} is not valid for the enum type " +
						$"{nameof(VideoQuality).SQuote()}.");
			}
		}

		/// <inheritdoc />
		public override VideoQuality ReadJson(
			JsonReader reader,
			Type objectType,
			VideoQuality existingValue,
			bool hasExistingValue,
			JsonSerializer serializer)
		{
			if (objectType == typeof(VideoQuality))
			{
				var strValue = reader.Value.ToString()
					.ToLower();

				switch (strValue)
				{
					case "sd":

						return VideoQuality.StandardDefinition;
					case "hd":

						return VideoQuality.HighDefinition;
					default:

						throw new FormatException(
							$"The value {strValue.Quote()} is not valid for conversion to the enum type " +
							$"{nameof(VideoQuality).SQuote()}.");
				}
			}

			throw new FormatException(
				$"The objectType {objectType.FormatName().SQuote()} is not valid for conversion to the " +
				$"enum type {nameof(VideoQuality).SQuote()}.");
		}
	}
}