using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Script.Serialization;
using System.Web.Security;
using GameStore.Services.Dto;
using GameStore.Services.Interfaces;
using GameStore.WebUI.Attributes;
using GameStore.WebUI.Authorization;
using GameStore.WebUI.ViewModels.User;

namespace GameStore.WebUI.Controllers.Api
{
    [ApiErrorHandler]
    public class AccountsController : BaseApiController
    {
        private readonly IUserService _userService;

        public AccountsController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPut]
        public HttpResponseMessage Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            UserDto user = _userService.Register(model.Email, model.Password, new List<string> { "User" });

            return Request.CreateResponse(HttpStatusCode.OK, GetEncryptedAuthenticationTicket(user));
        }

        [HttpPost]
        public HttpResponseMessage Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            UserDto user = _userService.Get(model.Email, model.Password);

            return Request.CreateResponse(HttpStatusCode.OK, GetEncryptedAuthenticationTicket(user));
        }

        private string GetEncryptedAuthenticationTicket(UserDto user)
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

            return FormsAuthentication.Encrypt(authenticationTicket);
        }
    }
}