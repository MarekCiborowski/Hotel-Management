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
    [Table("User")]
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Identity { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        [StringLength(50)]
        [Index(IsUnique = true)]
        public string Login { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        public string Zipcode { get; set; }

        [Required]
        public bool IsConfirmed { get; set; }

        [Required]
        public Roles Role { get; set; }

        [ForeignKey("Company")]
        public int CompanyId { get; set; }

        public virtual Company Company { get; set; }

        public virtual ICollection<Review> Reviews { get; set; }

        public virtual ICollection<Reservation> Reservations { get; set; }

        public virtual ICollection<News> News { get; set; }
    }
}
