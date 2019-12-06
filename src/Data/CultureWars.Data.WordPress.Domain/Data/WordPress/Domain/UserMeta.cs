using System.ComponentModel.DataAnnotations.Schema;

namespace CultureWars.Data.WordPress.Domain
{
	public class UserMeta
	{
		public ulong UserMetaId { get; set; }

		//[ForeignKey("User")]
		public ulong UserId { get; set; }

		//public virtual User User { get; set; }

		public string MetaKey { get; set; }

		public string MetaValue { get; set; }
	}
}
