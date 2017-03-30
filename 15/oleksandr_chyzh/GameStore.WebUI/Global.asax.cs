using System;
using System.Data.Entity;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Script.Serialization;
using System.Web.Security;
using GameStore.Infrastructure.EFDataAccess;
using GameStore.Infrastructure.EFDataAccess.Migrations;
using GameStore.WebUI.Authorization;

namespace GameStore.WebUI
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<ApplicationContext, Configuration>());

            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);           
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        protected void Application_PostAuthenticateRequest(object sender, EventArgs e)
        {
            HttpCookie authCookie = Request.Cookies[FormsAuthentication.FormsCookieName];

            if (authCookie == null)
            {
                HttpContext.Current.User = new UserPrincipal();
            }
            else
            {
                FormsAuthenticationTicket authTicket;

                try
                {
                    authTicket = FormsAuthentication.Decrypt(authCookie.Value);
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
        }
    }
}