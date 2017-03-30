using System.Globalization;
using System.Web.Mvc;
using GameStore.WebUI.PaymentStrategy.ResultBuilders.Interfaces;
using GameStore.WebUI.Utils;

namespace GameStore.WebUI.PaymentStrategy.ResultBuilders.Implementations
{
    public class IBoxPaymentHtmlResultBuilder : IIBoxPaymentResultBuilder
    {
        public string GetResult(string customerId, int orderId, decimal sum)
        {
            var dl = new TagBuilder("dl");
            dl.InnerHtml += TermDataTag.GetPair("Customer Id", customerId);
            dl.InnerHtml += TermDataTag.GetPair("Order Id", orderId.ToString());
            dl.InnerHtml += TermDataTag.GetPair("Sum", sum.ToString(CultureInfo.InvariantCulture));
            return dl.ToString();
        }
    }
}