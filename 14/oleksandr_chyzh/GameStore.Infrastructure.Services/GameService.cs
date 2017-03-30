using System;
using System.Collections.Generic;
using System.Linq;
using GameStore.Domain.Core.Models;
using GameStore.Domain.Interfaces;
using GameStore.Infrastructure.DataAccess.Implementations;
using GameStore.Infrastructure.DataAccess.Interfaces;
using GameStore.Services.Dto;
using GameStore.Services.Dto.Utils;
using GameStore.Services.Interfaces;
using GameStore.Services.Interfaces.Enums;
using GameStore.Services.Interfaces.Exceptions;
using GameStore.Services.Interfaces.Utils;

namespace GameStore.Infrastructure.Services
{
    public class GameService : Service, IGameService
    {
        private readonly IFilterFactory _filterFactory;
        private readonly IPipeline<Game> _pipeline;

        public GameService(IUnitOfWork unitOfWork, IFilterFactory filterFactory, IPipeline<Game> pipline) : base(unitOfWork)
        {
            _filterFactory = filterFactory;
            _pipeline = pipline;
        }

        public int Create(
            string key,
            IDictionary<string, string> languagesNames,
            IDictionary<string, string> languagesDescriptions,
            decimal price,
            short unitsInStock,
            bool discounted,
            DateTime publishingDate,
            ICollection<int> genreIds,
            ICollection<int> platformTypeIds,
            ICollection<int> publisherIds)
        {
            CheckGameTranslationArgumentsValidity(languagesNames, languagesDescriptions);
          
            ICollection<Genre> fullGenreTree;
            if (genreIds.Count == 0)
            {
                var defaultGenre = UnitOfWork.Genres.SingleOrDefault(g => g.Name == "Other");
                if (defaultGenre == null)
                {
                    throw new EntityNotFoundException(typeof(Genre));
                }

                fullGenreTree = new List<Genre> { defaultGenre };
            }
            else
            {
                ICollection<Genre> selectedGenres = GetGenresFromIds(genreIds);
                fullGenreTree = BuildGenreTree(selectedGenres);
            }

            Game game = UnitOfWork.Games.SingleOrDefaultDeleted(g => g.Key == key);
            if (game == null)
            {
                game = new Game
                {
                    Key = key,
                    Name = languagesNames["ru"],
                    Description = languagesDescriptions["ru"],
                    Price = price,
                    UnitsInStock = unitsInStock,
                    Discounted = discounted,
                    PublishingDate = publishingDate,
                    Genres = fullGenreTree.ToList(),
                    PlatformTypes = GetPlatformTypesFromIds(platformTypeIds),
                    Publishers = GetPublishersFromIds(publisherIds)
                };

                AddTranslationsToGame(game, languagesNames, languagesDescriptions);

                UnitOfWork.Games.Create(game);
            }
            else
            {
                game.Deleted = false;
                game.Genres.Clear();
                game.PlatformTypes.Clear();
                game.Publishers.Clear();
                game.Comments.Clear();

                game.Name = languagesNames["ru"];
                game.Description = languagesDescriptions["ru"];
                game.Price = price;
                game.UnitsInStock = unitsInStock;
                game.Discounted = discounted;
                game.PublishingDate = publishingDate;
                game.Genres = fullGenreTree.ToList();
                game.PlatformTypes = GetPlatformTypesFromIds(platformTypeIds);
                game.Publishers = GetPublishersFromIds(publisherIds);

                SetTranslationsForGame(game, languagesNames, languagesDescriptions);

                UnitOfWork.Games.Update(game);
            }

            UnitOfWork.Save();

            return game.Id;
        }

        public void Edit(
            int gameId,
            IDictionary<string, string> languagesNames,
            IDictionary<string, string> languagesDescriptions,
            decimal price,
            short unitsInStock,
            bool discounted,
            ICollection<int> platformTypeIds,
            ICollection<int> publisherIds,
            ICollection<int> genreIds)
        {
            CheckGameTranslationArgumentsValidity(languagesNames, languagesDescriptions);

            Game game = GetGame(gameId);
            game.Name = languagesNames["ru"];
            game.Description = languagesDescriptions["ru"];
            game.Price = price;
            game.UnitsInStock = unitsInStock;
            game.Discounted = discounted;

            ICollection<PlatformType> selectedPlatformTypes = GetPlatformTypesFromIds(platformTypeIds);
            game.PlatformTypes.Clear();
            game.PlatformTypes = selectedPlatformTypes;

            ICollection<Publisher> selectedPublishers = GetPublishersFromIds(publisherIds);
            game.Publishers.Clear();
            game.Publishers = selectedPublishers;

            ICollection<Genre> fullGenreTree;
            if (genreIds.Count == 0)
            {
                var defaultGenre = UnitOfWork.Genres.SingleOrDefault(g => g.Name == "Other");
                if (defaultGenre == null)
                {
                    defaultGenre = UnitOfWork.Genres.SingleOrDefaultDeleted(g => g.Name == "Other");
                    if (defaultGenre == null)
                    {
                        throw new EntityNotFoundException(typeof(Genre));
                    }
                }

                fullGenreTree = new List<Genre> { defaultGenre };
            }
            else
            {
                ICollection<Genre> selectedGenres = GetGenresFromIds(genreIds);
                fullGenreTree = BuildGenreTree(selectedGenres);
            }

            game.Genres.Clear();
            game.Genres = fullGenreTree;

            SetTranslationsForGame(game, languagesNames, languagesDescriptions);

            UnitOfWork.Games.Update(game);
            UnitOfWork.Save();
        }

        public IEnumerable<GameDto> GetGamesByIds(ICollection<int> gameIds)
        {
            return !gameIds.Any() ? new List<GameDto>() : UnitOfWork.Games.Find(g => gameIds.Contains(g.Id)).Select(g => g.ToDto());
        }

        public void Delete(int gameId)
        {
            Game game = GetGame(gameId);
            UnitOfWork.Games.Delete(game);
            UnitOfWork.Save();
        }

        public GameDto GetGameByKey(string key)
        {
            Game game = UnitOfWork.Games.SingleOrDefault(g => g.Key == key);
            if (game == null)
            {
                game = UnitOfWork.Games.SingleOrDefaultDeleted(g => g.Key == key);
                if (game == null)
                {
                    throw new EntityNotFoundException(typeof(Game), $"Game with key: {key} was not found");
                }
            }

            return game.ToDto();
        }

        public IEnumerable<GameDto> GetGamesByPublisher(int id)
        {
            return UnitOfWork.Games.Find(game => game.Publishers.Any(p => p.Id == id))
                                   .Select(g => g.ToDto());
        }

        public IEnumerable<GameDto> GetGames(GamesFilterAttributes attributes)
        {
            if (attributes == null)
            {
                return UnitOfWork.Games.GetAll().Select(g => g.ToDto()).ToList();
            }

            _pipeline.Register(_filterFactory.MakeSortedGamesFilter())
                     .Register(_filterFactory.MakeGamesByPriceRangeFilter(attributes.MinPrice, attributes.MaxPrice))
                     .Register(_filterFactory.MakeGamesByPublishingDateFilter(attributes.PublishingDatePeriod));

            if (attributes.Genres != null && attributes.Genres.Count > 0)
            {
                _pipeline.Register(_filterFactory.MakeGamesByGenresWithIdsFilter(attributes.Genres));
            }

            if (attributes.PlatformTypes != null && attributes.PlatformTypes.Count > 0)
            {
                _pipeline.Register(_filterFactory.MakeGamesByPlatformTypesWithIdsFilter(attributes.PlatformTypes));
            }

            if (attributes.Publishers != null && attributes.Publishers.Count > 0)
            {
                _pipeline.Register(_filterFactory.MakeGamesByPublishersWithIdsFilter(attributes.Publishers));
            }

            if (attributes.SortingObject != SortingObject.Default)
            {
                _pipeline.Register(_filterFactory.MakeGamesBySortingObjectFilter(attributes.SortingObject));
            }

            if (!string.IsNullOrEmpty(attributes.GameNameSearchingString))
            {
                _pipeline.Register(_filterFactory.MakeGamesByGameNameFilter(attributes.GameNameSearchingString));
            }

            if (attributes.PageInfo.PageSize != PageSize.AllItems)
            {
                _pipeline.Register(_filterFactory.MakeGamesPaginationFilter(attributes.PageInfo));
            }

            return UnitOfWork.Games.Find(_pipeline)
                                   .Select(g => g.ToDto());
        }

        public IEnumerable<GameDto> GetGamesByGenre(string genre)
        {
            return UnitOfWork.Games.Find(game => game.Genres.Any(g => g.Name == genre))
                                   .Select(game => game.ToDto());
        }

        public IEnumerable<GameDto> GetGamesByGenre(int id)
        {
            return UnitOfWork.Games.Find(game => game.Genres.Any(g => g.Id == id))
                                   .Select(game => game.ToDto());
        }

        public IEnumerable<GameDto> GetGamesByPlatformType(string type)
        {
            return UnitOfWork.Games.Find(game => game.PlatformTypes.Any(pt => pt.Type == type))
                                   .Select(g => g.ToDto());
        }

        public int GetAmountOfGames()
        {
            return UnitOfWork.Games.Count();
        }

        public IDictionary<string, short> GetInformationAboutGamesDeficit(IDictionary<int, short> gamesQuantity)
        {
            var result = new Dictionary<string, short>();
            foreach (KeyValuePair<int, short> pair in gamesQuantity)
            {
                Game game = GetGame(pair.Key);
                if (game.UnitsInStock < pair.Value)
                {
                    result.Add(game.Name, (short)(pair.Value - game.UnitsInStock));
                }
            }

            return result;
        }

        public void AddView(int gameId)
        {
            Game game = UnitOfWork.Games.Get(gameId);
            if (game == null)
            {
                game = UnitOfWork.Games.GetDeletedEntity(gameId);
                if (game == null)
                {
                    throw new EntityNotFoundException(typeof(Game));
                }
            }

            game.ViewsCount++;
            UnitOfWork.Games.Update(game);
            UnitOfWork.Save();
        }

        public GameDto Get(int id)
        {
            Game game = GetGame(id);
            return game.ToDto();
        }

        private ICollection<PlatformType> GetPlatformTypesFromIds(ICollection<int> platformTypeIds)
        {
            var pipeline = new Pipeline<PlatformType>();
            pipeline.Register(_filterFactory.MakePlatformTypesForIdsFilter(platformTypeIds));

            List<PlatformType> results = UnitOfWork.PlatformTypes.Find(pipeline).ToList();
            if (results.Count != platformTypeIds.Count)
            {
                throw new EntityNotFoundException(typeof(PlatformType));
            }

            return results;
        }

        private ICollection<Genre> GetGenresFromIds(ICollection<int> genreIds)
        {
            var pipeline = new Pipeline<Genre>();
            pipeline.Register(_filterFactory.MakeGenresForIdsFilter(genreIds));

            List<Genre> results = UnitOfWork.Genres.Find(pipeline).ToList();
            if (results.Count != genreIds.Count)
            {
                throw new EntityNotFoundException(typeof(Genre));
            }

            return results;
        }

        private ICollection<Publisher> GetPublishersFromIds(ICollection<int> publisherIds)
        {
            var pipeline = new Pipeline<Publisher>();
            pipeline.Register(_filterFactory.MakePublishersForIdsFilter(publisherIds));

            List<Publisher> results = UnitOfWork.Publishers.Find(pipeline).ToList();
            if (results.Count != publisherIds.Count)
            {
                throw new EntityNotFoundException(typeof(Publisher));
            }

            return results;
        }

        private ICollection<Genre> BuildGenreTree(ICollection<Genre> genres)
        {
            List<Genre> genreTree = genres.ToList();
            foreach (Genre genre in genres)
            {
                AddNotContainedParent(genre, genreTree);
            }

            return genreTree;
        }

        private void AddNotContainedParent(Genre currentGenre, ICollection<Genre> genres)
        {
            Genre genre = currentGenre;
            while (true)
            {
                if (genre.ParentGenre == null)
                {
                    return;
                }

                if (!genres.Contains(genre.ParentGenre))
                {
                    genres.Add(genre.ParentGenre);
                }

                genre = genre.ParentGenre;
            }
        }

        private Game GetGame(int id)
        {
            Game game = UnitOfWork.Games.Get(id);
            if (game == null)
            {
                throw new EntityNotFoundException(typeof(Game));
            }

            return game;
        }

        private void SetTranslationsForGame(
            Game game, 
            IDictionary<string, string> languagesNames,
            IDictionary<string, string> languagesDescriptions)
        {
            List<GameTranslation> translationOnRemoving = game.Translations
                                                              .Where(t => !languagesNames.Keys.Contains(t.Language))
                                                              .ToList();

            translationOnRemoving.ForEach(t => UnitOfWork.GameTranslations.Delete(t));

            for (int i = 0; i < languagesNames.Count; i++)
            {
                string language = languagesNames.ElementAt(i).Key;
                string gameName = languagesNames.ElementAt(i).Value;
                string gameDescription = languagesDescriptions.ElementAt(i).Value;

                if (game.Translations.Count(t => t.Language == language) == 0)
                {
                    game.Translations.Add(new GameTranslation
                    {
                        Language = language,
                        Description = gameDescription,
                        Name = gameName
                    });
                }
                else
                {
                    GameTranslation translation = game.Translations.Single(t => t.Language == language);

                    translation.Name = gameName;
                    translation.Description = gameDescription;
                    translation.Deleted = false;

                    UnitOfWork.GameTranslations.Update(translation);
                }
            }
        }

        private void CheckGameTranslationArgumentsValidity(IDictionary<string, string> languagesNames, IDictionary<string, string> languagesDescriptions)
        {
            if (languagesNames == null)
            {
                throw new ArgumentNullException(nameof(languagesNames));
            }

            if (languagesDescriptions == null)
            {
                throw new ArgumentNullException(nameof(languagesDescriptions));
            }

            if (!languagesNames.ContainsKey("ru") ||
                !languagesDescriptions.ContainsKey("ru"))
            {
                throw new DefaultCultureNotFoundException();
            }

            if (languagesNames.Count != languagesDescriptions.Count)
            {
                throw new CultureArgumentException("Amount of names translations not equal amount of descriptions tranlations");
            }
        }

        private void AddTranslationsToGame(
            Game game,
            IDictionary<string, string> languagesNames,
            IDictionary<string, string> languagesDescriptions)
        {
            for (int i = 0; i < languagesNames.Count; i++)
            {
                string language = languagesNames.ElementAt(i).Key;
                string gameName = languagesNames.ElementAt(i).Value;
                string gameDescription = languagesDescriptions.ElementAt(i).Value;

                game.Translations.Add(new GameTranslation
                {
                    Language = language,
                    Description = gameDescription,
                    Name = gameName
                });
            }
        }
    }
}
