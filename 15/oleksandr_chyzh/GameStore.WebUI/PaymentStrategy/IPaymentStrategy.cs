using System.Web.Mvc;

namespace GameStore.WebUI.PaymentStrategy
{
    public interface IPaymentStrategy
    {
        ActionResult GetActionResult();
    }
}
