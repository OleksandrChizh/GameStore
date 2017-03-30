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
    public class CommentService : Service, ICommentService
    {
        public CommentService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public int Create(string name, string body, int? parentCommentId, int gameId, bool isQuote)
        {
            Game game = UnitOfWork.Games.Get(gameId);
            if (game == null)
            {
                game = UnitOfWork.Games.GetDeletedEntity(gameId);
                if (game == null)
                {
                    throw new EntityNotFoundException(typeof(Game));
                }
            }

            Comment parentComment = null;
            if (parentCommentId != null)
            {
                parentComment = game.Comments.FirstOrDefault(c => c.Id == parentCommentId && c.Deleted == false);
                if (parentComment == null)
                {
                    throw new EntityNotFoundException(typeof(Comment), "Parent comment not contains in this game");
                }
            }

            var comment = new Comment(name, body, parentComment, game, isQuote);
            UnitOfWork.Comments.Create(comment);
            UnitOfWork.Save();
            return comment.Id;
        }

        public IEnumerable<CommentDto> GetAllCommentsByGameKey(string key)
        {
            Game game = UnitOfWork.Games.SingleOrDefault(g => g.Key == key);
            if (game == null)
            {
                game = UnitOfWork.Games.SingleOrDefaultDeleted(g => g.Key == key);
                if (game == null)
                {
                    throw new EntityNotFoundException(typeof(Game));
                }
            }

            return game.Comments.Where(c => c.Deleted == false).Select(c => c.ToDto());
        }

        public IEnumerable<CommentDto> GetAllCommentsByGameId(int id)
        {
            Game game = UnitOfWork.Games.Get(id);
            if (game == null)
            {
                game = UnitOfWork.Games.GetDeletedEntity(id);
                if (game == null)
                {
                    throw new EntityNotFoundException(typeof(Game));
                }
            }

            return game.Comments.Where(c => !c.Deleted).Select(c => c.ToDto());
        }

        public CommentDto GetCommentByIdForGame(int gameId, int id)
        {
            Game game = UnitOfWork.Games.Get(id);
            if (game == null)
            {
                game = UnitOfWork.Games.GetDeletedEntity(id);
                if (game == null)
                {
                    throw new EntityNotFoundException(typeof(Game));
                }
            }

            Comment comment = game.Comments.SingleOrDefault(c => !c.Deleted && c.Id == id);
            if (comment == null)
            {
                throw new EntityNotFoundException(typeof(Comment));
            }

            return comment.ToDto();
        }

        public void Delete(int commentId)
        {
            Comment comment = GetComment(commentId);
            UnitOfWork.Comments.Delete(comment);
            UnitOfWork.Save();
        }

        public CommentDto Get(int id)
        {
            Comment comment = GetComment(id);
            return comment.ToDto();
        }

        private Comment GetComment(int id)
        {
            Comment comment = UnitOfWork.Comments.Get(id);
            if (comment == null)
            {
                throw new EntityNotFoundException(typeof(Comment));
            }

            return comment;
        }
    }
}
