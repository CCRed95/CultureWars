using CultureWars.Data.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CultureWars.Data.Maps
{
	public class CultureWarsMagazineArticleTypeConfiguration
		: IEntityTypeConfiguration<CultureWarsMagazineArticle>
	{
		/// <inheritdoc/>
		public void Configure(
			EntityTypeBuilder<CultureWarsMagazineArticle> builder)
		{
			builder.ToTable("CultureWarsMagazineArticles");

			builder.HasKey(t => t.CultureWarsMagazineArticleID);
			builder.Property(t => t.CultureWarsMagazineArticleID)
				.ValueGeneratedOnAdd();

			//builder.Property(t => t.CultureWarsMagazineIssueID);

			builder.Property(t => t.CategoryName)
				.HasMaxLength(100)
				.IsRequired();

			builder.Property(t => t.ArticleName)
				.HasMaxLength(500)
				.IsRequired();

			builder.Property(t => t.AuthorName)
				.HasMaxLength(200)
				.IsRequired();
		}
	}
}