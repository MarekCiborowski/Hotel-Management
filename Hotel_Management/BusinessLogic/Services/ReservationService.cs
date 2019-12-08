using DataAccessLayer;
using DomainObjects.Entities;
using Repositories.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DomainObjects.Enums;

namespace BusinessLogic.Services
{
    public class ReservationService
    {

        private ReservationRepository reservationRepository;
        private RoomRepository roomRepository;

        public ReservationService(DatabaseContext databaseContext)
        {
            this.reservationRepository = new ReservationRepository(databaseContext);
            this.roomRepository = new RoomRepository(databaseContext);
        }

        public Reservation AddReservation(int roomId, int arrangerId, DateTime requestedAccomodationDate, DateTime requestedCheckOutDate, ReservationStatusEnum reservationStatus, HotelBookingSiteEnum hotelBookingSite)
        {
            var room = this.roomRepository.GetRoom(roomId);
            var numberOfDays = (int)Math.Ceiling((requestedCheckOutDate - requestedAccomodationDate).TotalDays);
            var totalCost = Math.Round(numberOfDays * room.Cost, 2);

            return this.reservationRepository.AddReservation(roomId, arrangerId, requestedAccomodationDate, requestedCheckOutDate, reservationStatus, hotelBookingSite, totalCost);
        }


    }
}
