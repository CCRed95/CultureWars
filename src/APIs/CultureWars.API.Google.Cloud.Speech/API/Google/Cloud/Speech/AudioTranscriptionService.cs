using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Google.Apis.Auth.OAuth2;
using Google.Cloud.Speech.V1;
using Google.Protobuf;
using Grpc.Auth;

namespace CultureWars.API.Google.Cloud.Speech
{
	public class AudioTranscriptionService
	{
		private static readonly string _credentialsPath =
			@"C:\Tools\Credentials\My First Project-27b635a139bd.json";

		
		public static IEnumerable<ClosedCaptionSegment> Convert(
			string gsLink)
		{
			using (Stream credentialsStream = new FileStream(
				_credentialsPath, FileMode.Open))
			{
				var googleCredential = GoogleCredential.FromStream(credentialsStream);

				var channel = new Grpc.Core.Channel(
					SpeechClient.DefaultEndpoint.Host,
					googleCredential.ToChannelCredentials());

				var speech = SpeechClient.Create(channel);

				var recognitionAudio = RecognitionAudio
					.FromStorageUri(gsLink);

				var longOperation = speech.LongRunningRecognize(
					new RecognitionConfig
					{
						Encoding = RecognitionConfig.Types.AudioEncoding.Flac,
						//SampleRateHertz = 44100,
						LanguageCode = "en",
						EnableWordTimeOffsets = true,
						EnableAutomaticPunctuation = true,
						ProfanityFilter = false,
						//Metadata = new RecognitionMetadata()
						//{
							
						//}
					}, recognitionAudio);

				longOperation = longOperation.PollUntilCompleted();
				var response = longOperation.Result;

				foreach (var result in response.Results)
				{
					foreach (var alternative in result.Alternatives)
					{
						yield return new ClosedCaptionSegment(alternative);
					}
				}
			}
		}
	}
}/*				var begin = alternative.Words.FirstOrDefault();
						var last = alternative.Words.LastOrDefault();

						if (begin == null)
							throw new NotSupportedException(
								$"");

						if (last == null)
							throw new NotSupportedException(
								$"");

						var vbeginSpan = begin.StartTime.ToTimeSpan();
						var vlastSpan = last.EndTime.ToTimeSpan();
						*/
