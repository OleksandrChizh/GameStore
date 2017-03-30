namespace GameStore.WebUI.ViewModels.Game
{
    public class ShortGameViewModel
    {
        public ShortGameViewModel(int id, string key, string name, decimal price)
        {
            Id = id;
            Key = key;
            Name = name;
            Price = price;
        }

        public int Id { get; private set; }

        public string Key { get; private set; }

        public string Name { get; private set; }

        public decimal Price { get; private set; }
    }
}