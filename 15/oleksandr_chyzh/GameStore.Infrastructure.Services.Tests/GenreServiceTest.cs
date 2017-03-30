using System.Collections.Generic;
using System.Linq;
using GameStore.Domain.Core.Models;
using GameStore.Domain.Interfaces;
using GameStore.Services.Dto;
using GameStore.Services.Interfaces;
using GameStore.Services.Interfaces.Exceptions;
using Moq;
using NUnit.Framework;
using Assert = NUnit.Framework.Assert;

namespace GameStore.Infrastructure.Services.Tests
{
    [TestFixture]
    public class GenreServiceTest
    {
        private Mock<IUnitOfWork> _mock;
        private IGenreService _service;

        [SetUp]
        public void SetUp()
        {
            _mock = new Mock<IUnitOfWork>();
            _service = new GenreService(_mock.Object);
        }

        [Test]
        public void ShouldGetExistingGenre_ById()
        {
            var genre = new Genre("strategy", parentGenre: null);
            _mock.Setup(m => m.Genres.Get(It.IsAny<int>()))
                 .Returns(genre);

            GenreDto genreDto = _service.Get(It.IsAny<int>());

            Assert.AreEqual("strategy", genreDto.Name);
        }

        [Test]
        public void ShouldThrowException_ForGettingNonexistentGenre_ById()
        {
            _mock.Setup(m => m.Genres.Get(It.IsAny<int>())).Returns((Genre)null);

            Assert.That(() => _service.Get(It.IsAny<int>()), Throws.TypeOf<EntityNotFoundException>());
        }

        [Test]
        public void ShouldReturnAllGenres()
        {
            _mock.Setup(m => m.Genres.GetAll())
                .Returns(new List<Genre>() { new Genre("name", null) });

            IEnumerable<GenreDto> genres = _service.GetAllGenres();
            GenreDto genre = genres.First();

            Assert.AreEqual("name", genre.Name);
        }
    }
}
