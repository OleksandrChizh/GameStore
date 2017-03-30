using System.ComponentModel.DataAnnotations;
using Resources;

namespace GameStore.WebUI.Models
{
    public class Purchase
    {
        public Purchase(int gameId, string gameName, decimal price, short quantity)
        {
            GameId = gameId;
            GameName = gameName;
            Price = price;
            Quantity = quantity;
        }

        [Display(Name = "Name", ResourceType = typeof(Resource))]
        public string GameName { get; set; }

        public int GameId { get; private set; }

        [Display(Name = "Price", ResourceType = typeof(Resource))]
        public decimal Price { get; private set; }

        [Display(Name = "Quantity", ResourceType = typeof(Resource))]
        public short Quantity { get; set; }
    }
}