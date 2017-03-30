using System.Data.Entity.ModelConfiguration;
using GameStore.Domain.Core.Models;

namespace GameStore.Infrastructure.EFDataAccess.Configurations
{
    public class OrderConfiguration : EntityTypeConfiguration<Order>
    {
        public OrderConfiguration()
        {
            Property(o => o.CustomerId).IsRequired();
            Property(o => o.PayingDate).IsRequired();
            Property(o => o.ShippedDate).IsOptional();
            HasMany(o => o.OrderDetails);
        }
    }
}
