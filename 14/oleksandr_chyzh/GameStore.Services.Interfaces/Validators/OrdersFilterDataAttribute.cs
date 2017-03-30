using GameStore.Services.Interfaces.Exceptions;
using GameStore.Services.Interfaces.Utils;

namespace GameStore.Services.Interfaces.Validators
{
    public class OrdersFilterDataAttribute : ArgumentValidationAttribute
    {
        public override void ValidateArgument(ArgumentExceptionDetails details)
        {
            var value = (OrdersFilterAttributes)details.Value;
            if (value == null)
            {
                return;
            }
            
            if (value.From > value.To)
            {
                details.Message = "Date 'To' must be after 'From'";
                throw new InvalidArgumentException(details);
            }
        }
    }
}
