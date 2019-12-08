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
    public class ReservationRepository
    {
        private DatabaseContext db;

        public ReservationRepository(DatabaseContext databaseContext)
        {
            this.db = databaseContext;
        }

        public Reservation AddReservation (int roomId, int arrangerId, DateTime requestedAccomodationDate, DateTime requestedCheckOutDate, ReservationStatusEnum reservationStatus, HotelBookingSiteEnum hotelBookingSite, decimal totalCost)
        {
            var newReservation = new Reservation
            {
                UserId = arrangerId,
                ReservationStatusId = reservationStatus,
                AccomodationDate = requestedAccomodationDate,
                CheckOutDate = requestedCheckOutDate,
                HotelBookingSiteId = hotelBookingSite,
                TotalCost = totalCost
            };

            using(var dbContextTransaction = db.Database.BeginTransaction())
            {
                try
                {
                    db.Reservations.Add(newReservation);
                    db.SaveChanges();
                    db.RoomReservations.Add(new RoomReservation
                    {
                        ReservationId = newReservation.ReservationId,
                        RoomId = roomId
                    });
                    db.SaveChanges();
                    dbContextTransaction.Commit();
                    return newReservation;
                }
                catch
                {
                    dbContextTransaction.Rollback();
                    return null;
                }
            }
        }

        public Reservation EditReservation(Reservation editedReservation)
        {

            using (var dbContextTransaction = db.Database.BeginTransaction())
            {
                try
                {
                    db.Entry(editedReservation).State = EntityState.Modified;
                    db.SaveChanges();
                    dbContextTransaction.Commit();
                    return editedReservation;
                }
                catch (Exception)
                {
                    dbContextTransaction.Rollback();
                    return editedReservation;
                }
            }

        }

        public Reservation ChangeReservationStatus(int reservationId, ReservationStatusEnum statusToSet)
        {
            var reservation = db.Reservations.FirstOrDefault(r => r.ReservationId == reservationId);
            reservation.ReservationStatusId = statusToSet;
            reservation = this.EditReservation(reservation);

            return reservation;
        }

        public void UpdateReservations()
        {
            foreach (var reservation in db.Reservations.Where(r => r.CheckOutDate < DateTime.Now && r.ReservationStatusId == ReservationStatusEnum.Confirmed 
                || r.AccomodationDate < DateTime.Now && r.ReservationStatusId == ReservationStatusEnum.AwaitingConfirmation))
            {
                if(reservation.ReservationStatusId == ReservationStatusEnum.Confirmed)
                {
                    reservation.ReservationStatusId = ReservationStatusEnum.Closed;
                }
                else if(reservation.ReservationStatusId == ReservationStatusEnum.AwaitingConfirmation){
                    reservation.ReservationStatusId = ReservationStatusEnum.Canceled;
                }
                this.EditReservation(reservation);
            }
        }


    }
}
