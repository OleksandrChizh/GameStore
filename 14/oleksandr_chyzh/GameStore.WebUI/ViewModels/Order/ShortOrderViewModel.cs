using System;
using System.ComponentModel.DataAnnotations;
using Resources;

namespace GameStore.WebUI.ViewModels.Order
{
    public class ShortOrderViewModel
    {
        public ShortOrderViewModel()
        {
        }

        public int Id { get; set; }

        [Display(Name = "CustomerIdentifier", ResourceType = typeof(Resource))]
        public string CustomerId { get; set; }

        [Required]
        [Display(Name = "OrderDate", ResourceType = typeof(Resource))]
        public DateTime OrderDate { get; set; }

        [Required]
        [Display(Name = "PayingDate", ResourceType = typeof(Resource))]
        public DateTime PayingDate { get; set; }

        [Required]
        [Display(Name = "ShippedDate", ResourceType = typeof(Resource))]
        public DateTime ShippedDate { get; set; }
    }
}