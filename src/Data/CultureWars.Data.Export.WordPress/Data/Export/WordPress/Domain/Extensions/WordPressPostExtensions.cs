using System.Xml;

namespace CultureWars.Data.Export.WordPress.Domain.Extensions
{
	public static class WordPressPostExtensions
	{
		public static void WritePostTo(
			this WordPressPost @this,
			WordPressXmlWriter writer)
		{
			writer
				.BeginWriteItem()
				.WritePostId(@this.PostID)
				.WritePostName(@this.PostName)
				.WriteTitle(@this.PostTitle)
				.WritePostLink(@this.PostLink)
				.WriteAuthor(@this.Author)
				.WritePostDate(@this.PostDateTime)//categories
				.WriteTags(@this.Tags)
				.WriteContent(@this.PostContent)
				.WriteExcerpt(@this.PostExcerpt)
				.WriteCommentStatus("open")
				//.WritePostMeta(@this)
				.WriteEndItem();				
		}

		public static void WritePost(
			this WordPressXmlWriter @this,
			WordPressPost post)
		{
			@this
				.BeginWriteItem()
				.WritePostId(post.PostID)
				.WritePostName(post.PostName)
				.WriteTitle(post.PostTitle)
				.WritePostLink(post.PostLink)
				.WriteAuthor(post.Author)
				.WritePostDate(post.PostDateTime)//categories
				.WriteTags(post.Tags)
				.WriteContent(post.PostContent)
				.WriteExcerpt(post.PostExcerpt)
				.WriteCommentStatus("open")
				//.WritePostMeta(@this)
				.WriteEndItem();
		}
	}
}
