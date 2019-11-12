using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication.ViewModels.Admin
{
    public class ProfileVM
    {
        public int accountID { get; set; }
        [Display(Name = "Quantity of following users")]
        public int followers { get; set; }
        [Display(Name = "Email")]

        public string email { get; set; }

        [Display(Name = "Nickname")]
        public string nickname { get; set; }
        [Display(Name = "Address")]
        
        public string address { get; set; }

        [Display(Name = "City")]
        
        public string city { get; set; }

        [Display(Name = "Zipcode")]
        
        public string zipcode { get; set; }

        [Display(Name = "State")]
        
        public string state { get; set; }

        [Display(Name = "Country")]
        
        public string country { get; set; }

        [Display(Name = "Profile type: ")]
        public bool isProfilePublic { get; set; } 
        public bool isFollowed { get; set; }
    }
}