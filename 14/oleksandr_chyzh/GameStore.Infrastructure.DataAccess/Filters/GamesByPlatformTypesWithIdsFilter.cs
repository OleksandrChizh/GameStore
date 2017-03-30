using System.Collections.Generic;
using System.Linq;
using GameStore.Domain.Core.Models;
using GameStore.Domain.Interfaces;

namespace GameStore.Infrastructure.DataAccess.Filters
{
    public class GamesByPlatformTypesWithIdsFilter : IFilter<Game>
    {
        private readonly ICollection<int> _platformTypesIds;

        public GamesByPlatformTypesWithIdsFilter(ICollection<int> platformTypesIds)
        {
            _platformTypesIds = platformTypesIds;
        }

        public IQueryable<Game> Execute(IQueryable<Game> entities)
        {
            return entities.Where(game => game.PlatformTypes
                                              .Select(pt => pt.Id)
                                              .Intersect(_platformTypesIds)
                                              .Any());
        }
    }
}
