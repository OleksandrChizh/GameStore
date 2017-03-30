using System.Linq;
using System.Threading;
using GameStore.Domain.Core.Models;
using GameStore.Domain.Interfaces;

namespace GameStore.Infrastructure.DataAccess.Filters
{
    public class GamesByGameNameFilter : IFilter<Game>
    {
        private readonly string _gameName;

        public GamesByGameNameFilter(string gameName)
        {
            _gameName = gameName;
        }

        public IQueryable<Game> Execute(IQueryable<Game> entities)
        {
            string currentCulture = Thread.CurrentThread.CurrentCulture.Name.Substring(0, 2);

            return entities.Where(g => g.Name.ToUpper().Contains(_gameName.ToUpper()) || 
                                       g.Translations.Any(t => t.Language == currentCulture && 
                                                               t.Name.ToUpper().Contains(_gameName.ToUpper())));
        }
    }
}
