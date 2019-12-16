using DataAccessLayer;
using DomainObjects.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Repositories.Repositories;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests.UnitTests
{
    [TestClass]
    public class ConversationRepositoryTests
    {
        private IQueryable<Conversation> conversationList;
        private readonly IQueryable<Conversation> emptyConversationList;
        private IQueryable<UserConversation> userConversations;

        public ConversationRepositoryTests()
        {
            List<Conversation> list = new List<Conversation>
            {
               new Conversation { ConversationID = 23},
               new Conversation { ConversationID = 22}
            };

            List<UserConversation> userConversationsList = new List<UserConversation>
            {
                new UserConversation {UserID = 2, ConversationID = 23}
            };

            // Convert the IEnumerable list to an IQueryable list
            conversationList = list.AsQueryable();
            emptyConversationList = new List<Conversation>().AsQueryable();
            userConversations = userConversationsList.AsQueryable();

        }

        [TestMethod]
        public void GetUserConversations_UserIdPassed_ListIsNotEmpty()
        {
            //Arrange
            var context = new Mock<DatabaseContext>();
            var dbSetMock = new Mock<DbSet<Conversation>>();
            var dbSetMockUserConversations = new Mock<DbSet<UserConversation>>();

            dbSetMock.As<IQueryable<Conversation>>().Setup(m => m.Provider).Returns(conversationList.Provider);
            dbSetMock.As<IQueryable<Conversation>>().Setup(m => m.Expression).Returns(conversationList.Expression);
            dbSetMock.As<IQueryable<Conversation>>().Setup(m => m.ElementType).Returns(conversationList.ElementType);
            dbSetMock.As<IQueryable<Conversation>>().Setup(m => m.GetEnumerator()).Returns(conversationList.GetEnumerator());
            dbSetMockUserConversations.As<IQueryable<UserConversation>>().Setup(m => m.Provider).Returns(userConversations.Provider);
            dbSetMockUserConversations.As<IQueryable<UserConversation>>().Setup(m => m.Expression).Returns(userConversations.Expression);
            dbSetMockUserConversations.As<IQueryable<UserConversation>>().Setup(m => m.ElementType).Returns(userConversations.ElementType);
            dbSetMockUserConversations.As<IQueryable<UserConversation>>().Setup(m => m.GetEnumerator()).Returns(userConversations.GetEnumerator());
            context.Setup(x => x.Conversations).Returns(dbSetMock.Object);
            context.Setup(x => x.UserConversations).Returns(dbSetMockUserConversations.Object);
            var repository = new ConversationRepository(context.Object);

            //Act
            var allConversations = repository.GetUserConversations(2);

            //Assert
            Assert.IsTrue(allConversations.Any());

        }

        [TestMethod]
        public void GetConversation_ConversationIdPassed_CorrectConversationReturned()
        {
            //Arrange
            var context = new Mock<DatabaseContext>();
            var dbSetMock = new Mock<DbSet<Conversation>>();

            dbSetMock.As<IQueryable<Conversation>>().Setup(m => m.Provider).Returns(conversationList.Provider);
            dbSetMock.As<IQueryable<Conversation>>().Setup(m => m.Expression).Returns(conversationList.Expression);
            dbSetMock.As<IQueryable<Conversation>>().Setup(m => m.ElementType).Returns(conversationList.ElementType);
            dbSetMock.As<IQueryable<Conversation>>().Setup(m => m.GetEnumerator()).Returns(conversationList.GetEnumerator());
            context.Setup(x => x.Conversations).Returns(dbSetMock.Object);
            var repository = new ConversationRepository(context.Object);

            //Act
            var conversation = repository.GetConversation(23);

            //Assert
            Assert.IsTrue(conversation.ConversationID == 23);

        }

        [TestMethod]
        public void IsUserInConversation_ConversationIdAndUserIdPassed_ReturnsTrue()
        {
            //Arrange
            var context = new Mock<DatabaseContext>();
            var dbSetMock = new Mock<DbSet<Conversation>>();
            var dbSetMockUserConversations = new Mock<DbSet<UserConversation>>();

            dbSetMock.As<IQueryable<Conversation>>().Setup(m => m.Provider).Returns(conversationList.Provider);
            dbSetMock.As<IQueryable<Conversation>>().Setup(m => m.Expression).Returns(conversationList.Expression);
            dbSetMock.As<IQueryable<Conversation>>().Setup(m => m.ElementType).Returns(conversationList.ElementType);
            dbSetMock.As<IQueryable<Conversation>>().Setup(m => m.GetEnumerator()).Returns(conversationList.GetEnumerator());
            dbSetMockUserConversations.As<IQueryable<UserConversation>>().Setup(m => m.Provider).Returns(userConversations.Provider);
            dbSetMockUserConversations.As<IQueryable<UserConversation>>().Setup(m => m.Expression).Returns(userConversations.Expression);
            dbSetMockUserConversations.As<IQueryable<UserConversation>>().Setup(m => m.ElementType).Returns(userConversations.ElementType);
            dbSetMockUserConversations.As<IQueryable<UserConversation>>().Setup(m => m.GetEnumerator()).Returns(userConversations.GetEnumerator());
            context.Setup(x => x.Conversations).Returns(dbSetMock.Object);
            context.Setup(x => x.UserConversations).Returns(dbSetMockUserConversations.Object);
            var repository = new ConversationRepository(context.Object);

            //Act
            var result = repository.IsUserInConversation(2,23);

            //Assert
            Assert.IsTrue(result);

        }
    }
}
