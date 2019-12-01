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

        public ReservationService(DatabaseContext databaseContext)
        {
            this.reservationRepository = new ReservationRepository(databaseContext);
        }

        public Reservation AddReservation(int roomId, int arrangerId, DateTime requestedAccomodationDate, DateTime requestedCheckOutDate, ReservationStatusEnum reservationStatus)
        {
            return this.reservationRepository.AddReservation(roomId, arrangerId, requestedAccomodationDate, requestedCheckOutDate, reservationStatus);
        }


    }
}
