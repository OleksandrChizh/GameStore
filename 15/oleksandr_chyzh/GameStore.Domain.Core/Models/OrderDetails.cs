namespace GameStore.Domain.Core.Models
{
    public class OrderDetails : Entity
    {
        public OrderDetails(int productId, decimal price, short quantity, float discount)
        {
            ProductId = productId;
            Price = price;
            Quantity = quantity;
            Discount = discount;
        }

        public OrderDetails()
        {
        }

        public int Id { get; set; }

        public int ProductId { get; set; }

        public decimal Price { get; set; }

        public short Quantity { get; set; }

        public float Discount { get; set; }
    }
}
