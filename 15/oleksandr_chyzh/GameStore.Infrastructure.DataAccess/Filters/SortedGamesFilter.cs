using System.Linq;
using GameStore.Domain.Core.Models;
using GameStore.Domain.Interfaces;

namespace GameStore.Infrastructure.DataAccess.Filters
{
    public class SortedGamesFilter : IFilter<Game>
    {
        public IQueryable<Game> Execute(IQueryable<Game> entities)
        {
            return entities.OrderBy(g => g.Id);
        }
    }
}
