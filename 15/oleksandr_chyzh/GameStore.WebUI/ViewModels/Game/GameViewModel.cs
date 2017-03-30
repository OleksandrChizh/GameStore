using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using GameStore.WebUI.ViewModels.Genre;
using Resources;

namespace GameStore.WebUI.ViewModels.Game
{
    public class GameViewModel
    {
        public GameViewModel(
            int id,
            bool isDeleted,
            string key,
            string name,
            string description,
            decimal price,
            short unitsOfStock,
            int viewsCount,
            DateTime publishingDate,
            DateTime addingDate,
            string imagePath,
            IList<GenreViewModel> genres,
            IList<string> platformTypes,
            IList<string> publishers)
        {
            Id = id;
            IsDeleted = isDeleted;
            Key = key;
            Name = name;
            Description = description;
            Price = price;
            UnitsInStock = unitsOfStock;
            ViewsCount = viewsCount;
            PublishingDate = publishingDate;
            AddingDate = addingDate;
            ImagePath = imagePath;
            Genres = genres;
            PlatformTypes = platformTypes;
            Genres = genres;
            Publishers = publishers;
        }

        public int Id { get; private set; }

        public bool IsDeleted { get; private set; }

        [DataType(DataType.Text)]
        [Display(Name = "Key", ResourceType = typeof(Resource))]
        public string Key { get; private set; }

        [Display(Name = "Name", ResourceType = typeof(Resource))]
        public string Name { get; private set; }

        [Display(Name = "Description", ResourceType = typeof(Resource))]
        public string Description { get; private set; }

        [Display(Name = "Price", ResourceType = typeof(Resource))]
        public decimal Price { get; private set; }

        [Display(Name = "UnitsInStock", ResourceType = typeof(Resource))]
        public short UnitsInStock { get; private set; }

        [Display(Name = "ViewsCount", ResourceType = typeof(Resource))]
        public int ViewsCount { get; private set; }

        [Display(Name = "PublishingDate", ResourceType = typeof(Resource))]
        public DateTime PublishingDate { get; private set; }

        [Display(Name = "AddingDate", ResourceType = typeof(Resource))]
        public DateTime AddingDate { get; private set; }

        [Display(Name = "Genres", ResourceType = typeof(Resource))]
        public IList<GenreViewModel> Genres { get; private set; }

        [Display(Name = "PlatformTypes", ResourceType = typeof(Resource))]
        public IList<string> PlatformTypes { get; private set; }

        [Display(Name = "Publishers", ResourceType = typeof(Resource))]
        public IList<string> Publishers { get; private set; }

        public string ImagePath { get; set; }
    }
}