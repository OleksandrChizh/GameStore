using System.Collections.Generic;
using GameStore.Services.Dto;
using GameStore.Services.Interfaces;
using GameStore.WebUI.Controllers;
using GameStore.WebUI.ViewModels.Comment;
using Moq;
using NUnit.Framework;

namespace GameStore.WebUI.Tests
{
    [TestFixture]
    public class CommentControllerTest
    {
        private Mock<ICommentService> _commentServiceMock;
        private Mock<IGameService> _gameServiceMock;

        private CommentController _commentController;

        [SetUp]
        public void SetUp()
        {
            _commentServiceMock = new Mock<ICommentService>();
            _gameServiceMock = new Mock<IGameService>();

            _commentController = new CommentController(_commentServiceMock.Object, _gameServiceMock.Object);
        }

        [Test]
        public void ShouldReturnView_WithCommentsForGame()
        {
            var comments = new List<CommentDto>
            {
                new CommentDto(
                    id: 1,
                    name: "name",
                    body: "body",
                    gameId: 1,
                    parentCommentId: null,
                    repliedTo: string.Empty,
                    quote: string.Empty,
                    isQuote: false)
            };
            _commentServiceMock
                .Setup(m => m.GetAllCommentsByGameKey(It.IsAny<string>()))
                .Returns(comments);

            _gameServiceMock
                .Setup(m => m.GetGameByKey(It.IsAny<string>()))
                .Returns(new GameDto { LanguagesNames = new Dictionary<string, string> { { "ru", "name" } } });

            var viewResult = _commentController.GetAllCommentsForGame(It.IsAny<string>());
            var model = viewResult.Model as CommentsViewModel;

            Assert.NotNull(model);
            Assert.NotNull(model.Comments);
            Assert.AreEqual(model.Comments.Count, comments.Count);
        }
    }
}
