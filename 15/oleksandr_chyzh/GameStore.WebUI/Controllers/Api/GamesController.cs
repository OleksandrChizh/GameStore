using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using GameStore.Services.Interfaces;
using GameStore.Services.Interfaces.Enums;
using GameStore.Services.Interfaces.Utils;
using GameStore.WebUI.Attributes;
using GameStore.WebUI.Models.Game;
using GameStore.WebUI.Utils;

namespace GameStore.WebUI.Controllers.Api
{
    [ApiErrorHandler]
    public class GamesController : BaseApiController
    {
        private readonly IGameService _gameService;

        public GamesController(IGameService gameService)
        {   
            _gameService = gameService;
        }

        [HttpGet]
        [Route("api/{lang}/publishers/{id}/games")]
        public HttpResponseMessage GetGamesByPublisher([FromUri] int id)
        {
            return Request.CreateResponse(HttpStatusCode.OK, _gameService.GetGamesByPublisher(id));
        }

        [HttpGet]
        [Route("api/{lang}/genres/{id}/games")]
        public HttpResponseMessage GetGamesByGenre([FromUri] int id)
        {
            return Request.CreateResponse(HttpStatusCode.OK, _gameService.GetGamesByGenre(id));
        }

        public HttpResponseMessage Get([FromUri] int id)
        {
            return Request.CreateResponse(HttpStatusCode.OK, _gameService.Get(id));
        }

        public HttpResponseMessage Get([FromUri] GamesFilterModel model)
        {
            SetUpModelForGamesFilter(model);

            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            GamesFilterAttributes attributes = GetAttributesFromModel(model);
            model.Games = _gameService.GetGames(attributes).ToList();

            return Request.CreateResponse(HttpStatusCode.OK, model);
        }

        [ApiAuthorize(Roles = "Manager")]
        public HttpResponseMessage Post([FromBody] UpdateGameModel model)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            _gameService.Edit(model.ToEditingDto());

            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [ApiAuthorize(Roles = "Manager")]
        public HttpResponseMessage Put([FromBody] CreateGameModel model)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            int gameId = _gameService.Create(model.ToCreatingDto());

            return Request.CreateResponse(HttpStatusCode.Created, gameId);
        }

        [ApiAuthorize(Roles = "Manager")]
        public HttpResponseMessage Delete([FromUri] int id)
        {
            _gameService.Delete(id);

            return Request.CreateResponse(HttpStatusCode.OK);
        }

        private GamesFilterAttributes GetAttributesFromModel(GamesFilterModel model)
        {
            var attributes = new GamesFilterAttributes
            {
                Genres = model.SelectedGenresIds.ToList(),
                PlatformTypes = model.SelectedPlatformTypesIds.ToList(),
                Publishers = model.SelectedPublishersIds.ToList(),
                SortingObject = model.SortingObject,
                PublishingDatePeriod = model.PublishingDatePeriod,
                MinPrice = model.MinPrice,
                MaxPrice = model.MaxPrice,
                GameNameSearchingString = model.GameName,
                PageInfo = model.PageInfo
            };

            return attributes;
        }

        private void SetUpModelForGamesFilter(GamesFilterModel model)
        {
            const int startMaxPrice = 1000000;

            model.SelectedGenresIds = model.SelectedGenresIds ?? new int[] { };
            model.SelectedPlatformTypesIds = model.SelectedPlatformTypesIds ?? new int[] { };
            model.SelectedPublishersIds = model.SelectedPublishersIds ?? new int[] { };

            if (model.MaxPrice == default(decimal))
            {
                model.MaxPrice = startMaxPrice;
            }

            model.PageInfo = model.PageInfo ?? new PageInfo
            {
                PageNumber = 1,
                PageSize = PageSize.AllItems
            };

            model.PageInfo.TotalItems = _gameService.GetAmountOfGames();

            PageInfo pageInfo = model.PageInfo;
            int lastPageNumber = pageInfo.TotalItems / (int)pageInfo.PageSize;
            if (pageInfo.TotalItems % (int)pageInfo.PageSize != 0)
            {
                lastPageNumber++;
            }

            if (pageInfo.PageNumber > lastPageNumber)
            {
                model.PageInfo.PageNumber = lastPageNumber;
            }
        }
    }
}