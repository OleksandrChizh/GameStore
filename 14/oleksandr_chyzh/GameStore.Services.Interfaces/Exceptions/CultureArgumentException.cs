using System;

namespace GameStore.Services.Interfaces.Exceptions
{
    public class CultureArgumentException : Exception
    {
        public CultureArgumentException(string message)
            : base(message)
        {
        }
    }
}
