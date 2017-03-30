using System.ComponentModel.DataAnnotations;
using Resources;

namespace GameStore.WebUI.ViewModels.User
{
    public class LoginViewModel
    {
        [Required]
        [Display(Name = "Email", ResourceType = typeof(Resource))]
        [DataType(DataType.EmailAddress, ErrorMessage = "InvalidEmail", ErrorMessageResourceType = typeof(Resource))]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password", ResourceType = typeof(Resource))]
        [RegularExpression("^[0-9a-zA-Z]{6,30}$", ErrorMessage = "InvalidPassword", ErrorMessageResourceType = typeof(Resource))]
        public string Password { get; set; }
    }
}