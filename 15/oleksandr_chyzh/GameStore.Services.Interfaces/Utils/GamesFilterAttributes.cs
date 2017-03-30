using System.Collections.Generic;
using GameStore.Services.Interfaces.Enums;

namespace GameStore.Services.Interfaces.Utils
{
    public class GamesFilterAttributes
    {
        public IList<int> Genres { get; set; } = new List<int>();

        public IList<int> Publishers { get; set; } = new List<int>();

        public IList<int> PlatformTypes { get; set; } = new List<int>();

        public SortingObject SortingObject { get; set; }

        public PublishingDatePeriod PublishingDatePeriod { get; set; }

        public decimal MinPrice { get; set; }

        public decimal MaxPrice { get; set; } = decimal.MaxValue;

        public string GameNameSearchingString { get; set; }

        public PageInfo PageInfo { get; set; }
    }
}
