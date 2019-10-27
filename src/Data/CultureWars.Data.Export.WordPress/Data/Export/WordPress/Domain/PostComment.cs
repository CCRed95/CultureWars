using System;
using CultureWars.Core.Extensions;

namespace CultureWars.Data.Export.WordPress.Domain
{
	public class PostComment
	{
		public int CommentId { get; }

		public int IsApproved { get; }

		public string Author { get; }

		public string AuthorEmail { get; }

		public string AuthorIP { get; }

		public DateTime CommentDate { get; }

		public DateTime CommentDateGMT { get; }

		public string Content { get; }

		public int ParentId { get; }


		public PostComment(
			int commentId,
			int isApproved,
			string author,
			string authorEmail,
			string authorIP,
			DateTime commentDate,
			DateTime commentDateGMT,
			string content,
			int parentId)
		{
			CommentId = commentId;
			IsApproved = isApproved;
			Author = author.EnforceNotNull(nameof(author));
			AuthorEmail = authorEmail.EnforceNotNull(nameof(authorEmail));
			AuthorIP = authorIP.EnforceNotNull(nameof(authorIP));
			CommentDate = commentDate;
			CommentDateGMT = commentDateGMT;
			Content = content.EnforceNotNull(nameof(content));
			ParentId = parentId;
		}
	}
}