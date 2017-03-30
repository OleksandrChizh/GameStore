using System;
using MongoDB.Bson;

namespace GameStore.Infrastructure.MongoDataAccess.Models
{
    public class Log
    {
        public DateTime Date { get; set; }

        public string Action { get; set; }

        public string EntityType { get; set; }

        public BsonJavaScript OldEntity { get; set; }

        public BsonJavaScript NewEntity { get; set; }
    }
}
