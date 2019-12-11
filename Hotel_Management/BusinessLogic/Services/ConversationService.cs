using DataAccessLayer;
using DomainObjects.Dto;
using DomainObjects.Entities;
using Repositories.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Services
{
    public class ConversationService
    {
        private MessageRepository messageRepository;
        private ConversationRepository conversationRepository;

        public ConversationService(DatabaseContext databaseContext)
        {
            this.messageRepository = new MessageRepository(databaseContext);
            this.conversationRepository = new ConversationRepository(databaseContext);
        }

        public ConversationDto GetConversationDto(int conversationId)
        {
            var conversation = this.conversationRepository.GetConversation(conversationId);
            var conversationDto = new ConversationDto
            {
                ConversationId = conversation.ConversationID,
                ConversationTitle = conversation.Title,
                Messages = new List<MessageDto>()
            };

            var messages = this.messageRepository.GetConversationMessagesWithUsers(conversationId);

            conversationDto.Messages.AddRange(messages.Select(m => new MessageDto
            {
                MessageId = m.MessageID,
                DisplayedSenderName = m.User.FirstName + " " + m.User.LastName,
                MessageContent = m.MessageContent,
                SenderId = m.User.Identity
            }));

            return conversationDto;
        }

        public List<Conversation> GetUserConversations(int userId)
        {
            var userConversations = this.conversationRepository.GetUserConversations(userId);

            return userConversations;
        }

        public Conversation AddConversationWithInitialMessage(int senderId, string message, string conversationTitle)
        {
            var newConversation = this.conversationRepository.AddConversationWithInitialMessage(senderId, message, conversationTitle);

            return newConversation;
        }

        public Message AddMessageToConversation(string messageContent, int senderId, int conversationId)
        {
            var message = this.messageRepository.AddMessageToConversation(messageContent, senderId, conversationId);

            return message;
        }

        public List<ConversationListItemDto> GetConversationsIncludingSenderNameInTitle()
        {
            var conversations = this.conversationRepository.GetConversationsIncludingSenderNameInTitle();

            return conversations;
        }

        public bool IsUserInConversation(int userId, int conversationId)
        {
            return this.conversationRepository.IsUserInConversation(userId, conversationId);
        }
    }
}
