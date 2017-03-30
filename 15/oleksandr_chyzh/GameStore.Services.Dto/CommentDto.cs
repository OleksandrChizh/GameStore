namespace GameStore.Services.Dto
{
    public class CommentDto 
    {
        public CommentDto(int id, string name, string body, int gameId, int? parentCommentId, string repliedTo, string quote, bool isQuote)
        {
            Id = id;
            Name = name;
            Body = body;
            GameId = gameId;
            ParentCommentId = parentCommentId;
            RepliedTo = repliedTo;
            Quote = quote;
            IsQuote = isQuote;
        }

        public int Id { get; private set; }

        public string Name { get; private set; }

        public string Body { get; private set; }

        public int GameId { get; private set; }

        public int? ParentCommentId { get; private set; }

        public string RepliedTo { get; private set; }

        public string Quote { get; private set; }

        public bool IsQuote { get; private set; }

        public bool Equals(CommentDto comment)
        {
            return Name == comment.Name && Body == comment.Body;
        }
    }
}
