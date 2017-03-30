using System.Collections.Generic;
using GameStore.Services.Dto;
using GameStore.Services.Interfaces.Validators;

namespace GameStore.Services.Interfaces
{
    public interface IPlatformTypeService : IDomainEntityService<PlatformTypeDto, int>
    {
        IEnumerable<PlatformTypeDto> GetAllPlatformTypes();

        int Create([NonEmptyString] string type);

        void Delete(int id);
    }
}
