using CultureWars.Data.WordPress.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CultureWars.Data.WordPress.Maps
{
	public class TermTaxonomyTypeConfiguration
		: IEntityTypeConfiguration<TermTaxonomy>
	{
		/// <inheritdoc/>
		public void Configure(
			EntityTypeBuilder<TermTaxonomy> builder)
		{
			builder.ToTable("wp_term_taxonomy");

			builder.HasKey(e => e.TermTaxonomyId);
			builder.Property(e => e.TermTaxonomyId)
			       .HasColumnName("term_taxonomy_id")
			       .IsRequired()
			       .ValueGeneratedOnAdd();

			builder.HasIndex(e => e.Taxonomy)
			       .HasName("taxonomy");

			builder.HasIndex(e => new { e.TermId, e.Taxonomy })
			       .HasName("term_id_taxonomy")
			       .IsUnique();

			builder.Property(e => e.Count)
			       .HasColumnName("count")
			       .HasColumnType("bigint")
			       .HasDefaultValueSql("'0'");

			builder.Property(e => e.Description)
			       .IsRequired()
			       .HasColumnName("description")
			       .HasColumnType("varchar(MAX)");

			builder.Property(e => e.Parent)
			       .HasColumnName("parent")
			       .HasDefaultValueSql("'0'");

			builder.Property(e => e.Taxonomy)
			       .IsRequired()
			       .HasColumnName("taxonomy")
			       .HasColumnType("varchar(32)")
			       .HasDefaultValueSql("''");

			builder.Property(e => e.TermId)
			       .HasColumnName("term_id")
			       .HasDefaultValueSql("'0'");
		}
	}
}