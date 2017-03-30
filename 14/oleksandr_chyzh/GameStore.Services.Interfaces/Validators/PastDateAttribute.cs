using System;
using GameStore.Services.Interfaces.Exceptions;

namespace GameStore.Services.Interfaces.Validators
{
    public class PastDateAttribute : ArgumentValidationAttribute
    {
        public override void ValidateArgument(ArgumentExceptionDetails details)
        {
            var data = (DateTime)details.Value;
            if (data > DateTime.UtcNow)
            {
                details.Message = "Date(time) must be before current date(time)";
                throw new InvalidArgumentException(details);
            }
        }
    }
}
