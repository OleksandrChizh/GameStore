using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using GameStore.Services.Dto;
using GameStore.Services.Interfaces.Enums;
using GameStore.Services.Interfaces.Utils;

namespace GameStore.WebUI.Models.Game
{
    public class GamesFilterModel : IValidatableObject
    {
        public IList<GameDto> Games { get; set; }

        public int[] SelectedGenresIds { get; set; }

        public int[] SelectedPlatformTypesIds { get; set; }

        public int[] SelectedPublishersIds { get; set; }

        public SortingObject SortingObject { get; set; }

        public PublishingDatePeriod PublishingDatePeriod { get; set; }

        [Range(0, int.MaxValue)]
        public decimal MinPrice { get; set; }

        [Range(0, int.MaxValue)]
        public decimal MaxPrice { get; set; }

        [MinLength(3)]
        [MaxLength(100)]
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