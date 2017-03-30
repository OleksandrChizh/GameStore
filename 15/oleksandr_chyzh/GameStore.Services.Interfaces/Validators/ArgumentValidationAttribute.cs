using System;
using GameStore.Services.Interfaces.Exceptions;

namespace GameStore.Services.Interfaces.Validators
{
    [AttributeUsage(AttributeTargets.Parameter)]
    public abstract class ArgumentValidationAttribute : Attribute
    {
        public abstract void ValidateArgument(ArgumentExceptionDetails details);
    }
}
