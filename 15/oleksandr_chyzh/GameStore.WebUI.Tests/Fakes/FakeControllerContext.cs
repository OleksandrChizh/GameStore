using System.Web.Mvc;
using System.Web.Routing;
using System.Web.SessionState;

namespace GameStore.WebUI.Tests.Fakes
{
    public class FakeControllerContext : ControllerContext
    {
        public FakeControllerContext(IController controller, SessionStateItemCollection sessionItems)
            : base(new FakeHttpContext(sessionItems), new RouteData(), controller as ControllerBase)
        { }
    }
}