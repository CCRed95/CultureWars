using System.ComponentModel.DataAnnotations.Schema;

namespace CultureWars.Data.WordPress.Domain
{
	public class TermTaxonomy
	{
		public ulong TermTaxonomyId { get; set; }

		//[ForeignKey("Term")]
		public ulong TermId { get; set; }

		//public virtual Term Term { get; set; }

		public string Taxonomy { get; set; }

		public string Description { get; set; }

		public ulong Parent { get; set; }
		
		public long Count { get; set; }
	}
}
