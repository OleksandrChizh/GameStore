using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Resources;

namespace GameStore.WebUI.ViewModels.Order
{
    public class HistoryViewModel : IValidatableObject
    {
        public HistoryViewModel()
        {
        }

        [DataType(DataType.Date)]
        [Display(Name = "From", ResourceType = typeof(Resource))]
        public DateTime DateFrom { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "To", ResourceType = typeof(Resource))]
        public DateTime DateTo { get; set; }

        public IList<ShortOrderViewModel> Orders { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var errors = new List<ValidationResult>();

            if (DateFrom > DateTo)
            {
                errors.Add(new ValidationResult(Resource.DateFromMustBeEarlierThenDateTo));
            }

            return errors;
        }
    }
}