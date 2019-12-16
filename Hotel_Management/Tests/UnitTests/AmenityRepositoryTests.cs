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
    public class AmenityRepositoryTests
    {
        private IQueryable<Amenity> amenityList;
        private readonly IQueryable<Amenity> emptyAmenityList;

        public AmenityRepositoryTests()
        {
         
            List<Amenity> list = new List<Amenity>
            {
               new Amenity { AmenityId = 23},
               new Amenity { AmenityId = 22}
            };

            // Convert the IEnumerable list to an IQueryable list
            amenityList = list.AsQueryable();
            emptyAmenityList = new List<Amenity>().AsQueryable();

        }

        [TestMethod]
        public void GetAllAmenities_NoArgumentsPassed_ListIsNotEmpty()
        {
            //Arrange
            var context = new Mock<DatabaseContext>();
            var dbSetMock = new Mock<DbSet<Amenity>>();

            dbSetMock.As<IQueryable<Amenity>>().Setup(m => m.Provider).Returns(amenityList.Provider);
            dbSetMock.As<IQueryable<Amenity>>().Setup(m => m.Expression).Returns(amenityList.Expression);
            dbSetMock.As<IQueryable<Amenity>>().Setup(m => m.ElementType).Returns(amenityList.ElementType);
            dbSetMock.As<IQueryable<Amenity>>().Setup(m => m.GetEnumerator()).Returns(amenityList.GetEnumerator());
            context.Setup(x => x.Amenities).Returns(dbSetMock.Object);
            var repository = new AmenityRepository(context.Object);

            //Act
            var allAmenities = repository.GetAllAmenities();

            //Assert
            Assert.IsTrue(allAmenities.Any());

        }

        [TestMethod]
        public void GetAmenity_AmenityIdPassed_CorrectAmenityReturned()
        {
            //Arrange
            var context = new Mock<DatabaseContext>();
            var dbSetMock = new Mock<DbSet<Amenity>>();

            dbSetMock.As<IQueryable<Amenity>>().Setup(m => m.Provider).Returns(amenityList.Provider);
            dbSetMock.As<IQueryable<Amenity>>().Setup(m => m.Expression).Returns(amenityList.Expression);
            dbSetMock.As<IQueryable<Amenity>>().Setup(m => m.ElementType).Returns(amenityList.ElementType);
            dbSetMock.As<IQueryable<Amenity>>().Setup(m => m.GetEnumerator()).Returns(amenityList.GetEnumerator());
            context.Setup(x => x.Amenities).Returns(dbSetMock.Object);
            var repository = new AmenityRepository(context.Object);

            //Act
            var amenity = repository.GetAmenityById(23);

            //Assert
            Assert.IsTrue(amenity.AmenityId == 23);

        }

        [TestMethod]
        public void RemoveAmenity_AmenityIdPassed_NoExceptionsThrown()
        {
            //Arrange
            var context = new Mock<DatabaseContext>();
            var dbSetMock = new Mock<DbSet<Amenity>>();

            dbSetMock.As<IQueryable<Amenity>>().Setup(m => m.Provider).Returns(amenityList.Provider);
            dbSetMock.As<IQueryable<Amenity>>().Setup(m => m.Expression).Returns(amenityList.Expression);
            dbSetMock.As<IQueryable<Amenity>>().Setup(m => m.ElementType).Returns(amenityList.ElementType);
            dbSetMock.As<IQueryable<Amenity>>().Setup(m => m.GetEnumerator()).Returns(amenityList.GetEnumerator());
            dbSetMock.Setup(x => x.Remove(It.IsAny<Amenity>()));
            context.Setup(x => x.Amenities).Returns(dbSetMock.Object);
            var repository = new AmenityRepository(context.Object);

            //Act
            repository.RemoveAmenity(23);

            //Assert
            dbSetMock.Verify(x => x.Remove(It.IsAny<Amenity>()));

        }
    }
}
