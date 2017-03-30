using System;
using System.Collections.Generic;
using System.Linq;
using GameStore.Services.Dto;
using GameStore.WebUI.Utils;
using GameStore.WebUI.ViewModels.Administrator;
using GameStore.WebUI.ViewModels.Game;
using GameStore.WebUI.ViewModels.Genre;
using GameStore.WebUI.ViewModels.Order;
using GameStore.WebUI.ViewModels.PlatformType;
using GameStore.WebUI.ViewModels.Publisher;
using GameStore.WebUI.ViewModels.User;
using NUnit.Framework;

namespace GameStore.WebUI.Tests
{
    [TestFixture]
    public class MapperToViewModelTest
    {
        [Test]
        public void ShouldMap_GameDto_To_UpdateGameViewModel()
        {
            var game = new GameDto
            {
                Id = 1,
                LanguagesNames = new Dictionary<string, string> { { "ru", "name" } },
                LanguagesDescriptions = new Dictionary<string, string> { { "ru", "description" } },
                PlatformTypes = new List<PlatformTypeDto> { new PlatformTypeDto(1, "type") },
                Publishers = new List<PublisherDto> { new PublisherDto { Id = 1 } },
                Genres = new List<GenreDto> { new GenreDto(1, "name", null) },
                Price = 10,
                UnitsInStock = 5,
                Discounted = false
            };

            UpdateGameViewModel result = game.ToUpdateViewModel();

            Assert.AreEqual(game.Id, result.GameId);
            Assert.AreEqual(game.Price, result.Price);
            Assert.AreEqual(game.UnitsInStock, result.UnitsInStock);
            Assert.AreEqual(game.LanguagesNames.Values.First(), result.Name);
            Assert.AreEqual(game.LanguagesDescriptions.Values.First(), result.Description);
            Assert.AreEqual(game.PlatformTypes.Count, result.PlatformTypeIds.Count);
            Assert.AreEqual(game.Publishers.Count, result.PublisherIds.Count);
            Assert.AreEqual(game.Genres.Count, result.GenreIds.Count);
            Assert.AreEqual(false, result.IsContainEnglishTranslation);
            Assert.AreEqual(string.Empty, result.EnglishName);
            Assert.AreEqual(string.Empty, result.EnglishDescription);
        }

        [Test]
        public void ShouldMap_UserDto_To_UpdateUserViewModel()
        {
            var user = new UserDto
            {
                Id = "1",
                BanExpiryDate = DateTime.UtcNow,
                Roles = new List<string> { "Role" },
                UserName = "UserName"
            };

            UpdateUserViewModel result = user.ToUpdateViewModel();

            Assert.AreEqual(user.Id, result.Id);
            Assert.AreEqual(user.BanExpiryDate, result.BanExpiryDate);
            Assert.AreEqual(user.Roles.Count, result.SelectedRoles.Count);
            Assert.AreEqual(user.UserName, result.UserName);
        }

        [Test]
        public void ShouldMap_UserDto_To_ViewModel()
        {
            var user = new UserDto
            {
                Id = "1",
                BanExpiryDate = DateTime.UtcNow,
                Roles = new List<string> { "Role" },
                UserName = "UserName"
            };

            UserViewModel result = user.ToViewModel();

            Assert.AreEqual(user.Id, result.UserId);
            Assert.AreEqual(user.BanExpiryDate, result.BanExpiryDate);
            Assert.AreEqual(user.Roles.Count, result.Roles.Count);
            Assert.AreEqual(user.UserName, result.UserName);
        }

        [Test]
        public void ShouldMap_OrderDto_To_ShortViewModel()
        {
            var order = new OrderDto
            {
                Id = 1,
                OrderDate = DateTime.UtcNow,
                PayingDate = DateTime.UtcNow,
                ShippedDate = DateTime.UtcNow,
                CustomerId = "1"
            };

            ShortOrderViewModel result = order.ToShortViewModel();

            Assert.AreEqual(order.Id, result.Id);
            Assert.AreEqual(order.OrderDate, result.OrderDate);
            Assert.AreEqual(order.PayingDate, result.PayingDate);
            Assert.AreEqual(order.ShippedDate, order.ShippedDate);
            Assert.AreEqual(order.CustomerId, result.CustomerId);
        }

        [Test]
        public void ShouldMap_GameDto_To_GameViewModel()
        {
            var game = new GameDto
            {
                Id = 1,
                Deleted = false,
                Key = "key",
                LanguagesNames = new Dictionary<string, string> { { "ru", "name" } },
                LanguagesDescriptions = new Dictionary<string, string> { { "ru", "descriptions" } },
                Price = 10,
                UnitsInStock = 10,
                ViewCount = 10,
                PublishingDate = DateTime.UtcNow,
                AddingDate = DateTime.UtcNow,
                Genres = new List<GenreDto> { new GenreDto(1, "name", null) },
                PlatformTypes = new List<PlatformTypeDto> { new PlatformTypeDto(1, "type") },
                Publishers = new List<PublisherDto> { new PublisherDto { CompanyName = "companyName" } }
            };

            GameViewModel result = game.ToViewModel();

            Assert.AreEqual(game.Id, result.Id);
            Assert.AreEqual(game.Deleted, result.IsDeleted);
            Assert.AreEqual(game.Key, result.Key);
            Assert.AreEqual(game.LanguagesNames.Values.First(), result.Name);
            Assert.AreEqual(game.LanguagesDescriptions.Values.First(), result.Description);
            Assert.AreEqual(game.Price, result.Price);
            Assert.AreEqual(game.UnitsInStock, result.UnitsInStock);
            Assert.AreEqual(game.ViewCount, result.ViewsCount);
            Assert.AreEqual(game.PublishingDate, result.PublishingDate);
            Assert.AreEqual(game.AddingDate, result.AddingDate);
            Assert.AreEqual(game.Genres.Count, result.Genres.Count);
            Assert.AreEqual(game.PlatformTypes.Count, result.PlatformTypes.Count);
            Assert.AreEqual(game.Publishers.Count, result.Publishers.Count);
        }

        [Test]
        public void ShouldMap_GameDto_To_ShortGameViewModel()
        {
            var game = new GameDto
            {
                Id = 1,
                Key = "key",
                LanguagesNames = new Dictionary<string, string> { { "ru", "name" } },
            };

            ShortGameViewModel result = game.ToShortViewModel();

            Assert.AreEqual(game.Id, result.Id);
            Assert.AreEqual(game.Key, result.Key);
            Assert.AreEqual(game.LanguagesNames.Values.First(), result.Name);
        }

        [Test]
        public void ShouldMap_GenreDto_To_GenreViewModel()
        {
            var genre = new GenreDto(1, "name", null);

            GenreViewModel result = genre.ToViewModel();

            Assert.AreEqual(genre.Id, result.Id);
            Assert.AreEqual(genre.Name, result.Name);
            Assert.IsNull(result.ParentGenreId);
        }

        [Test]
        public void ShouldMap_PublisherDto_To_PublisherViewModel()
        {
            var publisher = new PublisherDto
            {
                Id = 1,
                CompanyName = "companyName",
                LanguagesDescriptions = new Dictionary<string, string> { { "ru", "descriptions" } },
                HomePage = "homePage"
            };

            PublisherViewModel result = publisher.ToViewModel();

            Assert.AreEqual(publisher.Id, result.Id);
            Assert.AreEqual(publisher.CompanyName, result.CompanyName);
            Assert.AreEqual(publisher.LanguagesDescriptions.Values.First(), result.Description);
            Assert.AreEqual(publisher.HomePage, result.HomePage);
            Assert.AreEqual(string.Empty, result.EnglishDescription);
        }

        [Test]
        public void ShouldMap_PublisherDto_To_ShortPublisherViewModel()
        {
            var publisher = new PublisherDto
            {
                Id = 1,
                CompanyName = "companyName",
            };

            ShortPublisherViewModel result = publisher.ToShortViewModel();

            Assert.AreEqual(publisher.Id, result.Id);
            Assert.AreEqual(publisher.CompanyName, result.CompanyName);
        }

        [Test]
        public void ShouldMap_PlatformTypeDto_To_PlatformTypeViewModel()
        {
            var platformType = new PlatformTypeDto(1, "type");

            PlatformTypeViewModel result = platformType.ToViewModel();

            Assert.AreEqual(platformType.Id, result.Id);
            Assert.AreEqual(platformType.Type, result.Type);
        }
    }
}
