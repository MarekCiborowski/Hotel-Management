using DataAccessLayer;
using DomainObjects.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Repositories.Repositories;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Tests.UnitTests
{
    [TestClass]
    public class UserRepositoryTests
    {
        private IQueryable<User> userList;
        private readonly IQueryable<User> emptyUserList;

        public UserRepositoryTests()
        {
            List<User> list = new List<User>
            {
               new User { Identity = 23, FirstName = "Logan" },
               new User { Identity = 22, FirstName = "George" }
            };

            // Convert the IEnumerable list to an IQueryable list
            userList = list.AsQueryable();
            emptyUserList = new List<User>().AsQueryable();

        }

        public User GetTestUser()
        {
            return new User
            {
                Address = "Świerkowa 2",
                City = "Warszawa",
                Email = "email@wp.pl",
                FirstName = "Jan",
                LastName = "Kowalski",
                Identity = 22,
                IsConfirmed = false,
                Login = "jankowalski",
                Password = "123123",
                Zipcode = "11-111",
                RoleId = DomainObjects.Enums.RolesEnum.RegularUser
            };
        } 

        [TestMethod]
        public void AddUser_UserModelPassed_UserAddedSuccessfully()
        {
            //Arrange
            var testUser = GetTestUser();

            var context = new Mock<DatabaseContext>();
            var dbSetMock = new Mock<DbSet<User>>();
            context.Setup(x => x.Users).Returns(dbSetMock.Object);
            dbSetMock.Setup(x => x.Add(It.IsAny<User>())).Returns(testUser);

            // Act
            var repository = new UserRepository(context.Object);
            repository.AddUser(testUser);

            //Assert
            context.Verify(x => x.Set<User>());
            dbSetMock.Verify(x => x.Add(It.IsAny<User>()));
        }

        [TestMethod]
        public void IsEmailCorrect_CorrectEmailPassed_ReturnsTrue()
        {
            //Arrange
            var email = "email@email.email";
            

            var context = new Mock<DatabaseContext>();
            var dbSetMock = new Mock<DbSet<User>>();
            
            dbSetMock.As<IQueryable<User>>().Setup(m => m.Provider).Returns(userList.Provider);
            dbSetMock.As<IQueryable<User>>().Setup(m => m.Expression).Returns(userList.Expression);
            dbSetMock.As<IQueryable<User>>().Setup(m => m.ElementType).Returns(userList.ElementType);
            dbSetMock.As<IQueryable<User>>().Setup(m => m.GetEnumerator()).Returns(userList.GetEnumerator());
            context.Setup(x => x.Users).Returns(dbSetMock.Object);
            var repository = new UserRepository(context.Object);

            // Act

            var result = repository.IsEmailCorrect(email);

            //Assert
           
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void IsLoginFree_CorrectLoginPassed_ReturnsTrue()
        {
            //Arrange
            var email = "email@email.email";


            var context = new Mock<DatabaseContext>();
            var dbSetMock = new Mock<DbSet<User>>();

            dbSetMock.As<IQueryable<User>>().Setup(m => m.Provider).Returns(userList.Provider);
            dbSetMock.As<IQueryable<User>>().Setup(m => m.Expression).Returns(userList.Expression);
            dbSetMock.As<IQueryable<User>>().Setup(m => m.ElementType).Returns(userList.ElementType);
            dbSetMock.As<IQueryable<User>>().Setup(m => m.GetEnumerator()).Returns(userList.GetEnumerator());
            context.Setup(x => x.Users).Returns(dbSetMock.Object);
            var repository = new UserRepository(context.Object);

            // Act

            var result = repository.IsLoginFree(email);

            //Assert

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void RemoveUser_UserIdPassed_NoExceptionThrown()
        {
            //Arrange
            var id = 23;


            var context = new Mock<DatabaseContext>();
            var dbSetMock = new Mock<DbSet<User>>();

            dbSetMock.As<IQueryable<User>>().Setup(m => m.Provider).Returns(userList.Provider);
            dbSetMock.As<IQueryable<User>>().Setup(m => m.Expression).Returns(userList.Expression);
            dbSetMock.As<IQueryable<User>>().Setup(m => m.ElementType).Returns(userList.ElementType);
            dbSetMock.As<IQueryable<User>>().Setup(m => m.GetEnumerator()).Returns(userList.GetEnumerator());
            dbSetMock.Setup(x => x.Remove(It.IsAny<User>()));
            context.Setup(x => x.Users).Returns(dbSetMock.Object);
            var repository = new UserRepository(context.Object);

            // Act

            repository.RemoveUser(id);

            //Assert
            dbSetMock.Verify(x => x.Remove(It.IsAny<User>()));

            //No exception thrown
        }

    }
}
