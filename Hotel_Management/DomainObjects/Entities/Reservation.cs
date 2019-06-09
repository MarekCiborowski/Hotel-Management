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
        public Status Status { get; set; }
        
        [ForeignKey("User")]
        public int UserId { get; set; }

        [Required]
        public virtual User User { get; set; }

        public virtual ICollection<Room> Rooms { get; set; }

    }
}
