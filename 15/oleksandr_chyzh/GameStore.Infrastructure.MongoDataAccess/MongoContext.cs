using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using GameStore.Domain.Core.Models;
using GameStore.Infrastructure.MongoDataAccess.Configurations;
using GameStore.Infrastructure.MongoDataAccess.Models;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace GameStore.Infrastructure.MongoDataAccess
{
    public class MongoContext
    {
        private readonly IMongoDatabase _database;

        public MongoContext()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["MongoDb"].ConnectionString;
            var connection = new MongoUrlBuilder(connectionString);
            var client = new MongoClient(connectionString);
            _database = client.GetDatabase(connection.DatabaseName);

            List<BsonClassMap> mapps = BsonClassMap.GetRegisteredClassMaps().ToList();

            if (mapps.All(m => m.GetType() != typeof(CategoryConfiguration)))
            {               
                BsonClassMap.RegisterClassMap(new CategoryConfiguration());
            }

            if (mapps.All(m => m.GetType() != typeof(SupplierConfiguration)))
            {
                BsonClassMap.RegisterClassMap(new SupplierConfiguration());
            }
            
            if (mapps.All(m => m.GetType() != typeof(GameConfiguration)))
            {
                BsonClassMap.RegisterClassMap(new GameConfiguration());
            }
        }

        public IMongoQueryable<Product> Products => _database.GetCollection<Product>("products").AsQueryable();

        public IMongoQueryable<Genre> Categories => _database.GetCollection<Genre>("categories").AsQueryable();

        public IMongoQueryable<Publisher> Suppliers => _database.GetCollection<Publisher>("suppliers").AsQueryable();

        public IMongoQueryable<MongoOrder> Orders => _database.GetCollection<MongoOrder>("orders").AsQueryable();

        public IMongoQueryable<MongoOrderDetails> OrdersDetails => _database.GetCollection<MongoOrderDetails>("order-details").AsQueryable();

        public IMongoCollection<Product> ProductsCollection => _database.GetCollection<Product>("products");

        public IMongoCollection<MongoOrder> OrderCollection => _database.GetCollection<MongoOrder>("orders");

        public IMongoCollection<Log> LogsCollection => _database.GetCollection<Log>("logs");
    }
}
