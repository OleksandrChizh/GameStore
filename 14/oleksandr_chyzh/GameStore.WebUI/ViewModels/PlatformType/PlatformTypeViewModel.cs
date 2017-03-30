using System.ComponentModel.DataAnnotations;
using Resources;

namespace GameStore.WebUI.ViewModels.PlatformType
{
    public class PlatformTypeViewModel
    {
        public PlatformTypeViewModel(int id, string type)
        {
            Id = id;
            Type = type;
        }

        public PlatformTypeViewModel()
        {
        }

        public int Id { get; set; }

        [Required]
        [Display(Name = "Name", ResourceType = typeof(Resource))]
        [MaxLength(30, ErrorMessage = "MaxPlatformTypeLength50", ErrorMessageResourceType = typeof(Resource))]
        public string Type { get; set; }
    }
}