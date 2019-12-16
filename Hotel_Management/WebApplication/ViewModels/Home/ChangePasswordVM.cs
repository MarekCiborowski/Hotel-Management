using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication.ViewModels.Home
{
    public class ChangePasswordVM
    {
        [Display(Name = "Old password")]
        [Required(ErrorMessage = "Old password required")]
        public string OldPassword { get; set; }

        [Display(Name = "New password")]
        [Required(ErrorMessage = "New password required")]
        public string NewPassword { get; set; }

        [Display(Name = "Repeat password")]
        [CompareAttribute("newPassword", ErrorMessage = "Passwords are not the same.")]
        public string RepeatPassword { get; set; }
    }
}