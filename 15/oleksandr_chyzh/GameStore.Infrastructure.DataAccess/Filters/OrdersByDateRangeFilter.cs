using System;
using System.Linq;
using GameStore.Domain.Core.Models;
using GameStore.Domain.Interfaces;

namespace GameStore.Infrastructure.DataAccess.Filters
{
    public class OrdersByDateRangeFilter : IFilter<Order>
    {
        private readonly DateTime _from;
        private readonly DateTime _to;

        public OrdersByDateRangeFilter(DateTime from, DateTime to)
        {
            _from = from;
            _to = to;
        }

        public IQueryable<Order> Execute(IQueryable<Order> entities)
        {
            return entities.Where(g => g.OrderDate >= _from && g.OrderDate <= _to);
        }
    }
}
