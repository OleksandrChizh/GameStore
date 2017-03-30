using System.Collections.Generic;
using System.Linq;
using GameStore.Domain.Core.Models;
using GameStore.Domain.Interfaces;

namespace GameStore.Infrastructure.DataAccess.Filters
{
    public class GamesByPublishersWithIdsFilter : IFilter<Game>
    {
        private readonly ICollection<int> _publishersIds;

        public GamesByPublishersWithIdsFilter(ICollection<int> publishersIds)
        {
            _publishersIds = publishersIds;
        }

        public IQueryable<Game> Execute(IQueryable<Game> entities)
        {
            return entities.Where(game => game.Publishers
                                              .Select(p => p.Id)
                                              .Intersect(_publishersIds)
                                              .Any());
        }
    }
}
