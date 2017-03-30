using System;
using GameStore.Services.Interfaces.Enums;
using GameStore.Services.Interfaces.Exceptions;
using GameStore.Services.Interfaces.Utils;

namespace GameStore.Services.Interfaces.Validators
{
    public class GamesFilterDataAttribute : ArgumentValidationAttribute
    {
        public override void ValidateArgument(ArgumentExceptionDetails details)
        {
            var value = (GamesFilterAttributes)details.Value;
            if (value == null)
            {
                return;
            }

            SortingObject sortingObject = value.SortingObject;
            PublishingDatePeriod publishingDatePeriod = value.PublishingDatePeriod;
            decimal minPrice = value.MinPrice;
            decimal maxPrice = value.MaxPrice;

            if (!Enum.IsDefined(sortingObject.GetType(), sortingObject) ||
                !Enum.IsDefined(publishingDatePeriod.GetType(), publishingDatePeriod))
            {
                details.Message = "Undefined value of enum";
                throw new InvalidArgumentException(details);
            }

            if (minPrice < 0 || maxPrice < 0)
            {
                details.Message = "Price must be more or equal then zero";
                throw new InvalidArgumentException(details);
            }

            if (minPrice > maxPrice)
            {
                details.Message = "Min price must be less then max price";
                throw new InvalidArgumentException(details);
            }
        }
    }
}
