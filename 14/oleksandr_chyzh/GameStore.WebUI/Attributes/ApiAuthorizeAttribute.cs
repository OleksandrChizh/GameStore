using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Script.Serialization;
using System.Web.Security;
using GameStore.WebUI.Authorization;

namespace GameStore.WebUI.Attributes
{
    public class ApiAuthorizeAttribute : AuthorizeAttribute
    {
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            SetUser(actionContext);

            if (!IsUserHaveRoles(Roles))
            {
                actionContext.Response = new HttpResponseMessage(HttpStatusCode.Forbidden);
            }
        }

        private void SetUser(HttpActionContext actionContext)
        {
            const string authorizationKey = "Authorization";

            var headers = actionContext.Request.Headers;

            if (headers.Any(p => p.Key == authorizationKey))
            {
                var encryptedTicket = headers.Single(p => p.Key == authorizationKey).Value;
                FormsAuthenticationTicket authTicket;

                try
                {
                    authTicket = FormsAuthentication.Decrypt(encryptedTicket.First());
                }
                catch (ArgumentException)
                {
                    HttpContext.Current.User = new UserPrincipal();
                    return;
                }

                var serializer = new JavaScriptSerializer();
                UserSerializeModel serializeModel = serializer.Deserialize<UserSerializeModel>(authTicket.UserData);

                var newUser = new UserPrincipal(
                    authTicket.Name,
                    serializeModel.Id,
                    serializeModel.BanExpiryDate,
                    serializeModel.Roles);

                HttpContext.Current.User = newUser;
            }
            else
            {
                HttpContext.Current.User = new UserPrincipal();
            }
        }

        private bool IsUserHaveRoles(string roles)
        {
            string[] rolesArray = GetRolesArray(roles);

            return rolesArray.Length == 0 || rolesArray.Any(r => HttpContext.Current.User.IsInRole(r));
        }

        private string[] GetRolesArray(string roles)
        {
            string[] rolesArray = { };

            if (!string.IsNullOrEmpty(roles))
            {
                rolesArray = roles.Split(',');

                for (int i = 0; i < rolesArray.Length; i++)
                {
                    rolesArray[i] = rolesArray[i].Trim();
                }
            }

            return rolesArray;
        }
    }
}