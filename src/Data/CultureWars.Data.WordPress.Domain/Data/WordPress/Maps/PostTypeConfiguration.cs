using CultureWars.Data.WordPress.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CultureWars.Data.WordPress.Maps
{
	public class PostTypeConfiguration
		: IEntityTypeConfiguration<Post>
	{
		/// <inheritdoc/>
		public void Configure(
			EntityTypeBuilder<Post> builder)
		{
			builder.ToTable("wp_posts");

			//builder.HasKey(e => e.PostId)
			//       .HasName("PRIMARY");

			builder.Property(e => e.PostId)
			       .HasColumnName("ID")
			       .IsRequired()
			       .ValueGeneratedOnAdd();

			builder.HasIndex(e => e.PostAuthor)
			       .HasName("post_author");

			builder.HasIndex(e => e.PostName)
			       .HasName("post_name");

			builder.HasIndex(e => e.PostParentId)
			       .HasName("post_parent");

			builder.HasIndex(e => new { e.PostType, e.PostStatus, e.PostDate, e.PostId })
			       .HasName("type_status_date");

			builder.Property(e => e.CommentCount)
			       .HasColumnName("comment_count")
			       .HasColumnType("bigint")
			       .HasDefaultValueSql("'0'");

			builder.Property(e => e.CommentStatus)
			       .IsRequired()
			       .HasColumnName("comment_status")
			       .HasColumnType("varchar(20)")
			       .HasDefaultValueSql("'open'");

			builder.Property(e => e.Guid)
			       .IsRequired()
			       .HasColumnName("guid")
			       .HasColumnType("varchar(255)")
			       .HasDefaultValueSql("''");

			builder.Property(e => e.MenuOrder)
			       .HasColumnName("menu_order")
			       .HasColumnType("int")
			       .HasDefaultValueSql("'0'");

			builder.Property(e => e.PingStatus)
			       .IsRequired()
			       .HasColumnName("ping_status")
			       .HasColumnType("varchar(20)")
			       .HasDefaultValueSql("'open'");

			builder.Property(e => e.Pinged)
			       .IsRequired()
			       .HasColumnName("pinged")
			       .HasColumnType("varchar(MAX)");

			builder.Property(e => e.PostAuthor)
			       .HasColumnName("post_author")
			       .HasDefaultValueSql("'0'");

			builder.Property(e => e.PostContent)
			       .IsRequired()
			       .HasColumnName("post_content")
			       .HasColumnType("varchar(MAX)");

			builder.Property(e => e.PostContentFiltered)
			       .IsRequired()
			       .HasColumnName("post_content_filtered")
			       .HasColumnType("varchar(MAX)");

			builder.Property(e => e.PostDate)
			       .HasColumnName("post_date")
			       .HasColumnType("datetime")
			       .HasDefaultValueSql("'0000-00-00 00:00:00'");

			builder.Property(e => e.PostDateGmt)
			       .HasColumnName("post_date_gmt")
			       .HasColumnType("datetime")
			       .HasDefaultValueSql("'0000-00-00 00:00:00'");

			builder.Property(e => e.PostExcerpt)
			       .IsRequired()
			       .HasColumnName("post_excerpt")
			       .HasColumnType("varchar(MAX)");

			builder.Property(e => e.PostMimeType)
			       .IsRequired()
			       .HasColumnName("post_mime_type")
			       .HasColumnType("varchar(100)")
			       .HasDefaultValueSql("''");

			builder.Property(e => e.PostModified)
			       .HasColumnName("post_modified")
			       .HasColumnType("datetime")
			       .HasDefaultValueSql("'0000-00-00 00:00:00'");

			builder.Property(e => e.PostModifiedGmt)
			       .HasColumnName("post_modified_gmt")
			       .HasColumnType("datetime")
			       .HasDefaultValueSql("'0000-00-00 00:00:00'");

			builder.Property(e => e.PostName)
			       .IsRequired()
			       .HasColumnName("post_name")
			       .HasColumnType("varchar(200)")
			       .HasDefaultValueSql("''");

			builder.Property(e => e.PostParentId)
			       .HasColumnName("post_parent")
			       .HasDefaultValueSql("'0'");

			builder.Property(e => e.PostPassword)
			       .IsRequired()
			       .HasColumnName("post_password")
			       .HasColumnType("varchar(255)")
			       .HasDefaultValueSql("''");

			builder.Property(e => e.PostStatus)
			       .IsRequired()
			       .HasColumnName("post_status")
			       .HasColumnType("varchar(20)")
			       .HasDefaultValueSql("'publish'");

			builder.Property(e => e.PostTitle)
			       .IsRequired()
			       .HasColumnName("post_title")
			       .HasColumnType("varchar(MAX)");

			builder.Property(e => e.PostType)
			       .IsRequired()
			       .HasColumnName("post_type")
			       .HasColumnType("varchar(20)")
			       .HasDefaultValueSql("'post'");

			builder.Property(e => e.ToPing)
			       .IsRequired()
			       .HasColumnName("to_ping")
			       .HasColumnType("varchar(MAX)");

		}
	}
}