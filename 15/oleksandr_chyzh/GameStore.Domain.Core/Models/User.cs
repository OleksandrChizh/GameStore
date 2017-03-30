using System;
using System.Collections.Generic;

namespace GameStore.Domain.Core.Models
{
    public class User : Entity
    {
        public User(string email, string passwordHash, string salt, IList<Role> roles)
        {
            UserId = email;
            UserName = email;
            PasswordHash = passwordHash;
            Salt = salt;
            Roles = roles;
        }

        public User()
        {
        }

        public string UserId { get; set; }

        public string UserName { get; set; }

        public string PasswordHash { get; set; }

        public string Salt { get; set; }

        public DateTime BanExpiryDate { get; set; }

        public NotificationType NotificationType { get; set; }

        public virtual ICollection<Role> Roles { get; set; } = new List<Role>();
    }
}
