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
    public class PlatformTypeServiceTest
    {
        private Mock<IUnitOfWork> _mock;
        private IPlatformTypeService _service;

        [SetUp]
        public void SetUp()
        {
            _mock = new Mock<IUnitOfWork>();
            _service = new PlatformTypeService(_mock.Object);
        }

        [Test]
        public void ShouldGetExistingPlatformType_ById()
        {
            var platformType = new PlatformType("type"); 
            _mock.Setup(m => m.PlatformTypes.Get(It.IsAny<int>()))
                 .Returns(platformType);

            PlatformTypeDto platformTypeDto = _service.Get(It.IsAny<int>());

            Assert.AreEqual("type", platformTypeDto.Type);
        }

        [Test]
        public void ShouldThrowException_ForGettingNonexistentPlatformType_ById()
        {
            _mock.Setup(m => m.PlatformTypes.Get(It.IsAny<int>()))
                 .Returns((PlatformType)null);             

            Assert.That(() => _service.Get(It.IsAny<int>()), Throws.TypeOf<EntityNotFoundException>());
        }

        [Test]
        public void ShouldReturnAllPlatformTypes()
        {
            _mock.Setup(m => m.PlatformTypes.GetAll())
                .Returns(new List<PlatformType> { new PlatformType("type") });

            IEnumerable<PlatformTypeDto> platformTypes = _service.GetAllPlatformTypes();
            PlatformTypeDto platformType = platformTypes.First();

            Assert.AreEqual("type", platformType.Type);
        }
    }
}