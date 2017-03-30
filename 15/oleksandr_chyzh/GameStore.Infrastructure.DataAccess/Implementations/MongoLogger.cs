using GameStore.Infrastructure.DataAccess.Interfaces;
using GameStore.Infrastructure.MongoDataAccess.Models;
using MongoDB.Driver;

namespace GameStore.Infrastructure.DataAccess.Implementations
{
    public class MongoLogger : IMongoLogger
    {
        private readonly IMongoCollection<Log> _logs;

        public MongoLogger(IMongoCollection<Log> logs)
        {
            _logs = logs;
        }

        public void LogOperation(Log log)
        {
            _logs.InsertOne(log);
        }
    }
}
