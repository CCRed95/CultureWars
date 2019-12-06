using CultureWars.Data.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CultureWars.Data.Maps
{
	//public class CultureWarsCategoryTypeConfiguration
	//	: IEntityTypeConfiguration<
	//		Category>
	//{
	//	public void Configure(
	//		EntityTypeBuilder<CultureWarsCategory> builder)
	//	{
	//		builder.ToTable("CultureWarsCategories");

	//		builder.HasKey(t => t.CultureWarsCategoryID);
	//		builder.Property(t => t.CultureWarsCategoryID)
	//			.ValueGeneratedOnAdd();

	//		builder.Property(t => t.CategoryName)
	//			.IsRequired()
	//			.HasMaxLength(100);

	//		builder.Property(t => t.CategoryNiceName)
	//			.IsRequired()
	//			.HasMaxLength(100);

	//		builder.Property(t => t.CategoryParent)
	//			.IsRequired(false)
	//			.HasMaxLength(100);
	//	}
	//}
}