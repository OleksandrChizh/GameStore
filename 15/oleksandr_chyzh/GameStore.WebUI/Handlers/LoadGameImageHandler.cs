using System.Web;
using System.Web.Routing;

namespace GameStore.WebUI.Handlers
{
    public class LoadGameImageHandler : IRouteHandler
    {
        public IHttpHandler GetHttpHandler(RequestContext requestContext)
        {
            return new AsyncGameImageLoader();
        }
    }
}