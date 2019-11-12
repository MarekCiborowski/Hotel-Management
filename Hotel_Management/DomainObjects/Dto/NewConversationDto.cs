using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainObjects.Dto
{
    public class NewConversationDto
    {
        [Display(Name = "New message")]
        [Required]
        [StringLength(500)]
        public string NewMessage { get; set; }

        [Display(Name = "Conversation title")]
        [Required]
        [StringLength(100)]
        public string NewConversationTitle { get; set; }
    }
}
