using System.Collections.Generic;
using System.Linq;
using GameStore.Domain.Core.Models;
using GameStore.Domain.Interfaces;

namespace GameStore.Infrastructure.DataAccess.Filters
{
    public class GenresForIdsFilter : IFilter<Genre>
    {
        private readonly ICollection<int> _entitiesIds;

        public GenresForIdsFilter(ICollection<int> entitiesIds)
        {
            _entitiesIds = entitiesIds;
        }

        public IQueryable<Genre> Execute(IQueryable<Genre> entities)
        {
            return _entitiesIds == null ? entities : entities.Where(e => _entitiesIds.Contains(e.Id));
        }
    }
}
