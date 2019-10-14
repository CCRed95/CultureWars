using System.Collections.Generic;
using CultureWars.API.InternetArchive.Domain;
using CultureWars.API.InternetArchive.Domain.Responses;

namespace CultureWars.API.InternetArchive.Interpreters
{
	internal class ArchiveItemInterpreter
	{
		public static InternetArchiveItem CreateArchiveItem(
			Doc doc)
		{
			return new InternetArchiveItem(
				doc.Identifier,
				doc.Title,
				"channelName",
				"collection",
				doc.Creator,
				doc.Date,
				doc.Description,
				"language",
				new List<IASubjectTag>());
		}
	}
}