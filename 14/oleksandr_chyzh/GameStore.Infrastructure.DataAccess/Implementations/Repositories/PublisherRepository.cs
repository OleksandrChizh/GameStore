using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading;
using GameStore.Domain.Core.Models;
using GameStore.Domain.Interfaces;
using GameStore.Infrastructure.EFDataAccess;

namespace GameStore.Infrastructure.DataAccess.Implementations.Repositories
{
    public class PublisherRepository : GenericRepository<Publisher, int>
    {
        public PublisherRepository(ApplicationContext dbContext) : base(dbContext)
        {
        }

        public override void Delete(Publisher item)
        {
            var games = item.Games.ToList();

            for (int i = 0; i < games.Count; i++)
            {
                games[i].Publishers.Remove(item);
                DbContext.Entry(games[i]).State = EntityState.Modified;
            }

            base.Delete(item);
        }

        public override IEnumerable<Publisher> GetAll()
        {            
            IQueryable<Publisher> publishers = DbContext.Set<Publisher>().Where(p => !p.Deleted);

            return GetPublishersWithCulture(publishers);
        }

        public override Publisher Get(int id)
        {
            string currentCulture = Thread.CurrentThread.CurrentCulture.Name.Substring(0, 2);
            Publisher publisher = DbContext.Set<Publisher>().Find(id);

            if (publisher == null || publisher.Deleted)
            {
                return null;
            }

            if (publisher.Translations.Any(t => t.Language == currentCulture))
            {
                publisher.Description = publisher.Translations.Single(t => t.Language == currentCulture).Description;
            }

            return publisher;
        }

        public override IEnumerable<Publisher> Find(IPipeline<Publisher> pipeline)
        {
            IQueryable<Publisher> notDeletedPublishers = DbContext.Set<Publisher>().Where(e => !e.Deleted);
            IQueryable<Publisher> filteredPublishers = pipeline.Process(notDeletedPublishers);

            return GetPublishersWithCulture(filteredPublishers);
        }

        private IList<Publisher> GetPublishersWithCulture(IQueryable<Publisher> publishers)
        {
            string currentCulture = Thread.CurrentThread.CurrentCulture.Name.Substring(0, 2);

            List<Publisher> publishersWithoutCulture =
                publishers.Where(p => p.Translations.All(t => t.Language != currentCulture)).ToList();

            List<Publisher> publishersWithCulture = publishers
                .Where(pub => pub.Translations.Any(t => t.Language == currentCulture))
                .Select(p => new
                {
                    p.Id,
                    p.CompanyName,
                    p.Translations.FirstOrDefault(t => t.Language == currentCulture).Description,
                    p.HomePage,
                    p.Deleted
                }).ToList()
                .Select(o => new Publisher
                {
                    Id = o.Id,
                    CompanyName = o.CompanyName,
                    Description = o.Description,
                    HomePage = o.HomePage,
                    Deleted = o.Deleted
                }).ToList();

            return publishersWithoutCulture.Union(publishersWithCulture).ToList();
        }
    }
}
