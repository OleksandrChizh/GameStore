using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using Resources;

namespace GameStore.WebUI.ViewModels.Game
{
    public class CreateGameViewModel : IValidatableObject
    {
        [Required]
        [Display(Name = "Key", ResourceType = typeof(Resource))]
        [StringLength(30, ErrorMessageResourceName = "MaxKeyLength30", ErrorMessageResourceType = typeof(Resource))]
        public string Key { get; set; }

        [Required]
        [Display(Name = "Name", ResourceType = typeof(Resource))]
        [StringLength(70, ErrorMessageResourceName = "MaxNameLenght70", ErrorMessageResourceType = typeof(Resource))]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Description", ResourceType = typeof(Resource))]
        [StringLength(500, ErrorMessageResourceName = "MaxDescriptionLength500", ErrorMessageResourceType = typeof(Resource))]
        public string Description { get; set; }

        [Required]
        [Display(Name = "Price", ResourceType = typeof(Resource))]
        public decimal Price { get; set; }

        [Display(Name = "UnitsInStock", ResourceType = typeof(Resource))]
        public short UnitsInStock { get; set; }

        [Display(Name = "Discounted", ResourceType = typeof(Resource))]
        public bool Discounted { get; set; } 

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "PublishingDate", ResourceType = typeof(Resource))]
        public DateTime PublishingDate { get; set; }

        [Display(Name = "Genres", ResourceType = typeof(Resource))]
        public List<int> GenresIds { get; set; } = new List<int>();

        [Display(Name = "PlatformTypes", ResourceType = typeof(Resource))]
        public List<int> PlatformTypesIds { get; set; } = new List<int>();

        [Display(Name = "Publishers", ResourceType = typeof(Resource))]
        public List<int> PublishersIds { get; set; } = new List<int>();

        public MultiSelectList Genres { get; set; }

        public MultiSelectList PlatformTypes { get; set; }

        public MultiSelectList Publishers { get; set; }

        [Display(Name = "Name", ResourceType = typeof(Resource))]
        public string EnglishName { get; set; }

        [Display(Name = "Description", ResourceType = typeof(Resource))]
        public string EnglishDescription { get; set; }

        [Display(Name = "ContainEnglishTranslation", ResourceType = typeof(Resource))]
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