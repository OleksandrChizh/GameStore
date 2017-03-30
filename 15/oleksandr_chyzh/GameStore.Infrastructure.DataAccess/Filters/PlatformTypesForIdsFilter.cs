using System.Collections.Generic;
using System.Linq;
using GameStore.Domain.Core.Models;
using GameStore.Domain.Interfaces;

namespace GameStore.Infrastructure.DataAccess.Filters
{
    public class PlatformTypesForIdsFilter : IFilter<PlatformType>
    {
        private readonly ICollection<int> _entitiesIds;

        public PlatformTypesForIdsFilter(ICollection<int> entitiesIds)
        {
            _entitiesIds = entitiesIds;
        }

        public IQueryable<PlatformType> Execute(IQueryable<PlatformType> entities)
        {
            return _entitiesIds == null ? entities : entities.Where(e => _entitiesIds.Contains(e.Id));
        }
    }
}
