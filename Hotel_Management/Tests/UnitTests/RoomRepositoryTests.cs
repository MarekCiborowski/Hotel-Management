﻿using DataAccessLayer;
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
    public class RoomRepositoryTests
    {
        private IQueryable<Room> roomList;
        private readonly IQueryable<Room> emptyRoomList;

        public RoomRepositoryTests()
        {
            List<Room> list = new List<Room>
            {
               new Room { RoomId = 23},
               new Room { RoomId = 22}
            };

            // Convert the IEnumerable list to an IQueryable list
            roomList = list.AsQueryable();
            emptyRoomList = new List<Room>().AsQueryable();

        }

        [TestMethod]
        public void GetAllRooms_NoArgumentsPassed_ListIsNotEmpty()
        {
            //Arrange
            var context = new Mock<DatabaseContext>();
            var dbSetMock = new Mock<DbSet<Room>>();

            dbSetMock.As<IQueryable<Room>>().Setup(m => m.Provider).Returns(roomList.Provider);
            dbSetMock.As<IQueryable<Room>>().Setup(m => m.Expression).Returns(roomList.Expression);
            dbSetMock.As<IQueryable<Room>>().Setup(m => m.ElementType).Returns(roomList.ElementType);
            dbSetMock.As<IQueryable<Room>>().Setup(m => m.GetEnumerator()).Returns(roomList.GetEnumerator());
            context.Setup(x => x.Rooms).Returns(dbSetMock.Object);
            var repository = new RoomRepository(context.Object);

            //Act
            var allRooms = repository.GetAllRooms();

            //Assert
            Assert.IsTrue(allRooms.Any());

        }

        [TestMethod]
        public void GetRoom_RoomIdPassed_CorrectRoomReturned()
        {
            //Arrange
            var context = new Mock<DatabaseContext>();
            var dbSetMock = new Mock<DbSet<Room>>();

            dbSetMock.As<IQueryable<Room>>().Setup(m => m.Provider).Returns(roomList.Provider);
            dbSetMock.As<IQueryable<Room>>().Setup(m => m.Expression).Returns(roomList.Expression);
            dbSetMock.As<IQueryable<Room>>().Setup(m => m.ElementType).Returns(roomList.ElementType);
            dbSetMock.As<IQueryable<Room>>().Setup(m => m.GetEnumerator()).Returns(roomList.GetEnumerator());
            context.Setup(x => x.Rooms).Returns(dbSetMock.Object);
            var repository = new RoomRepository(context.Object);

            //Act
            var room = repository.GetRoom(23);

            //Assert
            Assert.IsTrue(room.RoomId == 23);

        }

        [TestMethod]
        public void RemoveRoom_RoomIdPassed_NoExceptionsThrown()
        {
            //Arrange
            var context = new Mock<DatabaseContext>();
            var dbSetMock = new Mock<DbSet<Room>>();
            var reservationsMock = new Mock<DbSet<Reservation>>();
            var roomReservationsMock = new Mock<DbSet<RoomReservation>>();

            dbSetMock.As<IQueryable<Room>>().Setup(m => m.Provider).Returns(roomList.Provider);
            dbSetMock.As<IQueryable<Room>>().Setup(m => m.Expression).Returns(roomList.Expression);
            dbSetMock.As<IQueryable<Room>>().Setup(m => m.ElementType).Returns(roomList.ElementType);
            dbSetMock.As<IQueryable<Room>>().Setup(m => m.GetEnumerator()).Returns(roomList.GetEnumerator());
            dbSetMock.Setup(x => x.Remove(It.IsAny<Room>()));
            context.Setup(x => x.Rooms).Returns(dbSetMock.Object);

            var reservationList = new List<Reservation>().AsQueryable();
            reservationsMock.As<IQueryable<Reservation>>().Setup(m => m.Provider).Returns(reservationList.Provider);
            reservationsMock.As<IQueryable<Reservation>>().Setup(m => m.Expression).Returns(reservationList.Expression);
            reservationsMock.As<IQueryable<Reservation>>().Setup(m => m.ElementType).Returns(reservationList.ElementType);
            reservationsMock.As<IQueryable<Reservation>>().Setup(m => m.GetEnumerator()).Returns(reservationList.GetEnumerator());
            context.Setup(x => x.Reservations).Returns(reservationsMock.Object);

            var roomReservationList = new List<RoomReservation>().AsQueryable();
            roomReservationsMock.As<IQueryable<RoomReservation>>().Setup(m => m.Provider).Returns(roomReservationList.Provider);
            roomReservationsMock.As<IQueryable<RoomReservation>>().Setup(m => m.Expression).Returns(roomReservationList.Expression);
            roomReservationsMock.As<IQueryable<RoomReservation>>().Setup(m => m.ElementType).Returns(roomReservationList.ElementType);
            roomReservationsMock.As<IQueryable<RoomReservation>>().Setup(m => m.GetEnumerator()).Returns(roomReservationList.GetEnumerator());
            context.Setup(x => x.RoomReservations).Returns(roomReservationsMock.Object);

            var repository = new RoomRepository(context.Object);

            //Act
            repository.RemoveRoom(23);

            //Assert
            dbSetMock.Verify(x => x.Remove(It.IsAny<Room>()));

        }

    }
}
