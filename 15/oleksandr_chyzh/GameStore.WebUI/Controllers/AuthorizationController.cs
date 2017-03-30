using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Web.Security;
using GameStore.Services.Dto;
using GameStore.Services.Interfaces;
using GameStore.WebUI.Attributes;
using GameStore.WebUI.Authorization;
using GameStore.WebUI.ViewModels.User;

namespace GameStore.WebUI.Controllers
{
    [ErrorLogger]
    [MvcAuthorise(Roles = "Guest")]
    public class AuthorizationController : BaseController
    {
        private const string Basket = "BASKET";

        private readonly IUserService _userService;

        public AuthorizationController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        [OverrideAuthorization]
        [MvcAuthorise(ForbiddenRoles = "Guest")]
        public ViewResult ManageAccount()
        {
            UserDto user = _userService.Get((User as IUserPrincipal).Id);
            return View(new UpdateManagerViewModel { Type = user.Type });
        }

        [HttpPost]
        [OverrideAuthorization]
        [MvcAuthorise(ForbiddenRoles = "Guest")]
        public ActionResult ManageAccount(UpdateManagerViewModel model)
        {
            _userService.UpdateNotificationMethod((User as IUserPrincipal).Id, model.Type);
            return RedirectToAction("ManageAccount");
        }

        [HttpGet]
        public ViewResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            UserDto user = _userService.Register(model.Email, model.Password, new List<string> { "User" });
            AddAuthenticationTicketToCookie(user);

            return RedirectToAction("GetAll", "Game");
        }

        [HttpGet]
        public ViewResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            UserDto user = _userService.Get(model.Email, model.Password);
            AddAuthenticationTicketToCookie(user);

            return RedirectToAction("GetAll", "Game");
        }

        [HttpGet]
        [OverrideAuthorization]
        [MvcAuthorise(ForbiddenRoles = "Guest")]
        public ActionResult Logout()
        {
            var cookie = Response.Cookies[FormsAuthentication.FormsCookieName];
            cookie.Expires = DateTime.UtcNow.AddDays(-1);

            Response.Cookies.Add(cookie);

            Session[Basket] = null;

            return RedirectToAction("Login");
        }

        private void AddAuthenticationTicketToCookie(UserDto user)
        {
            var serializeModel = new UserSerializeModel
            {
                Id = user.Id,
                UserName = user.UserName,
                BanExpiryDate = user.BanExpiryDate,
                Roles = user.Roles
            };

            var serializer = new JavaScriptSerializer();
            string userData = serializer.Serialize(serializeModel);

            const int ticketVersion = 1;
            const bool isPersistent = false;
            var authenticationTicket = new FormsAuthenticationTicket(
                ticketVersion,
                user.UserName,
                DateTime.UtcNow,
                DateTime.UtcNow.AddMinutes(15),
                isPersistent,
                userData);

            string encryptedTicket = FormsAuthentication.Encrypt(authenticationTicket);
            var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
            Response.Cookies.Add(cookie);
        }
    }
}