using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using GameStore.Services.Interfaces;
using GameStore.WebUI.Authorization;
using GameStore.WebUI.Controllers.Api;
using GameStore.WebUI.Models.Order;
using Moq;
using NUnit.Framework;

namespace GameStore.WebUI.Tests
{
    [TestFixture]
    public class OrdersControllerTest
    {
        private OrdersController _ordersController;
        private Mock<IOrderService> _orderService;

        [SetUp]
        public void SetUp()
        {
            _orderService = new Mock<IOrderService>();
            _ordersController = new OrdersController(_orderService.Object)
            {
                User = new UserPrincipal("email", "id", DateTime.UtcNow, new List<string> { "manager" }),
                Request = new HttpRequestMessage()
            };
            _ordersController.Request.SetConfiguration(new HttpConfiguration());
        }

        [Test]
        public void ShouldUpdateOrder()
        {
            var model = new UpdateOrder
            {
                OrderId = 1,
                ProductsQuantities = new List<ProductQuantity> { new ProductQuantity { ProductId = 1, Quantity = 1 } }
            };

            _orderService.Setup(m => m.Update(It.IsAny<int>(), It.IsAny<IDictionary<int, short>>(), It.IsAny<string>()));

            HttpResponseMessage response = _ordersController.Post(model);

            _orderService.Verify(m => m.Update(It.IsAny<int>(), It.IsAny<IDictionary<int, short>>(), It.IsAny<string>()), Times.AtLeastOnce);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }
    }
}
