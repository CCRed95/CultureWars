﻿namespace CultureWars.API.InternetArchive.Domain
{
	public class IASubjectTag
	{
		public string Subject { get; }


		public IASubjectTag(
			string subject)
		{
			Subject = subject;
		}


		/// <inheritdoc />
		public override string ToString()
		{
			return Subject;
		}
	}
}