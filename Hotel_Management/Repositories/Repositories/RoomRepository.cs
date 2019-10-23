﻿using DataAccessLayer;
using DomainObjects.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Repositories
{
    public class RoomRepository
    {
        private DatabaseContext db;

        public RoomRepository(DatabaseContext databaseContext)
        {
            this.db = databaseContext;
        }
        public Room AddRoom(decimal cost, int maxNumberOfGuests)
        {
            var newRoom = new Room
            {
                Cost = cost,
                MaxNumberOfGuests = maxNumberOfGuests
            };

            using (var dbContextTransaction = db.Database.BeginTransaction())
            {
                try
                {
                    newRoom = db.Rooms.Add(newRoom);
                    db.SaveChanges();
                    dbContextTransaction.Commit();
                    return newRoom;
                }
                catch (Exception)
                {
                    dbContextTransaction.Rollback();
                    return null;
                }
            }
        }

        public Room EditRoom(Room editedRoom)
        {
            using (var dbContextTransaction = db.Database.BeginTransaction())
            {
                try
                {
                    db.Entry(editedRoom).State = EntityState.Modified;
                    db.SaveChanges();
                    dbContextTransaction.Commit();
                    return editedRoom;
                }
                catch (Exception)
                {
                    dbContextTransaction.Rollback();
                    return editedRoom;
                }
            }

        }

        public List<Room> GetRoomsWithAmenities(List<int> amenityIds)
        {
            var roomsWithAmenities = db.Rooms.Where(r => r.RoomAmenities.Where(ra => amenityIds.Contains(ra.AmenityId)).Count() == amenityIds.Count()).ToList();
            return roomsWithAmenities;
        }

        public bool IsAmenityAlreadyAddedToRoom(int amenityId, int roomId)
        {
            var roomAmenity = db.RoomAmenities.FirstOrDefault(ra => ra.AmenityId == amenityId && ra.RoomId == roomId);
            return roomAmenity == null ? false : true;
        }

        public void AddAmenityToRoom(int amenityId, int roomId)
        {
            var roomAmenity = new RoomAmenity
            {
                AmenityId = amenityId,
                RoomId = roomId
            };

            using (var dbContextTransaction = db.Database.BeginTransaction())
            {
                try
                {
                    db.RoomAmenities.Add(roomAmenity);
                    db.SaveChanges();
                    dbContextTransaction.Commit();
                }
                catch (Exception)
                {
                    dbContextTransaction.Rollback();
                }
            }
        }

        public void RemoveAmenityFromRoom(int amenityId, int roomId)
        {
            var roomAmenity = db.RoomAmenities.FirstOrDefault(ra => ra.AmenityId == amenityId && ra.RoomId == roomId);

            using (var dbContextTransaction = db.Database.BeginTransaction())
            {
                try
                {
                    db.RoomAmenities.Remove(roomAmenity);
                    db.SaveChanges();
                    dbContextTransaction.Commit();
                }
                catch (Exception)
                {
                    dbContextTransaction.Rollback();
                }
            }
        }

        public List<Amenity> GetRoomAmenities(int? roomId)
        {
            if (!roomId.HasValue)
            {
                throw new ArgumentNullException("Null argument");
            }
            var roomAmenities = db.Amenities.Where(a => db.RoomAmenities.Where(ra => ra.RoomId == roomId).Any(ra => ra.AmenityId == a.AmenityId)).ToList();

            return roomAmenities;
        }
    }
}
