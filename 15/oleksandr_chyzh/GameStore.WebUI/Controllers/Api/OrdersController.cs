using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using GameStore.Services.Dto;
using GameStore.Services.Interfaces;
using GameStore.WebUI.Attributes;
using GameStore.WebUI.Authorization;
using GameStore.WebUI.Models.Order;

namespace GameStore.WebUI.Controllers.Api
{
    [ApiErrorHandler]
    [ApiAuthorize(Roles = "User")]
    public class OrdersController : BaseApiController
    {
        private readonly IOrderService _orderService;

        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        public HttpResponseMessage Put()
        {
            var user = User as IUserPrincipal;
            OrderDto order = _orderService.GetCurrentOrderForCustomer(user.Id);

            if (order == null)
            {                
                return Request.CreateResponse(HttpStatusCode.Created, _orderService.Create(user.Id, new List<int>()));
            }

            return Request.CreateResponse(HttpStatusCode.OK, order.Id);
        }

        public HttpResponseMessage Get([FromUri] int id)
        {
            var user = User as IUserPrincipal;
            OrderDto order = _orderService.GetCurrentOrderForCustomer(user.Id);

            if (order.Id != id || user.Id != order.CustomerId)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Incorrect id of current user order");
            }

            OrderDto currentOrder = _orderService.Get(id);

            return Request.CreateResponse(HttpStatusCode.OK, currentOrder);
        }

        public HttpResponseMessage Post([FromBody] UpdateOrder model)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            var pairs = model.ProductsQuantities.ToDictionary(productQuantity => productQuantity.ProductId, productQuantity => productQuantity.Quantity);

            _orderService.Update(model.OrderId, pairs, (User as IUserPrincipal).Id);

            return Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}