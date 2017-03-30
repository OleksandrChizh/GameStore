using GameStore.Domain.Core.Models;
using MongoDB.Bson.Serialization;

namespace GameStore.Infrastructure.MongoDataAccess.Configurations
{
    public class GameConfiguration : BsonClassMap<Game>
    {
        public GameConfiguration()
        {
            UnmapMember(g => g.Genres);
            UnmapMember(g => g.Publishers);
            UnmapMember(g => g.Comments);
            UnmapMember(g => g.PlatformTypes);
        }
    }
}
