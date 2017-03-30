using System.Collections.Generic;
using GameStore.Domain.Core.Models;

namespace GameStore.Domain.Core.Utils
{
    public class PublisherEqualityComparer : IEqualityComparer<Publisher>
    {
        public bool Equals(Publisher first, Publisher second)
        {
            return first.CompanyName == second.CompanyName;
        }

        public int GetHashCode(Publisher publisher)
        {
            return publisher.CompanyName.GetHashCode();
        }
    }
}
