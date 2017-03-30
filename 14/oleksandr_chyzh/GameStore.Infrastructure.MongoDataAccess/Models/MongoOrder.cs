using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace GameStore.Infrastructure.MongoDataAccess.Models
{
    [BsonIgnoreExtraElements]
    public class MongoOrder
    {
        [BsonElement("_id")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("OrderID")]
        public int OrderId { get; set; }

        public string OrderDate { get; set; }

        public bool OutDated { get; set; }

        [BsonElement("CustomerID")]
        public string CustomerId { get; set; }
    }
}
