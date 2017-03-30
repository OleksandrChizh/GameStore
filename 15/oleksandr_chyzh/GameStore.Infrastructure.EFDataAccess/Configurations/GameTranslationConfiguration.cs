using System.Data.Entity.ModelConfiguration;
using GameStore.Domain.Core.Models;

namespace GameStore.Infrastructure.EFDataAccess.Configurations
{
    public class GameTranslationConfiguration : EntityTypeConfiguration<GameTranslation>
    {
        public GameTranslationConfiguration()
        {
            Property(gt => gt.Name).IsRequired();
            Property(gt => gt.Description).IsRequired();
            Property(gt => gt.Language).IsRequired().HasMaxLength(2);
            HasRequired(gt => gt.Game).WithMany(g => g.Translations);
        }
    }
}
