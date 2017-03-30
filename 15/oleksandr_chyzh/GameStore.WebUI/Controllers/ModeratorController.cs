using System.Web.Mvc;
using GameStore.Services.Interfaces;
using GameStore.WebUI.Attributes;
using GameStore.WebUI.ViewModels.User;

namespace GameStore.WebUI.Controllers
{
    [MvcAuthorise(Roles = "Moderator")]
    public class ModeratorController : BaseController
    {
        private readonly IUserService _userService;

        public ModeratorController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public ViewResult Ban(string userName)
        {
            var model = new BanUserViewModel { UserName = userName };
            return View(model);
        }

        [HttpPost]
        public ActionResult Ban(BanUserViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            _userService.BanUser(model.UserName, model.Duration);

            return RedirectToAction("GetAll", "Game");
        }
    }
}