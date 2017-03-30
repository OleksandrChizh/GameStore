using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using GameStore.Services.Interfaces;
using GameStore.WebUI.Attributes;
using GameStore.WebUI.Utils;
using GameStore.WebUI.ViewModels.Publisher;

namespace GameStore.WebUI.Controllers
{
    [ErrorLogger]
    [PerfomanceCalculator]
    [MvcAuthorise(Roles = "Manager")]
    public class PublisherController : BaseController
    {
        private readonly IPublisherService _publisherService;

        public PublisherController(IPublisherService publisherService)
        {
            _publisherService = publisherService;
        }

        [HttpGet]
        [OverrideAuthorization]
        public ViewResult GetPublisherByCompanyName(string companyName)
        {
            return View(_publisherService.GetPublisherByCompanyName(companyName).ToViewModel());
        }

        [HttpGet]
        public ViewResult GetAll()
        {
            return View(_publisherService.GetAllPublishers().Select(p => p.ToShortViewModel()).ToList());
        }

        [HttpGet]
        public ViewResult Update(string companyName)
        {
            return View(_publisherService.GetPublisherByCompanyName(companyName).ToViewModel());
        }

        [HttpPost]
        public ActionResult Update(PublisherViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            IDictionary<string, string> descriptions = new Dictionary<string, string>();

            descriptions.Add("ru", model.Description);

            if (model.IsContainEnglishTranslation)
            {
                descriptions.Add("en", model.EnglishDescription);
            }

            _publisherService.Update(model.Id, descriptions, model.HomePage);

            return RedirectToAction("GetAll");
        }

        [HttpGet]
        public ViewResult Delete(string companyName)
        {
            return View(_publisherService.GetPublisherByCompanyName(companyName).ToShortViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(ShortPublisherViewModel model)
        {
            _publisherService.Delete(model.Id);

            return RedirectToAction("GetAll");
        }

        [HttpGet]
        [ActionName("New")]
        public ViewResult CreatePublisher()
        {
            return View();
        }

        [HttpPost]
        [EventLogger]
        [ActionName("New")]
        public ActionResult CreatePublisher(PublisherViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            IDictionary<string, string> descriptions = new Dictionary<string, string>();

            descriptions.Add("ru", model.Description);

            if (model.IsContainEnglishTranslation)
            {
                descriptions.Add("en", model.EnglishDescription);
            }

            _publisherService.Create(model.CompanyName, descriptions, model.HomePage);

            return RedirectToAction("GetPublisherByCompanyName", new { companyName = model.CompanyName });
        }
    }
}