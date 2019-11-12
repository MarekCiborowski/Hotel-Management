using DomainObjects.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication.ViewModels.Admin
{
    public class NewAdminVM
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

        [Display(Name = "Login")]
        [Required(ErrorMessage = "Login required")]
        public string Login { get; set; }

        [Display(Name = "Password")]
        [Required(ErrorMessage = "Password required")]
        public string Password { get; set; }

        [Display(Name = "Repeat password")]
        [Required(ErrorMessage = "Password must be repeated")]
        [Compare("Password", ErrorMessage = "Passwords are not the same.")]
        public string RepeatPassword { get; set; }

        [Display(Name = "Address")]
        [Required(ErrorMessage = "Address Required")]
        public string Address { get; set; }

        [Display(Name = "City")]
        [Required(ErrorMessage = "City required")]
        public string City { get; set; }

        [Display(Name = "Zipcode")]
        [Required(ErrorMessage = "Zipcode required")]
        public string Zipcode { get; set; }

        public User ToEntityModel()
        {
            return new User
            {
                Address = this.Address,
                City = this.City,
                Email = this.Email,
                FirstName = this.FirstName,
                IsConfirmed = false,
                LastName = this.LastName,
                Login = this.Login,
                Password = this.Password,
                Zipcode = this.Zipcode
            };
        }
    }
}