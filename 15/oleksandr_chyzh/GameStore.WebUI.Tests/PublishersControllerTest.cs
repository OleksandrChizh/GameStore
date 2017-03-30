using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using GameStore.Services.Interfaces;
using GameStore.WebUI.Controllers.Api;
using GameStore.WebUI.Models.Publisher;
using Moq;
using NUnit.Framework;

namespace GameStore.WebUI.Tests
{
    [TestFixture]
    public class PublishersControllerTest
    {
        private PublishersController _publishersController;
        private Mock<IPublisherService> _publisherServiceMock;

        [SetUp]
        public void SetUp()
        {
            _publisherServiceMock = new Mock<IPublisherService>();
            _publishersController = new PublishersController(_publisherServiceMock.Object)
            {
                Request = new HttpRequestMessage()
            };
            _publishersController.Request.SetConfiguration(new HttpConfiguration());
        }

        [Test]
        public void ShouldUpdatePublisher()
        {
            var model = new UpdatePublisherModel
            {
                Description = "Description",
                EnglishDescription = "EnglishDescription",
                HomePage = "HomePage",
                Id = 1,
                IsContainEnglishTranslation = true
            };

            _publisherServiceMock.Setup(m => m.Update(It.IsAny<int>(), It.IsAny<IDictionary<string, string>>(), It.IsAny<string>()));

            HttpResponseMessage response = _publishersController.Post(model);
          
            _publisherServiceMock.Verify(m => m.Update(It.IsAny<int>(), It.IsAny<IDictionary<string, string>>(), It.IsAny<string>()), Times.AtLeastOnce);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [Test]
        public void ShouldCreatePublisher()
        {
            var model = new CreatePublisherModel
            {
                CompanyName = "CompanyName",
                Description = "Description",
                EnglishDescription = "EnglishDescription",
                HomePage = "HomePage",
                IsContainEnglishTranslation = true
            };

            _publisherServiceMock.Setup(m => m.Create(It.IsAny<string>(), It.IsAny<IDictionary<string, string>>(), It.IsAny<string>()));

            HttpResponseMessage response = _publishersController.Put(model);

            _publisherServiceMock.Verify(m => m.Create(It.IsAny<string>(), It.IsAny<IDictionary<string, string>>(), It.IsAny<string>()), Times.AtLeastOnce);
            Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);
        }
    }
}