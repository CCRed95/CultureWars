using System;

namespace CultureWars.API.YouTube.Metadata
{
	
	public class ClosedCaptionSegment
	{
		public TimeSpan TimeStamp { get; set; }

		public TimeSpan EndTimeStamp { get; set; }

		public string Text { get; set; }


		public ClosedCaptionSegment(
			TimeSpan timeStamp,
			TimeSpan endTimeStamp,
			string text)
		{
			TimeStamp = timeStamp;
			EndTimeStamp = endTimeStamp;
			Text = text;
		}


		/// <inheritdoc />
		public override string ToString()
		{
			var timeStampStr = $"{TimeStamp.Hours:0}" +
				$":{TimeStamp.Minutes:00}" +
				$":{TimeStamp.Seconds:00}" +
				$".{TimeStamp.Milliseconds:000}";

			var endTimeStampStr = $"{EndTimeStamp.Hours:0}" +
				$":{EndTimeStamp.Minutes:00}" +
				$":{EndTimeStamp.Seconds:00}" +
				$".{EndTimeStamp.Milliseconds:000}";

			return $"{timeStampStr} --> {endTimeStampStr}\r\n" +
				$"{Text}";
		}
	}
}
