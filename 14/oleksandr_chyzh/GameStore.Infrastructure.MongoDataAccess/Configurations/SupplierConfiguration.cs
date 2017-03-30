using GameStore.Domain.Core.Models;
using MongoDB.Bson.Serialization;

namespace GameStore.Infrastructure.MongoDataAccess.Configurations
{
    public class SupplierConfiguration : BsonClassMap<Publisher>
    {
        public SupplierConfiguration()
        {
            MapMember(p => p.Id).SetElementName("SupplierID");
            MapMember(p => p.CompanyName);
            MapMember(p => p.HomePage);
        }
    }
}
