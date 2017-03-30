using System.Collections.Generic;
using System.Linq;
using GameStore.Domain.Core.Models;
using GameStore.Domain.Interfaces;
using GameStore.Services.Dto;
using GameStore.Services.Dto.Utils;
using GameStore.Services.Interfaces;
using GameStore.Services.Interfaces.Exceptions;

namespace GameStore.Infrastructure.Services
{
    public class PublisherService : Service, IPublisherService
    {
        public PublisherService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public PublisherDto Get(int id)
        {          
            return GetPublisher(id).ToDto();
        }

        public void Update(int id, IDictionary<string, string> languagesDescriptions, string homePage)
        {
            if (!languagesDescriptions.ContainsKey("ru"))
            {
                throw new DefaultCultureNotFoundException();
            }

            Publisher publisher = GetPublisher(id);
            publisher.Description = languagesDescriptions["ru"];
            publisher.HomePage = homePage;

            SetTranslationForPublisher(publisher, languagesDescriptions);

            UnitOfWork.Publishers.Update(publisher);
            UnitOfWork.Save();
        }

        public void Delete(int id)
        {
            Publisher publisher = GetPublisher(id);
            UnitOfWork.Publishers.Delete(publisher);
            UnitOfWork.Save();
        }

        public IEnumerable<PublisherDto> GetAllPublishers()
        {
            IEnumerable<Publisher> publishers = UnitOfWork.Publishers.GetAll();
            return publishers.Select(p => p.ToDto());
        }

        public PublisherDto GetPublisherByCompanyName(string companyName)
        {
            Publisher publisher = UnitOfWork.Publishers.SingleOrDefault(p => p.CompanyName == companyName);
            if (publisher == null)
            {
                throw new EntityNotFoundException(typeof(Publisher), $"Publisher with company name: {companyName} was not found");
            }

            return publisher.ToDto();
        }

        public int Create(string companyName, IDictionary<string, string> languagesDescriptions, string homePage)
        {
            if (!languagesDescriptions.ContainsKey("ru"))
            {
                throw new DefaultCultureNotFoundException();
            }

            Publisher publisher = UnitOfWork.Publishers.SingleOrDefaultDeleted(p => p.CompanyName == companyName);
            if (publisher == null)
            {
                publisher = new Publisher(companyName, languagesDescriptions["ru"], homePage);

                UnitOfWork.Publishers.Create(publisher);

                foreach (KeyValuePair<string, string> pair in languagesDescriptions)
                {
                    UnitOfWork.PublisherTranslations.Create(new PublisherTranslation
                    {
                        Publisher = publisher,
                        Language = pair.Key,
                        Description = pair.Value
                    });
                }
            }
            else
            {
                publisher.HomePage = homePage;
                publisher.Deleted = false;
                SetTranslationForPublisher(publisher, languagesDescriptions);

                UnitOfWork.Publishers.Update(publisher);
            }

            UnitOfWork.Save();
            return publisher.Id;
        }

        public void AddTranslationToPublisherDescription(
            Publisher publisher,
            IDictionary<string, string> languagesDescriptions)
        {
            foreach (KeyValuePair<string, string> pair in languagesDescriptions)
            {
                publisher.Translations.Add(new PublisherTranslation
                {
                    Language = pair.Key,
                    Description = pair.Value
                });
            }
        }

        private Publisher GetPublisher(int id)
        {
            Publisher publisher = UnitOfWork.Publishers.Get(id);
            if (publisher == null)
            {
                throw new EntityNotFoundException(typeof(Publisher));
            }

            return publisher;
        }

        private void SetTranslationForPublisher(Publisher publisher, IDictionary<string, string> languagesDescriptions)
        {
            List<PublisherTranslation> translationOnRemoving = publisher.Translations
                                                     .Where(t => !languagesDescriptions.Keys.Contains(t.Language))
                                                     .ToList();

            translationOnRemoving.ForEach(t => UnitOfWork.PublisherTranslations.Delete(t));

            for (int i = 0; i < languagesDescriptions.Count; i++)
            {
                string language = languagesDescriptions.ElementAt(i).Key;
                string publisherDescription = languagesDescriptions.ElementAt(i).Value;

                if (publisher.Translations.Count(t => t.Language == language) == 0)
                {
                    publisher.Translations.Add(new PublisherTranslation
                    {
                        Language = language,
                        Description = publisherDescription
                    });
                }
                else
                {
                    PublisherTranslation translation = publisher.Translations.Single(t => t.Language == language);

                    translation.Description = publisherDescription;
                    translation.Deleted = false;

                    UnitOfWork.PublisherTranslations.Update(translation);
                }
            }
        }
    }
}
