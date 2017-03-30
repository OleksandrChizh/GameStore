using System.Data.Entity.ModelConfiguration;
using GameStore.Domain.Core.Models;

namespace GameStore.Infrastructure.EFDataAccess.Configurations
{
    public class PlatformTypeConfiguration : EntityTypeConfiguration<PlatformType>
    {
        public PlatformTypeConfiguration()
        {
            Property(t => t.Type).IsRequired().HasMaxLength(200);
        }
    }
}
