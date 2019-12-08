using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DomainObjects.Enums;

namespace DomainObjects.Entities
{
    [Table("Reservation")]
    public class Reservation
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ReservationId { get; set; }

        [Required]
        public ReservationStatusEnum ReservationStatusId { get; set; }

        public ReservationStatus ReservationStatus { get; set; }
        
        [ForeignKey("User")]
        [Required]
        public int UserId { get; set; }

        public virtual User User { get; set; }

        public virtual ICollection<RoomReservation> RoomReservations { get; set; }

        [Required]
        public HotelBookingSiteEnum HotelBookingSiteId { get; set; }

        public virtual HotelBookingSite HotelBookingSite { get; set; }

        [Required]
        public DateTime AccomodationDate { get; set; }

        [Required]
        public DateTime CheckOutDate { get; set; }

        [Required]
        public decimal TotalCost { get; set; }
        //komentarz

    }
}
