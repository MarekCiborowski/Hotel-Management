using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication.ViewModels.Home
{
    public class ProfileVM
    {
        [Required(ErrorMessage = "Email required")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Display(Name = "First name")]
        [Required(ErrorMessage = "First name required")]
        public string FirstName { get; set; }

        [Display(Name = "Last name")]
        [Required(ErrorMessage = "Last name required")]
        public string LastName { get; set; }

        [Display(Name = "Address")]
        [Required(ErrorMessage = "Address Required")]
        public string Address { get; set; }

        [Display(Name = "City")]
        [Required(ErrorMessage = "City required")]
        public string City { get; set; }

        [Display(Name = "Zipcode")]
        [Required(ErrorMessage = "Zipcode required")]
        public string Zipcode { get; set; }
    }
}