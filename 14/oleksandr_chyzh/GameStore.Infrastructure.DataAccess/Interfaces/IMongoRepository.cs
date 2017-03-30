using GameStore.Infrastructure.MongoDataAccess.Models;
using MongoDB.Driver;

namespace GameStore.Infrastructure.DataAccess.Interfaces
{
    public interface IMongoRepository<in TEntity>
    {
        void Update(TEntity mongoModel);

        IMongoCollection<Log> GetLogsCollection();

        bool Contains(TEntity entity);
    }
}
