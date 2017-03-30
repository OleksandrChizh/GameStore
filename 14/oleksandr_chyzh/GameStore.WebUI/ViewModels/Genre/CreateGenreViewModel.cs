using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using Resources;

namespace GameStore.WebUI.ViewModels.Genre
{
    public class CreateGenreViewModel
    {
        [Required]
        [Display(Name = "Name", ResourceType = typeof(Resource))]
        [MaxLength(70, ErrorMessage = "MaxNameLenght70", ErrorMessageResourceType = typeof(Resource))]
        public string Name { get; set; }

        [Display(Name = "ParentGenre", ResourceType = typeof(Resource))]
        public int? ParentGenreId { get; set; }

        public SelectList Genres { get; set; }
    }
}