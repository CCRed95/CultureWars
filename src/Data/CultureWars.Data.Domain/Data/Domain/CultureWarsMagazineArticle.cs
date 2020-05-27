namespace CultureWars.Data.Domain
{
	public class CultureWarsMagazineArticle
	{
		public int CultureWarsMagazineArticleID { get; set; }
		
		public string CategoryName { get; set; }

		public string ArticleName { get; set; }
		
		public string AuthorName { get; set; }

		//public int CultureWarsMagazineIssueID { get; set; }
		// ForeignKey
		public CultureWarsMagazineIssue CultureWarsMagazineIssue { get; set; }
	}
}