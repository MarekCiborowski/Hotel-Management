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

        [Required]
        public int MaxNumberOfGuests { get; set; }

        public virtual ICollection<RoomReservation> RoomReservations { get; set; }

        public virtual ICollection<RoomAmenity> RoomAmenities { get; set; }
    }
}
