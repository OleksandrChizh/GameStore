using System.Web.Mvc;
using GameStore.WebUI.Attributes;
using GameStore.WebUI.Filters;

namespace GameStore.WebUI
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new GlobalErrorFilter());
            filters.Add(new IpAdressLoggerAttribute());
        }
    }
}
