using System.ComponentModel.DataAnnotations;
using Resources;

namespace GameStore.WebUI.ViewModels.User
{
    public class RegisterViewModel
    {
        [Display(Name = "Email", ResourceType = typeof(Resource))]
        [Required]
        [DataType(DataType.EmailAddress, ErrorMessage = "InvalidEmail", ErrorMessageResourceType = typeof(Resource))]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password", ResourceType = typeof(Resource))]
        [RegularExpression("^[0-9a-zA-Z]{6,30}$", ErrorMessage = "InvalidPassword", ErrorMessageResourceType = typeof(Resource))]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "ConfirmPassword", ResourceType = typeof(Resource))]
        [Compare("Password", ErrorMessage = "NotEqualPasswords", ErrorMessageResourceType = typeof(Resource))]
        public string ConfirmPassword { get; set; }
    }
}