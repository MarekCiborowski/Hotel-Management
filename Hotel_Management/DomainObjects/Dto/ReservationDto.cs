using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DomainObjects.Enums;

namespace DomainObjects.Dto
{
    public class ReservationDto
    {
        public int ReservationId { get; set; }

        public int RoomId { get; set; }

        public string ArrangerName { get; set; }

        [DisplayFormat(DataFormatString = "{0:0.00}", ApplyFormatInEditMode = true)]
        public decimal TotalCost { get; set; }

        public ReservationStatusEnum ReservationStatusId { get; set; }

        public string ReservationStatus { get; set; }

        public HotelBookingSiteEnum HotelBookingSiteId { get; set; }

        public string HotelBookingSite { get; set; }

        public DateTime AccomodationDate { get; set; }

        public DateTime CheckOutDate { get; set; }

        public int ArrangerId { get; set; }
    }
}
