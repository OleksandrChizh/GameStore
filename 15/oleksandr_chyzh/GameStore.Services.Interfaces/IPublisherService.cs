using System.Collections.Generic;
using GameStore.Services.Dto;
using GameStore.Services.Interfaces.Validators;

namespace GameStore.Services.Interfaces
{
    public interface IPublisherService : IDomainEntityService<PublisherDto, int>
    {
        PublisherDto GetPublisherByCompanyName([NonEmptyString] string companyName);

        int Create(
            [StringWithLength(MaxLength = 50)] string companyName,
            [KeysValuesStringsWithLenghts(MaxKeyLength = 2, MaxValueLength = 200)] IDictionary<string, string> languagesDescriptions, 
            [StringWithLength(MaxLength = 50)] string homePage);

        void Update(
            int id,
            [KeysValuesStringsWithLenghts(MaxKeyLength = 2, MaxValueLength = 200)] IDictionary<string, string> languagesDescriptions,
            [StringWithLength(MaxLength = 50)] string homePage);

        void Delete(int id);

        IEnumerable<PublisherDto> GetAllPublishers();
    }
}
