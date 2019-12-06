using CultureWars.Data.WordPress.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CultureWars.Data.WordPress.Maps
{
	public class CommentTypeConfiguration
		: IEntityTypeConfiguration<Comment>
	{
		/// <inheritdoc/>
		public void Configure(
			EntityTypeBuilder<Comment> builder)
		{
			builder.ToTable("wp_comments");

			builder.HasKey(e => e.CommentId);
			builder.Property(e => e.CommentId)
			       .HasColumnName("comment_ID")
			       .IsRequired()
							.ValueGeneratedOnAdd();

			builder.HasIndex(e => e.CommentAuthorEmail)
			       .HasName("comment_author_email");

			builder.HasIndex(e => e.CommentDateGmt)
			       .HasName("comment_date_gmt");

			builder.HasIndex(e => e.CommentParent)
			       .HasName("comment_parent");

			builder.HasIndex(e => e.CommentPostId)
			       .HasName("comment_post_ID");

			builder.HasIndex(e => new { e.CommentApproved, e.CommentDateGmt })
			       .HasName("comment_approved_date_gmt");

			builder.Property(e => e.CommentAgent)
			       .IsRequired()
			       .HasColumnName("comment_agent")
			       .HasColumnType("varchar(255)")
			       .HasDefaultValueSql("''");

			builder.Property(e => e.CommentApproved)
			       .IsRequired()
			       .HasColumnName("comment_approved")
			       .HasColumnType("varchar(20)")
			       .HasDefaultValueSql("'1'");

			builder.Property(e => e.CommentAuthor)
			       .IsRequired()
			       .HasColumnName("comment_author")
			       .HasColumnType("varchar(255)");

			builder.Property(e => e.CommentAuthorEmail)
			       .IsRequired()
			       .HasColumnName("comment_author_email")
			       .HasColumnType("varchar(100)")
			       .HasDefaultValueSql("''");

			builder.Property(e => e.CommentAuthorIP)
			       .IsRequired()
			       .HasColumnName("comment_author_IP")
			       .HasColumnType("varchar(100)")
			       .HasDefaultValueSql("''");

			builder.Property(e => e.CommentAuthorUrl)
			       .IsRequired()
			       .HasColumnName("comment_author_url")
			       .HasColumnType("varchar(200)")
			       .HasDefaultValueSql("''");

			builder.Property(e => e.CommentContent)
			       .IsRequired()
			       .HasColumnName("comment_content")
			       .HasColumnType("varchar(MAX)");

			builder.Property(e => e.CommentDate)
			       .HasColumnName("comment_date")
			       .HasColumnType("datetime")
			       .HasDefaultValueSql("'0000-00-00 00:00:00'");

			builder.Property(e => e.CommentDateGmt)
			       .HasColumnName("comment_date_gmt")
			       .HasColumnType("datetime")
			       .HasDefaultValueSql("'0000-00-00 00:00:00'");

			builder.Property(e => e.CommentKarma)
			       .HasColumnName("comment_karma")
			       .HasColumnType("int")
			       .HasDefaultValueSql("'0'");

			builder.Property(e => e.CommentParent)
			       .HasColumnName("comment_parent")
			       .HasDefaultValueSql("'0'");

			builder.Property(e => e.CommentPostId)
			       .HasColumnName("comment_post_ID")
			       .HasDefaultValueSql("'0'");

			builder.Property(e => e.CommentType)
			       .IsRequired()
			       .HasColumnName("comment_type")
			       .HasColumnType("varchar(20)")
			       .HasDefaultValueSql("''");

			builder.Property(e => e.UserId)
			       .HasColumnName("user_id")
			       .HasDefaultValueSql("'0'");

			//builder.HasMany(t => t.CommentMetas)
			//       .WithOne(t => t.Comment)
			//       .HasForeignKey(t => t.CommentId);

			//builder.HasMany(t => t.CommentMetas)
			//       .WithOne(t => t.Comment)
			//       .HasForeignKey(t => t.Comment);

		}
	}
}