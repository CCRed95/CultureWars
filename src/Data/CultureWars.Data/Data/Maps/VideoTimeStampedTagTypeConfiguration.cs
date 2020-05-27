using CultureWars.Data.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CultureWars.Data.Maps
{
	public class VideoTimeStampedTagTypeConfiguration
		: IEntityTypeConfiguration<VideoTimeStampedTag>
	{
		public void Configure(EntityTypeBuilder<VideoTimeStampedTag> builder)
		{
			builder.ToTable("VideoTimeStampedTags");

			builder.HasKey(t => t.VideoTimeStampedTagID);
			builder.Property(t => t.VideoTimeStampedTagID)
				.ValueGeneratedOnAdd();

			//builder.Property(t => t.CultureWarsTag)
			//builder.HasOne<CultureWarsVideo>()
			//	.WithOne(t => t.)
			//	.HasForeignKey<VideoTimeStampedTag>(ad => ad.CultureWarsVideoID);

			//builder.HasOne<CultureWarsVideo>()
			//	.WithOne(t => t.CultureWarsVideoID)
			//	.HasForeignKey<CultureWarsVideo>(ad => ad.CultureWarsVideoID);
		}
	}
}