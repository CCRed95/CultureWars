using CultureWars.Data.WordPress.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CultureWars.Data.WordPress.Maps
{
	public class TermTypeConfiguration
		: IEntityTypeConfiguration<Term>
	{
		/// <inheritdoc/>
		public void Configure(
			EntityTypeBuilder<Term> builder)
		{
			builder.ToTable("wp_terms");

			builder.HasKey(e => e.TermId);
			builder.Property(e => e.TermId)
			       .HasColumnName("term_id")
			       // .UseIdentityColumn()()
			       .IsRequired()
			       .ValueGeneratedOnAdd();

			builder.HasIndex(e => e.Name)
			       .HasName("name");

			builder.HasIndex(e => e.Slug)
			       .HasName("slug");
			
			builder.Property(e => e.Name)
			       .IsRequired()
			       .HasColumnName("name")
			       .HasColumnType("varchar(200)")
			       .HasDefaultValueSql("''");

			builder.Property(e => e.Slug)
			       .IsRequired()
			       .HasColumnName("slug")
			       .HasColumnType("varchar(200)")
			       .HasDefaultValueSql("''");

			builder.Property(e => e.TermGroup)
			       .HasColumnName("term_group")
			       .HasColumnType("bigint")
			       .HasDefaultValueSql("'0'");
		}
	}
}