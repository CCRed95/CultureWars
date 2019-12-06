using System.ComponentModel.DataAnnotations.Schema;

namespace CultureWars.Data.WordPress.Domain
{
	public class TermRelationship
	{
		public ulong TermRelationshipId { get; set; }

		//ForeignKey("TermTaxonomy")]
		public ulong TermTaxonomyId { get; set; }

		//public virtual TermTaxonomy TermTaxonomy { get; set; }

		public int TermOrder { get; set; }
	}
}
