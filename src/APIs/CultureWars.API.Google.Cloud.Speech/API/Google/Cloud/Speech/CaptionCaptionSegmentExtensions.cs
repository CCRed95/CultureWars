using System;
using Google.Cloud.Speech.V1;
using System.Collections.Generic;
using System.Linq;

namespace CultureWars.API.Google.Cloud.Speech
{
	public static class CaptionCaptionSegmentExtensions
	{
		public static IEnumerable<IEnumerable<ClosedCaptionSegment>> SliceClosedCaptionSegments(
			this IEnumerable<ClosedCaptionSegment> @this,
			int maxWords)
		{
			var entireTranscript = @this
				.OrderBy(t => t.StartTimeStamp)
				.SelectMany(t => t.SegmentWords);
			
			foreach (var word in entireTranscript)
			{
				var ccs = new List<WordInfo>();

				var i = 0;
				while (i < maxWords)
				{
					ccs.Add(word);
					i++;
				}

				//			yield return ccs;
			}
			throw new NotImplementedException();
		}
	}
}
