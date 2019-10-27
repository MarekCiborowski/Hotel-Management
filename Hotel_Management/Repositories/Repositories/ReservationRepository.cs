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

        public Reservation AddReservation (int roomId, int arrangerId, DateTime requestedAccomodationDate, DateTime requestedCheckOutDate)
        {
            var newReservation = new Reservation
            {
                UserId = arrangerId,
                ReservationStatusId = ReservationStatusEnum.AwaitingConfirmation,
                AccomodationDate = requestedAccomodationDate,
                CheckOutDate = requestedCheckOutDate,
                RoomReservations = new List<RoomReservation> { new RoomReservation
                {
                    RoomId = roomId,
                } }
            };

            using(var dbContextTransaction = db.Database.BeginTransaction())
            {
                try
                {
                    db.Reservations.Add(newReservation);
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


    }
}
