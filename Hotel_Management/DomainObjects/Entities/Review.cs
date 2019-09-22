﻿using System;
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
        [StringLength(500)]
        public string Comment { get; set; }

        [Required]
        public bool Score { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }

        [Required]
        public virtual User User { get; set; }
        
        [ForeignKey("Reservation")]
        public int ReservationId { get; set; }

        [Required]
        public virtual Reservation Reservation { get; set; }

    }
}
