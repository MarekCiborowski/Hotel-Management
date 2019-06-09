using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainObjects.Entities
{
    [Table("RoomOffer")]
    public class RoomOffer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RoomOfferId { get; set; }

        [ForeignKey("Room")]
        public int RoomId { get; set; }

        [Required]
        public virtual Room Room { get; set; }

        [ForeignKey("Offer")]
        public int OfferId { get; set; }

        [Required]
        public virtual Offer Offer { get; set; }
    }
}
