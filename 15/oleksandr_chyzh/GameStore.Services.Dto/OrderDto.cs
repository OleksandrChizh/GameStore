using System;
using System.Collections.Generic;

namespace GameStore.Services.Dto
{
    public class OrderDto
    {
        public OrderDto(int id, string customerId, DateTime orderDate, DateTime payingDate, DateTime shippedDate, ICollection<OrderDetailsDto> orderDetails)
        {
            Id = id;
            CustomerId = customerId;
            OrderDate = orderDate;
            PayingDate = payingDate;
            ShippedDate = shippedDate;
            OrderDetails = orderDetails;
        }

        public OrderDto()
        {
        }

        public int Id { get;  set; }

        public string CustomerId { get; set; }

        public DateTime OrderDate { get; set; }

        public DateTime PayingDate { get; set; }

        public DateTime ShippedDate { get; set; }

        public ICollection<OrderDetailsDto> OrderDetails { get; set; }
    }
}
