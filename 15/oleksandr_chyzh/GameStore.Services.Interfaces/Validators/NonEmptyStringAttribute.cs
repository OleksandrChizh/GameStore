using GameStore.Services.Interfaces.Exceptions;

namespace GameStore.Services.Interfaces.Validators
{
    public class NonEmptyStringAttribute : ArgumentValidationAttribute
    {
        public override void ValidateArgument(ArgumentExceptionDetails details)
        {
            var value = (string)details.Value;
            if (value == null)
            {
                details.Message = "String must be not null";
                throw new InvalidArgumentException(details);
            }

            if (value.Length == 0)
            {
                details.Message = "String must be not empty";
                throw new InvalidArgumentException(details);
            }
        }
    }
}
