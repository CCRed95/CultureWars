using CultureWars.Data.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CultureWars.Data.Maps
{
	public class CultureWarsVideoTypeConfiguration
		: IEntityTypeConfiguration<CultureWarsVideo>
	{
		public void Configure(EntityTypeBuilder<CultureWarsVideo> builder)
		{
			builder.ToTable("CultureWarsVideos");

			builder.HasKey(t => t.CultureWarsVideoID);
			builder.Property(t => t.CultureWarsVideoID)
				.ValueGeneratedOnAdd();

			builder.Property(t => t.VideoSourceUrl)
				.IsRequired()
				.HasMaxLength(500);
		}
	}
}
