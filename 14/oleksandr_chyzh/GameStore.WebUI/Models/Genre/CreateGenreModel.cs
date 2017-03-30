using System.ComponentModel.DataAnnotations;

namespace GameStore.WebUI.Models.Genre
{
    public class CreateGenreModel
    {
        [Required]
        [MaxLength(70)]
        public string Name { get; set; }

        public int? ParentGenreId { get; set; }
    }
}