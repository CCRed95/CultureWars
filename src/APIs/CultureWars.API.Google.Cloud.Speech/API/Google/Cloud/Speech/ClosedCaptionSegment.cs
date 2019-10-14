using Google.Cloud.Speech.V1;
using System;
using System.IO;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.WellKnownTypes;

namespace CultureWars.API.Google.Cloud.Speech
{
	public class ClosedCaptionSegment
	{
		private readonly RepeatedField<SpeechRecognitionResult> _results
			= new RepeatedField<SpeechRecognitionResult>();

		
		//private readonly SpeechRecognitionAlternative _recognizedSegment;
		private TimeSpan _startTimeStamp;
		private TimeSpan _endTimeStamp;
		private RepeatedField<WordInfo> _segmentWords;
		private string _transcript;
		private float _confidence;

		public TimeSpan StartTimeStamp
		{
			get => _startTimeStamp;
		}

		public TimeSpan EndTimeStamp
		{
			get => _endTimeStamp;
		}

		public Duration SegmentDuration
		{
			get => (EndTimeStamp - StartTimeStamp)
				.ToDuration();
		}
		 
		public RepeatedField<WordInfo> SegmentWords
		{
			get => _segmentWords;
		}

		public string Transcript
		{
			get => _transcript;
		}

		public float Confidence
		{
			get => _confidence;
		}


		public ClosedCaptionSegment(
			SpeechRecognitionAlternative recognizedSegment)
				: this(
					recognizedSegment.Words,
					recognizedSegment.Confidence)
		{
		}
		
		public ClosedCaptionSegment(
			RepeatedField<WordInfo> segmentWords,
			float confidence)
		{
			if (!_segmentWords.Any())
				throw new NotSupportedException(
					$"");

			_segmentWords = segmentWords;
			_confidence = confidence;
			
			_startTimeStamp = _segmentWords
				.First()
				.StartTime
				.ToTimeSpan(); 

			_endTimeStamp = _segmentWords
				.Last()
				.EndTime
				.ToTimeSpan();

			//CodedOutputStream.ComputeMessageSize(segmentWords.CalculateSize(codec: FieldCodec.));
			var codedInputStream = new CodedOutputStream(new byte[512]);
			segmentWords.WriteTo(codedInputStream, FieldCodec.ForMessage(
				WordInfo.WordFieldNumber,
				WordInfo.Parser));

			foreach (var i in SegmentWords)
			{
				codedInputStream.WriteMessage(i);
			}
			var generatedTranscript = codedInputStream.ToString();

			_transcript = generatedTranscript;
		}

		private void writer(CodedOutputStream arg1, WordInfo arg2)
		{
			
		}

		private WordInfo reader(CodedInputStream arg)
		{
			return null;
		}
	}
}
