using System.ComponentModel.DataAnnotations;
using GameStore.Services.Interfaces.Enums;

namespace GameStore.WebUI.ViewModels.User
{
    public class BanUserViewModel
    {
        public string UserName { get; set; }

        [Required]
        public BanDuration Duration { get; set; }
    }
}