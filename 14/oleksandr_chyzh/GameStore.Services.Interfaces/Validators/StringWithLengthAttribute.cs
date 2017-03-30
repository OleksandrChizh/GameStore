using GameStore.Services.Interfaces.Exceptions;

namespace GameStore.Services.Interfaces.Validators
{
    public class StringWithLengthAttribute : NonEmptyStringAttribute
    {
        public int MaxLength { get; set; }

        public override void ValidateArgument(ArgumentExceptionDetails details)
        {
            base.ValidateArgument(details);
            var value = (string)details.Value;
            if (value.Length > MaxLength)
            {
                details.Message = $"String length must be less or equal {MaxLength}";
                throw new InvalidArgumentException(details);
            }
        }
    }
}
