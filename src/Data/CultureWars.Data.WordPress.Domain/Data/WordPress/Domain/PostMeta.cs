using System.ComponentModel.DataAnnotations.Schema;

namespace CultureWars.Data.WordPress.Domain
{
	public class PostMeta
	{
		public ulong PostMetaId { get; set; }

		//[ForeignKey("Post")]
		public ulong PostId { get; set; }

		//public virtual Post Post { get; set; }

		public string MetaKey { get; set; }

		public string MetaValue { get; set; }
	}
}
