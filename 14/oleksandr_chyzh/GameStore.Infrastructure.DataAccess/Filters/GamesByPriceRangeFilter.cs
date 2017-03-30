using System.Linq;
using GameStore.Domain.Core.Models;
using GameStore.Domain.Interfaces;

namespace GameStore.Infrastructure.DataAccess.Filters
{
    public class GamesByPriceRangeFilter : IFilter<Game>
    {
        private readonly decimal _minPrice;
        private readonly decimal _maxPrice;

        public GamesByPriceRangeFilter(decimal minPrice, decimal maxPrice)
        {
            _minPrice = minPrice;
            _maxPrice = maxPrice;
        }

        public IQueryable<Game> Execute(IQueryable<Game> entities)
        {
            return entities.Where(g => g.Price >= _minPrice && 
                                       g.Price <= _maxPrice);
        }
    }
}
