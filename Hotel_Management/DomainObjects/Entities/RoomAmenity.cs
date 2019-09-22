using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainObjects.Entities
{
    [Table("RoomAmenity")]
    public class RoomAmenity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RoomAmenityId { get; set; }

        [ForeignKey("Room")]
        public int RoomId { get; set; }

        [Required]
        public virtual Room Room { get; set; }

        [ForeignKey("Amenity")]
        public int AmenityId { get; set; }

        [Required]
        public virtual Amenity Amenity { get; set; }
    }
}
