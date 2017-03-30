using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.Infrastructure.Annotations;
using GameStore.Domain.Core.Models;
using GameStore.Infrastructure.EFDataAccess.Configurations;

namespace GameStore.Infrastructure.EFDataAccess
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext() : base("DbGameStore")
        {
        }

        public DbSet<Comment> Comments { get; set; }

        public DbSet<Game> Games { get; set; }

        public DbSet<Genre> Genres { get; set; }

        public DbSet<PlatformType> PlatformTypes { get; set; }

        public DbSet<Publisher> Publishers { get; set; }

        public DbSet<OrderDetails> OrderDetails { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<PublisherTranslation> PublisherTranslations { get; set; }

        public DbSet<GameTranslation> GameTranslations { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<Role> Roles { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Configurations.Add(new CommentConfiguration());
            modelBuilder.Configurations.Add(new GameConfiguration());
            modelBuilder.Configurations.Add(new GenreConfiguration());
            modelBuilder.Configurations.Add(new PlatformTypeConfiguration());
            modelBuilder.Configurations.Add(new PublisherConfiguration());
            modelBuilder.Configurations.Add(new OrderConfiguration());
            modelBuilder.Configurations.Add(new GameTranslationConfiguration());
            modelBuilder.Configurations.Add(new PublisherTranslationConfiguration());
            modelBuilder.Configurations.Add(new RoleConfiguration());
            modelBuilder.Configurations.Add(new UserConfiguration());

            modelBuilder
                .Entity<Order>()
                .Property(o => o.OrderDate)
                .HasColumnType("datetime2");

            modelBuilder
                .Entity<Order>()
                .Property(o => o.PayingDate)
                .HasColumnType("datetime2");

            modelBuilder
                .Entity<Order>()
                .Property(o => o.ShippedDate)
                .HasColumnType("datetime2");

            modelBuilder
                .Entity<Game>()
                .Property(g => g.AddingDate)
                .HasColumnType("datetime2");

            modelBuilder
                .Entity<Game>()
                .Property(g => g.PublishingDate)
                .HasColumnType("datetime2");

            modelBuilder
                .Entity<User>()
                .Property(u => u.BanExpiryDate)
                .HasColumnType("datetime2");

            modelBuilder
                .Entity<Game>()
                .Property(g => g.Key)
                .HasColumnAnnotation(
                    IndexAnnotation.AnnotationName,
                    new IndexAnnotation(new IndexAttribute("UniqueGameKey", 1) { IsUnique = true }));

            modelBuilder
                .Entity<Genre>()
                .Property(g => g.Name)
                .HasColumnAnnotation(
                    IndexAnnotation.AnnotationName,
                    new IndexAnnotation(new IndexAttribute("UniqueGenreName", 1) { IsUnique = true }));

            modelBuilder
                .Entity<PlatformType>()
                .Property(t => t.Type)
                .HasColumnAnnotation(
                    IndexAnnotation.AnnotationName,
                    new IndexAnnotation(new IndexAttribute("UniquePlatformeType", 1) { IsUnique = true }));

            modelBuilder
                .Entity<Publisher>()
                .Property(p => p.CompanyName)
                .HasColumnAnnotation(
                    IndexAnnotation.AnnotationName,
                    new IndexAnnotation(new IndexAttribute("UniqueCompanyName", 1) { IsUnique = true }));

            modelBuilder
                .Entity<Role>()
                .Property(p => p.Name)
                .HasColumnAnnotation(
                    IndexAnnotation.AnnotationName,
                    new IndexAnnotation(new IndexAttribute("UniqueRoleName", 1) { IsUnique = true }));

            modelBuilder
                .Entity<User>()
                .Property(p => p.UserName)
                .HasColumnAnnotation(
                    IndexAnnotation.AnnotationName,
                    new IndexAnnotation(new IndexAttribute("UniqueUserName", 1) { IsUnique = true }));
        }
    }
}