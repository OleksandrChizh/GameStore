using System;
using System.Collections.Generic;

namespace GameStore.WebUI.Authorization
{
    public class UserSerializeModel
    {
        public string Id { get; set; }

        public int CurrentOrderId { get; set; }

        public DateTime BanExpiryDate { get; set; }

        public string UserName { get; set; }

        public IList<string> Roles { get; set; }
    }
}