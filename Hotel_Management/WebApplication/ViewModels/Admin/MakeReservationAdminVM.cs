using DomainObjects.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using static DomainObjects.Enums;

namespace WebApplication.ViewModels.Admin
{
    public class MakeReservationAdminVM
    {
        public int RoomId { get; set; }

        [Display(Name = "Accomodation date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime AccomodationDate { get; set; }

        [Display(Name = "Check out date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime CheckOutDate { get; set; }
        
        [Display(Name ="Hotel booking site")]
        public HotelBookingSiteEnum HotelBookingSite { get; set; }
    }
}