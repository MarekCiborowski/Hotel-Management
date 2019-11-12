using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication.ViewModels.Home
{
    public class UserConversationsVM
    {
        public int ConversationId { get; set; }

        [Display(Name = "Conversation title")]
        public string ConversationTitle { get; set; }

    }
}