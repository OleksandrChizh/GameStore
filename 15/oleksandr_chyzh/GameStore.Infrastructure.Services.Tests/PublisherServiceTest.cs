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

namespace GameStore.Infrastructure.Services.Tests
{
    [TestFixture]
    public class PublisherServiceTest
    {
        private Mock<IUnitOfWork> _mock;
        private IPublisherService _service;

        [SetUp]
        public void SetUp()
        {
            _mock = new Mock<IUnitOfWork>();
            _service = new PublisherService(_mock.Object);
        }

        [Test]
        public void ShouldThrowException_WhenTryGetNonExistentPublisher()
        {
            _mock.Setup(m => m.Publishers.Get(It.IsAny<int>()))
                .Returns((Publisher)null);

            Assert.That(() => _service.Get(It.IsAny<int>()), Throws.TypeOf<EntityNotFoundException>());
        }

        [Test]
        public void ShouldReturnPublisher()
        {
            var publisher = new Publisher("companyName", "description", "homePage");
            _mock.Setup(m => m.Publishers.Get(It.IsAny<int>()))
                .Returns(publisher);

            PublisherDto publisherDto = _service.Get(It.IsAny<int>());

            Assert.IsTrue(publisherDto.Equals(publisher.ToDto()));
        }

        [Test]
        public void ShouldReturnAllPublishers()
        {
            var publisher = new Publisher("companyName", "description", "homePage");
            _mock.Setup(m => m.Publishers.GetAll())
                .Returns(new List<Publisher> { publisher });

            IEnumerable<PublisherDto> publishers = _service.GetAllPublishers();
            PublisherDto publisherDto = publishers.First();

            Assert.IsTrue(publisherDto.Equals(publisher.ToDto()));
        }

        [Test]
        public void ShouldThrowException_WhenTryGet_NonExistentPublisher_ByCompanyName()
        {
            _mock.Setup(m => m.Publishers.Find(It.IsAny<IPipeline<Publisher>>()))
                .Returns(new List<Publisher>());

            Assert.That(() => _service.GetPublisherByCompanyName("companyName"), Throws.TypeOf<EntityNotFoundException>());
        }

        [Test]
        public void ShouldReturnPublisher_ByCompanyName()
        {
            var publisher = new Publisher("companyName", "description", "homePage");
            _mock.Setup(m => m.Publishers.SingleOrDefault(It.IsAny<Expression<Func<Publisher, bool>>>()))
                .Returns(publisher);

            PublisherDto publisherDto = _service.GetPublisherByCompanyName("companyName");

            Assert.IsTrue(publisherDto.Equals(publisher.ToDto()));
        }

        [Test]
        public void ShouldCreatePublisher()
        {
            _mock.Setup(m => m.Publishers.Create(It.IsAny<Publisher>()));
            _mock.Setup(m => m.PublisherTranslations.Create(It.IsAny<PublisherTranslation>()));
            _mock.Setup(m => m.Save());

            _service.Create("companyName", new Dictionary<string, string>() { { "ru", "описание" } }, "homePage");

            _mock.Verify(m => m.Publishers.Create(It.IsAny<Publisher>()), Times.AtLeastOnce);
            _mock.Verify(m => m.PublisherTranslations.Create(It.IsAny<PublisherTranslation>()), Times.AtLeastOnce);
            _mock.Verify(m => m.Save());
        }
    }
}
