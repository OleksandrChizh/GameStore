﻿using System;
using System.Collections.Generic;

namespace GameStore.Services.Dto
{
    public class UserDto
    {
        public string Id { get; set; }

        public DateTime BanExpiryDate { get; set; }

        public string UserName { get; set; }

        public IList<string> Roles { get; set; }
    }
}
