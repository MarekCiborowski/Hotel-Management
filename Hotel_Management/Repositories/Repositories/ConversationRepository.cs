using DataAccessLayer;
using DomainObjects.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Repositories
{
    public class ConversationRepository
    {
        private DatabaseContext db;

        public ConversationRepository(DatabaseContext databaseContext)
        {
            this.db = databaseContext;
        }

        public Conversation AddConversationWithInitialMessage (int senderId, int receiverId, string message, string conversationTitle)
        {
            var newMessage = new Message
            {
                MessageContent = message,
                UserId = senderId,
            };

            var newConversation = new Conversation
            {
                Title = conversationTitle,
                Messages = new List<Message> { newMessage },
                UserConversations = new List<UserConversation>
                {
                    new UserConversation{ UserID = senderId },
                    new UserConversation{ UserID = receiverId}
                }
            };

            using (var dbContextTransaction = db.Database.BeginTransaction())
            {
                try
                {
                    this.db.Conversations.Add(newConversation);
                    db.SaveChanges();
                    dbContextTransaction.Commit();
                    return newConversation;
                }
                catch
                {
                    dbContextTransaction.Rollback();
                    return null;
                }
            }
        }

        public Conversation GetConversation(int conversationId)
        {
            var conversation = this.db.Conversations.FirstOrDefault(c => c.ConversationID == conversationId);

            return conversation;
        }

        public List<Conversation> GetUserConversations(int userId)
        {
            var userConversations = this.db.Conversations.Where(
                c => db.UserConversations.Where(uc => uc.UserID == userId).Select(uc => uc.ConversationID).Contains(c.ConversationID)
            ).ToList();

            return userConversations;
        }


    }
}
