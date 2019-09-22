using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainObjects.Entities
{
    [Table("Message")]
    public class Message
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MessageID { get; set; }

        [Required]
        [StringLength(500)]
        public string MessageContent { get; set; }

        [ForeignKey("Conversation")]
        public int ConversationID { get; set; }

        [Required]
        public virtual Conversation Conversation { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }

        [Required]
        public virtual User User { get; set; }
    }
}
