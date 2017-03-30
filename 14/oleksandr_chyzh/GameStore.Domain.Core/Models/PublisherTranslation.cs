namespace GameStore.Domain.Core.Models
{
    public class PublisherTranslation : Entity
    {
        public int Id { get; set; }

        public string Description { get; set; }

        public Publisher Publisher { get; set; }

        public string Language { get; set; }
    }
}
