using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;
using DomainObjects.Entities;

namespace Repositories.Repositories
{
    public class AmenityRepository
    {
        private DatabaseContext db;

        public AmenityRepository(DatabaseContext databaseContext)
        {
            this.db = databaseContext;
        }

        public Amenity GetAmenityById(int amenityId)
        {
            return this.db.Amenities.FirstOrDefault(a => a.AmenityId == amenityId);
        }

        public Amenity AddAmenity (string amenityName)
        {
            var newAmenity = new Amenity
            {
                AmenityName = amenityName
            };
            
            using(var dbContextTransaction = db.Database.BeginTransaction())
            {
                try
                {
                    newAmenity = db.Amenities.Add(newAmenity);
                    db.SaveChanges();
                    dbContextTransaction.Commit();
                    return newAmenity;
                }
                catch (Exception)
                {
                    dbContextTransaction.Rollback();
                    return null;
                }
            }
        }

        public bool IsAmenityAlreadySaved (string amenityName)
        {
            var amenity = db.Amenities.FirstOrDefault(a => a.AmenityName == amenityName);

            return amenity == null ? false : true;
        }

        public void RemoveAmenity (int? id)
        {
            if (!id.HasValue)
            {
                throw new ArgumentNullException("Null argument");
            }

            using(var dbContextTransaction = db.Database.BeginTransaction())
            {
                try
                {
                    var amenityToRemove = db.Amenities.FirstOrDefault(a => a.AmenityId == id);
                    db.Amenities.Remove(amenityToRemove);
                    db.SaveChanges();
                    dbContextTransaction.Commit();
                }
                catch(Exception)
                {
                    dbContextTransaction.Rollback();
                }
            }
        }

        public List<Amenity> GetAmenitiesOfRoom(int roomId)
        {
            return this.db.Amenities.Where(a => db.RoomAmenities.Where(ra => ra.RoomId == roomId).Select(ra => ra.AmenityId).Contains(a.AmenityId)).ToList();
        }

        public List<Amenity> GetAllAmenities()
        {
            return this.db.Amenities.ToList();
        }
    }
}
