using DomainObjects.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication.ViewModels.Admin
{
    public class UserListItemVM
    {
        public int UserId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public bool IsConfirmed { get; set; }

        public string Role { get; set; }

        public UserListItemVM(User user)
        {
            UserId = user.Identity;
            Email = user.Email;
            FirstName = user.FirstName;
            LastName = user.LastName;
            Role = user.RoleId == DomainObjects.Enums.RolesEnum.RegularUser ? "Regular user" : "Admin";
            IsConfirmed = user.IsConfirmed;
        }

        public UserListItemVM()
        {

        }
    }
}