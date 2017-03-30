using MongoDB.Bson.Serialization.Attributes;

namespace GameStore.Infrastructure.MongoDataAccess.Models
{
    [BsonIgnoreExtraElements]
    public class MongoOrderDetails
    {
        [BsonElement("ProductID")]
        public int ProductId { get; set; }

        [BsonElement("OrderID")]
        public int OrderId { get; set; }

        public decimal UnitPrice { get; set; }

        public short Quantity { get; set; }

        public double Discount { get; set; }
    }
}
