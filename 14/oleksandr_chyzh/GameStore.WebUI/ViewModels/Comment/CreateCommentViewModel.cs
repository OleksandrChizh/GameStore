using System.ComponentModel.DataAnnotations;
using Resources;

namespace GameStore.WebUI.ViewModels.Comment
{
    public class CreateCommentViewModel
    {
        public bool IsQuote { get; set; }

        public int? ParentCommentId { get; set; }

        public string GameKey { get; set; }

        [Required]
        [StringLength(200, ErrorMessageResourceName = "MaxCommentBodyLength200", ErrorMessageResourceType = typeof(Resource))]
        [DataType(DataType.MultilineText)]
        [Display(Name = "Body", ResourceType = typeof(Resource))]
        public string Body { get; set; }
    }
}