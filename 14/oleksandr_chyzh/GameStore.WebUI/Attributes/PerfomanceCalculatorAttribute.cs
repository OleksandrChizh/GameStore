using System.Diagnostics;
using System.Web.Mvc;
using NLog;

namespace GameStore.WebUI.Attributes
{
    public class PerfomanceCalculatorAttribute : ActionFilterAttribute
    {
        private readonly Stopwatch _stopWatch = new Stopwatch();
        private readonly ILogger _logger = LogManager.GetLogger("PerfomanceLogger");

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            _stopWatch.Reset();
            _stopWatch.Start();
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            _stopWatch.Stop();
            long executionTime = _stopWatch.ElapsedMilliseconds;

            _logger.Info($"Controller: {filterContext.RouteData.Values["Controller"]}, action: {filterContext.RouteData.Values["action"]}, perfomance: {executionTime}ms");
        }
    }
}