using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication.ViewModels.Admin
{
    public class AmenitiesVM
    {
        [Required]
        [MaxLength()]
        public string NewAmenity { get; set; }
        
        public List<AmenityVM> AvailableAmenities { get; set; }
    }
}