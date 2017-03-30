using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace GameStore.Infrastructure.MongoDataAccess.Models
{
    [BsonIgnoreExtraElements]
    public class Product
    {
        [BsonElement("_id")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("ProductID")]
        public int ProductId { get; set; }

        public string ProductName { get; set; }

        [BsonElement("SupplierID")]
        public int SupplierId { get; set; }

        [BsonElement("CategoryID")]
        public int CategoryId { get; set; }

        public decimal UnitPrice { get; set; }

        public int UnitsInStock { get; set; }

        public int Discontinued { get; set; }
    }
}
