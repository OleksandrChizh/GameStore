using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using GameStore.Services.Dto;
using GameStore.Services.Interfaces;
using GameStore.Services.Interfaces.Enums;
using GameStore.Services.Interfaces.Utils;
using GameStore.WebUI.Controllers.Api;
using GameStore.WebUI.Models.Game;
using Moq;
using NUnit.Framework;

namespace GameStore.WebUI.Tests
{
    [TestFixture]
    public class GamesControllerTest
    {
        private GamesController _gamesController;
        private Mock<IGameService> _gameService;

        [SetUp]
        public void SetUp()
        {
            _gameService = new Mock<IGameService>();
            _gamesController = new GamesController(_gameService.Object)
            {
                Request = new HttpRequestMessage()
            };
            _gamesController.Request.SetConfiguration(new HttpConfiguration());
        }

        [Test]
        public void ShouldCreateGame()
        {
            var model = new CreateGameModel
            {
                Description = "Description",
                Discounted = false,
                EnglishDescription = "EnglishDescription",
                EnglishName = "EnglishName",
                Key = "Key",
                GenresIds = new List<int>(),
                IsContainEnglishTranslation = true,
                Name = "Name",
                PlatformTypesIds = new List<int>(),
                Price = 10,
                PublishersIds = new List<int>(),
                UnitsInStock = 10,
                PublishingDate = DateTime.UtcNow
            };

            _gameService.Setup(m => m.Create(It.IsAny<CreatingGameDto>()));

            HttpResponseMessage response = _gamesController.Put(model);

            _gameService.Verify(m => m.Create(It.IsAny<CreatingGameDto>()), Times.AtLeastOnce);

            Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);
        }

        [Test]
        public void ShouldUpdateGame()
        {
            var model = new UpdateGameModel
            {
                Description = "Description",
                Discounted = false,
                EnglishDescription = "EnglishDescription",
                EnglishName = "EnglishName",
                GameId = 1,
                GenreIds = new List<int>(),
                IsContainEnglishTranslation = true,
                Name = "Name",
                PlatformTypeIds = new List<int>(),
                Price = 10,
                PublisherIds = new List<int>(),
                UnitsInStock = 10
            };

            _gameService.Setup(m => m.Edit(It.IsAny<EditingGameDto>()));

            HttpResponseMessage response = _gamesController.Post(model);

            _gameService.Verify(m => m.Edit(It.IsAny<EditingGameDto>()), Times.AtLeastOnce);

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [Test]
        public void ShouldReturnGamesByFilter()
        {
            var model = new GamesFilterModel
            {
                GameName = "GameName",
                MaxPrice = 10,
                MinPrice = 5,
                PageInfo = new PageInfo
                {
                    PageNumber = 1,
                    PageSize = PageSize.TenItems,
                    TotalItems = 20
                },

                PublishingDatePeriod = PublishingDatePeriod.AllTime,
                SelectedGenresIds = new int[] { },
                SelectedPublishersIds = new int[] { },
                SelectedPlatformTypesIds = new int[] { },
                SortingObject = SortingObject.Default
            };

            _gameService
                .Setup(m => m.GetGames(It.IsAny<GamesFilterAttributes>()))
                .Returns(new List<GameDto>());

            _gamesController.Get(model);

            _gameService.Verify(m => m.GetGames(It.IsAny<GamesFilterAttributes>()), Times.AtLeastOnce);
        }
    }
}
