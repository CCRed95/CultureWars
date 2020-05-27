using Ccr.Std.Core.Extensions;
using JetBrains.Annotations;

namespace CultureWars.API.CultureWars.Domain
{
	public class CWMArticle
	{
		[NotNull]
		public string CategoryName { get; set; }
		
		[NotNull]
		public string ArticleName { get; set; }

		[NotNull]
		public string AuthorName { get; set; }

		[NotNull]
		public CWMIssue OwnerIssue { get; }


		public CWMArticle(
			[NotNull] string categoryName,
			[NotNull] string articleName,
			[NotNull] string authorName,
			[NotNull] CWMIssue ownerIssue)
		{
			categoryName.IsNotNull(nameof(categoryName));
			articleName.IsNotNull(nameof(articleName));
			authorName.IsNotNull(nameof(authorName));
			ownerIssue.IsNotNull(nameof(ownerIssue));

			CategoryName = categoryName;
			ArticleName = articleName;
			AuthorName = authorName;
			OwnerIssue = ownerIssue;
		}
	}
}