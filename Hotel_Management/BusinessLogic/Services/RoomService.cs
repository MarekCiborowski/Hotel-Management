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
        private RoomService roomService;

        public RoomService(DatabaseContext context)
        {
            this.amenityRepository = new AmenityRepository(context);
            this.roomService = new RoomService(context);
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
            return this.roomService.GetRoomsWithAmenities(amenityIds);
        }


    }
}
