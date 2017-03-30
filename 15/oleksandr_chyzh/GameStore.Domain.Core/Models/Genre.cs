namespace GameStore.Domain.Core.Models
{
    public class Genre : Entity
    {
        public Genre(string name, Genre parentGenre)
        {
            Name = name;
            ParentGenre = parentGenre;
        }

        public Genre()
        {
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public virtual Genre ParentGenre { get; set; }
    }
}
