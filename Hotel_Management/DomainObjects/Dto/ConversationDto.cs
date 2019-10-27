﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainObjects.Dto
{
    public class ConversationDto
    {
        public int ConversationId { get; set; }

        public string ConversationTitle { get; set; }

        public List<MessageDto> Messages { get; set; }
    }
}