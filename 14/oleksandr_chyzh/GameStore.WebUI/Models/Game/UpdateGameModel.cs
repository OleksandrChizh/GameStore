using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GameStore.WebUI.Models.Game
{
    public class UpdateGameModel : IValidatableObject
    {
        public int GameId { get; set; }

        [Required]
        [StringLength(70)]
        public string Name { get; set; }

        [Required]
        [StringLength(500)]
        public string Description { get; set; }

        [Required]
        public IList<int> PlatformTypeIds { get; set; } = new List<int>();

        [Required]
        public IList<int> PublisherIds { get; set; } = new List<int>();

        [Required]
        public IList<int> GenreIds { get; set; } = new List<int>();

        public decimal Price { get; set; }

        public short UnitsInStock { get; set; }

        public bool Discounted { get; set; }

        public string EnglishName { get; set; }

        public string EnglishDescription { get; set; }

        public bool IsContainEnglishTranslation { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var errors = new List<ValidationResult>();

            if (!IsContainEnglishTranslation)
            {
                return errors;
            }

            if (string.IsNullOrEmpty(EnglishName))
            {
                errors.Add(new ValidationResult("English name is required", new[] { "EnglishName" }));
            }
            else
            {
                if (EnglishName.Length > 70)
                {
                    errors.Add(new ValidationResult("Max name lenght: 70", new[] { "EnglishName" }));
                }
            }

            if (string.IsNullOrEmpty(EnglishDescription))
            {
                errors.Add(new ValidationResult("English description is required", new[] { "EnglishDescription" }));
            }
            else
            {
                if (EnglishDescription.Length > 500)
                {
                    errors.Add(new ValidationResult("Max description lenght: 500", new[] { "EnglishDescription" }));
                }
            }

            return errors;
        }
    }
}