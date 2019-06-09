using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainObjects.Entities
{
    [Table("News")]
    public class News
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int NewsId { get; set; }

        [Required]
        public string NewsContent { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }

        [Required]
        public virtual User User { get; set; }
    }
}
