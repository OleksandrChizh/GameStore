using System.Collections.Generic;

namespace GameStore.Domain.Core.Models
{
    public class Publisher : Entity
    {
        public Publisher(string companyName, string description, string homePage)
        {
            CompanyName = companyName;
            Description = description;
            HomePage = homePage;
        }

        public Publisher()
        {
        }

        public int Id { get; set; }

        public string CompanyName { get; set; }

        public string Description { get; set; }

        public string HomePage { get; set; }

        public virtual ICollection<Game> Games { get; set; } = new List<Game>();

        public virtual ICollection<PublisherTranslation> Translations { get; set; } = new List<PublisherTranslation>();
    }
}
