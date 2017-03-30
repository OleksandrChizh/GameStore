using System.Net;
using System.Net.Http;
using System.Web.Http;
using GameStore.Services.Dto;
using GameStore.Services.Interfaces;
using GameStore.WebUI.Controllers.Api;
using GameStore.WebUI.Models.Comment;
using Moq;
using NUnit.Framework;

namespace GameStore.WebUI.Tests
{
    [TestFixture]
    public class CommentsControllerTest
    {
        private CommentsController _commentsController;
        private Mock<ICommentService> _commentService;
        private Mock<IGameService> _gameService;

        [SetUp]
        public void SetUp()
        {
            _commentService = new Mock<ICommentService>();
            _gameService = new Mock<IGameService>();
            _commentsController = new CommentsController(_commentService.Object, _gameService.Object)
            {
                Request = new HttpRequestMessage()
            };
            _commentsController.Request.SetConfiguration(new HttpConfiguration());
        }

        [Test]
        public void ShouldCreateComment()
        {
            var model = new CreateCommentModel
            {
                Body = "Body",
                IsQuote = true,
                GameKey = "Key",
                ParentCommentId = 1
            };

            _gameService
                .Setup(m => m.GetGameByKey(It.IsAny<string>()))
                .Returns(new GameDto());

            _commentService
                .Setup(m => m.Create(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<bool>()));

            HttpResponseMessage response = _commentsController.Post(It.IsAny<int>(), model);

            _gameService.Verify(m => m.GetGameByKey(It.IsAny<string>()), Times.AtLeastOnce);
            _commentService.Verify(m => m.Create(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<bool>()), Times.AtLeastOnce);
            Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);
        }
    }
}
