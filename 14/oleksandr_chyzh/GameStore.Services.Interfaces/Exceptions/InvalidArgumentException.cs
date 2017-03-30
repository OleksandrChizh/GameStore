using System;

namespace GameStore.Services.Interfaces.Exceptions
{
    public class InvalidArgumentException : Exception
    {
        public InvalidArgumentException(ArgumentExceptionDetails details)
        {
            Details = details;
        }

        public InvalidArgumentException()
        {
        }

        public ArgumentExceptionDetails Details { get; set; }
    }
}
