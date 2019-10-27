using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication.ViewModels.Home
{
    public class RoomVM
    {
        public int RoomNumber { get; set; }

        public string Amenities { get; set; }

        public int MaxNumberOfGuests { get; set; }

        public decimal Cost { get; set; }
    }
}