using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GameStore.Infrastructure.PaymentService.Models
{
    public class TransferInformation : IValidatableObject
    {        
        [Required]
        [RegularExpression(@"\d{16}")]
        public string CardNumber { get; set; }

        [Required]
        public string FullName { get; set; }

        [Required]
        [RegularExpression(@"\d{2,3}")]
        public string CvvCode { get; set; }

        [Range(1, 12)]
        public int ExpirationMonth { get; set; }

        [Range(2000, 2100)]
        public int ExpirationYear { get; set; }

        [Required]
        public string Purpose { get; set; }

        [Range(0, double.MaxValue)]
        public decimal AmountOfPayment { get; set; }

        [RegularExpression(@"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*")]
        public string Email { get; set; }

        [RegularExpression(@"\+\d{10,12}")]
        public string PhoneNumber { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var errors = new List<ValidationResult>();

            var expirationDate = new DateTime(ExpirationYear, ExpirationMonth, 1);
            if (expirationDate <= DateTime.UtcNow)
            {
                errors.Add(new ValidationResult("Incorrect expiration date"));
            }

            return errors;
        }
    }
}