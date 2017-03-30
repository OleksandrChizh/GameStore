using System;
using System.Collections.Generic;

namespace GameStore.WebUI.ViewModels.User
{
    public class UserViewModel
    {
        public string UserId { get; set; }

        public DateTime BanExpiryDate { get; set; }

        public string UserName { get; set; }

        public IList<string> Roles { get; set; }
    }
}