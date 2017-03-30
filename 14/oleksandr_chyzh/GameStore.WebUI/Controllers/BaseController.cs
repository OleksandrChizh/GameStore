using System.Globalization;
using System.Threading;
using System.Web.Mvc;
using System.Web.Routing;

namespace GameStore.WebUI.Controllers
{
    public class BaseController : Controller
    {
        private static string lastController = string.Empty;

        protected override void Initialize(RequestContext requestContext)
        {
            const string langKey = "lang";
            const string controllerKey = "controller";

            string cultureName = "ru";

            // Fixs error when request sent from 401, 403, 404 by path defined at web.config and culture changing don't work
            string controller = (string)requestContext.RouteData.Values[controllerKey];
            if (controller == "Error")
            {
                if (lastController == controller)
                {
                    lastController = string.Empty;
                }
                else
                {
                    var query = requestContext.HttpContext.Request.Url.Query;
                    if (query.Length > 4)
                    {
                        string httpStatus = query.Substring(1, 3);
                        int statusNumber;
                        int.TryParse(httpStatus, out statusNumber);

                        if (statusNumber == 401 ||
                            statusNumber == 403 ||
                            statusNumber == 404)
                        {
                            lastController = controller;
                            base.Initialize(requestContext);
                            return;
                        }
                    }
                }
            }

            if (!requestContext.RouteData.Values.ContainsKey(langKey))
            {
                requestContext.RouteData.Values[langKey] = cultureName;
            }
            else
            {
                cultureName = requestContext.RouteData.Values[langKey].ToString();
            }          

            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(cultureName);
            Thread.CurrentThread.CurrentUICulture = CultureInfo.CreateSpecificCulture(cultureName);

            base.Initialize(requestContext);
        }
    }
}