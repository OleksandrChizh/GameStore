using System;
using System.Collections.Generic;
using System.Linq;
using GameStore.Domain.Core.Models;
using GameStore.Domain.Interfaces;
using GameStore.Infrastructure.DataAccess.Filters;
using GameStore.Infrastructure.DataAccess.Implementations;
using GameStore.Infrastructure.DataAccess.Interfaces;
using GameStore.Services.Dto;
using GameStore.Services.Dto.Utils;
using GameStore.Services.Interfaces;
using GameStore.Services.Interfaces.Exceptions;
using GameStore.Services.Interfaces.Utils;

namespace GameStore.Infrastructure.Services
{
    public class OrderService : Service, IOrderService
    {
        private readonly IFilterFactory _filterFactory;
        private readonly IPipeline<Order> _pipeline;

        public OrderService(IUnitOfWork unitOfWork, IFilterFactory filterFactory, IPipeline<Order> pipline) : base(unitOfWork)
        {
            _filterFactory = filterFactory;
            _pipeline = pipline;
        }

        public void Update(int orderId, DateTime orderDate, DateTime payingDate, DateTime shippedDate)
        {
            Order order = GetOrder(orderId);
            if (orderDate > payingDate ||
                orderDate > shippedDate ||
                payingDate > shippedDate)
            {
                throw new IncorrectFollowingDateException();
            }

            order.OrderDate = orderDate;
            order.PayingDate = payingDate;
            order.ShippedDate = shippedDate;

            UnitOfWork.Orders.Update(order);
            UnitOfWork.Save();
        }

        public IEnumerable<OrderDto> GetOrders(OrdersFilterAttributes attributes)
        {
            if (attributes == null)
            {
                return UnitOfWork.Orders.GetAll().Select(o => o.ToDto()).ToList();
            }

            _pipeline.Register(new OrdersByDateRangeFilter(attributes.From, attributes.To));

            return UnitOfWork.Orders.Find(_pipeline).Select(o => o.ToDto());
        }

        public OrderDto Get(int id)
        {
            Order order = GetOrder(id);
            return order.ToDto();
        }

        public int Create(string customerId, IList<int> orderDetailIds)
        {
            ICollection<OrderDetails> orderDetails = GetOrderDetailsFromIds(orderDetailIds);
            var order = new Order(customerId, orderDetails) { OrderDate = DateTime.UtcNow };

            UnitOfWork.Orders.Create(order);
            UnitOfWork.Save();
            return order.Id;
        }

        public void Pay(int orderId)
        {
            Order order = GetOrder(orderId);

            if (order.PayingDate != default(DateTime))
            {
                throw new OrderPaidException();
            }

            order.PayingDate = DateTime.UtcNow;

            UnitOfWork.Orders.Update(order);
            UnitOfWork.Save();
        }

        public void Deliver(int orderId)
        {
            Order order = GetOrder(orderId);

            if (order.ShippedDate != default(DateTime))
            {
                throw new OrderDeliveringException();
            }

            order.ShippedDate = DateTime.UtcNow;

            UnitOfWork.Orders.Update(order);
            UnitOfWork.Save();
        }

        private ICollection<OrderDetails> GetOrderDetailsFromIds(ICollection<int> orderDetailsIds)
        {
            var pipeline = new Pipeline<OrderDetails>();
            pipeline.Register(_filterFactory.MakeOrderDetailsForIdsFilter(orderDetailsIds));

            List<OrderDetails> results = UnitOfWork.OrderDetails.Find(pipeline).ToList();
            if (results.Count != orderDetailsIds.Count)
            {
                throw new EntityNotFoundException(typeof(OrderDetails));
            }

            return results;
        }

        private Order GetOrder(int id)
        {
            Order order = UnitOfWork.Orders.Get(id);
            if (order == null)
            {
                throw new EntityNotFoundException(typeof(Order));
            }

            return order;
        }
    }
}
