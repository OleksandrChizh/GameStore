using System.Web.Mvc;

namespace GameStore.WebUI.PaymentStrategy
{
    public class VisaPayment : IPaymentStrategy
    {
        private readonly string _url;

        public VisaPayment(string url)
        {
            _url = url;
        }

        public ActionResult GetActionResult()
        {
            return new RedirectResult(_url);
        }
    }
}