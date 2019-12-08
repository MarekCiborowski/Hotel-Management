using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainObjects.Dto
{
    public class ConversationListItemDto
    {
        public int ConversationId { get; set; }

        [Display(Name = "Conversation title")]
        public string ConversationTitle { get; set; }
    }
}
