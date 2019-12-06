using CultureWars.Data.WordPress.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CultureWars.Data.WordPress.Maps
{
	public class TermMetaTypeConfiguration
		: IEntityTypeConfiguration<TermMeta>
	{
		/// <inheritdoc/>
		public void Configure(
			EntityTypeBuilder<TermMeta> builder)
		{
			builder.ToTable("wp_termmeta");

			builder.HasKey(e => e.TermMetaId);
			builder.Property(e => e.TermMetaId)
			       .HasColumnName("meta_id")
			       // .UseIdentityColumn()()
			       .IsRequired()
			       .ValueGeneratedOnAdd();

			builder.HasIndex(e => e.MetaKey)
			       .HasName("meta_key");

			builder.HasIndex(e => e.TermId)
			       .HasName("term_id");
			
			builder.Property(e => e.MetaKey)
			       .HasColumnName("meta_key")
			       .HasColumnType("varchar(255)");

			builder.Property(e => e.MetaValue)
			       .HasColumnName("meta_value")
			       .HasColumnType("varchar(MAX)");

			builder.Property(e => e.TermId)
			       .HasColumnName("term_id")
			       .HasDefaultValueSql("'0'");
		}
	}
}