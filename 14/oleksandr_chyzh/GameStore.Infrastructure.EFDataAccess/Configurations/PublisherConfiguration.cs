using System.Data.Entity.ModelConfiguration;
using GameStore.Domain.Core.Models;

namespace GameStore.Infrastructure.EFDataAccess.Configurations
{
    public class PublisherConfiguration : EntityTypeConfiguration<Publisher>
    {
        public PublisherConfiguration()
        {
            Property(c => c.CompanyName).IsRequired().HasMaxLength(200);
        }
    }
}
