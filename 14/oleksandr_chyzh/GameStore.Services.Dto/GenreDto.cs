namespace GameStore.Services.Dto
{
    public class GenreDto
    {
        public GenreDto(int id, string name, int? parentGenreId)
        {
            Id = id;
            Name = name;
            ParentGenreId = parentGenreId;
        }

        public GenreDto()
        {
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public int? ParentGenreId { get; set; }
    }
}
