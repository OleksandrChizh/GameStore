using System.Collections.Generic;

namespace GameStore.Services.Interfaces
{
    public interface IRoleService
    {
        IEnumerable<string> GetAll();
    }
}
