using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using GameStore.Domain.Core.Models;
using GameStore.Domain.Interfaces;
using GameStore.Infrastructure.DataAccess.Interfaces;
using GameStore.Services.Dto;
using GameStore.Services.Dto.Utils;
using GameStore.Services.Interfaces;
using GameStore.Services.Interfaces.Enums;
using GameStore.Services.Interfaces.Exceptions;
using GameStore.Services.Interfaces.Utils;
using Moq;
using NUnit.Framework;

namespace GameStore.Infrastructure.Services.Tests
{
    [TestFixture]
    public class GameServiceTest
    {
        private Mock<IUnitOfWork> _unitOfWorkMock;
        private Mock<IPipeline<Game>> _pipelineMock;
        private Mock<IFilterFactory> _filterFactoryMock;
        private IGameService _service;

        [SetUp]
        public void SetUp()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _pipelineMock = new Mock<IPipeline<Game>>();
            _filterFactoryMock = new Mock<IFilterFactory>();
            _service = new GameService(_unitOfWorkMock.Object, _filterFactoryMock.Object, _pipelineMock.Object);
        }

        [Test]
        public void ShouldDeleteGame()
        {
            _unitOfWorkMock.Setup(m => m.Games.Get(It.IsAny<int>()))
                .Returns(new Game());
            _unitOfWorkMock.Setup(m => m.Games.Delete(It.IsAny<Game>()));
            _unitOfWorkMock.Setup(m => m.Save());

            _service.Delete(It.IsAny<int>());

            _unitOfWorkMock.Verify(m => m.Games.Delete(It.IsAny<Game>()), Times.Once);
            _unitOfWorkMock.Verify(m => m.Save(), Times.Once);
        }

        [Test]
        public void ShouldReturnGamesForGenre()
        {
            Game game = GetGameTestData();
            _unitOfWorkMock.Setup(m => m.Games.Find(It.IsAny<Expression<Func<Game, bool>>>()))
                .Returns(new List<Game> { game });

            IEnumerable<GameDto> result = _service.GetGamesByGenre("genre");
            GameDto gameDto = result.First();

            Assert.IsTrue(gameDto.Equals(game.ToDto()));
        }

        [Test]
        public void ShouldReturnGames_ForPlatformType()
        {
            Game game = GetGameTestData();
            _unitOfWorkMock.Setup(m => m.Games.Find(It.IsAny<Expression<Func<Game, bool>>>()))
                .Returns(new List<Game> { game });

            IEnumerable<GameDto> result = _service.GetGamesByPlatformType("type");
            GameDto gameDto = result.First();

            Assert.IsTrue(gameDto.Equals(game.ToDto()));
        }

        [Test]
        public void ShouldCreateGame()
        {
            var platformTypes = new List<PlatformType>
            {
                new PlatformType(),
                new PlatformType()
            };
            var publishers = new List<Publisher>
            {
                new Publisher(),
                new Publisher()
            };
            var genres = new List<Genre>
            {
                new Genre(),
                new Genre()
            };
            _unitOfWorkMock.Setup(m => m.PlatformTypes.Find(It.IsAny<IPipeline<PlatformType>>()))
                .Returns(platformTypes);
            _unitOfWorkMock.Setup(m => m.Publishers.Find(It.IsAny<IPipeline<Publisher>>()))
                .Returns(publishers);
            _unitOfWorkMock.Setup(m => m.Genres.Find(It.IsAny<IPipeline<Genre>>()))
                .Returns(genres);
            _unitOfWorkMock.Setup(m => m.Games.Create(It.IsAny<Game>()));
            _unitOfWorkMock.Setup(m => m.Save());

            var game = new CreatingGameDto
            {
                Key = "Key",
                LanguagesNames = new Dictionary<string, string> { { "ru", "имя" } },
                LanguagesDescriptions = new Dictionary<string, string> { { "ru", "описание" } },
                Price = 10,
                UnitsInStock = 2,
                Discounted = false,
                PublishingDate = new DateTime(2005, 5, 5),
                GenreIds = new List<int> { 1, 2 },
                PlatformTypeIds = new List<int> { 1, 2 },
                PublisherIds = new List<int> { 1, 2 }
            };

            _service.Create(game);

            _unitOfWorkMock.Verify(m => m.Games.Create(It.IsAny<Game>()), Times.AtLeastOnce);
            _unitOfWorkMock.Verify(m => m.Save(), Times.AtLeastOnce);
        }

        [Test]
        public void ShouldReturnAmountOfGames()
        {
            const int exceptedCount = 3;
            Expression<Func<Game, bool>> predicate = null;
            _unitOfWorkMock.Setup(m => m.Games.Count(predicate))
                .Returns(exceptedCount);

            int count = _service.GetAmountOfGames();

            Assert.AreEqual(exceptedCount, count);
        }

        [Test]
        public void ShouldReturnAllGamesWhenAttributesEqualNull()
        {
            var games = new List<Game>
            {
                new Game(),
                new Game()
            };
            const int exceptedGameCount = 2;
            _unitOfWorkMock.Setup(m => m.Games.GetAll())
                 .Returns(games);

            List<GameDto> gamesDtos = _service.GetGames(null).ToList();

            Assert.AreEqual(exceptedGameCount, gamesDtos.Count);
        }

        [Test]
        public void ShouldGetGame_ByKey()
        {
            var game = new Game() { Key = "key", Name = "name", Description = "description" };
            _unitOfWorkMock.Setup(m => m.Games.SingleOrDefault(It.IsAny<Expression<Func<Game, bool>>>()))
                 .Returns(game);

            GameDto gameDto = _service.GetGameByKey("key");

            Assert.IsTrue(gameDto.Equals(game.ToDto()));
        }

        [Test]
        public void ShouldThrowException_ForGettingGameByKey_WhichNotExist()
        {
            _unitOfWorkMock.Setup(m => m.Games.Find(It.IsAny<IPipeline<Game>>()))
                 .Returns(new List<Game>());

            Assert.That(() => _service.GetGameByKey("key"), Throws.TypeOf<EntityNotFoundException>());
        }

        [Test]
        public void ShouldEditGame()
        {
            var game = new Game()
            {
                Key = "key",
                Name = "name",
                Description = "description",
                PlatformTypes = new List<PlatformType>(),
            };
            var gameDto = new GameDto
            {
                Key = "key",
                LanguagesNames = new Dictionary<string, string> { { "ru", "имя" } },
                LanguagesDescriptions = new Dictionary<string, string> { { "ru", "описание" } }
            };
            var platformTypes = new List<PlatformType>
            {
                new PlatformType(),
                new PlatformType()
            };
            var publishers = new List<Publisher> { new Publisher { CompanyName = "CompanyName" } };
            var genres = new List<Genre> { new Genre() };

            _unitOfWorkMock
                .Setup(m => m.PlatformTypes.Find(It.IsAny<IPipeline<PlatformType>>()))
                .Returns(platformTypes);

            _unitOfWorkMock
                .Setup(m => m.Publishers.Find(It.IsAny<IPipeline<Publisher>>()))
                .Returns(publishers);

            _unitOfWorkMock
                .Setup(m => m.Genres.Find(It.IsAny<IPipeline<Genre>>()))
                .Returns(genres);

            _unitOfWorkMock
                .Setup(m => m.Games.Get(It.IsAny<int>()))
                .Returns(game);

            var editingGame = new EditingGameDto
            {
                GameId = 1, 
                LanguagesNames = new Dictionary<string, string>() { { "ru", "имя" } },
                LanguagesDescriptions = new Dictionary<string, string>() { { "ru", "описание" } },
                Price = 10,
                UnitsInStock = 2,
                Discounted = false,
                PlatformTypeIds = new List<int>() { 1, 2 },
                PublisherIds = new List<int>() { 1 },
                GenreIds = new List<int>() { 1 }
            };

            _service.Edit(editingGame);

            Assert.IsTrue(gameDto.Equals(game.ToDto()));
        }

        [Test]
        public void GetExistingGameForId()
        {
            var game = new Game() { Key = "key", Name = "name", Description = "description" };
            _unitOfWorkMock.Setup(m => m.Games.Get(It.IsAny<int>()))
                .Returns(game);

            GameDto gameDto = _service.Get(id: 1);

            Assert.IsTrue(gameDto.Equals(game.ToDto()));
        }

        [Test]
        public void ShouldThrowException_ForGettingNonexistentGame_ById()
        {
            _unitOfWorkMock.Setup(m => m.Games.Get(It.IsAny<int>()))
                 .Returns((Game)null);

            Assert.That(() => _service.Get(It.IsAny<int>()), Throws.TypeOf<EntityNotFoundException>());
        }

        [Test]
        public void ShouldReturnFilteredGamesWhenAttributesNotEqualNull()
        {
            const int processCallingCount = 9;
            var games = new List<Game>
            {
                new Game()
            };
            _unitOfWorkMock.Setup(m => m.Games.Find(It.IsAny<IPipeline<Game>>()))
                .Returns(games);

            var attributes = new GamesFilterAttributes();
           
            SetDataForGettingFilteredGames(attributes);

            IEnumerable<GameDto> gamesDtos = _service.GetGames(attributes);

            Assert.AreEqual(games.Count, gamesDtos.Count());
            _pipelineMock.Verify(p => p.Register(It.IsAny<IFilter<Game>>()), Times.Exactly(processCallingCount));
        }

        private void SetDataForGettingFilteredGames(GamesFilterAttributes attributes)
        {
            const SortingObject anySortingObject = SortingObject.New;
            const PageSize anyPageSize = PageSize.OneHundredItems;
            attributes.Genres = new List<int>() { It.IsAny<int>() };
            attributes.PlatformTypes = new List<int>() { It.IsAny<int>() };
            attributes.Publishers = new List<int>() { It.IsAny<int>() };
            attributes.SortingObject = anySortingObject;
            attributes.GameNameSearchingString = "some searching string";
            attributes.PageInfo = new PageInfo() { PageSize = anyPageSize };

            _pipelineMock.Setup(m => m.Register(It.IsAny<IFilter<Game>>()))
                .Returns(_pipelineMock.Object);

            _pipelineMock.Setup(m => m.Process(It.IsAny<IQueryable<Game>>()))
                .Returns(It.IsAny<IQueryable<Game>>());
        }

        private Game GetGameTestData()
        {
            var game = new Game()
            {
                Comments = new List<Comment>(),
                Deleted = false,
                Description = "description",
                Discounted = false,
                Genres = new List<Genre>(),
                Id = 1,
                Key = "key",
                Name = "name",
                PlatformTypes = new List<PlatformType>(),
                Price = 10
            };

            return game;
        }
    }
}
