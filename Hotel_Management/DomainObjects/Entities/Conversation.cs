using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainObjects.Entities
{

    [Table("Conversation")]
    public class Conversation
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ConversationID { get; set; }

        [Display(Name = "Title")]
        [Required(ErrorMessage = "Title required.")]
        public string Title { get; set; }

        public virtual ICollection<UserConversation> UserConversations { get; set; }
        public virtual ICollection<Message> Messages { get; set; }
    }
}
