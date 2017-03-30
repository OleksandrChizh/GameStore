using System.Data.Entity;
using System.Linq;
using GameStore.Domain.Core.Models;
using GameStore.Infrastructure.EFDataAccess;

namespace GameStore.Infrastructure.DataAccess.Implementations.Repositories
{
    public class PlatfornTypeRepository : GenericRepository<PlatformType, int>
    {
        public PlatfornTypeRepository(ApplicationContext dbContext) : base(dbContext)
        {
        }

        public override void Delete(PlatformType item)
        {
            var games = DbContext.Games.Where(game => game.PlatformTypes.Any(pt => pt.Id == item.Id)).ToList();

            foreach (var game in games)
            {
                game.PlatformTypes.Remove(item);
                DbContext.Entry(game).State = EntityState.Modified;
            }

            base.Delete(item);
        }
    }
}
