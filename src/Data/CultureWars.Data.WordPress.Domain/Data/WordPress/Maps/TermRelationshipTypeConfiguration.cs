using CultureWars.Data.WordPress.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CultureWars.Data.WordPress.Maps
{
	public class TermRelationshipTypeConfiguration
		: IEntityTypeConfiguration<TermRelationship>
	{
		/// <inheritdoc/>
		public void Configure(
			EntityTypeBuilder<TermRelationship> builder)
		{
			builder.ToTable("wp_term_relationships");

			builder.Property(e => e.TermRelationshipId)
						 .HasColumnName("object_id")
						 //.HasDefaultValueSql("'0'")
						 // .UseIdentityColumn()()
						 .IsRequired()
						 .ValueGeneratedOnAdd();

			builder.HasKey(
				e => new
				{
					e.TermRelationshipId,
					e.TermTaxonomyId
				});

			builder.HasIndex(e => e.TermTaxonomyId)
						 .HasName("term_taxonomy_id");

			builder.Property(e => e.TermTaxonomyId)
			       .HasColumnName("term_taxonomy_id");

			builder.Property(e => e.TermOrder)
						 .HasColumnName("term_order")
						 .HasColumnType("int")
						 .HasDefaultValueSql("'0'");
		}
	}
}