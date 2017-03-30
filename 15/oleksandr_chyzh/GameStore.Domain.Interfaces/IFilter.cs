using System.Linq;

namespace GameStore.Domain.Interfaces
{
    public interface IFilter<T>
    {
        IQueryable<T> Execute(IQueryable<T> entities);
    }
}
