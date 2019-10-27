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
        public DateTime AccomodationDate { get; set; }

        [Display(Name = "Check out date")]
        [DataType(DataType.Date)]
        public DateTime CheckOutDate { get; set; }

        public List<AmenitySearchVM> AmenitiesToSearch { get; set; }

        public int NumberOfGuests { get; set; }

        public string[] SelectedAmenityIds { get; set; }

        public List<RoomVM> FoundRooms { get; set; } = new List<RoomVM>();
    }
}