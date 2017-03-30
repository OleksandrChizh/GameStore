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
    public class PlatformTypeService : Service, IPlatformTypeService
    {
        public PlatformTypeService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public PlatformTypeDto Get(int id)
        {          
            return GetPlatformType(id).ToDto();
        }

        public IEnumerable<PlatformTypeDto> GetAllPlatformTypes()
        {
            IEnumerable<PlatformType> platformTypes = UnitOfWork.PlatformTypes.GetAll();
            return platformTypes.Select(pt => pt.ToDto());
        }

        public int Create(string type)
        {
            PlatformType platformType = UnitOfWork.PlatformTypes.SingleOrDefaultDeleted(pt => pt.Type == type);
            if (platformType == null)
            {
                platformType = new PlatformType(type);
                UnitOfWork.PlatformTypes.Create(platformType);
            }
            else
            {
                platformType.Deleted = false;
                UnitOfWork.PlatformTypes.Update(platformType);
            }

            UnitOfWork.Save();

            return platformType.Id;
        }

        public void Delete(int id)
        {
            PlatformType platformType = GetPlatformType(id);
            UnitOfWork.PlatformTypes.Delete(platformType);
            UnitOfWork.Save();
        }

        private PlatformType GetPlatformType(int id)
        {
            PlatformType platformType = UnitOfWork.PlatformTypes.Get(id);
            if (platformType == null)
            {
                throw new EntityNotFoundException(typeof(PlatformType));
            }

            return platformType;
        }
    }
}
