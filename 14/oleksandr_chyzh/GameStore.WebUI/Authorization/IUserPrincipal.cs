using System;
using System.Collections.Generic;
using System.Security.Principal;

namespace GameStore.WebUI.Authorization
{
    public interface IUserPrincipal : IPrincipal
    {
        string Id { get; }

        DateTime BanExpiryDate { get; }

        IList<string> Roles { get; }

        bool IsNotInRole(string role);
    }
}
