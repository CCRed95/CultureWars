using CultureWars.Data.WordPress.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CultureWars.Data.WordPress.Maps
{
	public class LinkTypeConfiguration
		: IEntityTypeConfiguration<Link>
	{
		/// <inheritdoc/>
		public void Configure(
			EntityTypeBuilder<Link> builder)
		{
			builder.ToTable("wp_links");

			builder.HasKey(e => e.LinkId);

			builder.Property(e => e.LinkId)
			       .HasColumnName("link_id")
			       .IsRequired()
			       .ValueGeneratedOnAdd();

			builder.HasIndex(e => e.LinkVisible)
			       .HasName("link_visible");

			builder.Property(e => e.LinkDescription)
			       .IsRequired()
			       .HasColumnName("link_description")
			       .HasColumnType("varchar(255)")
			       .HasDefaultValueSql("''");

			builder.Property(e => e.LinkImage)
			       .IsRequired()
			       .HasColumnName("link_image")
			       .HasColumnType("varchar(255)")
			       .HasDefaultValueSql("''");

			builder.Property(e => e.LinkName)
			       .IsRequired()
			       .HasColumnName("link_name")
			       .HasColumnType("varchar(255)")
			       .HasDefaultValueSql("''");

			builder.Property(e => e.LinkNotes)
			       .IsRequired()
			       .HasColumnName("link_notes")
			       .HasColumnType("varchar(MAX)");

			builder.Property(e => e.LinkOwner)
			       .HasColumnName("link_owner")
			       .HasDefaultValueSql("'1'");

			builder.Property(e => e.LinkRating)
			       .HasColumnName("link_rating")
			       .HasColumnType("int")
			       .HasDefaultValueSql("'0'");

			builder.Property(e => e.LinkRel)
			       .IsRequired()
			       .HasColumnName("link_rel")
			       .HasColumnType("varchar(255)")
			       .HasDefaultValueSql("''");

			builder.Property(e => e.LinkRss)
			       .IsRequired()
			       .HasColumnName("link_rss")
			       .HasColumnType("varchar(255)")
			       .HasDefaultValueSql("''");

			builder.Property(e => e.LinkTarget)
			       .IsRequired()
			       .HasColumnName("link_target")
			       .HasColumnType("varchar(25)")
			       .HasDefaultValueSql("''");

			builder.Property(e => e.LinkUpdated)
			       .HasColumnName("link_updated")
			       .HasColumnType("datetime")
			       .HasDefaultValueSql("'0000-00-00 00:00:00'");

			builder.Property(e => e.LinkUrl)
			       .IsRequired()
			       .HasColumnName("link_url")
			       .HasColumnType("varchar(255)")
			       .HasDefaultValueSql("''");

			builder.Property(e => e.LinkVisible)
			       .IsRequired()
			       .HasColumnName("link_visible")
			       .HasColumnType("varchar(20)")
			       .HasDefaultValueSql("'Y'");
		}
	}
}