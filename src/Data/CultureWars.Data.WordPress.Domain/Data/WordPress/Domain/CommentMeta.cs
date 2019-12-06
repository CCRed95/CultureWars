using System.ComponentModel.DataAnnotations.Schema;

namespace CultureWars.Data.WordPress.Domain
{
	public class CommentMeta
	{
		public ulong CommentMetaId { get; set; }

		//[ForeignKey("Comment")]
		public ulong CommentId { get; set; }

		//public virtual Comment Comment { get; set; }

		public string MetaKey { get; set; }

		public string MetaValue { get; set; }
	}
}
