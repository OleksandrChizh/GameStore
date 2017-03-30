using System.Linq;
using GameStore.Domain.Core.Models;
using GameStore.Domain.Interfaces;
using GameStore.Services.Interfaces.Utils;

namespace GameStore.Infrastructure.DataAccess.Filters
{
    public class GamesPaginationFilter : IFilter<Game>
    {
        private readonly PageInfo _pageInfo;

        public GamesPaginationFilter(PageInfo pageInfo)
        {
            _pageInfo = pageInfo;
        }

        public IQueryable<Game> Execute(IQueryable<Game> entities)
        {
            _pageInfo.TotalItems = entities.Count();
            int pageNumber = _pageInfo.PageNumber;
            var pageSize = (int)_pageInfo.PageSize;

            return entities.Skip(pageSize * (pageNumber - 1)).Take(pageSize);
        }
    }
}
