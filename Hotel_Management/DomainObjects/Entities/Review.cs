using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainObjects.Entities
{
    [Table("Review")]
    public class Review
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ReviewId { get; set; }

        [Required]
        public string Comment { get; set; }

        [Required]
        public bool Score { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }

        [Required]
        public virtual User User { get; set; }

        [ForeignKey("Offer")]
        public int OfferId { get; set; }

        [Required]
        public virtual Offer Offer { get; set; }

    }
}
