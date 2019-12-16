using DataAccessLayer;
using DomainObjects.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using NSubstitute;
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
    public class MessageRepositoryTests
    {
        private IQueryable<Message> messageList;
        private readonly IQueryable<Message> emptyMessageList;
        private IQueryable<User> userList;

        public MessageRepositoryTests()
        {
            List<Message> list = new List<Message>
            {
               new Message { MessageID = 23, ConversationID = 2, UserId = 2, User = new User
                {
                    Identity = 2
                }},
               new Message { MessageID = 22 , ConversationID =2, UserId = 3, User = new User
                {
                    Identity = 3
                }}
            };

            List <User> users = new List<User>
            {
                new User
                {
                    Identity = 2
                },
                new User
                {
                    Identity = 3
                }
            };

            // Convert the IEnumerable list to an IQueryable list
            messageList = list.AsQueryable();
            emptyMessageList = new List<Message>().AsQueryable();
            userList = users.AsQueryable();

        }

        [TestMethod]
        public void AddMessageToConversation_AllArgumentsPassed_AddedSuccesfully()
        {
            //Arrange
            var context = new Mock<DatabaseContext>();
            var dbSetMock = new Mock<DbSet<Message>>();

            dbSetMock.Setup(x => x.Add(It.IsAny<Message>())).Returns(new Message());
            context.Setup(x => x.Messages).Returns(dbSetMock.Object);
            var repository = new MessageRepository(context.Object);

            //Act
            repository.AddMessageToConversation("message", 1,1);

            //Assert
            context.Verify(x => x.Set<Message>());
            dbSetMock.Verify(x => x.Add(It.IsAny<Message>()));

        }

        [TestMethod]
        public void GetConversationMessagesWithUsers_ConversationIdPassed_CorrectMessagesReturned()
        {
            //Arrange
            var context = new Mock<DatabaseContext>();
            var dbSetMock = new Mock<DbSet<Message>>();
            var dbSetMockUsers = new Mock<DbSet<User>>();

            dbSetMock.As<IQueryable<Message>>().Setup(m => m.Provider).Returns(messageList.Provider);
            dbSetMock.As<IQueryable<Message>>().Setup(m => m.Expression).Returns(messageList.Expression);
            dbSetMock.As<IQueryable<Message>>().Setup(m => m.ElementType).Returns(messageList.ElementType);
            dbSetMock.As<IQueryable<Message>>().Setup(m => m.GetEnumerator()).Returns(messageList.GetEnumerator());
            context.Setup(x => x.Messages).Returns(dbSetMock.Object);

            dbSetMockUsers.As<IQueryable<User>>().Setup(m => m.Provider).Returns(userList.Provider);
            dbSetMockUsers.As<IQueryable<User>>().Setup(m => m.Expression).Returns(userList.Expression);
            dbSetMockUsers.As<IQueryable<User>>().Setup(m => m.ElementType).Returns(userList.ElementType);
            dbSetMockUsers.As<IQueryable<User>>().Setup(m => m.GetEnumerator()).Returns(userList.GetEnumerator());
            context.Setup(x => x.Users).Returns(dbSetMockUsers.Object);

            var mockSet = Substitute.For<DbSet<Message>, IQueryable<Message>>();
            ((IQueryable<Message>)mockSet).Provider.Returns(messageList.Provider);
            ((IQueryable<Message>)mockSet).Expression.Returns(messageList.Expression);
            ((IQueryable<Message>)mockSet).ElementType.Returns(messageList.ElementType);
            ((IQueryable<Message>)mockSet).GetEnumerator().Returns(messageList.GetEnumerator());

            mockSet.Include(Arg.Any<string>()).Returns(mockSet);

            var repository = new MessageRepository(context.Object);
            context.Setup(x => x.Messages).Returns(mockSet);

            //Act
            var messages = repository.GetConversationMessagesWithUsers(2);

            //Assert
            Assert.IsTrue(messages.Any());

        }
    }
}
