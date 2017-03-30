using System.Web.Mvc;
using NLog;

namespace GameStore.WebUI.Attributes
{
    public class EventLoggerAttribute : ActionFilterAttribute
    {
        private readonly ILogger _logger = LogManager.GetLogger("EventLogger");

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            _logger.Info($"Controller: {filterContext.RouteData.Values["Controller"]}, action: {filterContext.RouteData.Values["action"]}");
        }
    }
}