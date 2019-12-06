using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace CultureWars.Data.WordPress.Domain
{
	public class Post
	{
		public ulong PostId { get; set; }

		public ulong PostAuthor { get; set; }
		
		public DateTime PostDate { get; set; }
		
		public DateTime PostDateGmt { get; set; }
		
		public string PostContent { get; set; }
		
		public string PostTitle { get; set; }
		
		public string PostExcerpt { get; set; }
		
		public string PostStatus { get; set; }
		
		public string CommentStatus { get; set; }
		
		public string PingStatus { get; set; }
		
		public string PostPassword { get; set; }
		
		public string PostName { get; set; }
		
		public string ToPing { get; set; }
		
		public string Pinged { get; set; }
		
		public DateTime PostModified { get; set; }
		
		public DateTime PostModifiedGmt { get; set; }
		
		public string PostContentFiltered { get; set; }

		//[ForeignKey("PostParent")]
		public ulong? PostParentId { get; set; }

		//public virtual Post PostParent { get; set; }

		//public virtual ICollection<Comment> Comments { get; set; }

		public string Guid { get; set; }
		
		public int MenuOrder { get; set; }
		
		public string PostType { get; set; }
		
		public string PostMimeType { get; set; }
		
		public long CommentCount { get; set; }
	}
}

