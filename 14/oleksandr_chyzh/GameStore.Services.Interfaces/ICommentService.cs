using System.Collections.Generic;
using GameStore.Services.Dto;
using GameStore.Services.Interfaces.Validators;

namespace GameStore.Services.Interfaces
{
    public interface ICommentService : IDomainEntityService<CommentDto, int>
    {
        int Create(
            [StringWithLength(MaxLength = 50)] string name, 
            [StringWithLength(MaxLength = 200)] string body, 
            int? parentCommentId, 
            int gameId, 
            bool isQuote);

        IEnumerable<CommentDto> GetAllCommentsByGameKey(string key);

        IEnumerable<CommentDto> GetAllCommentsByGameId(int id);

        CommentDto GetCommentByIdForGame(int gameId, int id);

        void Delete(int commentId);
    }
}
