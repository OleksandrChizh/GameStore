using System.Collections.Generic;
using GameStore.Services.Dto;
using GameStore.WebUI.Models.Game;
using GameStore.WebUI.ViewModels.Game;

namespace GameStore.WebUI.Utils
{
    public static class MapperToDto
    {
        public static EditingGameDto ToEditingDto(this UpdateGameModel model)
        {
            var languagesNames = new Dictionary<string, string>();
            var languagesDescriptions = new Dictionary<string, string>();

            languagesNames.Add("ru", model.Name);
            languagesDescriptions.Add("ru", model.Description);

            if (model.IsContainEnglishTranslation)
            {
                languagesNames.Add("en", model.EnglishName);
                languagesDescriptions.Add("en", model.EnglishDescription);
            }

            return new EditingGameDto
            {
                GameId = model.GameId,
                LanguagesNames = languagesNames,
                LanguagesDescriptions = languagesDescriptions,
                Price = model.Price,
                UnitsInStock = model.UnitsInStock,
                Discounted = model.Discounted,
                PlatformTypeIds = model.PlatformTypeIds,
                PublisherIds = model.PublisherIds,
                GenreIds = model.GenreIds
            };
        }

        public static EditingGameDto ToEditingDto(this UpdateGameViewModel model)
        {
            var languagesNames = new Dictionary<string, string>();
            var languagesDescriptions = new Dictionary<string, string>();

            languagesNames.Add("ru", model.Name);
            languagesDescriptions.Add("ru", model.Description);

            if (model.IsContainEnglishTranslation)
            {
                languagesNames.Add("en", model.EnglishName);
                languagesDescriptions.Add("en", model.EnglishDescription);
            }

            return new EditingGameDto
            {
                GameId = model.GameId,
                LanguagesNames = languagesNames,
                LanguagesDescriptions = languagesDescriptions,
                Price = model.Price,
                UnitsInStock = model.UnitsInStock,
                Discounted = model.Discounted,
                PlatformTypeIds = model.PlatformTypeIds,
                PublisherIds = model.PublisherIds,
                GenreIds = model.GenreIds
            };
        }

        public static CreatingGameDto ToCreatingDto(this CreateGameModel model)
        {
            Dictionary<string, string> languagesNames = new Dictionary<string, string>();
            Dictionary<string, string> languagesDescriptions = new Dictionary<string, string>();

            languagesNames.Add("ru", model.Name);
            languagesDescriptions.Add("ru", model.Description);

            if (model.IsContainEnglishTranslation)
            {
                languagesNames.Add("en", model.EnglishName);
                languagesDescriptions.Add("en", model.EnglishDescription);
            }

            return new CreatingGameDto
            {
                Key = model.Key,
                LanguagesNames = languagesNames,
                LanguagesDescriptions = languagesDescriptions,
                Price = model.Price,
                UnitsInStock = model.UnitsInStock,
                Discounted = model.Discounted,
                PublishingDate = model.PublishingDate,
                GenreIds = model.GenresIds,
                PlatformTypeIds = model.PlatformTypesIds,
                PublisherIds = model.PublishersIds
            };
        }

        public static CreatingGameDto ToCreatingDto(this CreateGameViewModel model)
        {
            Dictionary<string, string> languagesNames = new Dictionary<string, string>();
            Dictionary<string, string> languagesDescriptions = new Dictionary<string, string>();

            languagesNames.Add("ru", model.Name);
            languagesDescriptions.Add("ru", model.Description);

            if (model.IsContainEnglishTranslation)
            {
                languagesNames.Add("en", model.EnglishName);
                languagesDescriptions.Add("en", model.EnglishDescription);
            }

            return new CreatingGameDto
            {
                Key = model.Key,
                LanguagesNames = languagesNames,
                LanguagesDescriptions = languagesDescriptions,
                Price = model.Price,
                UnitsInStock = model.UnitsInStock,
                Discounted = model.Discounted,
                PublishingDate = model.PublishingDate,
                GenreIds = model.GenresIds,
                PlatformTypeIds = model.PlatformTypesIds,
                PublisherIds = model.PublishersIds
            };
        }
    }
}