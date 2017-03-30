using System.Data.Entity;
using System.Linq;
using GameStore.Domain.Core.Models;
using GameStore.Infrastructure.EFDataAccess;

namespace GameStore.Infrastructure.DataAccess.Implementations.Repositories
{
    public class GenreRepository : GenericRepository<Genre, int>
    {
        public GenreRepository(ApplicationContext dbContext) : base(dbContext)
        {
        }

        public override void Delete(Genre item)
        {
            const string otherGenreName = "Other";

            var defaultGenre = DbContext.Genres.SingleOrDefault(g => g.Name == otherGenreName);
            if (defaultGenre == null)
            {
                DbContext.Genres.Add(new Genre(otherGenreName, null));
            }

            var games = DbContext.Games.Where(game => game.Genres.Any(genre => genre.Id == item.Id)).ToList();
            foreach (var game in games)
            {
                game.Genres.Remove(item);

                if (!game.Genres.Any())
                {
                    game.Genres.Add(defaultGenre);
                }

                DbContext.Entry(game).State = EntityState.Modified;
            }

            base.Delete(item);
        }
    }
}
