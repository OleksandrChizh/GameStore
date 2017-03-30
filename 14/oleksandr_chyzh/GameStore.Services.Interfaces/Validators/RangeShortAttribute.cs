using System;
using GameStore.Services.Interfaces.Exceptions;

namespace GameStore.Services.Interfaces.Validators
{
    public class RangeShortAttribute : ArgumentValidationAttribute
    {
        public short MinValue { get; set; } = 0;

        public short MaxValue { get; set; } = short.MaxValue;

        public bool IncludingMin { get; set; } = true;

        public bool IncludingMax { get; set; } = true;

        public override void ValidateArgument(ArgumentExceptionDetails details)
        {
            try
            {
                CheckValue((short)details.Value);
            }
            catch (ArgumentException e)
            {
                details.Message = e.Message;
                throw new InvalidArgumentException(details);
            }
        }

        protected void CheckValue(short value)
        {
            if ((IncludingMin && value.CompareTo(MinValue) < 0) ||
                (!IncludingMin && value.CompareTo(MinValue) <= 0))
            {
                throw new ArgumentException("Minimal range violated");
            }

            if ((IncludingMax && value.CompareTo(MaxValue) > 0) ||
                (!IncludingMax && value.CompareTo(MaxValue) >= 0))
            {
                throw new ArgumentException("Maximal range violated");
            }
        }
    }
}
