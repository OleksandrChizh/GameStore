using System.Linq;
using System.Web.Mvc;
using GameStore.Services.Interfaces;
using GameStore.WebUI.Attributes;
using GameStore.WebUI.Utils;
using GameStore.WebUI.ViewModels.PlatformType;

namespace GameStore.WebUI.Controllers
{
    [ErrorLogger]
    [PerfomanceCalculator]
    [MvcAuthorise(Roles = "Manager")]
    public class PlatformTypeController : BaseController
    {
        private readonly IPlatformTypeService _platformTypeService;

        public PlatformTypeController(IPlatformTypeService platformTypeService)
        {
            _platformTypeService = platformTypeService;
        }

        [HttpGet]
        public ViewResult GetAll()
        {
            return View(_platformTypeService.GetAllPlatformTypes().Select(pt => pt.ToViewModel()).ToList());
        }

        [HttpGet]
        public ViewResult New()
        {
            return View();
        }

        [HttpPost]
        public ActionResult New(PlatformTypeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            _platformTypeService.Create(model.Type);

            return RedirectToAction("GetAll");
        }

        [HttpGet]
        public ViewResult Delete(int id)
        {
            return View(_platformTypeService.Get(id).ToViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(PlatformTypeViewModel model)
        {
            _platformTypeService.Delete(model.Id);

            return RedirectToAction("GetAll");
        }
    }
}