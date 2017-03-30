using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using GameStore.Services.Dto;
using GameStore.Services.Interfaces;
using GameStore.WebUI.Attributes;
using GameStore.WebUI.Authorization;
using GameStore.WebUI.Utils;
using GameStore.WebUI.ViewModels.Administrator;
using GameStore.WebUI.ViewModels.User;

namespace GameStore.WebUI.Controllers
{
    [MvcAuthorise(Roles = "Administrator")]    
    public class AdministratorController : BaseController
    {
        private readonly IUserService _userService;
        private readonly IRoleService _roleService;

        public AdministratorController(
            IUserService userService,
            IRoleService roleService)
        {
            _userService = userService;
            _roleService = roleService;
        }

        [HttpGet]
        public ViewResult UpdateUser(string userId)
        {
            UserDto user = _userService.Get(userId);
            UpdateUserViewModel model = user.ToUpdateViewModel();
            model.Roles = new MultiSelectList(_roleService.GetAll());

            return View(model);
        }

        [HttpPost]
        public ActionResult UpdateUser(UpdateUserViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            _userService.Update(model.Id, model.BanExpiryDate, GetValidUserRoles(model.SelectedRoles));

            return RedirectToAction("Users");
        }

        [HttpGet]
        public ViewResult DeleteUser(string userId)
        {
            UserDto user = _userService.Get(userId);
            return View(user.ToViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteUserConfirmed(string userId)
        {
            if (userId != (User as IUserPrincipal).Id)
            {
                _userService.Delete(userId);
            }

            return RedirectToAction("Users");
        }

        [HttpGet]
        public ViewResult Register()
        {
            var model = new AdministratorRegisterViewModel { Roles = new MultiSelectList(_roleService.GetAll()) };
            return View(model);
        }

        [HttpPost]
        public ActionResult Register(AdministratorRegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
           
            _userService.Register(model.Email, model.Password, GetValidUserRoles(model.SelectedRoles));

            return RedirectToAction("Users");
        }

        [HttpGet]
        public ViewResult Users()
        {
            List<UserViewModel> users = _userService.GetAll().Select(u => u.ToViewModel()).ToList();
            return View(users);
        }

        [HttpGet]
        public ViewResult Roles()
        {
            return View(_roleService.GetAll());
        }

        private List<string> GetValidUserRoles(IList<string> roles)
        {
            const string guestRole = "Guest";
            const string userRole = "User";

            var result = roles;

            if (result.Contains(guestRole))
            {
                result.Remove(guestRole);
            }

            if (result.Count == 0)
            {
                result.Add(userRole);
            }

            return result.ToList();
        }
    }
}