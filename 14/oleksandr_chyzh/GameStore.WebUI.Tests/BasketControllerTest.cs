using System.Collections.Generic;
using System.Linq;
using System.Web.SessionState;
using GameStore.Services.Dto;
using GameStore.Services.Interfaces;
using GameStore.WebUI.Controllers;
using GameStore.WebUI.Models;
using GameStore.WebUI.Tests.Fakes;
using GameStore.WebUI.ViewModels.Basket;
using Moq;
using NUnit.Framework;

namespace GameStore.WebUI.Tests
{
    [TestFixture]
    public class BasketControllerTest
    {
        private const string Basket = "Basket";

        private BasketController _basketController;
        private Mock<IOrderService> _orderService;

        [SetUp]
        public void SetUp()
        {
            _orderService = new Mock<IOrderService>();

            _basketController = new BasketController(
                orderService: _orderService.Object,
                orderDetailsService: null,
                iBoxPaymentResultBuilder: null,
                gameService: null,
                paymentService: null,
                emailService: null,
                smsService: null);
        }

        [Test]
        public void ShouldChangeGameQuantityAtBasket()
        {
            var model = new ChangeGameQuantityViewModel
            {
                Id = 1,
                Quantity = 3
            };
            var sessionItems = new SessionStateItemCollection
            {
                [Basket] = new List<Purchase>
                {
                    new Purchase(model.Id, It.IsAny<string>(), It.IsAny<decimal>(), quantity: 1)
                }
            };
            _basketController.ControllerContext = new FakeControllerContext(_basketController, sessionItems);

            _orderService
                .Setup(m => m.GetCurrentOrderForCustomer(It.IsAny<string>()))
                .Returns(new OrderDto { Id = 1 });

            _orderService
                .Setup(m => m.Update(It.IsAny<int>(), It.IsAny<Dictionary<int, short>>(), It.IsAny<string>()));

            _basketController.ChangeGameQuantity(model);

            var purchases = _basketController.Session[Basket] as List<Purchase>;
            Assert.IsNotNull(purchases);
            Assert.AreEqual(model.Quantity, purchases.First().Quantity);
        }
    }
}
