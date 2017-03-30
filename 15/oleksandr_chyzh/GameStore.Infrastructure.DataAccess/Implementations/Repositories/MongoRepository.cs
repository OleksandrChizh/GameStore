using System.Linq;
using System.Text.RegularExpressions;
using GameStore.Domain.Core.Models;
using GameStore.Infrastructure.DataAccess.Interfaces;
using GameStore.Infrastructure.MongoDataAccess;
using GameStore.Infrastructure.MongoDataAccess.Models;
using MongoDB.Bson;
using MongoDB.Driver;

namespace GameStore.Infrastructure.DataAccess.Implementations.Repositories
{
    public class MongoRepository<T> : IMongoRepository<T>
    {
        private readonly MongoContext _mongoContext;

        public MongoRepository(MongoContext mongoContext)
        {
            _mongoContext = mongoContext;
        }

        public void Update(T entity)
        {
            var game = entity as Game;
            if (game != null)
            {
                FilterDefinition<Product> filter = Builders<Product>.Filter.Eq("_id", new ObjectId(game.Key));

                const int mongoFalse = 0;
                const int mongoTrue = 1;
                UpdateDefinition<Product> update = Builders<Product>.Update
                    .Set("ProductName", game.Name)
                    .Set("UnitsInStock", game.UnitsInStock)
                    .Set("UnitPrice", game.Price)
                    .Set("Discontinued", game.Discounted ? mongoTrue : mongoFalse);

                _mongoContext.ProductsCollection.UpdateOne(filter, update);
            }
        }

        public bool Contains(T entity)
        {
            const string objectIdRegex = @"[0-9a-f]{24}";

            var game = entity as Game;
            if (game != null && Regex.IsMatch(game.Key, objectIdRegex))
            {
                var product = _mongoContext.Products.SingleOrDefault(p => p.Id == game.Key);
                if (product != null)
                {
                    return true;
                }
            }

            return false;
        }

        public IMongoCollection<Log> GetLogsCollection()
        {
            return _mongoContext.LogsCollection;
        }
    }
}
