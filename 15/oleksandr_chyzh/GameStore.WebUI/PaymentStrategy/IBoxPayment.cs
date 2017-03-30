using System.Web.Mvc;
using GameStore.WebUI.ActionResults;
using GameStore.WebUI.PaymentStrategy.ResultBuilders.Interfaces;

namespace GameStore.WebUI.PaymentStrategy
{
    public class IBoxPayment : IPaymentStrategy
    {
        private readonly string _customerId;
        private readonly int _orderId;
        private readonly decimal _sum;
        private readonly IIBoxPaymentResultBuilder _builder;

        public IBoxPayment(string customerId, int orderId, decimal sum, IIBoxPaymentResultBuilder builder)
        {
            _customerId = customerId;
            _orderId = orderId;
            _sum = sum;
            _builder = builder;
        }

        public ActionResult GetActionResult()
        {
            return new CustomHtmlResult(_builder.GetResult(_customerId, _orderId, _sum), "IBox");
        }
    }
}