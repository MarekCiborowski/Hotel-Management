using DataAccessLayer;
using DomainObjects.Entities;
using Repositories.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Services
{
    public class RoomService
    {
        private AmenityRepository amenityRepository;
        private RoomRepository roomRepository;

        public RoomService(DatabaseContext context)
        {
            this.amenityRepository = new AmenityRepository(context);
            this.roomRepository = new RoomRepository(context);
        }

        public Amenity AddAmenity(string amenityName)
        {
            var newAmenity = this.amenityRepository.AddAmenity(amenityName);

            return newAmenity;
        }

        public bool IsAmenityAlreadySaved(string amenityName)
        {
            return this.amenityRepository.IsAmenityAlreadySaved(amenityName);
        }

        public void RemoveAmenity(int? id)
        {
            this.amenityRepository.RemoveAmenity(id);
        }

        public List<Room> GetRoomsWithAmenities (List<int> amenityIds)
        {
            return this.roomRepository.GetRoomsWithAmenities(amenityIds);
        }

        public List<Amenity> GetAllAmenities()
        {
            return this.amenityRepository.GetAllAmenities();
        }

        public Room GetRoom(int roomId)
        {
            return this.roomRepository.GetRoom(roomId);
        }

        public List<Room> GetAvailableRooms(DateTime requestedAccomodationDate, DateTime requestedCheckOutDate, string[] requestedAmenityIds, int numberOfGuests, decimal minRoomSize)
        {
            var amenityIds = new List<int>();
            foreach(var amenityId in requestedAmenityIds)
            {
                amenityIds.Add(int.Parse(amenityId));
            }
            return this.roomRepository.GetAvailableRooms(requestedAccomodationDate, requestedCheckOutDate, amenityIds, numberOfGuests, minRoomSize);    
        }

        public List<Amenity> GetAmenitiesOfRoom(int roomId)
        {
            return this.amenityRepository.GetAmenitiesOfRoom(roomId);
        }


    }
}
