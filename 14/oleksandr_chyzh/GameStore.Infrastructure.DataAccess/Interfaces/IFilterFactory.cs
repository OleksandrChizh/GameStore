using System.Collections.Generic;
using GameStore.Domain.Core.Models;
using GameStore.Domain.Interfaces;
using GameStore.Services.Interfaces.Enums;
using GameStore.Services.Interfaces.Utils;

namespace GameStore.Infrastructure.DataAccess.Interfaces
{
    public interface IFilterFactory
    {
        IFilter<PlatformType> MakePlatformTypesForIdsFilter(ICollection<int> entitiesIds);

        IFilter<Genre> MakeGenresForIdsFilter(ICollection<int> entitiesIds);

        IFilter<Publisher> MakePublishersForIdsFilter(ICollection<int> entitiesIds);

        IFilter<OrderDetails> MakeOrderDetailsForIdsFilter(ICollection<int> entitiesIds);
                      
        IFilter<Game> MakeGamesByGameNameFilter(string gameName);                   
                      
        IFilter<Game> MakeGamesByGenresWithIdsFilter(ICollection<int> genresIds);                    
                      
        IFilter<Game> MakeGamesByPlatformTypesWithIdsFilter(ICollection<int> platformTypesIds);
                      
        IFilter<Game> MakeGamesByPriceRangeFilter(decimal minPrice, decimal maxPrice);
                      
        IFilter<Game> MakeGamesByPublishersWithIdsFilter(ICollection<int> publishersIds);
                      
        IFilter<Game> MakeGamesByPublishingDateFilter(PublishingDatePeriod period);
                      
        IFilter<Game> MakeGamesBySortingObjectFilter(SortingObject sortingObject);
                      
        IFilter<Game> MakeGamesPaginationFilter(PageInfo pageInfo);
                      
        IFilter<Game> MakeSortedGamesFilter();
    }
}
