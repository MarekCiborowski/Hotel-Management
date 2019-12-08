using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication.ViewModels.Admin
{
    public class RoomVM
    {
        public int RoomNumber { get; set; }

        [Required]
        [Range(0d, (double)decimal.MaxValue)]
        [DisplayFormat(DataFormatString = "{0:0.00}", ApplyFormatInEditMode = true)]
        public decimal Cost { get; set; }

        [Required]
        [Range(1,10)]
        public int MaxNumberOfGuests { get; set; }

        [DisplayFormat(DataFormatString = "{0:0.00}", ApplyFormatInEditMode = true)]
        [Range(0,50)]
        public decimal RoomSize { get; set; }

        public List<AmenityVM> AllAmenities { get; set; }

        public string[] SelectedAmenityIds { get; set; }
    }
}