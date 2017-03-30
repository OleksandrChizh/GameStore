using GameStore.Domain.Interfaces;

namespace GameStore.Infrastructure.Services
{
    public class Service
    {
        protected Service(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }

        protected IUnitOfWork UnitOfWork { get; }
    }
}
