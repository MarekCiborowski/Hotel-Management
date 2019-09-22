using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainObjects.Entities
{
    [Table("RoomReservation")]
    public class RoomReservation
    {
        [Key]
        public int RoomReservationId { get; set; }

        [ForeignKey("Room")]
        public int RoomId { get; set; }

        public virtual Room Room { get; set; }

        [ForeignKey("Reservation")]
        public int ReservationId { get; set; }

        public virtual Reservation Reservation { get; set; }
    }
}
