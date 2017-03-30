using GameStore.Domain.Core.Models;
using GameStore.Domain.Interfaces;
using GameStore.Services.Dto;
using GameStore.Services.Dto.Utils;
using GameStore.Services.Interfaces.Exceptions;
using Moq;
using NUnit.Framework;

namespace GameStore.Infrastructure.Services.Tests
{
    [TestFixture]
    public class OrderDetailsServiceTest
    {
        private Mock<IUnitOfWork> _mock;
        private OrderDetailsService _service;

        [SetUp]
        public void SetUp()
        {
            _mock = new Mock<IUnitOfWork>();
            _service = new OrderDetailsService(_mock.Object);
        }

        [Test]
        public void ShouldThrowException_ForNonExistentOrderDetails()
        {
            _mock.Setup(m => m.OrderDetails.Get(It.IsAny<int>()))
                .Returns((OrderDetails)null);

            Assert.That(() => _service.Get(It.IsAny<int>()), Throws.TypeOf<EntityNotFoundException>());
        }

        [Test]
        public void ShouldReturnOrderDetails()
        {
            var orderDetails = new OrderDetails(
                productId: 1,
                price: 10,
                quantity: 2,
                discount: 0);
            _mock.Setup(m => m.OrderDetails.Get(It.IsAny<int>()))
                .Returns(orderDetails);

            OrderDetailsDto orderDetailsDto = _service.Get(It.IsAny<int>());

            Assert.IsTrue(orderDetailsDto.Equals(orderDetails.ToDto()));
        }

        [Test]
        public void ShouldThrowException_ForNonExistentGame_WhenCreateOrderDetails()
        {
            _mock.Setup(m => m.Games.Get(It.IsAny<int>()))
                .Returns((Game)null);

            Assert.That(
                () => _service.Create(
                    productId: 0,
                    price: 0,
                    quantity: 0, 
                    discount: 0), 
                Throws.TypeOf<EntityNotFoundException>());
        }

        [Test]
        public void ShouldThrowException_ForGameDeficit_WhenCreateOrderDetails()
        {
            _mock.Setup(m => m.Games.Get(It.IsAny<int>()))
                .Returns(new Game() { UnitsInStock = 2 });

            Assert.That(
                () => _service.Create(
                    productId: 0,
                    price: 0, 
                    quantity: 3,
                    discount: 0), 
                Throws.TypeOf<GameDeficitException>());
        }

        [Test]
        public void ShouldCreateOrderDetails()
        {
            _mock.Setup(m => m.Games.Get(It.IsAny<int>()))
                .Returns(new Game() { UnitsInStock = 2 });
            _mock.Setup(m => m.Games.Update(It.IsAny<Game>()));
            _mock.Setup(m => m.OrderDetails.Create(It.IsAny<OrderDetails>()));
            _mock.Setup(m => m.Save());

            _service.Create(
                productId: 1,
                price: 10,
                quantity: 1,
                discount: 0);

            _mock.Verify(m => m.Games.Update(It.IsAny<Game>()), Times.AtLeastOnce);
            _mock.Verify(m => m.OrderDetails.Create(It.IsAny<OrderDetails>()), Times.AtLeastOnce);
            _mock.Verify(m => m.Save(), Times.AtLeastOnce);
        }
    }
}
