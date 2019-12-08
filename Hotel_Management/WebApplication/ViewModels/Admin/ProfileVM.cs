using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication.ViewModels.Admin
{
    public class ProfileVM
    {
        [Display(Name = "First name")]
        public string FirstName { get; set; }

        [Display(Name = "Email")]

        public string Email { get; set; }

        [Display(Name = "Last name")]
        public string LastName { get; set; }

        [Display(Name = "Address")]
        
        public string Address { get; set; }

        [Display(Name = "City")]
        
        public string City { get; set; }

        [Display(Name = "Zipcode")]
        
        public string Zipcode { get; set; }
    }
}