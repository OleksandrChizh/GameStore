using System.Data.Entity.ModelConfiguration;
using GameStore.Domain.Core.Models;

namespace GameStore.Infrastructure.EFDataAccess.Configurations
{
    public class PublisherTranslationConfiguration : EntityTypeConfiguration<PublisherTranslation>
    {
        public PublisherTranslationConfiguration()
        {
            Property(pt => pt.Description).IsRequired();
            Property(pt => pt.Language).IsRequired().HasMaxLength(2);
            HasRequired(pt => pt.Publisher).WithMany(p => p.Translations);
        }
    }
}
