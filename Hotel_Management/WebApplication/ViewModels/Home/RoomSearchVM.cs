using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication.ViewModels.Home
{
    public class RoomSearchVM
    {
        [Display(Name ="Accomodation date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime AccomodationDate { get; set; }

        [Display(Name = "Check out date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime CheckOutDate { get; set; }

        public List<AmenitySearchVM> AmenitiesToSearch { get; set; }

        [Display(Name = "Number of guests")]
        public int NumberOfGuests { get; set; }

        [Display(Name = "Room size [square meters]")]
        [DisplayFormat(DataFormatString = "{0:0.00}", ApplyFormatInEditMode = true)]
        public decimal RoomSize { get; set; }

        public string[] SelectedAmenityIds { get; set; }

        public List<RoomVM> FoundRooms { get; set; } = new List<RoomVM>();
    }
}