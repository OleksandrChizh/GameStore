using System.Collections.Generic;
using System.Linq;
using GameStore.Domain.Interfaces;
using GameStore.Services.Interfaces;

namespace GameStore.Infrastructure.Services
{
    public class RoleService : Service, IRoleService
    {
        public RoleService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public IEnumerable<string> GetAll()
        {
            return UnitOfWork.Roles.GetAll().Select(r => r.Name);
        }
    }
}
