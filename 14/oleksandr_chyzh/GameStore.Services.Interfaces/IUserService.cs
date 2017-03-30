using System;
using System.Collections.Generic;
using GameStore.Services.Dto;
using GameStore.Services.Interfaces.Enums;
using GameStore.Services.Interfaces.Validators;

namespace GameStore.Services.Interfaces
{
    public interface IUserService : IDomainEntityService<UserDto, string>
    {
        IEnumerable<UserDto> GetAll();
                      
        UserDto Register(string userName, string password, IList<string> roles);

        UserDto Get(string userName, string password);

        void Update(string id, DateTime banExpiryDate, IList<string> roles);

        void Delete(string id);

        void BanUser(
            [NonEmptyString] string userName,
            [ExistenEnum] BanDuration duration);

        void AddRoleToUser(
            string id, 
            [NonEmptyString] string role);
    }
}
