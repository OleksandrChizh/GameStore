using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using GameStore.Domain.Core.Models;
using GameStore.Domain.Interfaces;
using GameStore.Services.Dto;
using GameStore.Services.Dto.Utils;
using GameStore.Services.Interfaces;
using GameStore.Services.Interfaces.Exceptions;
using Moq;
using NUnit.Framework;
using Assert = NUnit.Framework.Assert;

namespace GameStore.Infrastructure.Services.Tests
{
    [TestFixture]
    public class CommentServiceTest
    {
        private Mock<IUnitOfWork> _mock;
        private ICommentService _service;

        [SetUp]
        public void SetUp()
        {
            _mock = new Mock<IUnitOfWork>();
            _service = new CommentService(_mock.Object);
        }      

        [Test]
        public void ShouldThrowException_ForCreatingComment_WhichHasParentComment_NotContainedInSelectedGame()
        {
            var game = new Game()
            {
                Comments = new List<Comment>()
            };
            _mock.Setup(m => m.Games.Get(It.IsAny<int>()))
                 .Returns(game);

            Assert.That(() => _service.Create("name", "body", gameId: 1, parentCommentId: 1, isQuote: false), Throws.TypeOf<EntityNotFoundException>(), "Parent comment not contains in this game");
        }

        [Test]
        public void ShouldCreateComment()
        {
            const int exceptedCommendId = 1;
            _mock.Setup(m => m.Games.Get(It.IsAny<int>()))
                 .Returns(new Game());
            _mock.Setup(m => m.Comments.Create(It.IsAny<Comment>()))
                .Callback(new Action<Comment>(comment => comment.Id = exceptedCommendId));
            _mock.Setup(m => m.Save());
            
            int id = _service.Create(
                "name", 
                "body", 
                parentCommentId: null, 
                gameId: 1, 
                isQuote: false);

            _mock.Verify(m => m.Comments.Create(It.IsAny<Comment>()), Times.AtLeastOnce);
            _mock.Verify(m => m.Save(), Times.AtLeastOnce);
            Assert.AreEqual(exceptedCommendId, id);
        }

        [Test]
        public void ShouldDeleteCommentById()
        {
            _mock.Setup(m => m.Comments.Get(It.IsAny<int>()))
                .Returns(new Comment());
            _mock.Setup(m => m.Comments.Delete(It.IsAny<Comment>()));
            _mock.Setup(m => m.Save());

            _service.Delete(commentId: 1);

            _mock.Verify(m => m.Comments.Delete(It.IsAny<Comment>()), Times.AtLeastOnce);
            _mock.Verify(m => m.Save(), Times.AtLeastOnce);
        }

        [Test]
        public void ShouldThrowException_WhenGameNotFound_ForSelectedKey_ToGetAllCommentsForGame()
        {
            _mock.Setup(m => m.Games.Find(It.IsAny<IPipeline<Game>>()))
                 .Returns(new List<Game>());
        
            Assert.That(() => _service.GetAllCommentsByGameKey("key"), Throws.TypeOf<EntityNotFoundException>());
        }

        [Test]
        public void ShouldGetAllComments_ByGameKey()
        {
            var game = new Game { Key = "key" };
            var comments = new List<Comment>
            {
                new Comment("name", "body", parentComment: null, game: game, isQuote: false),
                new Comment("name", "body", parentComment: null, game: game, isQuote: false),
                new Comment("name", "body", parentComment: null, game: game, isQuote: false),
            };
            int exceptedCommentsCount = comments.Count;
            game.Comments = comments;    
            _mock.Setup(m => m.Games.SingleOrDefault(It.IsAny<Expression<Func<Game, bool>>>()))
                 .Returns(game);

            List<CommentDto> commentDtos = _service.GetAllCommentsByGameKey("key").ToList();

            Assert.AreEqual(exceptedCommentsCount, commentDtos.Count);
        }

        [Test]
        public void ShouldGetComment_ById()
        {
            var comment = new Comment("Name", "Body", new Comment(), new Game(), isQuote: false);           
            _mock.Setup(m => m.Comments.Get(It.IsAny<int>()))
                 .Returns(comment);

            CommentDto commentDto = _service.Get(It.IsAny<int>());

            Assert.IsTrue(commentDto.Equals(comment.ToDto()));
        }

        [Test]
        public void ShouldThrowException_ForGettingNonexistentComment_ById()
        {
            _mock.Setup(m => m.Comments.Get(It.IsAny<int>()))
                 .Returns((Comment)null);

            Assert.That(() => _service.Get(It.IsAny<int>()), Throws.TypeOf<EntityNotFoundException>());
        }
    }
}
