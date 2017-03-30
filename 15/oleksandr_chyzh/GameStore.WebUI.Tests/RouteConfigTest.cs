using System.Web;
using System.Web.Routing;
using Moq;
using NUnit.Framework;

namespace GameStore.WebUI.Tests
{
    [TestFixture]
    public class RouteConfigTest
    {
        private Mock<HttpContextBase> _mock;
        private RouteCollection _routes;

        [SetUp]
        public void SetUp()
        {
            _routes = new RouteCollection();
            RouteConfig.RegisterRoutes(_routes);
            _mock = new Mock<HttpContextBase>();
        }

        [Test]
        public void GetAllGamesUrlTest()
        {
            _mock.Setup(m => m.Request.AppRelativeCurrentExecutionFilePath).Returns("~/en/games");

            var routeData = _routes.GetRouteData(_mock.Object);

            Assert.IsNotNull(routeData);
            Assert.AreEqual("Game", routeData.Values["Controller"]);
            Assert.AreEqual("GetAll", routeData.Values["Action"]);
        }

        [Test]
        public void GetGameByKeyUrlTest()
        {
            _mock.Setup(m => m.Request.AppRelativeCurrentExecutionFilePath).Returns("~/en/game/gamekey");

            var routeData = _routes.GetRouteData(_mock.Object);

            Assert.IsNotNull(routeData);
            Assert.AreEqual("Game", routeData.Values["Controller"]);
            Assert.AreEqual("GetGameByKey", routeData.Values["Action"]);
        }

        [Test]
        public void DownloadGameUrlTest()
        {
            _mock.Setup(m => m.Request.AppRelativeCurrentExecutionFilePath).Returns("~/en/game/gamekey/download");

            var routeData = _routes.GetRouteData(_mock.Object);

            Assert.IsNotNull(routeData);
            Assert.AreEqual("Game", routeData.Values["Controller"]);
            Assert.AreEqual("download", routeData.Values["Action"]);
        }

        [Test]
        public void RemoveGameUrlTest()
        {
            _mock.Setup(m => m.Request.AppRelativeCurrentExecutionFilePath).Returns("~/en/games/remove");

            var routeData = _routes.GetRouteData(_mock.Object);

            Assert.IsNotNull(routeData);
            Assert.AreEqual("Game", routeData.Values["Controller"]);
            Assert.AreEqual("remove", routeData.Values["Action"]);
        }

        [Test]
        public void NewGameUrlTest()
        {
            _mock.Setup(m => m.Request.AppRelativeCurrentExecutionFilePath).Returns("~/en/games/new");

            var routeData = _routes.GetRouteData(_mock.Object);

            Assert.IsNotNull(routeData);
            Assert.AreEqual("Game", routeData.Values["Controller"]);
            Assert.AreEqual("new", routeData.Values["Action"]);
        }

        [Test]
        public void UpdateGameUrlTest()
        {
            _mock.Setup(m => m.Request.AppRelativeCurrentExecutionFilePath).Returns("~/en/games/update");

            var routeData = _routes.GetRouteData(_mock.Object);

            Assert.IsNotNull(routeData);
            Assert.AreEqual("Game", routeData.Values["Controller"]);
            Assert.AreEqual("update", routeData.Values["Action"]);
        }

        [Test]
        public void GetAllCommentsUrlTest()
        {
            _mock.Setup(m => m.Request.AppRelativeCurrentExecutionFilePath).Returns("~/en/game/gamekey/comments");

            var routeData = _routes.GetRouteData(_mock.Object);

            Assert.IsNotNull(routeData);
            Assert.AreEqual("Comment", routeData.Values["Controller"]);
            Assert.AreEqual("comments", routeData.Values["Action"]);
        }

        [Test]
        public void NewCommentUrlTest()
        {
            _mock.Setup(m => m.Request.AppRelativeCurrentExecutionFilePath).Returns("~/en/game/gamekey/newcomment");

            var routeData = _routes.GetRouteData(_mock.Object);

            Assert.IsNotNull(routeData);
            Assert.AreEqual("Comment", routeData.Values["Controller"]);
            Assert.AreEqual("newcomment", routeData.Values["Action"]);
        }
    }
}
