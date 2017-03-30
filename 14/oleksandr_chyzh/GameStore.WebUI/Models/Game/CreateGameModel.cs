using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Resources;

namespace GameStore.WebUI.Models.Game
{
    public class CreateGameModel : IValidatableObject
    {
        [Required]
        [StringLength(30)]
        public string Key { get; set; }

        [Required]
        [StringLength(70)]
        public string Name { get; set; }

        [Required]
        [StringLength(500)]
        public string Description { get; set; }

        [Required]
        public decimal Price { get; set; }

        public short UnitsInStock { get; set; }

        public bool Discounted { get; set; }

        [Required]
        public DateTime PublishingDate { get; set; }

        public List<int> GenresIds { get; set; } = new List<int>();

        public List<int> PlatformTypesIds { get; set; } = new List<int>();

        public List<int> PublishersIds { get; set; } = new List<int>();

        public string EnglishName { get; set; }

        public string EnglishDescription { get; set; }

        public bool IsContainEnglishTranslation { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var errors = new List<ValidationResult>();

            if (PlatformTypesIds.Count == 0)
            {
                errors.Add(new ValidationResult(Resource.GameMustContainAtLeastOnePlatformType, new[] { "PlatformTypesIds" }));
            }

            if (IsContainEnglishTranslation)
            {
                if (string.IsNullOrEmpty(EnglishName))
                {
                    errors.Add(new ValidationResult(Resource.EnglishNameIsRequired, new[] { "EnglishName" }));
                }
                else
                {
                    if (EnglishName.Length > 70)
                    {
                        errors.Add(new ValidationResult(Resource.MaxNameLenght70, new[] { "EnglishName" }));
                    }
                }

                if (string.IsNullOrEmpty(EnglishDescription))
                {
                    errors.Add(new ValidationResult(Resource.EnglishDescriptionIsRequired, new[] { "EnglishDescription" }));
                }
                else
                {
                    if (EnglishDescription.Length > 500)
                    {
                        errors.Add(new ValidationResult(Resource.MaxDescriptionLength500, new[] { "EnglishDescription" }));
                    }
                }
            }

            return errors;
        }
    }
}