using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using GameStore.WebUI.ViewModels.User;

namespace GameStore.WebUI.ViewModels.Administrator
{
    public class AdministratorRegisterViewModel : RegisterViewModel
    {
        public MultiSelectList Roles { get; set; }

        [Display(Name = "Roles", ResourceType = typeof(Resources.Resource))]
        public List<string> SelectedRoles { get; set; }
    }
}