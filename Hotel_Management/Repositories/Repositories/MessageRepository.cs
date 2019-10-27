using DataAccessLayer;
using DomainObjects.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Repositories
{
    public class MessageRepository
    {
        private DatabaseContext db;

        public MessageRepository(DatabaseContext databaseContext)
        {
            this.db = databaseContext;
        }

        public Message AddMessageToConversation(string messageContent, int senderId, int conversationId)
        {
            var newMessage = new Message
            {
                ConversationID = conversationId,
                UserId = senderId,
                MessageContent = messageContent
            };

            using(var dbContextTransaction = this.db.Database.BeginTransaction())
            {
                try
                {
                    db.Messages.Add(newMessage);
                    db.SaveChanges();
                    dbContextTransaction.Commit();
                    return newMessage;
                }
                catch
                {
                    dbContextTransaction.Rollback();
                    return null;
                }
            }
        }

        public List<Message> GetConversationMessagesWithUsers(int conversationId)
        {
            var messages = this.db.Messages.Include("User").Where(m => m.ConversationID == conversationId).ToList();

            return messages;
        }
    }
}
