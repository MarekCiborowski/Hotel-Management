using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication.ViewModels.Admin
{
    public class UserSearchVM
    {
        public List<UserListItemVM> UserList { get; set; }

        [Display(Name = "Show admins only")]
        public bool ShowAdmin { get; set; } = false;

        [Display(Name = "Show unconfirmed users only")]
        public bool ShowUnconfirmed { get; set; } = false;
    }
}