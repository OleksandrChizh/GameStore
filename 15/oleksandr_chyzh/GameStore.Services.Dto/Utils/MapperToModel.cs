using System.Collections.Generic;
using GameStore.Domain.Core.Models;

namespace GameStore.Services.Dto.Utils
{
    public class MapperToModel
    {
        public static void MapToModel(
            CreatingGameDto game, 
            Game model, 
            ICollection<Publisher> publishers,
            ICollection<PlatformType> platformTypes,
            ICollection<Genre> genres)
        {
            model.Genres.Clear();
            model.PlatformTypes.Clear();
            model.Publishers.Clear();
            model.Comments.Clear();

            model.Deleted = false;
            model.Name = game.LanguagesNames["ru"];
            model.Description = game.LanguagesDescriptions["ru"];
            model.Price = game.Price;
            model.UnitsInStock = game.UnitsInStock;
            model.Discounted = game.Discounted;
            model.PublishingDate = game.PublishingDate;
            model.Publishers = publishers;
            model.PlatformTypes = platformTypes;
            model.Genres = genres;
        }

        public static void MapToModel(
            EditingGameDto game,
            Game model,
            ICollection<Publisher> publishers,
            ICollection<PlatformType> platformTypes,
            ICollection<Genre> genres)
        {
            model.PlatformTypes.Clear();
            model.Publishers.Clear();
            model.Genres.Clear();

            model.Name = game.LanguagesNames["ru"];
            model.Description = game.LanguagesDescriptions["ru"];
            model.Price = game.Price;
            model.UnitsInStock = game.UnitsInStock;
            model.Discounted = game.Discounted;
            model.Publishers = publishers;
            model.PlatformTypes = platformTypes;
            model.Genres = genres;
        }
    }
}
