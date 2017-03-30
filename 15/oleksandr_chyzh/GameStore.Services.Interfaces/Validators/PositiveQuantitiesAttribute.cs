using System.Collections.Generic;
using GameStore.Services.Interfaces.Exceptions;

namespace GameStore.Services.Interfaces.Validators
{
    public class PositiveQuantitiesAttribute : ArgumentValidationAttribute
    {
        public override void ValidateArgument(ArgumentExceptionDetails details)
        {
            IDictionary<int, short> productQuantity = details.Value as IDictionary<int, short>;

            foreach (var pair in productQuantity)
            {
                if (pair.Value <= 0)
                {
                    details.Message = "Quantity must be positive";
                    throw new InvalidArgumentException(details);
                }
            }
        }
    }
}
