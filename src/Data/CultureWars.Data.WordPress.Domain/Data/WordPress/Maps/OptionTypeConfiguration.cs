using CultureWars.Data.WordPress.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CultureWars.Data.WordPress.Maps
{
	public class OptionTypeConfiguration
		: IEntityTypeConfiguration<Option>
	{
		/// <inheritdoc/>
		public void Configure(
			EntityTypeBuilder<Option> builder)
		{
			builder.ToTable("wp_options");

			builder.HasKey(e => e.OptionId);
			builder.Property(e => e.OptionId)
			       .HasColumnName("option_id")
			       // .UseIdentityColumn()()
			       .IsRequired()
			       .ValueGeneratedOnAdd();

			builder.HasIndex(e => e.OptionName)
			       .HasName("option_name")
			       .IsUnique();

			builder.Property(e => e.Autoload)
			       .IsRequired()
			       .HasColumnName("autoload")
			       .HasColumnType("varchar(20)")
			       .HasDefaultValueSql("'yes'");

			builder.Property(e => e.OptionName)
			       .IsRequired()
			       .HasColumnName("option_name")
			       .HasColumnType("varchar(191)")
			       .HasDefaultValueSql("''");

			builder.Property(e => e.OptionValue)
			       .IsRequired()
			       .HasColumnName("option_value")
			       .HasColumnType("varchar(MAX)");
		}
	}
}