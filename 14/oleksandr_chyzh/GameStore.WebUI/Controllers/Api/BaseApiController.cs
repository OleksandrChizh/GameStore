using System.Globalization;
using System.Threading;
using System.Web.Http;
using System.Web.Http.Controllers;

namespace GameStore.WebUI.Controllers.Api
{
    public class BaseApiController : ApiController
    {
        protected override void Initialize(HttpControllerContext controllerContext)
        {
            const string langKey = "lang";

            string cultureName = "ru";            

            if (!controllerContext.RouteData.Values.ContainsKey(langKey))
            {
                controllerContext.RouteData.Values[langKey] = cultureName;
            }
            else
            {
                cultureName = controllerContext.RouteData.Values[langKey].ToString();
            }

            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(cultureName);
            Thread.CurrentThread.CurrentUICulture = CultureInfo.CreateSpecificCulture(cultureName);

            base.Initialize(controllerContext);
        }
    }
}