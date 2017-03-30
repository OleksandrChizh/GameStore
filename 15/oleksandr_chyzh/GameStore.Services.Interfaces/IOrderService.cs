using System;
using System.Collections.Generic;
using GameStore.Services.Dto;
using GameStore.Services.Interfaces.Utils;
using GameStore.Services.Interfaces.Validators;

namespace GameStore.Services.Interfaces
{
    public interface IOrderService : IDomainEntityService<OrderDto, int>
    {
        IEnumerable<OrderDto> GetOrders([OrdersFilterData]OrdersFilterAttributes attributes = null);

        int Create([NonEmptyString] string customerId, IList<int> orderDetailIds);

        void Update(int orderId, DateTime orderDate, DateTime payingDate, DateTime shippedDate);

        void Pay(int orderId);

        void Deliver(int orderId);

        OrderDto GetCurrentOrderForCustomer([NonEmptyString] string customerId);

        void Update(int orderId, [PositiveQuantities] IDictionary<int, short> productQuantity, [NonEmptyString] string customerId);
    }
}
