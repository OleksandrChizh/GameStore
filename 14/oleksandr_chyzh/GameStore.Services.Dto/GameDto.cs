using System;
using System.Collections.Generic;
using GameStore.Services.Dto.Utils;

namespace GameStore.Services.Dto
{
    public class GameDto
    {
        public GameDto(
            int id,
            string key,
            IDictionary<string, string> languagesNames,
            IDictionary<string, string> languagesDescriptions,
            ICollection<PlatformTypeDto> platformTypes,
            ICollection<GenreDto> genres,
            decimal price,
            short unitInShock,
            bool discounted,
            bool deleted,
            int viewCount,
            DateTime addingDate,
            DateTime publishingDate,
            ICollection<PublisherDto> publishers)
        {
            Id = id;
            Key = key;
            LanguagesNames = languagesNames;
            LanguagesDescriptions = languagesDescriptions;
            PlatformTypes = platformTypes;
            Genres = genres;
            Price = price;
            UnitsInStock = unitInShock;
            Discounted = discounted;
            Deleted = deleted;
            ViewCount = viewCount;
            PublishingDate = publishingDate;
            AddingDate = addingDate;
            Publishers = publishers;
        }

        public GameDto()
        {
        }

        public int Id { get; set; }

        public string Key { get; set; }

        public IDictionary<string, string> LanguagesNames { get; set; }

        public IDictionary<string, string> LanguagesDescriptions { get; set; }

        public ICollection<PlatformTypeDto> PlatformTypes { get; set; }

        public ICollection<GenreDto> Genres { get; set; }

        public decimal Price { get; set; }

        public short UnitsInStock { get; set; }

        public bool Discounted { get; set; }

        public bool Deleted { get; set; }

        public int ViewCount { get; set; }

        public DateTime AddingDate { get; set; }

        public DateTime PublishingDate { get; set; }

        public ICollection<PublisherDto> Publishers { get; set; }

        public bool Equals(GameDto game)
        {
            return Key == game.Key &&
                   LanguagesNames.IsEqual(game.LanguagesNames) &&
                   LanguagesDescriptions.IsEqual(game.LanguagesDescriptions);
        }
    }
}
