using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using GameStore.Domain.Core.Models;
using GameStore.Domain.Interfaces;
using GameStore.Infrastructure.EFDataAccess.Utils;
using GameStore.Services.Dto;
using GameStore.Services.Dto.Utils;
using GameStore.Services.Interfaces;
using GameStore.Services.Interfaces.Enums;
using GameStore.Services.Interfaces.Exceptions;

namespace GameStore.Infrastructure.Services
{
    public class UserService : Service, IUserService
    {
        public UserService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public void Update(string id, DateTime banExpiryDate, IList<string> rolesNames)
        {
            User user = GetUser(id);
            IList<Role> roles = UnitOfWork.Roles.Find(r => rolesNames.Contains(r.Name)).ToList();

            user.BanExpiryDate = banExpiryDate;
            user.Roles.Clear();
            user.Roles = roles;

            UnitOfWork.Users.Update(user);
            UnitOfWork.Save();
        }

        public void Delete(string id)
        {
            User user = GetUser(id);
            UnitOfWork.Users.Delete(user);
            UnitOfWork.Save();
        }

        public void BanUser(string userName, BanDuration duration)
        {
            User user = UnitOfWork.Users.SingleOrDefault(u => u.UserName == userName);
            if (user == null)
            {
                user = UnitOfWork.Users.SingleOrDefaultDeleted(u => u.UserName == userName);
                if (user == null)
                {
                    throw new EntityNotFoundException(typeof(User));
                }
            }

            user.BanExpiryDate = GetBanExpiryDate(user.BanExpiryDate > DateTime.UtcNow ? user.BanExpiryDate : DateTime.UtcNow, duration);

            UnitOfWork.Users.Update(user);
            UnitOfWork.Save();
        }

        public IEnumerable<UserDto> GetAll()
        {
            return UnitOfWork.Users.GetAll().Select(u => u.ToDto());
        }

        public UserDto Register(string userName, string password, IList<string> rolesNames)
        {
            const int saltLength = 10;
            string salt = SaltGenerator.GetSalt(saltLength);
            string passwordHash = Encoding.ASCII.GetString(new SHA256Managed().ComputeHash(Encoding.ASCII.GetBytes(salt + password)));
            List<Role> roles = UnitOfWork.Roles.Find(r => rolesNames.Contains(r.Name)).ToList();

            User user = UnitOfWork.Users.SingleOrDefaultDeleted(u => u.UserName == userName);
            if (user == null)
            {
                user = new User(userName, passwordHash, salt, roles);
                UnitOfWork.Users.Create(user);
            }
            else
            {
                user.Deleted = false;
                user.PasswordHash = passwordHash;
                user.Salt = salt;
                user.Roles.Clear();
                user.Roles = roles;

                UnitOfWork.Users.Update(user);
            }

            UnitOfWork.Save();

            return user.ToDto();
        }

        public UserDto Get(string userName, string password)
        {
            User user = UnitOfWork.Users.SingleOrDefault(u => u.UserName == userName);
            if (user != null)
            {
                string passwordHash = Encoding.ASCII.GetString(new SHA256Managed().ComputeHash(Encoding.ASCII.GetBytes(user.Salt + password)));

                if (passwordHash != user.PasswordHash)
                {
                    user = null;
                }
            }
         
            return user?.ToDto();
        }

        public void AddRoleToUser(string id, string roleName)
        {
            User user = GetUser(id);
            Role role = UnitOfWork.Roles.SingleOrDefault(r => r.Name == roleName);
            if (role == null)
            {
                throw new EntityNotFoundException(typeof(Role));
            }

            if (user.Roles.Contains(role))
            {
                throw new RoleDoubleAddingException();
            }

            user.Roles.Add(role);

            UnitOfWork.Users.Update(user);
            UnitOfWork.Save();
        }

        public UserDto Get(string id)
        {
            User user = GetUser(id);
            return user.ToDto();
        }

        public void UpdateNotificationMethod(string id, NotificationType type)
        {
            User user = GetUser(id);
            user.NotificationType = type;
            UnitOfWork.Users.Update(user);
            UnitOfWork.Save();
        }

        private DateTime GetBanExpiryDate(DateTime beginingDate, BanDuration duration)
        {
            switch (duration)
            {
                case BanDuration.Hour:
                {
                    return beginingDate.AddHours(1);
                }

                case BanDuration.Day:
                {
                    return beginingDate.AddDays(1);
                }

                case BanDuration.Week:
                {
                    return beginingDate.AddDays(7);
                }

                case BanDuration.Month:
                {
                    return beginingDate.AddMonths(1);
                }

                case BanDuration.Permanent:
                {
                    return DateTime.MaxValue;
                }

                default:
                {
                    return beginingDate;
                }
            }
        }

        private User GetUser(string id)
        {
            User user = UnitOfWork.Users.Get(id);
            if (user == null)
            {
                throw new EntityNotFoundException(typeof(User));
            }

            return user;
        }
    }
}
