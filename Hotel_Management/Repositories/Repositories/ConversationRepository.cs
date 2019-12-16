using DataAccessLayer;
using DomainObjects.Dto;
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

        public List<ConversationListItemDto> GetConversationsIncludingSenderNameInTitle()
        {
            var conversationList = db.Conversations.Select(c => new ConversationListItemDto
            {
                ConversationId = c.ConversationID,
                ConversationTitle = db.Users.Where(u => u.RoleId == DomainObjects.Enums.RolesEnum.RegularUser
                    && db.UserConversations.Where(uc => uc.ConversationID == c.ConversationID).Select(uc => uc.UserID).Contains(u.Identity)).Select(us => us.FirstName + " " + us.LastName + ": ").FirstOrDefault() + c.Title
            }).ToList();

            return conversationList;
        }

        public Conversation AddConversationWithInitialMessage (int senderId, string message, string conversationTitle)
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
            };

            using (var dbContextTransaction = db.Database.BeginTransaction())
            {
                try
                {
                    this.db.Conversations.Add(newConversation);
                    db.SaveChanges();
                    this.db.UserConversations.Add(new UserConversation
                    {
                        ConversationID = newConversation.ConversationID,
                        UserID = senderId
                    });
                    db.SaveChanges();
                    dbContextTransaction.Commit();
                    return newConversation;
                }
                catch(Exception)
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

        public bool IsUserInConversation(int userId, int conversationId)
        {
            return this.db.UserConversations.Any(uc => uc.UserID == userId && uc.ConversationID == conversationId);
        }


    }
}
