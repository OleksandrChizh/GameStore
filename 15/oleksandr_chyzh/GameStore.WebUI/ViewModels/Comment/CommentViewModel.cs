using System.ComponentModel.DataAnnotations;
using Resources;

namespace GameStore.WebUI.ViewModels.Comment
{
    public class CommentViewModel
    {
        public CommentViewModel(int id, string name, string body, string repliedTo, string quote, bool isQuote)
        {
            Id = id;
            Name = name;
            Body = body;
            RepliedTo = repliedTo;
            Quote = quote;
            IsQuote = isQuote;
        }

        public int Id { get; private set; }

        [Display(Name = "Name", ResourceType = typeof(Resource))]
        public string Name { get; private set; }

        [Display(Name = "Body", ResourceType = typeof(Resource))]
        public string Body { get; private set; }

        public string RepliedTo { get; private set; }

        public string Quote { get; private set; }

        public bool IsQuote { get; private set; }
    }
}