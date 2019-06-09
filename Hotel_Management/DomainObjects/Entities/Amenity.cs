using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainObjects.Entities
{
    [Table("Amenity")]
    public class Amenity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AmenityId { get; set; }

        [Required]
        public string AmenityName { get; set; }

        public virtual ICollection<RoomAmenity> RoomAmenities { get; set; }
    }
}
