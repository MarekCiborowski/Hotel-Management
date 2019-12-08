using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication.ViewModels.Home
{
    public class MakeReservationVM
    {
        public int RoomId { get; set; }

        public DateTime AccomodationDate { get; set; }

        public DateTime CheckOutDate { get; set; }
        
        public string Amenities { get; set; }

        public decimal RoomSize { get; set; }

        public decimal Cost { get; set; }

        public int MaxNumberOfGuests { get; set; }
    }
}