using System.Web.Mvc;
using GameStore.WebUI.ViewModels.Error;
using Resources;

namespace GameStore.WebUI.Controllers
{
    public class ErrorController : BaseController
    {
        [HttpGet]
        public ActionResult ErrorMessage(string message)
        {
            return View(new ErrorViewModel { Message = message });
        }

        [HttpGet]
        public ActionResult ErrorPage()
        {
            return Content(Resource.ErrorOccured);
        }

        [HttpGet]
        public ActionResult NotFound()
        {
            return Content(Resource.PageNotFound);
        }

        [HttpGet]
        public ActionResult Forbidden()
        {
            if (!Request.IsAuthenticated)
            {
                return RedirectToAction("Login", "Authorization");
            }

            return View();
        }
    }
}