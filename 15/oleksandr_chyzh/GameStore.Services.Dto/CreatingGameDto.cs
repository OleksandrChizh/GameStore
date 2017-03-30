using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GameStore.Services.Dto
{
    public class CreatingGameDto
    {
        [MaxLength(20)]
        public string Key { get; set; }

        public Dictionary<string, string> LanguagesNames { get; set; }

        public Dictionary<string, string> LanguagesDescriptions { get; set; }

        [Range(0, int.MaxValue)]
        public decimal Price { get; set; }

        [Range(0, int.MaxValue)]
        public short UnitsInStock { get; set; }

        public bool Discounted { get; set; }

        public DateTime PublishingDate { get; set; }

        public ICollection<int> GenreIds { get; set; }

        public ICollection<int> PlatformTypeIds { get; set; }

        public ICollection<int> PublisherIds { get; set; }
    }
}
