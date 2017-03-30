using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using GameStore.Services.Dto;
using GameStore.Services.Interfaces;
using GameStore.Services.Interfaces.Utils;
using GameStore.WebUI.Attributes;
using GameStore.WebUI.Utils;
using GameStore.WebUI.ViewModels.Order;

namespace GameStore.WebUI.Controllers
{
    [MvcAuthorise(Roles = "Manager")]
    public class OrderController : BaseController
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet]
        public ViewResult Update(int id)
        {
            return View(_orderService.Get(id).ToShortViewModel());
        }

        [HttpPost]
        public ActionResult Update(ShortOrderViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            _orderService.Update(model.Id, model.OrderDate, model.PayingDate, model.ShippedDate);

            return RedirectToAction("Orders");
        }

        [HttpGet]
        public ViewResult Orders(HistoryViewModel model)
        {
            var currentDate = DateTime.UtcNow;
            var defaultDate = default(DateTime);

            if (!ModelState.IsValid)
            {
                model.Orders = GetOrders(currentDate.AddDays(-30), currentDate);
                return View(model);
            }

            if (model.DateFrom == defaultDate)
            {
                model.DateFrom = currentDate.AddDays(-30);
            }

            if (model.DateTo == defaultDate)
            {
                model.DateTo = currentDate;
            }

            model.Orders = GetOrders(model.DateFrom, model.DateTo);

            return View(model);
        }

        [HttpGet]
        public ViewResult History(HistoryViewModel model)
        {
            var defaultDate = default(DateTime);

            if (!ModelState.IsValid)
            {
                model.Orders = GetOrders(defaultDate, DateTime.UtcNow.AddDays(-30));
                return View(model);
            }

            if (model.DateTo == defaultDate)
            {
                model.DateTo = DateTime.UtcNow.AddDays(-30);
            }

            model.Orders = GetOrders(model.DateFrom, model.DateTo);

            return View(model);
        }

        [HttpGet]
        public ActionResult Deliver(int id)
        {
            _orderService.Deliver(id);
            return View();
        }

        [HttpGet]
        public ActionResult Pay(int id)
        {
            _orderService.Pay(id);
            return View("OrderPaid");
        }

        private IList<ShortOrderViewModel> GetOrders(DateTime from, DateTime to)
        {
            OrdersFilterAttributes attributes = new OrdersFilterAttributes
            {
                From = from,
                To = to
            };

            IEnumerable<OrderDto> ordersDtos = _orderService.GetOrders(attributes);

            return ordersDtos.Select(o => o.ToShortViewModel()).ToList();
        }
    }
}