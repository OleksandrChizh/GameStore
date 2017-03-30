namespace GameStore.WebUI.ViewModels.Publisher
{
    public class ShortPublisherViewModel
    {
        public ShortPublisherViewModel(int id, string companyName)
        {
            Id = id;
            CompanyName = companyName;
        }

        public ShortPublisherViewModel()
        {
        }

        public int Id { get; set; }

        public string CompanyName { get; set; }
    }
}