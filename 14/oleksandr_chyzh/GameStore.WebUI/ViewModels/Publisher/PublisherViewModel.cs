using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Resources;

namespace GameStore.WebUI.ViewModels.Publisher
{
    public class PublisherViewModel : IValidatableObject
    {
        public PublisherViewModel(int id, string companyName, string description, string homePage, string englishDescription)
        {
            Id = id;
            CompanyName = companyName;
            Description = description;
            HomePage = homePage;
            EnglishDescription = englishDescription;
            IsContainEnglishTranslation = !string.IsNullOrEmpty(companyName);
        }

        public PublisherViewModel()
        {
        }

        public int Id { get; set; }

        [Required]
        [MaxLength(70, ErrorMessageResourceName = "MaxNameLenght70", ErrorMessageResourceType = typeof(Resource))]
        [Display(Name = "CompanyName", ResourceType = typeof(Resource))]
        public string CompanyName { get; set; }

        [Display(Name = "Description", ResourceType = typeof(Resource))]
        [MaxLength(500, ErrorMessage = "MaxDescriptionLength500", ErrorMessageResourceType = typeof(Resource))]
        public string Description { get; set; }

        [Display(Name = "HomePage", ResourceType = typeof(Resource))]
        public string HomePage { get; set; }

        [Display(Name = "EnglishDescription", ResourceType = typeof(Resource))]
        public string EnglishDescription { get; set; }

        [Display(Name = "ContainEnglishTranslation", ResourceType = typeof(Resource))]
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