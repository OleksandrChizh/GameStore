using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GameStore.WebUI.Models.Publisher
{
    public class UpdatePublisherModel : IValidatableObject
    {
        [Required]
        public int Id { get; set; }

        [MaxLength(500)]
        public string Description { get; set; }

        public string HomePage { get; set; }

        public string EnglishDescription { get; set; }

        public bool IsContainEnglishTranslation { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var errors = new List<ValidationResult>();

            if (IsContainEnglishTranslation)
            {
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
            }

            return errors;
        }
    }
}