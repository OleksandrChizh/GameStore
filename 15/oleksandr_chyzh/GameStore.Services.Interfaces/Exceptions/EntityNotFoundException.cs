using System;

namespace GameStore.Services.Interfaces.Exceptions
{
    public class EntityNotFoundException : Exception
    {
        public EntityNotFoundException(Type entityType, string message) : base(message)
        {
            EntityType = entityType;
        }

        public EntityNotFoundException(Type entityType)
        {
            EntityType = entityType;
        }

        public EntityNotFoundException()
        {
        }

        public Type EntityType { get; private set; }
    }
}
