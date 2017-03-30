using System.Web.Mvc;
using NLog;

namespace GameStore.WebUI.Attributes
{ 
    public class IpAdressLoggerAttribute : ActionFilterAttribute
    {
        private readonly ILogger _logger = LogManager.GetLogger("IpAddressLogger");

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var ipAddress = filterContext.HttpContext.Profile.UserName;
            _logger.Info($"Ip address: {ipAddress}, controller: {filterContext.RouteData.Values["Controller"]}, action: {filterContext.RouteData.Values["action"]}");
        }
    }
}