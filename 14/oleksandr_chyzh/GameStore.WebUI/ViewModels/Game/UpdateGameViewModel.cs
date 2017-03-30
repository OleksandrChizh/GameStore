using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using Resources;

namespace GameStore.WebUI.ViewModels.Game
{
    public class UpdateGameViewModel
    {
        public UpdateGameViewModel(
            int gameId,
            IDictionary<string, string> languagesNames,
            IDictionary<string, string> languagesDescriptions,
            IList<int> platformTypeIds,
            IList<int> publisherIds,
            IList<int> genreIds,
            decimal price,
            short unitsInStock,
            bool discounted)
        {
            GameId = gameId;
            Price = price;
            UnitsInStock = unitsInStock;
            Discounted = discounted;

            Name = languagesNames["ru"];
            Description = languagesDescriptions["ru"];

            PlatformTypeIds = platformTypeIds;
            PublisherIds = publisherIds;
            GenreIds = genreIds;

            IsContainEnglishTranslation = languagesNames.ContainsKey("en");
            EnglishName = IsContainEnglishTranslation ? languagesNames["en"] : string.Empty;
            EnglishDescription = IsContainEnglishTranslation ? languagesDescriptions["en"] : string.Empty;
        }

        public UpdateGameViewModel()
        {
        }

        public int GameId { get; set; }

        [Required]
        [Display(Name = "Name", ResourceType = typeof(Resource))]
        [StringLength(70, ErrorMessageResourceName = "MaxNameLenght70", ErrorMessageResourceType = typeof(Resource))]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Description", ResourceType = typeof(Resource))]
        [StringLength(500, ErrorMessageResourceName = "MaxDescriptionLength500", ErrorMessageResourceType = typeof(Resource))]
        public string Description { get; set; }

        [Display(Name = "PlatformTypes", ResourceType = typeof(Resource))]
        public IList<int> PlatformTypeIds { get; set; } = new List<int>();

        [Display(Name = "Publishers", ResourceType = typeof(Resource))]
        public IList<int> PublisherIds { get; set; } = new List<int>();

        [Display(Name = "Genres", ResourceType = typeof(Resource))]
        public IList<int> GenreIds { get; set; } = new List<int>();

        public MultiSelectList AllPlatformTypes { get; set; }

        public MultiSelectList AllPublishers { get; set; }

        public MultiSelectList AllGenres { get; set; }

        [Display(Name = "Price", ResourceType = typeof(Resource))]
        public decimal Price { get; set; }

        [Display(Name = "UnitsInStock", ResourceType = typeof(Resource))]
        public short UnitsInStock { get; set; }

        [Display(Name = "Discounted", ResourceType = typeof(Resource))]
        public bool Discounted { get; set; }

        [Display(Name = "Name", ResourceType = typeof(Resource))]
        public string EnglishName { get; set; }

        [Display(Name = "Description", ResourceType = typeof(Resource))]
        public string EnglishDescription { get; set; }

        [Display(Name = "ContainEnglishTranslation", ResourceType = typeof(Resource))]
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