namespace GameStore.Services.Dto
{
    public class PlatformTypeDto
    {
        public PlatformTypeDto(int id, string type)
        {
            Id = id;
            Type = type;
        }

        public PlatformTypeDto()
        {
        }

        public int Id { get; set; }

        public string Type { get; set; }
    }
}
