using CultureWars.Data.WordPress.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CultureWars.Data.WordPress.Maps
{
	public class CommentMetaTypeConfiguration
		: IEntityTypeConfiguration<CommentMeta>
	{
		/// <inheritdoc/>
		public void Configure(
			EntityTypeBuilder<CommentMeta> builder)
		{
			builder.ToTable("wp_commentmeta");

			builder.HasKey(e => e.CommentMetaId);
			builder.Property(e => e.CommentMetaId)
						 .HasColumnName("meta_id")
						 // .UseIdentityColumn()()
						 .IsRequired()
						 .ValueGeneratedOnAdd();

			builder.HasIndex(e => e.CommentId)
						 .HasName("comment_id");

			builder.HasIndex(e => e.MetaKey)
						 .HasName("meta_key");

			builder.Property(e => e.CommentId)
						 .HasColumnName("comment_id")
						 .HasDefaultValueSql("'0'");

			builder.Property(e => e.MetaKey)
						 .HasColumnName("meta_key")
						 .HasColumnType("varchar(255)");

			builder.Property(e => e.MetaValue)
						 .HasColumnName("meta_value")
						 .HasColumnType("varchar(MAX)");
		}
	}
}
