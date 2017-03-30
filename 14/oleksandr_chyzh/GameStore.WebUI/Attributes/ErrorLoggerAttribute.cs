using System.Web.Mvc;
using NLog;

namespace GameStore.WebUI.Attributes
{
    public class ErrorLoggerAttribute : HandleErrorAttribute
    {
        private readonly ILogger _logger = LogManager.GetLogger("ErrorLogger");

        public override void OnException(ExceptionContext filterContext)
        {
            _logger.Error($"Controller: {filterContext.RouteData.Values["Controller"]}, action: {filterContext.RouteData.Values["action"]}, " +
                          $"exception: {filterContext.Exception}");
        }
    }
}