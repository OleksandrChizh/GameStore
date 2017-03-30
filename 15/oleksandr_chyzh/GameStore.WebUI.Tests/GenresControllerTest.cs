using System.Net;
using System.Net.Http;
using System.Web.Http;
using GameStore.Services.Interfaces;
using GameStore.WebUI.Controllers.Api;
using GameStore.WebUI.Models.Genre;
using Moq;
using NUnit.Framework;

namespace GameStore.WebUI.Tests
{
    [TestFixture]
    public class GenresControllerTest
    {
        private GenresController _genresController;
        private Mock<IGenreService> _genreService;
        private Mock<IGameService> _gameService;

        [SetUp]
        public void SetUp()
        {
            _genreService = new Mock<IGenreService>();
            _gameService = new Mock<IGameService>();
            _genresController = new GenresController(_genreService.Object, _gameService.Object)
            {
                Request = new HttpRequestMessage()
            };
            _genresController.Request.SetConfiguration(new HttpConfiguration());
        }

        [Test]
        public void ShouldCreateGenre()
        {
            var model = new CreateGenreModel
            {
                Name = "Name",
                ParentGenreId = 1
            };

            _genreService.Setup(m => m.Create(It.IsAny<string>(), It.IsAny<int>()));

            HttpResponseMessage response = _genresController.Put(model);

            _genreService.Verify(m => m.Create(It.IsAny<string>(), It.IsAny<int>()), Times.AtLeastOnce);
            Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);
        }
    }
}
