using System.Collections.Generic;
using GameStore.Services.Dto;
using GameStore.Services.Interfaces.Utils;
using GameStore.Services.Interfaces.Validators;

namespace GameStore.Services.Interfaces
{
    public interface IGameService : IDomainEntityService<GameDto, int>
    {
        void UpdateImage(
            [NonEmptyString] string key, 
            [NonEmptyString] string imagePath);

        int Create([CreatingGameDtoValidation] CreatingGameDto game);

        void Edit([EditingGameDtoValidation] EditingGameDto game);
       
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