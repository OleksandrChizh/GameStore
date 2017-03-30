using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using Resources;

namespace GameStore.WebUI.ViewModels.Administrator
{
    public class UpdateUserViewModel
    {
        public string Id { get; set; }

        public string UserName { get; set; }

        [DataType(DataType.DateTime)]
        [Display(Name = "BannedTo", ResourceType = typeof(Resource))]
        public DateTime BanExpiryDate { get; set; }

        public MultiSelectList Roles { get; set; }

        [Display(Name = "Roles", ResourceType = typeof(Resource))]
        public List<string> SelectedRoles { get; set; }
    }
}