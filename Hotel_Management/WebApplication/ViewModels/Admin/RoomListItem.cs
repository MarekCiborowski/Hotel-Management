using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication.ViewModels.Admin
{
    public class RoomListItem
    {
        public int RoomId { get; set; }

        public decimal Cost { get; set; }

        public int MaxNumberOfGuests { get; set; }

        public decimal RoomSize { get; set; }

        public string Amenities { get; set; }
    }
}