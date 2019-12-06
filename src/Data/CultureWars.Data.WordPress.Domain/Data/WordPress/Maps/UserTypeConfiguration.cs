using CultureWars.Data.WordPress.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CultureWars.Data.WordPress.Maps
{
	public class UserTypeConfiguration
		: IEntityTypeConfiguration<User>
	{
		/// <inheritdoc/>
		public void Configure(
			EntityTypeBuilder<User> builder)
		{
			builder.ToTable("wp_users");

			builder.Property(e => e.UserId)
			       .HasColumnName("ID")
			       // .UseIdentityColumn()()
			       .IsRequired()
			       .ValueGeneratedOnAdd();

			builder.HasIndex(e => e.UserEmail)
			       .HasName("user_email");

			builder.HasIndex(e => e.UserLogin)
			       .HasName("user_login_key");

			builder.HasIndex(e => e.UserNicename)
			       .HasName("user_nicename");

			builder.Property(e => e.DisplayName)
			       .IsRequired()
			       .HasColumnName("display_name")
			       .HasColumnType("varchar(250)")
			       .HasDefaultValueSql("''");

			builder.Property(e => e.UserActivationKey)
			       .IsRequired()
			       .HasColumnName("user_activation_key")
			       .HasColumnType("varchar(255)")
			       .HasDefaultValueSql("''");

			builder.Property(e => e.UserEmail)
			       .IsRequired()
			       .HasColumnName("user_email")
			       .HasColumnType("varchar(100)")
			       .HasDefaultValueSql("''");

			builder.Property(e => e.UserLogin)
			       .IsRequired()
			       .HasColumnName("user_login")
			       .HasColumnType("varchar(60)")
			       .HasDefaultValueSql("''");

			builder.Property(e => e.UserNicename)
			       .IsRequired()
			       .HasColumnName("user_nicename")
			       .HasColumnType("varchar(50)")
			       .HasDefaultValueSql("''");

			builder.Property(e => e.UserPass)
			       .IsRequired()
			       .HasColumnName("user_pass")
			       .HasColumnType("varchar(255)")
			       .HasDefaultValueSql("''");

			builder.Property(e => e.UserRegistered)
			       .HasColumnName("user_registered")
			       .HasColumnType("datetime")
			       .HasDefaultValueSql("'0000-00-00 00:00:00'");

			builder.Property(e => e.UserStatus)
			       .HasColumnName("user_status")
			       .HasColumnType("int")
			       .HasDefaultValueSql("'0'");

			builder.Property(e => e.UserUrl)
			       .IsRequired()
			       .HasColumnName("user_url")
			       .HasColumnType("varchar(100)")
			       .HasDefaultValueSql("''");
		}
	}
}