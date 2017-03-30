using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace GameStore.WebUI.Controllers
{
    public class CultureController : Controller
    {
        private readonly List<string> cultures = new List<string>() { "ru", "en" };

        [HttpPost]
        public ActionResult ChangeCulture(string lang)
        {
            string url = Request.UrlReferrer.AbsolutePath;

            if (!cultures.Contains(lang))
            {
                lang = "ru";
            }

            List<string> segments = url.Split('/').ToList();
            segments.RemoveAt(0);
            segments[0] = lang;

            string returnUrl = string.Join("/", segments).Insert(0, "/");

            return Redirect(returnUrl);
        }
    }
}