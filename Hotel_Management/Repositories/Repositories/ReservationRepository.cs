using DataAccessLayer;
using DomainObjects.Dto;
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
            var tomorrowDate = DateTime.Now.AddDays(1);
            var reservations = db.Reservations.Where(r => r.CheckOutDate < tomorrowDate && r.ReservationStatusId == ReservationStatusEnum.Confirmed
                || r.AccomodationDate > tomorrowDate && r.ReservationStatusId == ReservationStatusEnum.AwaitingConfirmation).ToList();

            foreach (var reservation in reservations)
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

        public List<ReservationDto> GetReservationsDto()
        {
            var reservations = db.Reservations.Select(r => new ReservationDto
            {
                AccomodationDate = r.AccomodationDate,
                CheckOutDate = r.CheckOutDate,
                TotalCost = r.TotalCost,
                ReservationId = r.ReservationId,
                RoomId = db.RoomReservations.FirstOrDefault(rr => rr.ReservationId == r.ReservationId).RoomId,
                HotelBookingSiteId = r.HotelBookingSiteId,
                HotelBookingSite = r.HotelBookingSiteId.ToString(),
                ReservationStatusId = r.ReservationStatusId,
                ReservationStatus = r.ReservationStatusId.ToString(),
                ArrangerName = db.Users.Where(u => u.Identity == r.UserId).Select(u => u.FirstName + " " + u.LastName).FirstOrDefault(),
                ArrangerId = r.UserId
            }).OrderByDescending(r => r.ReservationId).ToList();

            return reservations;
        }

        public List<ReservationDto> GetUserReservationsDto(int userId)
        {
            var reservations = db.Reservations.Where(r => r.UserId == userId).Select(r => new ReservationDto
            {
                AccomodationDate = r.AccomodationDate,
                CheckOutDate = r.CheckOutDate,
                TotalCost = r.TotalCost,
                ReservationId = r.ReservationId,
                RoomId = db.RoomReservations.FirstOrDefault(rr => rr.ReservationId == r.ReservationId).RoomId,
                HotelBookingSiteId = r.HotelBookingSiteId,
                HotelBookingSite = r.HotelBookingSiteId.ToString(),
                ReservationStatusId = r.ReservationStatusId,
                ReservationStatus = r.ReservationStatusId.ToString(),
                ArrangerName = db.Users.Where(u => u.Identity == r.UserId).Select(u => u.FirstName + " " + u.LastName).FirstOrDefault(),
                ArrangerId = r.UserId
            }).OrderByDescending(r => r.ReservationId).ToList();

            return reservations;
        }

        public List<ReservationCalendarListItemDto> GetReservationsCalendarDto(int roomId)
        {
            return db.Reservations.Where(r => db.RoomReservations.Where(rr => rr.RoomId == roomId).Select(rr => rr.ReservationId).Contains(r.ReservationId))
                .Select(r => new ReservationCalendarListItemDto
                {
                    Sr = r.ReservationId,
                    Title = r.User.FirstName + " " + r.User.LastName,
                    StartDate = r.AccomodationDate,
                    EndDate = r.CheckOutDate,
                    Desc = "Status: " + r.ReservationStatusId.ToString() + "; Hotel booking site: " + r.HotelBookingSiteId.ToString()
                }).ToList();
        }

        public bool CanMakeReservation(int roomId, DateTime requestedAccomodationDate, DateTime requestedCheckOutDate)
        {
            return !this.db.Reservations
                        .Where(reservation => db.RoomReservations.Where(rr => rr.RoomId == roomId).Select(rr => rr.ReservationId).Contains(reservation.ReservationId) 
                        && ((reservation.CheckOutDate > requestedAccomodationDate && reservation.AccomodationDate <= requestedAccomodationDate)
                            || (reservation.CheckOutDate >= requestedCheckOutDate && reservation.AccomodationDate < requestedCheckOutDate)
                            || (reservation.AccomodationDate >= requestedAccomodationDate && reservation.CheckOutDate <= requestedCheckOutDate))
                        && reservation.ReservationStatusId != ReservationStatusEnum.Canceled).Any();
        }


    }
}
