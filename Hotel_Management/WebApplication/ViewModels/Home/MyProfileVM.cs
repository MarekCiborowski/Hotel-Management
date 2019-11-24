using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication.ViewModels.Home
{
    public class MyProfileVM
    {
        [Required(ErrorMessage = "Email required")]
        [Display(Name = "Email")]
        public string Email { get; set; }


        [Display(Name = "Login")]
        public string login { get; set; }

        [Display(Name = "Address")]
        [Required(ErrorMessage = "Address Required")]
        public string address { get; set; }

        [Display(Name = "City")]
        [Required(ErrorMessage = "City required")]
        public string city { get; set; }

        [Display(Name = "Zipcode")]
        [Required(ErrorMessage = "Zipcode required")]
        public string zipcode { get; set; }

        [Display(Name = "State")]
        [Required(ErrorMessage = "State required")]
        public string state { get; set; }

        [Display(Name = "Country")]
        [Required(ErrorMessage = "Country required")]
        public string country { get; set; }

        [Display(Name = "Profile type: ")]
        public bool isProfilePublic { get; set; } = false;

        [Display(Name = "Quantity of following users")]
        public int followers { get; set; }

        [Display(Name = "Quantity of followed users")]
        public int followed { get; set; }
    }
}