using System.Linq;
using GameStore.Domain.Core.Models;
using GameStore.Domain.Interfaces;
using GameStore.Services.Interfaces.Enums;
using GameStore.Services.Interfaces.Exceptions;

namespace GameStore.Infrastructure.DataAccess.Filters
{
    public class GamesBySortingObjectFilter : IFilter<Game>
    {
        private readonly SortingObject _sortingObject;

        public GamesBySortingObjectFilter(SortingObject sortingObject)
        {
            _sortingObject = sortingObject;
        }

        public IQueryable<Game> Execute(IQueryable<Game> entities)
        {
            switch (_sortingObject)
            {
                case SortingObject.MostPopular:
                    entities = entities.OrderByDescending(g => g.ViewsCount).ThenBy(g => g.Id);
                    break;

                case SortingObject.MostCommended:
                    entities = entities.OrderByDescending(g => g.Comments.Count).ThenBy(g => g.Id);
                    break;

                case SortingObject.ByPriceAsc:
                    entities = entities.OrderBy(g => g.Price).ThenBy(g => g.Id);
                    break;

                case SortingObject.ByPriceDesc:
                    entities = entities.OrderByDescending(g => g.Price).ThenBy(g => g.Id);
                    break;

                case SortingObject.New:
                    entities = entities.OrderByDescending(g => g.AddingDate).ThenBy(g => g.Id);
                    break;

                case SortingObject.Default:
                    break;

                default:
                    throw new UndefinedSortingObjectException();
            }

            return entities;
        }
    }
}
