using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainObjects.Dto
{
    public class MessageDto
    {
        public int MessageId { get; set; }

        public string MessageContent { get; set; }

        public string DisplayedSenderName { get; set; }
    }
}
