namespace GameStore.Domain.Core.Models
{
    public class GameTranslation : Entity
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public Game Game { get; set; }

        public string Language { get; set; }
    }
}
