namespace GameStore.Services.Dto
{
    public class PlatformTypeDto
    {
        public PlatformTypeDto(int id, string type)
        {
            Id = id;
            Type = type;
        }

        public int Id { get; private set; }

        public string Type { get; private set; }
    }
}
