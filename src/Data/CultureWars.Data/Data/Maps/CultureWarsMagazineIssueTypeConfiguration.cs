using CultureWars.Data.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CultureWars.Data.Maps
{
	public class CultureWarsMagazineIssueTypeConfiguration
		: IEntityTypeConfiguration<CultureWarsMagazineIssue>
	{
		/// <inheritdoc/>
		public void Configure(
			EntityTypeBuilder<CultureWarsMagazineIssue> builder)
		{
			builder.ToTable("CultureWarsMagazineIssues");

			builder.HasKey(t => t.CultureWarsMagazineIssueID);
			builder.Property(t => t.CultureWarsMagazineIssueID)
				.ValueGeneratedOnAdd();

			builder.Property(t => t.VolumeNumber)
				.IsRequired();

			builder.Property(t => t.IssueNumber)
				.IsRequired();

			builder.HasMany(t => t.CultureWarsMagazineArticles)
				.WithOne(t => t.CultureWarsMagazineIssue)
				.IsRequired()
				.OnDelete(DeleteBehavior.Restrict);

		}
	}
}