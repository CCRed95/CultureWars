using CultureWars.Data.Domain;
using Microsoft.EntityFrameworkCore;
using CultureWars.Core.Data.Extensions;
using CultureWars.Data.Maps;

namespace CultureWars.Data.Context
{
	public class CultureWarsMagazineContext
		: DbContext
	{
		public virtual DbSet<CultureWarsMagazineVolume> CultureWarsMagazineVolumes { get; set; }

		public virtual DbSet<CultureWarsMagazineIssue> CultureWarsMagazineIssues { get; set; }

		public virtual DbSet<CultureWarsMagazineArticle> CultureWarsMagazineArticles { get; set; }



		public CultureWarsMagazineContext()
		{
		}

		public CultureWarsMagazineContext(
			DbContextOptions<CultureWarsMagazineContext> options)
				: base(options)
		{
		}


		protected override void OnConfiguring(
			DbContextOptionsBuilder optionsBuilder)
		{
			if (!optionsBuilder.IsConfigured)
			{
				const string connectionStr = "";
				optionsBuilder.UseSqlServer(connectionStr);
			}
		}

		protected override void OnModelCreating(
			ModelBuilder modelBuilder)
		{
			modelBuilder
				.WithConfiguration<CultureWarsMagazineVolume, CultureWarsMagazineVolumeTypeConfiguration>()
				.WithConfiguration<CultureWarsMagazineIssue, CultureWarsMagazineIssueTypeConfiguration>()
				.WithConfiguration<CultureWarsMagazineArticle, CultureWarsMagazineArticleTypeConfiguration>();
		}
	}
}
