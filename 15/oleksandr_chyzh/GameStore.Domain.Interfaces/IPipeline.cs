using System.Linq;

namespace GameStore.Domain.Interfaces
{
    public interface IPipeline<T>
    {
        IPipeline<T> Register(IFilter<T> filter);

        IQueryable<T> Process(IQueryable<T> entities);
    }
}
