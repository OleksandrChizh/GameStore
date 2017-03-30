using System.Collections.Generic;
using System.Linq;
using GameStore.Domain.Core.Models;
using GameStore.Domain.Interfaces;

namespace GameStore.Infrastructure.DataAccess.Filters
{
    public class PublishersForIdsFilter : IFilter<Publisher>
    {
        private readonly ICollection<int> _entitiesIds;

        public PublishersForIdsFilter(ICollection<int> entitiesIds)
        {
            _entitiesIds = entitiesIds;
        }

        public IQueryable<Publisher> Execute(IQueryable<Publisher> entities)
        {
            return _entitiesIds == null ? entities : entities.Where(e => _entitiesIds.Contains(e.Id));
        }
    }
}
