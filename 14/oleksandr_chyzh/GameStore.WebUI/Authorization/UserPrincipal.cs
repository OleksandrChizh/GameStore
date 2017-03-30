using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;

namespace GameStore.WebUI.Authorization
{
    public class UserPrincipal : IUserPrincipal
    {
        public UserPrincipal(string email, string id, DateTime banExpiryDate, IList<string> roles)
        {
            Identity = new GenericIdentity(email);
            Id = id;
            BanExpiryDate = banExpiryDate;
            Roles = roles;
        }

        public UserPrincipal()
        {
            Identity = new GenericIdentity("Unauthorized");
            Roles = new List<string> { "Guest" };
        }

        public string Id { get; private set; }

        public DateTime BanExpiryDate { get; private set; }

        public IIdentity Identity { get; private set; }

        public IList<string> Roles { get; private set; }

        public bool IsInRole(string role)
        {
            return Roles.Any(r => r == role);
        }

        public bool IsNotInRole(string role)
        {
            return Roles.All(r => r != role);
        }
    }
}