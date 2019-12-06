using CultureWars.Data.WordPress.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CultureWars.Data.WordPress.Maps
{
	public class UserMetaTypeConfiguration
		: IEntityTypeConfiguration<UserMeta>
	{
		/// <inheritdoc/>
		public void Configure(
			EntityTypeBuilder<UserMeta> builder)
		{
			builder.ToTable("wp_usermeta");

			builder.HasKey(e => e.UserMetaId);
			builder.Property(e => e.UserMetaId)
			       .HasColumnName("umeta_id")
			       .IsRequired()
			       .ValueGeneratedOnAdd();

			builder.HasIndex(e => e.MetaKey)
			       .HasName("meta_key");

			builder.HasIndex(e => e.UserId)
			       .HasName("user_id");
			
			builder.Property(e => e.MetaKey)
			       .HasColumnName("meta_key")
			       .HasColumnType("varchar(255)");

			builder.Property(e => e.MetaValue)
			       .HasColumnName("meta_value")
			       .HasColumnType("varchar(MAX)");

			builder.Property(e => e.UserId)
			       .HasColumnName("user_id")
			       .HasDefaultValueSql("'0'");
		}
	}
}