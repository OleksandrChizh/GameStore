using System.Collections.Generic;
using System.Linq;
using GameStore.Domain.Interfaces;

namespace GameStore.Infrastructure.DataAccess.Implementations
{
    public class Pipeline<T> : IPipeline<T>
    {
        private readonly IList<IFilter<T>> _filters = new List<IFilter<T>>();

        public IPipeline<T> Register(IFilter<T> filter)
        {
            _filters.Add(filter);
            return this;
        }

        public IQueryable<T> Process(IQueryable<T> entities)
        {
            return _filters.Aggregate(entities, (current, filter) => filter.Execute(current));
        }
    }
}
