using CultureWars.Data.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CultureWars.Data.Maps
{
	public class CultureWarsMagazineVolumeTypeConfiguration
		: IEntityTypeConfiguration<CultureWarsMagazineVolume>
	{
		/// <inheritdoc/>
		public void Configure(
			EntityTypeBuilder<CultureWarsMagazineVolume> builder)
		{
			builder.ToTable("CultureWarsMagazineVolumes");

			builder.HasKey(t => t.CultureWarsMagazineVolumeID);
			builder.Property(t => t.CultureWarsMagazineVolumeID)
				.ValueGeneratedOnAdd();

			builder.Property(t => t.Year)
				.IsRequired();

			builder.Property(t => t.VolumeNumber)
				.IsRequired();

			builder.HasMany(t => t.CultureWarsMagazineIssues)
				.WithOne(t => t.CultureWarsMagazineVolume)
				.IsRequired()
				.OnDelete(DeleteBehavior.Restrict);
		}
	}
}