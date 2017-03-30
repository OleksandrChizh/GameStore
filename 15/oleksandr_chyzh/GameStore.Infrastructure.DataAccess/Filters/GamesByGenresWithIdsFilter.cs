using System.Collections.Generic;
using System.Linq;
using GameStore.Domain.Core.Models;
using GameStore.Domain.Interfaces;

namespace GameStore.Infrastructure.DataAccess.Filters
{
    public class GamesByGenresWithIdsFilter : IFilter<Game>
    {
        private readonly ICollection<int> _genresIds;

        public GamesByGenresWithIdsFilter(ICollection<int> genresIds)
        {
            _genresIds = genresIds;
        }

        public IQueryable<Game> Execute(IQueryable<Game> entities)
        {
            return entities.Where(game => game.Genres
                                              .Select(genre => genre.Id)
                                              .Intersect(_genresIds)
                                              .Any());
        }
    }
}
