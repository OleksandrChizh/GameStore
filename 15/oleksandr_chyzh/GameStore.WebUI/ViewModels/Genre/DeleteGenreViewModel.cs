using System.Collections.Generic;

namespace GameStore.WebUI.ViewModels.Genre
{
    public class DeleteGenreViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public IEnumerable<string> SubGenres { get; set; }
    }
}