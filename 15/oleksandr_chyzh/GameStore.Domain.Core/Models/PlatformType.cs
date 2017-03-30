namespace GameStore.Domain.Core.Models
{
    public class PlatformType : Entity
    {
        public PlatformType(string type)
        {
            Type = type;
        }

        public PlatformType()
        {
        }

        public int Id { get; set; }

        public string Type { get; set; }
    }
}
