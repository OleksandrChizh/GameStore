using GameStore.Infrastructure.MongoDataAccess.Models;

namespace GameStore.Infrastructure.DataAccess.Interfaces
{
    public interface IMongoLogger
    {
        void LogOperation(Log log);
    }
}
