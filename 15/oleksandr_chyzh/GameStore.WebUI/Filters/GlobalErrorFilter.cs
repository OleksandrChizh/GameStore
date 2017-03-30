using System.Threading;
using System.Web.Mvc;
using GameStore.Services.Interfaces.Exceptions;
using GameStore.WebUI.Exceptions;

namespace GameStore.WebUI.Filters
{
    public class GlobalErrorFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext filterContext)
        {
            string currentCulture = Thread.CurrentThread.CurrentCulture.Name.Substring(0, 2);

            if (filterContext.Exception.GetType() == typeof(OrderPaidException))
            {
                filterContext.Result = new RedirectResult($"/{currentCulture}/Order/OrderPaid");
            }
            else
            {
                filterContext.Result = filterContext.Exception.GetType() == typeof(BasketEmptyException) ? new RedirectResult($"/{currentCulture}/Basket") : new RedirectResult("/Error/ErrorPage");
            }

            filterContext.ExceptionHandled = true;
        }
    }
}