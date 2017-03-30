namespace GameStore.WebUI.ViewModels.Genre
{
    public class GenreViewModel
    {
        public GenreViewModel(int id, string name, int? parentGenreId)
        {
            Id = id;
            Name = name;
            ParentGenreId = parentGenreId;
        }

        public GenreViewModel()
        {
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public int? ParentGenreId { get; set; }
    }
}