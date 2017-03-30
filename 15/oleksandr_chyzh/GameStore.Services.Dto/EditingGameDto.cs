using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GameStore.Services.Dto
{
    public class EditingGameDto
    {
        public int GameId { get; set; }

        public Dictionary<string, string> LanguagesNames { get; set; }

        public Dictionary<string, string> LanguagesDescriptions { get; set; }

        [Range(0, int.MaxValue)]
        public decimal Price { get; set; }

        [Range(0, int.MaxValue)]
        public short UnitsInStock { get; set; }

        public bool Discounted { get; set; }

        public IList<int> PlatformTypeIds { get; set; }

        public IList<int> PublisherIds { get; set; }

        public IList<int> GenreIds { get; set; }
    }
}
