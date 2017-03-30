using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using GameStore.Services.Dto;
using GameStore.Services.Interfaces;
using GameStore.WebUI.Attributes;
using GameStore.WebUI.Models.Comment;

namespace GameStore.WebUI.Controllers.Api
{
    [ApiErrorHandler]
    public class CommentsController : BaseApiController
    {
        private readonly ICommentService _commentService;
        private readonly IGameService _gameService;

        public CommentsController(
            ICommentService commentService,
            IGameService gameService)
        {
            _commentService = commentService;
            _gameService = gameService;
        }

        public HttpResponseMessage Get([FromUri] int gameId)
        {
            IEnumerable<CommentDto> comments = _commentService.GetAllCommentsByGameId(gameId);

            return Request.CreateResponse(HttpStatusCode.OK, comments);
        }

        public HttpResponseMessage Get([FromUri] int gameId, [FromUri] int id)
        {
            CommentDto comment = _commentService.GetCommentByIdForGame(gameId, id);

            return Request.CreateResponse(HttpStatusCode.OK, comment);
        }

        [ApiAuthorize(Roles = "Guest, User, Moderator")]
        public HttpResponseMessage Post([FromUri] int gameId, [FromBody] CreateCommentModel model)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            GameDto game = _gameService.GetGameByKey(model.GameKey);

            if (game.Deleted && !User.IsInRole("Moderator"))
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            int commentId = _commentService.Create(User.Identity.Name, model.Body, model.ParentCommentId, game.Id, model.IsQuote);

            return Request.CreateResponse(HttpStatusCode.Created, commentId);
        }

        [ApiAuthorize(Roles = "Moderator")]
        public HttpResponseMessage Delete([FromUri] int id)
        {
            _commentService.Delete(id);

            return Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}