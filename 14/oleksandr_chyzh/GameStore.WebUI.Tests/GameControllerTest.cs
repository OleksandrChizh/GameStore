using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using GameStore.Services.Dto;
using GameStore.Services.Interfaces;
using GameStore.WebUI.Controllers;
using GameStore.WebUI.ViewModels.Game;
using Moq;
using NUnit.Framework;

namespace GameStore.WebUI.Tests
{
    [TestFixture]
    public class GameControllerTest
    {
        private Mock<IGameService> _gameServiceMock;
        private Mock<IGenreService> _genreServiceMock;
        private Mock<IPlatformTypeService> _platformTypeServiceMock;
        private Mock<IPublisherService> _publisherServiceMock;
        private GameController _gameController;

        [SetUp]
        public void SetUp()
        {
            _gameServiceMock = new Mock<IGameService>();
            _genreServiceMock = new Mock<IGenreService>();
             _platformTypeServiceMock = new Mock<IPlatformTypeService>();
            _publisherServiceMock = new Mock<IPublisherService>();

            _gameController = new GameController(
                _gameServiceMock.Object,
                _platformTypeServiceMock.Object,
                _publisherServiceMock.Object,
                _genreServiceMock.Object);
        }

        [Test]
        public void ShouldReturnViewModel_WithAllGenres_AsMultiSelectList()
        {           
            SetUpServicesMockForGameCreatingOnController();

            var viewResult = _gameController.NewGame();
            var model = viewResult.Model as CreateGameViewModel;

            Assert.NotNull(model);
            Assert.NotNull(model.GenresIds);
            Assert.AreEqual("1", model.Genres.ToList().First().Value);
            Assert.AreEqual("Strategy", model.Genres.ToList().First().Text);
        }

        [Test]
        public void ShouldReturnViewModel_WithAllPlatformTypes_AsMultiSelectList()
        {
            SetUpServicesMockForGameCreatingOnController();

            var viewResult = _gameController.NewGame();
            var model = viewResult.Model as CreateGameViewModel;

            Assert.NotNull(model);
            Assert.NotNull(model.PlatformTypesIds);
            Assert.AreEqual("1", model.PlatformTypes.ToList().First().Value);
            Assert.AreEqual("Desktop", model.PlatformTypes.ToList().First().Text);
        }

        [Test]
        public void ShouldRenderCurrentView_WithCreation_ForInvalidModel()
        {
            SetUpServicesMockForGameCreatingOnController();
            _gameController.ModelState.AddModelError("Key", "Key is requred");

            var viewResult = _gameController.NewGame(new CreateGameViewModel()) as ViewResult;

            Assert.IsNotNull(viewResult);
            Assert.IsFalse(viewResult.ViewData.ModelState.IsValid);
        }

        [Test]
        public void ShouldCreateGame_And_DoRedirectToAllGamesPage()
        {
            _gameServiceMock.Setup(m => m.Create(
                 It.IsAny<string>(),
                 It.IsAny<IDictionary<string, string>>(),
                 It.IsAny<IDictionary<string, string>>(),
                 It.IsAny<decimal>(),
                 It.IsAny<short>(),
                 It.IsAny<bool>(),
                 It.IsAny<DateTime>(),
                 It.IsAny<ICollection<int>>(),
                 It.IsAny<ICollection<int>>(),
                 It.IsAny<ICollection<int>>()));

            var redirectResult = _gameController.NewGame(new CreateGameViewModel()) as RedirectToRouteResult;

            _gameServiceMock.Verify(m => m.Create(
                It.IsAny<string>(),
                 It.IsAny<IDictionary<string, string>>(),
                 It.IsAny<IDictionary<string, string>>(),
                 It.IsAny<decimal>(),
                 It.IsAny<short>(),
                 It.IsAny<bool>(),
                 It.IsAny<DateTime>(),
                 It.IsAny<ICollection<int>>(),
                 It.IsAny<ICollection<int>>(),
                 It.IsAny<ICollection<int>>()));

            Assert.IsNotNull(redirectResult);
            Assert.AreEqual("GetAll", redirectResult.RouteValues["action"]);
        }

        private void SetUpServicesMockForGameCreatingOnController()
        {
            const int id = 1;
            var genres = new List<GenreDto> { new GenreDto(id, "Strategy", parentGenreId: null) };
            var platformTypes = new List<PlatformTypeDto> { new PlatformTypeDto(id, "Desktop") };
            var publishers = new List<PublisherDto> { new PublisherDto(id, "EAGame", new Dictionary<string, string>() { { "ru", "Производит игры" } }, "www.eagame.com") };

            _genreServiceMock.Setup(m => m.GetAllGenres()).Returns(genres);
            _platformTypeServiceMock.Setup(m => m.GetAllPlatformTypes()).Returns(platformTypes);
            _publisherServiceMock.Setup(m => m.GetAllPublishers()).Returns(publishers);
        }
    }
}
