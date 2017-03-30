using System.Collections.Generic;
using GameStore.Services.Interfaces.Exceptions;

namespace GameStore.Services.Interfaces.Validators
{
    public class KeysValuesStringsWithLenghtsAttribute : ArgumentValidationAttribute
    {
        public int MaxKeyLength { get; set; }

        public int MaxValueLength { get; set; }

        public override void ValidateArgument(ArgumentExceptionDetails details)
        {
            var dictionary = (IDictionary<string, string>)details.Value;

            if (dictionary == null)
            {
                details.Message = "Dictionary must be not nullable";
                throw new InvalidArgumentException();
            }

            foreach (KeyValuePair<string, string> pair in dictionary)
            {
                if (pair.Key.Length > MaxKeyLength)
                {
                    details.Message = "Key length is not available";
                    throw new InvalidArgumentException(details);
                }

                if (pair.Value.Length > MaxValueLength)
                {
                    details.Message = "Value length is not available";
                    throw new InvalidArgumentException(details);
                }
            }
        }
    }
}
