using System.Collections.Generic;
using System.Linq;
using System.Threading;
using GameStore.Domain.Core.Models;
using GameStore.Domain.Interfaces;
using GameStore.Infrastructure.EFDataAccess;

namespace GameStore.Infrastructure.DataAccess.Implementations.Repositories
{
    public class GameRepository : GenericRepository<Game, int>
    {
        public GameRepository(ApplicationContext dbContext) : base(dbContext)
        {
        }

        public override IEnumerable<Game> GetAll()
        {
            IQueryable<Game> games = DbContext.Set<Game>().Where(p => !p.Deleted);

            return GetGamesWithCulture(games);
        }

        public override Game Get(int id)
        {
            string currentCulture = Thread.CurrentThread.CurrentCulture.Name.Substring(0, 2);
            Game game = DbContext.Set<Game>().Find(id);

            if (game == null || game.Deleted)
            {
                return null;
            }

            if (game.Translations.Any(t => t.Language == currentCulture))
            {
                game.Name = game.Translations.Single(t => t.Language == currentCulture).Name;
                game.Description = game.Translations.Single(t => t.Language == currentCulture).Description;
            }

            return game;
        }

        public override IEnumerable<Game> Find(IPipeline<Game> pipeline)
        {
            IQueryable<Game> notDeletedGames = DbContext.Set<Game>().Where(e => !e.Deleted);
            IQueryable<Game> filteredGames = pipeline.Process(notDeletedGames);

            return GetGamesWithCulture(filteredGames);
        }

        private IList<Game> GetGamesWithCulture(IQueryable<Game> games)
        {
            string currentCulture = Thread.CurrentThread.CurrentCulture.Name.Substring(0, 2);

            List<Game> gamesWithoutCulture =
                games.Where(g => g.Translations.All(t => t.Language != currentCulture)).ToList();

            List<Game> gamesWithCulture = games
                .Where(game => game.Translations.Any(t => t.Language == currentCulture))
                .Select(g => new
                {
                    g.Id,
                    g.Key,
                    g.Translations.FirstOrDefault(t => t.Language == currentCulture).Name,
                    g.Translations.FirstOrDefault(t => t.Language == currentCulture).Description,
                    g.Price,
                    g.UnitsInStock,
                    g.Discounted,
                    g.ViewsCount,
                    g.AddingDate,
                    g.PublishingDate,
                    g.Deleted,
                    g.Translations,
                    g.Genres,
                    g.PlatformTypes,
                    g.Comments,
                    g.Publishers
                }).ToList()
                .Select(g => new Game
                {
                    Id = g.Id,
                    Key = g.Key,
                    Name = g.Name,
                    Description = g.Description,
                    Price = g.Price,
                    UnitsInStock = g.UnitsInStock,
                    Discounted = g.Discounted,
                    ViewsCount = g.ViewsCount,
                    AddingDate = g.AddingDate,
                    PublishingDate = g.PublishingDate,
                    Deleted = g.Deleted,
                    Translations = g.Translations.ToList(),
                    PlatformTypes = g.PlatformTypes.ToList(),
                    Genres = g.Genres.ToList(),
                    Comments = g.Comments.ToList(),
                    Publishers = g.Publishers.ToList()
                }).ToList();

            return gamesWithoutCulture.Union(gamesWithCulture).ToList();
        }
    }
}