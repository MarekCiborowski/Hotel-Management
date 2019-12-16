using DataAccessLayer;
using DomainObjects.Dto;
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

        private readonly ReservationRepository reservationRepository;
        private readonly RoomRepository roomRepository;

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

        public void UpdateReservations()
        {
            this.reservationRepository.UpdateReservations();
        }

        public Reservation ChangeReservationStatus(int reservationId, ReservationStatusEnum statusToSet)
        {
            return this.reservationRepository.ChangeReservationStatus(reservationId, statusToSet);
        }

        public List<ReservationDto> GetReservationsDto()
        {
            return this.reservationRepository.GetReservationsDto();
        }

        public List<ReservationDto> GetUserReservationsDto(int userId)
        {
            return this.reservationRepository.GetUserReservationsDto(userId);
        }

        public List<ReservationCalendarListItemDto> GetReservationsCalendarDto(int roomId)
        {
            return this.reservationRepository.GetReservationsCalendarDto(roomId);
        }

        public bool CanMakeReservation(int roomId, DateTime accomodationDate, DateTime checkOutDate)
        {
            return this.reservationRepository.CanMakeReservation(roomId, accomodationDate, checkOutDate);
        }


    }
}
