using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainObjects.Entities
{
    [Table("Room")]
    public class Room
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RoomId { get; set; }

        [Required]
        public decimal Cost { get; set; }

        [ForeignKey("Reservation")]
        public int ReservationId { get; set; }

        [Required]
        public virtual Reservation Reservation { get; set; }

        public virtual ICollection<RoomOffer> RoomOffers { get; set; }

        public virtual ICollection<RoomAmenity> RoomAmenities { get; set; }
    }
}
