using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using GameStore.Services.Dto;
using GameStore.Services.Interfaces;
using GameStore.WebUI.Attributes;
using GameStore.WebUI.Models.Genre;

namespace GameStore.WebUI.Controllers.Api
{
    [ApiErrorHandler]
    [ApiAuthorize(Roles = "Manager")]
    public class GenresController : BaseApiController
    {
        private readonly IGenreService _genreService;
        private readonly IGameService _gameService;

        public GenresController(IGenreService genreService, IGameService gameService)
        {
            _genreService = genreService;
            _gameService = gameService;
        }

        [HttpGet]
        [OverrideAuthorization]
        [Route("api/games/{id}/genres")]
        public HttpResponseMessage GetGenresByGame([FromUri] int id)
        {
            GameDto game = _gameService.Get(id);

            return Request.CreateResponse(HttpStatusCode.OK, game.Genres.ToList());
        }

        public HttpResponseMessage Get()
        {
            IEnumerable<GenreDto> genres = _genreService.GetAllGenres();

            return Request.CreateResponse(HttpStatusCode.OK, genres);
        }

        public HttpResponseMessage Get([FromUri] int id)
        {
            GenreDto genre = _genreService.Get(id);

            return Request.CreateResponse(HttpStatusCode.OK, genre);
        }

        public HttpResponseMessage Put([FromBody] CreateGenreModel model)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            if (model.ParentGenreId != null && model.ParentGenreId == default(int))
            {
                model.ParentGenreId = null;
            }

            int genreId = _genreService.Create(model.Name, model.ParentGenreId);

            return Request.CreateResponse(HttpStatusCode.Created, genreId);
        }

        public HttpResponseMessage Delete([FromUri] int id)
        {
            _genreService.Delete(id);

            return Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}