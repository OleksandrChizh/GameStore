using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using GameStore.WebUI.Models;
using Resources;

namespace GameStore.WebUI.ViewModels.Basket
{
    public class VisaPaymentViewModel : IValidatableObject
    {
        [Required]
        [RegularExpression(@"^\d{16}$", ErrorMessageResourceName = "CardNumberMustContain16Digits", ErrorMessageResourceType = typeof(Resource))]
        [Display(Name = "CardNumber", ResourceType = typeof(Resource))]
        public string CardNumber { get; set; }

        [Required]
        [RegularExpression(@"^\w+\s\w+$", ErrorMessageResourceName = "FullNameExample", ErrorMessageResourceType = typeof(Resource))]
        [Display(Name = "CartHoldersName", ResourceType = typeof(Resource))]
        public string CartHoldersName { get; set; }

        [Required]
        [RegularExpression(@"^\d{2,3}$", ErrorMessageResourceName = "Cvv2Contains2Or3Digits", ErrorMessageResourceType = typeof(Resource))]
        public string CVV2 { get; set; }

        [Required]
        [RegularExpression(@"^\d{2}/\d{2}$", ErrorMessageResourceName = "ExampleExpireDate", ErrorMessageResourceType = typeof(Resource))]
        [Display(Name = "ExpiryDate", ResourceType = typeof(Resource))]
        public string ExpireDate { get; set; }

        [Required]
        [Display(Name = "ConfirmationType", ResourceType = typeof(Resource))]
        public ConfirmationType ConfirmationType { get; set; }

        [RegularExpression(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$", ErrorMessageResourceName = "IncorrectEmail", ErrorMessageResourceType = typeof(Resource))]
        [Display(Name = "Email", ResourceType = typeof(Resource))]
        public string Email { get; set; }

        [RegularExpression(@"^\+\d{10,12}$", ErrorMessageResourceName = "PhoneNumberExample", ErrorMessageResourceType = typeof(Resource))]
        [Display(Name = "PhoneNumber", ResourceType = typeof(Resource))]
        public string PhoneNumber { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var errors = new List<ValidationResult>();
           
            if (ConfirmationType == ConfirmationType.UsingEmail)
            {
                if (string.IsNullOrEmpty(Email))
                {
                    errors.Add(new ValidationResult(Resource.EmailRequired, new[] { "Email" }));
                }
            }

            if (ConfirmationType == ConfirmationType.UsingPhoneNumber)
            {
                if (string.IsNullOrEmpty(PhoneNumber))
                {
                    errors.Add(new ValidationResult(Resource.PhoneNumberRequired, new[] { "PhoneNumber" }));
                }
            }

            string[] expirationDateData = ExpireDate.Split('/');
            int expirationMonth = int.Parse(expirationDateData[0]);
            int expirationYear = 2000 + int.Parse(expirationDateData[1]);
            var expiryDate = new DateTime(expirationYear, expirationMonth, 1);

            if (expiryDate <= DateTime.UtcNow)
            {
                errors.Add(new ValidationResult(Resource.YourCreditCardExpirationDateIsInThePast, new[] { "ExpireDate" }));
            }

            return errors;
        }
    }
}