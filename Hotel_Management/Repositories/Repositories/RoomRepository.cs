using DataAccessLayer;
using DomainObjects.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DomainObjects.Enums;

namespace Repositories.Repositories
{
    public class RoomRepository
    {
        private DatabaseContext db;

        public RoomRepository(DatabaseContext databaseContext)
        {
            this.db = databaseContext;
        }

        public List<Room> GetAllRooms()
        {
            return this.db.Rooms.ToList();
        }

        public Room AddRoom(decimal cost, int maxNumberOfGuests, decimal roomSize, List<int> amenityIds)
        {
            var newRoom = new Room
            {
                Cost = cost,
                MaxNumberOfGuests = maxNumberOfGuests,
                RoomSize = roomSize,
            };

            using (var dbContextTransaction = db.Database.BeginTransaction())
            {
                try
                {
                    newRoom = db.Rooms.Add(newRoom);
                    db.SaveChanges();
                    var roomAmenities = amenityIds.Select(amenityId => new RoomAmenity
                    {
                        AmenityId = amenityId,
                        RoomId = newRoom.RoomId
                    }).ToList();

                    db.RoomAmenities.AddRange(roomAmenities);
                    db.SaveChanges();
                    dbContextTransaction.Commit();
                    return newRoom;
                }
                catch (Exception e)
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

        public Room GetRoom(int roomId)
        {
            return this.db.Rooms.FirstOrDefault(r => r.RoomId == roomId);
        }

        public List<Room> GetAvailableRooms(DateTime requestedAccomodationDate, DateTime requestedCheckOutDate, List<int> requestedAmenityIds, int numberOfGuests, decimal minRoomSize)
        {

            var roomsWithAmenities = this.GetRoomsWithAmenities(requestedAmenityIds);

            var availableRooms = db.Rooms
                .Where(room => !db.Reservations
                        .Where(reservation => room.RoomReservations.Select(rr => rr.ReservationId).Contains(reservation.ReservationId)
                        && ((reservation.CheckOutDate > requestedAccomodationDate && reservation.AccomodationDate <= requestedAccomodationDate)
                            || (reservation.CheckOutDate >= requestedCheckOutDate && reservation.AccomodationDate < requestedCheckOutDate)
                            || (reservation.AccomodationDate >= requestedAccomodationDate && reservation.CheckOutDate <= requestedCheckOutDate))
                        && reservation.ReservationStatusId != ReservationStatusEnum.Canceled).Any()
                    && room.MaxNumberOfGuests >= numberOfGuests
                    && room.RoomSize >= minRoomSize).ToList();
            availableRooms = availableRooms.Where(ar => roomsWithAmenities.Select(r => r.RoomId).Contains(ar.RoomId)).ToList();

            //var rom = db.Rooms
            //    .Where(rrr => !db.Reservations
            //            .Where(reservation => rrr.RoomReservations.Select(rr => rr.ReservationId).Contains(reservation.ReservationId) && ((reservation.CheckOutDate > requestedAccomodationDate && reservation.AccomodationDate <= requestedAccomodationDate)
            //                || (reservation.CheckOutDate >= requestedCheckOutDate && reservation.AccomodationDate < requestedCheckOutDate)
            //                || (reservation.AccomodationDate >= requestedAccomodationDate && reservation.CheckOutDate <= requestedCheckOutDate))
            //            && reservation.ReservationStatusId != ReservationStatusEnum.Canceled).Any()).ToList();
            return availableRooms;
        }

        public void RemoveRoom(int roomId)
        {
            var roomToDelete = this.db.Rooms.FirstOrDefault(r => r.RoomId == roomId);
            this.db.Rooms.Remove(roomToDelete);
            db.SaveChanges();
        }
    }
}
