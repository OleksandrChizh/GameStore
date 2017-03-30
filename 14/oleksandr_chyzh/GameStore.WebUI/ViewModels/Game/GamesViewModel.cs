using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using GameStore.Services.Interfaces.Enums;
using GameStore.Services.Interfaces.Utils;
using GameStore.WebUI.Models.Game;
using GameStore.WebUI.ViewModels.Genre;
using GameStore.WebUI.ViewModels.PlatformType;
using GameStore.WebUI.ViewModels.Publisher;
using Resources;

namespace GameStore.WebUI.ViewModels.Game
{
    public class GamesViewModel : IValidatableObject
    {
        public GamesResource Resource { get; set; }

        public IList<ShortGameViewModel> Games { get; set; }

        public IList<GenreViewModel> Genres { get; set; }

        public int[] SelectedGenresIds { get; set; }

        public IList<PlatformTypeViewModel> PlatformTypes { get; set; }

        public int[] SelectedPlatformTypesIds { get; set; }

        public IList<ShortPublisherViewModel> Publishers { get; set; }

        public int[] SelectedPublishersIds { get; set; }

        public SortingObject SortingObject { get; set; }

        public PublishingDatePeriod PublishingDatePeriod { get; set; }

        [Display(Name = "Min", ResourceType = typeof(Resource))]
        [Range(0, int.MaxValue)]
        public decimal MinPrice { get; set; }

        [Display(Name = "Max", ResourceType = typeof(Resource))]
        [Range(0, int.MaxValue)]
        public decimal MaxPrice { get; set; }

        [MinLength(3, ErrorMessageResourceName = "MinGameNameLength3", ErrorMessageResourceType = typeof(Resource))]
        [MaxLength(100, ErrorMessageResourceName = "MaxGameNameLength100", ErrorMessageResourceType = typeof(Resource))]
        public string GameName { get; set; }

        public PageInfo PageInfo { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var errors = new List<ValidationResult>();

            if (MinPrice > MaxPrice)
            {
                errors.Add(new ValidationResult(Resources.Resource.MinPriceMustBeLessThenMax, new[] { "MinPrice", "MaxPrice" }));
            }

            return errors;
        }
    }
}