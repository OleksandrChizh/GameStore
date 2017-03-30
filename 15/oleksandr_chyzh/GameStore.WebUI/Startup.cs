using System.Web.Http;
using System.Web.Routing;
using GameStore.Infrastructure.DataAccess.Interfaces;
using GameStore.WebUI;
using Hangfire;
using Microsoft.Owin;
using Ninject;
using Ninject.Web.Common.OwinHost;
using Ninject.Web.WebApi.OwinHost;
using Owin;
using GlobalConfiguration = Hangfire.GlobalConfiguration;

[assembly: OwinStartup(typeof(Startup))]

namespace GameStore.WebUI
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var webApiConfig = new HttpConfiguration();
            WebApiConfig.Register(webApiConfig);

            System.Web.Http.GlobalConfiguration.Configure(WebApiConfig.Register);
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            var kernel = new StandardKernel();
            DependenciesConfiguration.RegisterDependencies(kernel);
            app.UseNinjectMiddleware(() => kernel).UseNinjectWebApi(webApiConfig);

            GlobalConfiguration.Configuration.UseSqlServerStorage("DbGameStore");
            GlobalConfiguration.Configuration.UseNinjectActivator(kernel);
            app.UseHangfireServer();

            RecurringJob.AddOrUpdate<IDatabaseSynchronizer>(s => s.Synchronize(), Cron.Minutely);
        }
    }
}
