using System.ComponentModel.DataAnnotations.Schema;

namespace CultureWars.Data.WordPress.Domain
{
	public class TermMeta
	{
		public ulong TermMetaId { get; set; }

		//[ForeignKey("Term")]
		public ulong TermId { get; set; }

		//public virtual Term Term { get; set; }

		public string MetaKey { get; set; }

		public string MetaValue { get; set; }
	}
}
