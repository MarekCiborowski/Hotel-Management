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
    public class ReservationRepositoryTests
    {
        private IQueryable<Reservation> reservationList;
        private IQueryable<RoomReservation> roomReservations;
        private IQueryable<User> users;
        private readonly IQueryable<Reservation> emptyReservationList;


        public ReservationRepositoryTests()
        {
            List<Reservation> list = new List<Reservation>
            {
               new Reservation { ReservationId = 23, UserId = 2, AccomodationDate = DateTime.Today, CheckOutDate = DateTime.Today.AddDays(1), User = new User{
                   Identity = 2, FirstName = "John", LastName = "Doe"
               } },
               new Reservation { ReservationId = 22, UserId = 3, AccomodationDate = DateTime.Today, CheckOutDate = DateTime.Today.AddDays(1), User = new User{
                   Identity = 3, FirstName = "Jan", LastName = "Kowalski"
               }}
            };

            List<RoomReservation> roomReservationsList = new List<RoomReservation>
            {
                new RoomReservation {ReservationId = 23, RoomId = 2},
                new RoomReservation {ReservationId = 22, RoomId = 3}
            };

            List<User> usersList = new List<User>
            {
                new User {Identity = 2, FirstName = "John", LastName = "Doe"},
                new User {Identity = 3, FirstName = "Jan", LastName = "Kowalski"}
            };

            // Convert the IEnumerable list to an IQueryable list
            reservationList = list.AsQueryable();
            roomReservations = roomReservationsList.AsQueryable();
            users = usersList.AsQueryable();
            emptyReservationList = new List<Reservation>().AsQueryable();

        }

        [TestMethod]
        public void GetReservationsDto_NoArgumentsPassed_ListIsNotEmpty()
        {
            //Arrange
            var context = new Mock<DatabaseContext>();
            var dbSetMock = new Mock<DbSet<Reservation>>();
            var dbSetRoomReservations = new Mock<DbSet<RoomReservation>>();
            var dbSetUsers = new Mock<DbSet<User>>();

            dbSetMock.As<IQueryable<Reservation>>().Setup(m => m.Provider).Returns(reservationList.Provider);
            dbSetMock.As<IQueryable<Reservation>>().Setup(m => m.Expression).Returns(reservationList.Expression);
            dbSetMock.As<IQueryable<Reservation>>().Setup(m => m.ElementType).Returns(reservationList.ElementType);
            dbSetMock.As<IQueryable<Reservation>>().Setup(m => m.GetEnumerator()).Returns(reservationList.GetEnumerator());
            context.Setup(x => x.Reservations).Returns(dbSetMock.Object);

            dbSetRoomReservations.As<IQueryable<RoomReservation>>().Setup(m => m.Provider).Returns(roomReservations.Provider);
            dbSetRoomReservations.As<IQueryable<RoomReservation>>().Setup(m => m.Expression).Returns(roomReservations.Expression);
            dbSetRoomReservations.As<IQueryable<RoomReservation>>().Setup(m => m.ElementType).Returns(roomReservations.ElementType);
            dbSetRoomReservations.As<IQueryable<RoomReservation>>().Setup(m => m.GetEnumerator()).Returns(roomReservations.GetEnumerator());
            context.Setup(x => x.RoomReservations).Returns(dbSetRoomReservations.Object);

            dbSetUsers.As<IQueryable<User>>().Setup(m => m.Provider).Returns(users.Provider);
            dbSetUsers.As<IQueryable<User>>().Setup(m => m.Expression).Returns(users.Expression);
            dbSetUsers.As<IQueryable<User>>().Setup(m => m.ElementType).Returns(users.ElementType);
            dbSetUsers.As<IQueryable<User>>().Setup(m => m.GetEnumerator()).Returns(users.GetEnumerator());
            context.Setup(x => x.Users).Returns(dbSetUsers.Object);

            var repository = new ReservationRepository(context.Object);

            //Act
            var allReservations = repository.GetReservationsDto();

            //Assert
            Assert.IsTrue(allReservations.Any());

        }

        [TestMethod]
        public void GetReservationsCalendarDto_RoomIdPassed_ListIsNotEmpty()
        {
            //Arrange
            //Arrange
            var context = new Mock<DatabaseContext>();
            var dbSetMock = new Mock<DbSet<Reservation>>();
            var dbSetRoomReservations = new Mock<DbSet<RoomReservation>>();
            var dbSetUsers = new Mock<DbSet<User>>();

            dbSetMock.As<IQueryable<Reservation>>().Setup(m => m.Provider).Returns(reservationList.Provider);
            dbSetMock.As<IQueryable<Reservation>>().Setup(m => m.Expression).Returns(reservationList.Expression);
            dbSetMock.As<IQueryable<Reservation>>().Setup(m => m.ElementType).Returns(reservationList.ElementType);
            dbSetMock.As<IQueryable<Reservation>>().Setup(m => m.GetEnumerator()).Returns(reservationList.GetEnumerator());
            context.Setup(x => x.Reservations).Returns(dbSetMock.Object);

            dbSetRoomReservations.As<IQueryable<RoomReservation>>().Setup(m => m.Provider).Returns(roomReservations.Provider);
            dbSetRoomReservations.As<IQueryable<RoomReservation>>().Setup(m => m.Expression).Returns(roomReservations.Expression);
            dbSetRoomReservations.As<IQueryable<RoomReservation>>().Setup(m => m.ElementType).Returns(roomReservations.ElementType);
            dbSetRoomReservations.As<IQueryable<RoomReservation>>().Setup(m => m.GetEnumerator()).Returns(roomReservations.GetEnumerator());
            context.Setup(x => x.RoomReservations).Returns(dbSetRoomReservations.Object);

            dbSetUsers.As<IQueryable<User>>().Setup(m => m.Provider).Returns(users.Provider);
            dbSetUsers.As<IQueryable<User>>().Setup(m => m.Expression).Returns(users.Expression);
            dbSetUsers.As<IQueryable<User>>().Setup(m => m.ElementType).Returns(users.ElementType);
            dbSetUsers.As<IQueryable<User>>().Setup(m => m.GetEnumerator()).Returns(users.GetEnumerator());
            context.Setup(x => x.Users).Returns(dbSetUsers.Object);

            var repository = new ReservationRepository(context.Object);

            //Act
            var room = repository.GetReservationsCalendarDto(2);

            //Assert
            Assert.IsTrue(true);

        }

        [TestMethod]
        public void GetUserReservationsDto_UserIdPassed_ListIsNotEmpty()
        {
            //Arrange
            //Arrange
            var context = new Mock<DatabaseContext>();
            var dbSetMock = new Mock<DbSet<Reservation>>();
            var dbSetRoomReservations = new Mock<DbSet<RoomReservation>>();
            var dbSetUsers = new Mock<DbSet<User>>();

            dbSetMock.As<IQueryable<Reservation>>().Setup(m => m.Provider).Returns(reservationList.Provider);
            dbSetMock.As<IQueryable<Reservation>>().Setup(m => m.Expression).Returns(reservationList.Expression);
            dbSetMock.As<IQueryable<Reservation>>().Setup(m => m.ElementType).Returns(reservationList.ElementType);
            dbSetMock.As<IQueryable<Reservation>>().Setup(m => m.GetEnumerator()).Returns(reservationList.GetEnumerator());
            context.Setup(x => x.Reservations).Returns(dbSetMock.Object);

            dbSetRoomReservations.As<IQueryable<RoomReservation>>().Setup(m => m.Provider).Returns(roomReservations.Provider);
            dbSetRoomReservations.As<IQueryable<RoomReservation>>().Setup(m => m.Expression).Returns(roomReservations.Expression);
            dbSetRoomReservations.As<IQueryable<RoomReservation>>().Setup(m => m.ElementType).Returns(roomReservations.ElementType);
            dbSetRoomReservations.As<IQueryable<RoomReservation>>().Setup(m => m.GetEnumerator()).Returns(roomReservations.GetEnumerator());
            context.Setup(x => x.RoomReservations).Returns(dbSetRoomReservations.Object);

            dbSetUsers.As<IQueryable<User>>().Setup(m => m.Provider).Returns(users.Provider);
            dbSetUsers.As<IQueryable<User>>().Setup(m => m.Expression).Returns(users.Expression);
            dbSetUsers.As<IQueryable<User>>().Setup(m => m.ElementType).Returns(users.ElementType);
            dbSetUsers.As<IQueryable<User>>().Setup(m => m.GetEnumerator()).Returns(users.GetEnumerator());
            context.Setup(x => x.Users).Returns(dbSetUsers.Object);

            var repository = new ReservationRepository(context.Object);

            //Act
            var userReservations = repository.GetUserReservationsDto(2);

            //Assert
            Assert.IsTrue(userReservations.Any());

        }
    }
}
