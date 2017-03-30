using System;
using System.Collections.Generic;

namespace GameStore.Domain.Core.Models
{
    public class Order : Entity
    {
        public Order(string customerId, ICollection<OrderDetails> orderDetails)
        {
            CustomerId = customerId;
            OrderDetails = orderDetails;
        }

        public Order()
        {
        }

        public int Id { get; set; }

        public string CustomerId { get; set; }

        public DateTime OrderDate { get; set; }

        public DateTime PayingDate { get; set; }

        public DateTime ShippedDate { get; set; }

        public virtual ICollection<OrderDetails> OrderDetails { get; set; } = new List<OrderDetails>();
    }
}
