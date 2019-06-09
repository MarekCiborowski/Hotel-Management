using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainObjects.Entities
{
    [Table("Offer")]
    public class Offer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OfferId { get; set; }

        [Required]
        public int NumberOfGuests { get; set; }

        [Required]
        public decimal TotalCost { get; set; }

        public virtual ICollection<Review> Reviews { get; set; }

        public virtual ICollection<RoomOffer> RoomOffers { get; set; }
    }
}
