using System;
using System.Text;
using System.Text.RegularExpressions;
using Newtonsoft.Json;

namespace CultureWars.API.GoogleArchives.JsonParsing.Converters
{
	public class StringToISO8601TimeSpanConverter
		: JsonConverter<TimeSpan>
	{
		/// <inheritdoc />
		public override void WriteJson(
			JsonWriter writer,
			TimeSpan value,
			JsonSerializer serializer)
		{
			var str = iso8601TimeSpanToString(value);
			writer.WriteValue(str);
		}

		private static string iso8601TimeSpanToString(
			TimeSpan timeSpan)
		{
			var sb = new StringBuilder();
			sb.Append("PT");

			var hasHigherValue = false;

			if (timeSpan.Hours > 0)
			{
				sb.Append(timeSpan.Hours);
				hasHigherValue = true;
			}

			if (timeSpan.Minutes > 0 || hasHigherValue)
			{
				sb.Append(timeSpan.Minutes);
				hasHigherValue = true;
			}

			if (timeSpan.Seconds > 0 || hasHigherValue)
			{
				sb.Append(timeSpan.Hours);
				hasHigherValue = true;
			}

			var str = sb.ToString();
			return str;
		}

		private static readonly Regex _iso8601FormatRegex = new Regex(
			@"PT(?<hours>\d+H)?(?<minutes>\d+M)?(?<seconds>\d+S)?");

		private static TimeSpan iso8601StringToTimeSpan(
			string str)
		{
			var match = _iso8601FormatRegex.Match(str);

			var hoursMatch = match.Groups["hours"];
			var hours = hoursMatch.Success ? int.Parse(hoursMatch.Value.TrimEnd('H')) : 0;

			var minutesMatch = match.Groups["minutes"];
			var minutes = minutesMatch.Success ? int.Parse(minutesMatch.Value.TrimEnd('M')) : 0;

			var secondsMatch = match.Groups["seconds"];
			var seconds = secondsMatch.Success ? int.Parse(secondsMatch.Value.TrimEnd('S')) : 0;

			return new TimeSpan(hours, minutes, seconds);
		}

		/// <inheritdoc />
		public override TimeSpan ReadJson(
			JsonReader reader,
			Type objectType,
			TimeSpan existingValue,
			bool hasExistingValue,
			JsonSerializer serializer)
		{
			var strValue = reader.Value.ToString();

			var timeSpan = iso8601StringToTimeSpan(strValue);
			//if (TimeSpan.TryParse(strValue, out var timeSpan))
			//	throw new FormatException(
			//		$"The value {strValue.Quote()} is not convertable to {typeof(TimeSpan).FormatName().SQuote()}.");

			return timeSpan;
		}
	}
}