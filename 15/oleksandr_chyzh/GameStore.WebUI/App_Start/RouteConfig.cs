using System.Web.Mvc;
using System.Web.Routing;
using GameStore.WebUI.Handlers;

namespace GameStore.WebUI
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Basket",
                url: "{lang}/basket",
                defaults: new { controller = "Basket", action = "GetCurrentOrderDetails", lang = "ru" },
                constraints: new { lang = @"ru|en" });

            routes.MapRoute(
                name: "History",
                url: "{lang}/orders/{action}",
                defaults: new { controller = "Order", action = "Orders", lang = "ru" },
                constraints: new { lang = @"ru|en", action = @"History|Orders" });

            routes.MapRoute(
                name: "Order",
                url: "{lang}/order",
                defaults: new { controller = "Basket", action = "MakeOrder", lang = "ru" },
                constraints: new { lang = @"ru|en" });

            routes.MapRoute(
                name: "AllGames",
                url: "{lang}/games/{action}/{gameKey}",
                defaults: new { controller = "Game", action = "GetAll", gameKey = UrlParameter.Optional, lang = "ru" },
                constraints: new { gameKey = string.Empty, lang = @"ru|en" });

            routes.MapRoute(
                name: "Comment",
                url: "{lang}/game/{gamekey}/{action}",
                defaults: new { controller = "Comment", lang = "ru" },
                constraints: new { action = @"\w*Comment\w*", lang = @"ru|en" });

            routes.MapRoute(
                name: "GameByKey",
                url: "{lang}/game/{gamekey}/{action}",
                defaults: new { controller = "Game", action = "GetGameByKey", lang = "ru" },
                constraints: new { lang = @"ru|en" });

            routes.MapRoute(
                name: "Publisher",
                url: "{lang}/publisher/{action}",
                defaults: new { controller = "Publisher", lang = "ru" },
                constraints: new { action = @"New|GetAll|DeleteConfirmed", lang = @"ru|en" });

            routes.MapRoute(
                name: "PublisherByCompanyName",
                url: "{lang}/publisher/{companyName}",
                defaults: new { controller = "Publisher", action = "GetPublisherByCompanyName", lang = "ru" },
                constraints: new { action = @"GetPublisherByCompanyName", lang = @"ru|en" });

            routes.MapRoute(
                name: "Publishers",
                url: "{lang}/publisher/{action}/{companyName}",
                defaults: new { controller = "Publisher", lang = "ru" },
                constraints: new { lang = @"ru|en" });

            routes.MapRoute(
                name: "StartPage",
                url: "{lang}/{controller}/{action}/{id}",
                defaults: new { controller = "Game", action = "GetAll", id = UrlParameter.Optional, lang = "ru" },
                constraints: new { lang = @"ru|en" });

            routes.MapRoute(
                name: "Default",
                url: "{lang}/{controller}/{action}/{id}",
                defaults: new { id = UrlParameter.Optional, lang = "ru" },
                constraints: new { lang = @"ru|en" });

            routes.Add(new Route("games/{gameKey}/httpAsyncLoad", new LoadGameImageHandler()));
        }
    }
}
