namespace GameStore.Domain.Core.Models
{
    public class Role : Entity
    {
        public Role()
        {
        }

        public int Id { get; set; }

        public string Name { get; set; }
    }
}
