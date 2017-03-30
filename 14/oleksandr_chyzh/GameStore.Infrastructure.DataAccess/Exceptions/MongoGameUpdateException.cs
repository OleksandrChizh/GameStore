using System;

namespace GameStore.Infrastructure.DataAccess.Exceptions
{
    public class MongoGameUpdateException : Exception
    {
        public MongoGameUpdateException(string message) : base(message)
        {
        }
    }
}
