using System;
using System.Collections.Generic;

namespace GameStore.Domain.Core.Models
{
    public class Game : Entity
    {
        public Game()
        {
        }

        public int Id { get; set; }

        public string Key { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public short UnitsInStock { get; set; }

        public bool Discounted { get; set; }

        public int ViewsCount { get; set; }

        public DateTime AddingDate { get; set; } = DateTime.UtcNow;

        public DateTime PublishingDate { get; set; }

        public string ImagePath { get; set; }

        public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();

        public virtual ICollection<PlatformType> PlatformTypes { get; set; } = new List<PlatformType>();

        public virtual ICollection<Genre> Genres { get; set; } = new List<Genre>();

        public virtual ICollection<Publisher> Publishers { get; set; } = new List<Publisher>();

        public virtual ICollection<GameTranslation> Translations { get; set; } = new List<GameTranslation>();
    }
}
