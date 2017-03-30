using System.Data.Entity.ModelConfiguration;
using GameStore.Domain.Core.Models;

namespace GameStore.Infrastructure.EFDataAccess.Configurations
{
    public class GameConfiguration : EntityTypeConfiguration<Game>
    {
        public GameConfiguration()
        {
            Property(c => c.Key).IsRequired().HasMaxLength(200);
            Property(c => c.Name).IsRequired();
            Property(c => c.Description).IsRequired();
            Property(c => c.PublishingDate).IsRequired();

            HasMany(c => c.Comments).WithOptional(c => c.Game);
            HasMany(c => c.PlatformTypes).WithMany();
            HasMany(c => c.Genres).WithMany();
            HasMany(g => g.Publishers).WithMany(p => p.Games);
        }
    }
}
