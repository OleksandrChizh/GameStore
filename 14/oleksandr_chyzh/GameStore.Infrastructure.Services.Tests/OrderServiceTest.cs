using System.Collections.Generic;
using GameStore.Domain.Core.Models;
using GameStore.Domain.Interfaces;
using GameStore.Infrastructure.DataAccess.Interfaces;
using GameStore.Services.Dto;
using GameStore.Services.Interfaces;
using GameStore.Services.Interfaces.Exceptions;
using Moq;
using NUnit.Framework;

namespace GameStore.Infrastructure.Services.Tests
{
    [TestFixture]
    public class OrderServiceTest
    {
        private Mock<IUnitOfWork> _mock;
        private IOrderService _service;

        [SetUp]
        public void SetUp()
        {
            _mock = new Mock<IUnitOfWork>();
            var serviceFactoryMock = new Mock<IFilterFactory>();
            _service = new OrderService(_mock.Object, serviceFactoryMock.Object, null);
        }

        [Test]
        public void ShouldThrowException_ForNonExistentOrder_WhenGetOrder()
        {
            _mock.Setup(m => m.Orders.Get(It.IsAny<int>()))
                .Returns((Order)null);

            Assert.That(() => _service.Get(It.IsAny<int>()), Throws.TypeOf<EntityNotFoundException>());
        }

        [Test]
        public void ShouldReturnOrder_ForOrderGetting()
        {
            const string exceptedCustomeId = "1"; 
            _mock.Setup(m => m.Orders.Get(It.IsAny<int>()))
                .Returns(new Order(
                    customerId: exceptedCustomeId,
                    orderDetails: new List<OrderDetails>()));

            OrderDto order = _service.Get(It.IsAny<int>());

            Assert.AreEqual(exceptedCustomeId, order.CustomerId);
        }

        [Test]
        public void ShouldThrowException_WhenOrderDetailsNotFound_ForOrderCreating()
        {
            _mock.Setup(m => m.OrderDetails.Find(It.IsAny<IPipeline<OrderDetails>>()))
                .Returns(new List<OrderDetails> { new OrderDetails() });

            Assert.That(
                () => _service.Create(
                    customerId: "1", 
                    orderDetailIds: new List<int> { 1, 2 }), 
                Throws.TypeOf<EntityNotFoundException>());
        }

        [Test]
        public void ShouldCreateOrder()
        {
            _mock.Setup(m => m.OrderDetails.Find(It.IsAny<IPipeline<OrderDetails>>()))
                .Returns(new List<OrderDetails> { new OrderDetails() });
            _mock.Setup(m => m.Orders.Create(It.IsAny<Order>()));
            _mock.Setup(m => m.Save());

            _service.Create(
                customerId: "1",
                orderDetailIds: new List<int>() { 1 });

            _mock.Verify(m => m.Orders.Create(It.IsAny<Order>()), Times.AtLeastOnce);
            _mock.Verify(m => m.Save(), Times.AtLeastOnce);
        }

        [Test]
        public void ShouldPayOrder()
        {
            var order = new Order();
            _mock.Setup(m => m.Orders.Get(It.IsAny<int>()))
                .Returns(order);
            _mock.Setup(m => m.Orders.Update(It.IsAny<Order>()));
            _mock.Setup(m => m.Save());

            _service.Pay(It.IsAny<int>());

            _mock.Verify(m => m.Orders.Update(It.IsAny<Order>()), Times.Once);
            _mock.Verify(m => m.Save(), Times.Once);
        }
    }
}
