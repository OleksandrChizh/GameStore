using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GameStore.WebUI.Models.Order
{
    public class UpdateOrder
    {
        [Required]
        public int OrderId { get; set; }

        public List<ProductQuantity> ProductsQuantities { get; set; }
    }
}