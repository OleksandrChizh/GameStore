using System.Collections.Generic;

namespace GameStore.WebUI.ViewModels.Comment
{
    public class CommentsViewModel
    {
        public string GameName { get; set; }

        public string GameKey { get; set; }

        public bool IsGameDeleted { get; set; }

        public IList<CommentViewModel> Comments { get; set; }
    }
}