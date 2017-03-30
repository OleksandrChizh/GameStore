using System.Collections.Generic;
using System.Linq;
using System.Threading;
using GameStore.Services.Dto;
using GameStore.WebUI.ViewModels.Administrator;
using GameStore.WebUI.ViewModels.Comment;
using GameStore.WebUI.ViewModels.Game;
using GameStore.WebUI.ViewModels.Genre;
using GameStore.WebUI.ViewModels.Order;
using GameStore.WebUI.ViewModels.PlatformType;
using GameStore.WebUI.ViewModels.Publisher;
using GameStore.WebUI.ViewModels.User;

namespace GameStore.WebUI.Utils
{
    public static class MapperToViewModel
    {
        public static UpdateGameViewModel ToUpdateViewModel(this GameDto game)
        {
            return new UpdateGameViewModel(
                game.Id,
                game.LanguagesNames,
                game.LanguagesDescriptions,
                game.PlatformTypes.Select(pt => pt.Id).ToList(),
                game.Publishers.Select(p => p.Id).ToList(),
                game.Genres.Select(g => g.Id).ToList(),
                game.Price,
                game.UnitsInStock,
                game.Discounted);
        }

        public static UpdateUserViewModel ToUpdateViewModel(this UserDto user)
        {
            return new UpdateUserViewModel
            {
                Id = user.Id,
                BanExpiryDate = user.BanExpiryDate,
                SelectedRoles = user.Roles.ToList(),
                UserName = user.UserName
            };
        }

        public static UserViewModel ToViewModel(this UserDto user)
        {
            return new UserViewModel
            {
                UserId = user.Id,
                BanExpiryDate = user.BanExpiryDate,
                Roles = user.Roles,
                UserName = user.UserName
            };
        }

        public static CommentViewModel ToViewModel(this CommentDto comment)
        {
            return new CommentViewModel(
                comment.Id,
                comment.Name,
                comment.Body,
                comment.RepliedTo,
                comment.Quote,
                comment.IsQuote);
        }

        public static ShortOrderViewModel ToShortViewModel(this OrderDto order)
        {
            return new ShortOrderViewModel
            {
                Id = order.Id,
                OrderDate = order.OrderDate,
                PayingDate = order.PayingDate,
                ShippedDate = order.ShippedDate,
                CustomerId = order.CustomerId
            };
        }

        public static GameViewModel ToViewModel(this GameDto game)
        {
            string currentCulture = Thread.CurrentThread.CurrentCulture.Name.Substring(0, 2);

            return new GameViewModel(
                game.Id,
                game.Deleted,
                game.Key,
                game.LanguagesNames.ContainsKey(currentCulture) ? game.LanguagesNames[currentCulture] : game.LanguagesNames["ru"],
                game.LanguagesDescriptions.ContainsKey(currentCulture) ? game.LanguagesDescriptions[currentCulture] : game.LanguagesDescriptions["ru"],
                game.Price,
                game.UnitsInStock,
                game.ViewCount,
                game.PublishingDate,
                game.AddingDate,
                game.ImagePath,
                game.Genres.Select(g => g.ToViewModel()).ToList(),
                game.PlatformTypes.Select(pt => pt.Type).ToList(),
                game.Publishers.Select(p => p.CompanyName).ToList());
        }

        public static ShortGameViewModel ToShortViewModel(this GameDto game)
        {
            string currentCulture = Thread.CurrentThread.CurrentCulture.Name.Substring(0, 2);

            return new ShortGameViewModel(
                game.Id,
                game.Key,
                game.LanguagesNames.ContainsKey(currentCulture) ? game.LanguagesNames[currentCulture] : game.LanguagesNames["ru"],
                game.Price);
        }

        public static GenreViewModel ToViewModel(this GenreDto genre)
        {
            return new GenreViewModel(
                genre.Id,
                genre.Name,
                genre.ParentGenreId);
        }

        public static DeleteGenreViewModel ToDeleteGenreViewModel(this GenreDto genre, IEnumerable<string> subGenres)
        {
            return new DeleteGenreViewModel
            {
                Id = genre.Id,
                Name = genre.Name,
                SubGenres = subGenres
            };
        }

        public static PublisherViewModel ToViewModel(this PublisherDto publisher)
        {
            const string englishKey = "en";
            string currentCulture = Thread.CurrentThread.CurrentCulture.Name.Substring(0, 2);

            return new PublisherViewModel(
                publisher.Id,
                publisher.CompanyName,
                publisher.LanguagesDescriptions.ContainsKey(currentCulture) ? publisher.LanguagesDescriptions[currentCulture] : publisher.LanguagesDescriptions["ru"],
                publisher.HomePage,
                publisher.LanguagesDescriptions.ContainsKey(englishKey) ? publisher.LanguagesDescriptions[englishKey] : string.Empty);
        }

        public static ShortPublisherViewModel ToShortViewModel(this PublisherDto publisher)
        {
            return new ShortPublisherViewModel(
                publisher.Id,
                publisher.CompanyName);
        }

        public static PlatformTypeViewModel ToViewModel(this PlatformTypeDto platformType)
        {
            return new PlatformTypeViewModel(
                platformType.Id,
                platformType.Type);
        }
    }
}