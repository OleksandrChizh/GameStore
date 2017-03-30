using System.Collections.Generic;
using GameStore.Services.Dto;
using GameStore.Services.Interfaces.Validators;

namespace GameStore.Services.Interfaces
{
    public interface IGenreService : IDomainEntityService<GenreDto, int>
    {
        IEnumerable<GenreDto> GetAllGenres();

        IEnumerable<GenreDto> GetDirectSubGenres(int genreId);

        int Create(
            [NonEmptyString] string name,
            int? parentGenreId);

        void Delete(int id);
    }
}
