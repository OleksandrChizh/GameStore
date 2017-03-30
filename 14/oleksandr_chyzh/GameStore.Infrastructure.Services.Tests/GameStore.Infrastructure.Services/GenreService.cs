using System.Collections.Generic;
using System.Linq;
using GameStore.Domain.Core.Models;
using GameStore.Domain.Interfaces;
using GameStore.Services.Dto;
using GameStore.Services.Dto.Utils;
using GameStore.Services.Interfaces;
using GameStore.Services.Interfaces.Exceptions;

namespace GameStore.Infrastructure.Services
{
    public class GenreService : Service, IGenreService
    {
        public GenreService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public GenreDto Get(int id)
        {
            return GetGenre(id).ToDto();
        }

        public IEnumerable<GenreDto> GetAllGenres()
        {
            IEnumerable<Genre> genres = UnitOfWork.Genres.GetAll();
            return genres.Select(g => g.ToDto());
        }

        public int Create(string name, int? parentGenreId)
        {
            Genre parentGenre = null;
            if (parentGenreId != null)
            {
                parentGenre = GetGenre((int)parentGenreId);
            }

            Genre genre = UnitOfWork.Genres.SingleOrDefaultDeleted(f => f.Name == name);
            if (genre == null)
            {
                genre = new Genre(name, parentGenre);
                UnitOfWork.Genres.Create(genre);
            }
            else
            {
                genre.Deleted = false;

                if (genre.ParentGenre != null)
                {
                    Genre oldParentGenre = genre.ParentGenre;
                    genre.ParentGenre = null;
                    UnitOfWork.Genres.Update(oldParentGenre);
                    UnitOfWork.Save();
                    UnitOfWork.Genres.Update(genre);
                }

                genre.ParentGenre = parentGenre;
            }

            UnitOfWork.Save();

            return genre.Id;
        }

        public void Delete(int id)
        {
            Genre genre = GetGenre(id);
            DeleteChildGenres(genre);

            UnitOfWork.Genres.Delete(genre);
            UnitOfWork.Save();
        }

        private void DeleteChildGenres(Genre genre)
        {
            List<Genre> childGenres = UnitOfWork.Genres.Find(g => g.ParentGenre.Id == genre.Id).ToList();

            foreach (var childGenre in childGenres)
            {
                DeleteChildGenres(childGenre);
                UnitOfWork.Genres.Delete(childGenre);
            }
        }

        private Genre GetGenre(int id)
        {
            Genre genre = UnitOfWork.Genres.Get(id);
            if (genre == null)
            {
                throw new EntityNotFoundException(typeof(Genre));
            }

            return genre;
        }
    }
}
