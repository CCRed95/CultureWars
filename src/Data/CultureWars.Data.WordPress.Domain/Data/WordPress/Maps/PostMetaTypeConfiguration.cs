using CultureWars.Data.WordPress.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CultureWars.Data.WordPress.Maps
{
	public class PostMetaTypeConfiguration
		: IEntityTypeConfiguration<PostMeta>
	{
		/// <inheritdoc/>
		public void Configure(
			EntityTypeBuilder<PostMeta> builder)
		{
			builder.ToTable("wp_postmeta");

			builder.HasKey(e => e.PostMetaId);
			builder.Property(e => e.PostMetaId)
						 .HasColumnName("meta_id")
						 // .UseIdentityColumn()()
						 .IsRequired()
						 .ValueGeneratedOnAdd();

			builder.HasIndex(e => e.MetaKey)
						 .HasName("meta_key");

			builder.HasIndex(e => e.PostId)
						 .HasName("post_id");

			builder.Property(e => e.MetaKey)
						 .HasColumnName("meta_key")
						 .HasColumnType("varchar(255)");

			builder.Property(e => e.MetaValue)
						 .HasColumnName("meta_value")
						 .HasColumnType("varchar(MAX)");

			builder.Property(e => e.PostId)
						 .HasColumnName("post_id")
						 .HasDefaultValueSql("'0'");
		}
	}
}