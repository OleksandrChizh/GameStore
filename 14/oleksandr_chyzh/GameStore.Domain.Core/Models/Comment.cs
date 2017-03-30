namespace GameStore.Domain.Core.Models
{
    public class Comment : Entity
    {
        public Comment(string name, string body, Comment parentComment, Game game, bool isQuote)
        {
            Name = name;
            Body = body;
            ParentComment = parentComment;
            Game = game;
            IsQuote = isQuote;
        }

        public Comment()
        {
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public string Body { get; set; }

        public bool IsQuote { get; set; }

        public virtual Comment ParentComment { get; set; }

        public virtual Game Game { get; set; }
    }
}
