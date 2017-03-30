using System.Collections.Generic;
using GameStore.Services.Dto.Utils;

namespace GameStore.Services.Dto
{
    public class PublisherDto
    {
        public PublisherDto(int id, string companyName, IDictionary<string, string> languagesDescriptions, string homePage)
        {
            Id = id;
            CompanyName = companyName;
            LanguagesDescriptions = languagesDescriptions;
            HomePage = homePage;
        }

        public PublisherDto()
        {
        }

        public int Id { get;  set; }

        public string CompanyName { get; set; }

        public IDictionary<string, string> LanguagesDescriptions { get; set; }

        public string HomePage { get; set; }

        public bool Equals(PublisherDto publisher)
        {
            return CompanyName == publisher.CompanyName &&
                   HomePage == publisher.HomePage &&
                   LanguagesDescriptions.IsEqual(publisher.LanguagesDescriptions);
        }
    }
}
