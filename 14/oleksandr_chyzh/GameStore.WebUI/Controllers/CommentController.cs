using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Web.Mvc;
using GameStore.Services.Dto;
using GameStore.Services.Interfaces;
using GameStore.WebUI.Attributes;
using GameStore.WebUI.Exceptions;
using GameStore.WebUI.Utils;
using GameStore.WebUI.ViewModels.Comment;

namespace GameStore.WebUI.Controllers
{
    [ErrorLogger]
    [PerfomanceCalculator]
    public class CommentController : BaseController
    {
        private readonly ICommentService _commentService;
        private readonly IGameService _gameService;

        public CommentController(
            ICommentService commentService,
            IGameService gameService)
        {
            _commentService = commentService;
            _gameService = gameService;
        }

        [HttpGet]
        [ActionName("Comments")]
        public ViewResult GetAllCommentsForGame(string gameKey)
        {
            string currentCulture = Thread.CurrentThread.CurrentCulture.Name.Substring(0, 2);

            GameDto game = _gameService.GetGameByKey(gameKey);
            IEnumerable<CommentDto> comments = _commentService.GetAllCommentsByGameKey(gameKey);
            var result = new CommentsViewModel
            {
                GameName = game.LanguagesNames.ContainsKey(currentCulture) ? game.LanguagesNames[currentCulture] : game.LanguagesNames["ru"],
                GameKey = gameKey,
                IsGameDeleted = game.Deleted,
                Comments = comments.Select(c => c.ToViewModel()).ToList()
            };

            return View(result);
        }

        [HttpGet]
        [ActionName("NewComment")]
        [MvcAuthorise(Roles = "Guest, User, Moderator")]
        public PartialViewResult AddNewCommentForGame(string gameKey, int? parentCommentId, bool isQuote)
        {
            var model = new CreateCommentViewModel { GameKey = gameKey, IsQuote = isQuote };
            if (parentCommentId != null)
            {
                model.ParentCommentId = parentCommentId;
            }

            return PartialView(model);
        }

        [HttpPost]
        [EventLogger]
        [ActionName("NewComment")]
        [MvcAuthorise(Roles = "Guest, User, Moderator")]
        public ActionResult AddNewCommentForGame(CreateCommentViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return PartialView(model);
            }

            GameDto game = _gameService.GetGameByKey(model.GameKey);
            if (game.Deleted && !User.IsInRole("Moderator"))
            {
                throw new GameDeletedException();
            }

            _commentService.Create(User.Identity.Name, model.Body, model.ParentCommentId, game.Id, model.IsQuote);

            return PartialView("CommentAdded");
        }

        [HttpPost]
        [EventLogger]
        [MvcAuthorise(Roles = "Moderator")]
        public ActionResult DeleteComment(int commentId)
        {
            _commentService.Delete(commentId);
            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }
    }
}