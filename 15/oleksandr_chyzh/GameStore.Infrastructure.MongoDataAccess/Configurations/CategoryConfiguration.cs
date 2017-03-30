using GameStore.Domain.Core.Models;
using MongoDB.Bson.Serialization;

namespace GameStore.Infrastructure.MongoDataAccess.Configurations
{
    public class CategoryConfiguration : BsonClassMap<Genre>
    {
        public CategoryConfiguration()
        {
            MapMember(g => g.Id).SetElementName("CategoryID");
            MapMember(g => g.Name).SetElementName("CategoryName");
        }
    }
}
