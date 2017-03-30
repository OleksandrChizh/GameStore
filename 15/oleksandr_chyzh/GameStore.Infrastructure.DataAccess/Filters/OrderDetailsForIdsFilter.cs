using System.Collections.Generic;
using System.Linq;
using GameStore.Domain.Core.Models;
using GameStore.Domain.Interfaces;

namespace GameStore.Infrastructure.DataAccess.Filters
{
    public class OrderDetailsForIdsFilter : IFilter<OrderDetails>
    {
        private readonly ICollection<int> _entitiesIds;

        public OrderDetailsForIdsFilter(ICollection<int> entitiesIds)
        {
            _entitiesIds = entitiesIds;
        }

        public IQueryable<OrderDetails> Execute(IQueryable<OrderDetails> entities)
        {
            return _entitiesIds == null ? entities : entities.Where(e => _entitiesIds.Contains(e.Id));
        }
    }
}
