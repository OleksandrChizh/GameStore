using System;
using GameStore.Services.Interfaces.Exceptions;

namespace GameStore.Services.Interfaces.Validators
{
    public class ExistenEnumAttribute : ArgumentValidationAttribute
    {
        public override void ValidateArgument(ArgumentExceptionDetails details)
        {
            var value = (Enum)details.Value;
            if (!Enum.IsDefined(value.GetType(), value))
            {
                details.Message = "Undefined value of enum";
                throw new InvalidArgumentException(details);
            }
        }
    }
}
