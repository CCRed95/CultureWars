namespace CultureWars.Data.WordPress.Domain
{
	public class Term
	{
		public ulong TermId { get; set; }

		public string Name { get; set; }

		public string Slug { get; set; }

		public long TermGroup { get; set; }


		public Term()
		{
		}

		public Term(
			ulong termId,
			string name,
			string slug,
			long termGroup = 0)
		{
			TermId = termId;
			Name = name;
			Slug = slug;
			TermGroup = termGroup;
		}
	}

	public static class TermExtensions
	{
		public static void WriteXml(
			this Term @this)
		{

		}
	}
}
