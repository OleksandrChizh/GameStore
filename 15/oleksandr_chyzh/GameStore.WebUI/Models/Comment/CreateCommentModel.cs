using System.ComponentModel.DataAnnotations;

namespace GameStore.WebUI.Models.Comment
{
    public class CreateCommentModel
    {
        [Required]
        [StringLength(200)]
        public string Body { get; set; }

        public bool IsQuote { get; set; }

        public int? ParentCommentId { get; set; }

        [Required]
        public string GameKey { get; set; }
    }
}