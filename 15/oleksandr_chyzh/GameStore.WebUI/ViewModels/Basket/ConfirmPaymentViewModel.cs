using System.ComponentModel.DataAnnotations;
using Resources;

namespace GameStore.WebUI.ViewModels.Basket
{
    public class ConfirmPaymentViewModel
    {        
        [Required] 
        [Display(Name = "Code", ResourceType = typeof(Resource))]
        [RegularExpression(@"[0-9a-zA-Z]{4}", ErrorMessage = "CodeContains4Symbols", ErrorMessageResourceType = typeof(Resource))]
        public string Code { get; set; }
    }
}