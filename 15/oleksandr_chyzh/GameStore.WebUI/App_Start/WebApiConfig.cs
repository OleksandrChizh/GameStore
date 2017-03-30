using System.Net.Http.Formatting;
using System.Web.Http;
using System.Web.Http.Routing.Constraints;

namespace GameStore.WebUI
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "CommentsApi",
                routeTemplate: "api/{lang}/games/{gameId}/comments/{id}",
                defaults: new { id = RouteParameter.Optional, controller = "Comments" },
                constraints: new
                {
                    controller = new RegexRouteConstraint("Comments"),
                    lang = new RegexRouteConstraint("ru|en")
                });

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{lang}/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional },
                constraints: new
                {
                    controller = new RegexRouteConstraint("Games|Publishers|Genres|Accounts|Orders"),
                    lang = new RegexRouteConstraint("ru|en")
                });

            config.Formatters.Clear();
            config.Formatters.Add(new JsonMediaTypeFormatter());
            config.Formatters.Add(new XmlMediaTypeFormatter());

            config.Formatters.JsonFormatter.MediaTypeMappings.Add(new QueryStringMapping("type", "json", "application/json"));
            config.Formatters.XmlFormatter.MediaTypeMappings.Add(new QueryStringMapping("type", "xml", "application/xml"));
        }
    }
}