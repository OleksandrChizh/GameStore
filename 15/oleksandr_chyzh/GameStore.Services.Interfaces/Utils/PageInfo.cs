using System;
using GameStore.Services.Interfaces.Enums;

namespace GameStore.Services.Interfaces.Utils
{
    public class PageInfo
    {
        public int PageNumber { get; set; }

        public PageSize PageSize { get; set; }

        public int TotalItems { get; set; }

        public int TotalPages => (int)Math.Ceiling((decimal)TotalItems / (int)PageSize);
    }
}
