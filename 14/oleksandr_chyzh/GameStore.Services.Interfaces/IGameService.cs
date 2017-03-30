using System;
using System.Collections.Generic;
using GameStore.Services.Dto;
using GameStore.Services.Interfaces.Utils;
using GameStore.Services.Interfaces.Validators;

namespace GameStore.Services.Interfaces
{
    public interface IGameService : IDomainEntityService<GameDto, int>
    {
        int Create(
            [StringWithLength(MaxLength = 20)] string key, 
            [KeysValuesStringsWithLenghtsAttribute(MaxKeyLength = 2, MaxValueLength = 70)] IDictionary<string, string> languagesNames,
            [KeysValuesStringsWithLenghtsAttribute(MaxKeyLength = 2, MaxValueLength = 200)] IDictionary<string, string> languagesDescriptions, 
            [RangeDecimal] decimal price,
            [RangeShort] short unitsInStock,
            bool discounted,
            [PastDate] DateTime publishingDate,
            ICollection<int> genreIds, 
            ICollection<int> platformTypeIds, 
            ICollection<int> publisherIds);

        void Edit(
            int gameId,
            [KeysValuesStringsWithLenghtsAttribute(MaxKeyLength = 2, MaxValueLength = 70)] IDictionary<string, string> languagesNames,
            [KeysValuesStringsWithLenghtsAttribute(MaxKeyLength = 2, MaxValueLength = 200)] IDictionary<string, string> languagesDescriptions,
            [RangeDecimal] decimal price,
            [RangeShort] short unitsInStock,
            bool discounted,
            ICollection<int> platformTypeIds,
            ICollection<int> publisherIds,
            ICollection<int> genreIds);
       
        GameDto GetGameByKey([NonEmptyString] string key);

        IEnumerable<GameDto> GetGamesByPublisher(int id);
            
        IEnumerable<GameDto> GetGamesByGenre([NonEmptyString] string genre);

        IEnumerable<GameDto> GetGamesByGenre(int id);

        IEnumerable<GameDto> GetGamesByPlatformType([NonEmptyString] string type);

        IEnumerable<GameDto> GetGames([GamesFilterData] GamesFilterAttributes attributes);

        IEnumerable<GameDto> GetGamesByIds(ICollection<int> gameIds);

        void Delete(int gameId);

        int GetAmountOfGames();

        IDictionary<string, short> GetInformationAboutGamesDeficit(IDictionary<int, short> gamesQuantity);

        void AddView(int gameId);
    }
}