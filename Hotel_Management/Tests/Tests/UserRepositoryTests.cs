using DataAccessLayer;
using DomainObjects.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Repositories.Repositories;
using System.Data.Entity;

namespace Tests.Tests
{
    public class UserRepositoryTests
    {
        private UserRepository userRepository;
        private DatabaseContext databaseContext;

        public UserRepositoryTests()
        {
            this.databaseContext = new DatabaseContext();
            this.userRepository = new UserRepository(this.databaseContext);
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
            context.Setup(x => x.Set<User>()).Returns(dbSetMock.Object);
            dbSetMock.Setup(x => x.Add(It.IsAny<User>())).Returns(testUser);

            // Act
            var repository = new UserRepository(context.Object);
            repository.AddUser(testUser);

            //Assert
            context.Verify(x => x.Set<User>());
            dbSetMock.Verify(x => x.Add(It.Is<User>(y => y == testUser)));
            Assert.IsTrue(2 == 2);
        }

    }
}
