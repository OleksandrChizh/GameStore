using System;
using System.Collections.Generic;
using GameStore.Domain.Core.Models;

namespace GameStore.Services.Dto
{
    public class UserDto
    {
        public string Id { get; set; }

        public DateTime BanExpiryDate { get; set; }

        public string UserName { get; set; }

        public NotificationType Type { get; set; }

        public IList<string> Roles { get; set; }
    }
}
