using CultureWars.Data.Extensions;
using CultureWars.Data.WordPress.Domain;
using CultureWars.Data.WordPress.Maps;
using Microsoft.EntityFrameworkCore;

namespace CultureWars.Data.WordPress.Context
{
	public class CoreContext
		: DbContext
	{
		public virtual DbSet<CommentMeta> CommentMetas { get; set; }

		public virtual DbSet<Comment> Comments { get; set; }

		public virtual DbSet<Link> Links { get; set; }

		public virtual DbSet<Option> Options { get; set; }

		public virtual DbSet<PostMeta> PostMeta { get; set; }

		public virtual DbSet<Post> Posts { get; set; }

		public virtual DbSet<TermRelationship> TermRelationships { get; set; }

		public virtual DbSet<TermTaxonomy> TermTaxonomies { get; set; }

		public virtual DbSet<TermMeta> TermMetas { get; set; }

		public virtual DbSet<Term> Terms { get; set; }

		public virtual DbSet<UserMeta> UserMetas { get; set; }

		public virtual DbSet<User> Users { get; set; }


		public CoreContext()
		{
		}

		public CoreContext(
			DbContextOptions<CoreContext> options)
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
				.WithConfiguration<Comment, CommentTypeConfiguration>()
				.WithConfiguration<CommentMeta, CommentMetaTypeConfiguration>()
				.WithConfiguration<Link, LinkTypeConfiguration>()
				.WithConfiguration<Option, OptionTypeConfiguration>()
				.WithConfiguration<Post, PostTypeConfiguration>()
				.WithConfiguration<PostMeta, PostMetaTypeConfiguration>()
				.WithConfiguration<Term, TermTypeConfiguration>()
				.WithConfiguration<TermMeta, TermMetaTypeConfiguration>()
				.WithConfiguration<TermRelationship, TermRelationshipTypeConfiguration>()
				.WithConfiguration<TermTaxonomy, TermTaxonomyTypeConfiguration>()
				.WithConfiguration<User, UserTypeConfiguration>()
				.WithConfiguration<UserMeta, UserMetaTypeConfiguration>();
		}
	}
}
