using System;
using static System.Single;

namespace GameStore.Services.Dto
{
    public class OrderDetailsDto
    {
        public OrderDetailsDto(int id, int productId, decimal price, short quantity, float discount)
        {
            Id = id;
            ProductId = productId;
            Price = price;
            Quantity = quantity;
            Discount = discount;
        }

        public int Id { get; private set; }

        public int ProductId { get; private set; }

        public decimal Price { get; private set; }

        public short Quantity { get; private set; }

        public float Discount { get; private set; }

        public bool Equals(OrderDetailsDto orderDetails)
        {
            const float TOLERANCE = Epsilon;
            return ProductId == orderDetails.ProductId &&
                   Price == orderDetails.Price &&
                   Quantity == orderDetails.Quantity &&
                   Math.Abs(Discount - orderDetails.Discount) < TOLERANCE;
        }
    }
}
