using DataAccessLayer;
using DomainObjects.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class UnitOfWork
    {
        private DatabaseContext context = new DatabaseContext();
        private GenericRepository<Amenity> amenityRepository;
        private GenericRepository<Conversation> conversationRepository;
        private GenericRepository<Message> messageRepository;
        private GenericRepository<Reservation> reservationRepository;
        private GenericRepository<Room> roomRepository;
        private GenericRepository<RoomAmenity> roomAmenityRepository;
        private GenericRepository<RoomReservation> roomReservationRepository;
        private GenericRepository<User> userRepository;
        private GenericRepository<UserConversation> userConversationRepository;
        

        public GenericRepository<Amenity> AmenityRepository
        {
            get
            {

                if (this.amenityRepository == null)
                {
                    this.amenityRepository = new GenericRepository<Amenity>(context);
                }
                return amenityRepository;
            }
        }

        public GenericRepository<Conversation> ConversationRepository
        {
            get
            {

                if (this.conversationRepository == null)
                {
                    this.conversationRepository = new GenericRepository<Conversation>(context);
                }
                return conversationRepository;
            }
        }

        public GenericRepository<Message> MessageRepository
        {
            get
            {

                if (this.messageRepository == null)
                {
                    this.messageRepository = new GenericRepository<Message>(context);
                }
                return messageRepository;
            }
        }

        public GenericRepository<Reservation> ReservationRepository
        {
            get
            {

                if (this.reservationRepository == null)
                {
                    this.reservationRepository = new GenericRepository<Reservation>(context);
                }
                return reservationRepository;
            }
        }

        public GenericRepository<Room> RoomRepository
        {
            get
            {

                if (this.roomRepository == null)
                {
                    this.roomRepository = new GenericRepository<Room>(context);
                }
                return roomRepository;
            }
        }

        public GenericRepository<RoomAmenity> RoomAmenityRepository
        {
            get
            {

                if (this.roomAmenityRepository == null)
                {
                    this.roomAmenityRepository = new GenericRepository<RoomAmenity>(context);
                }
                return roomAmenityRepository;
            }
        }

        public GenericRepository<RoomReservation> RoomReservationRepository
        {
            get
            {

                if (this.roomReservationRepository == null)
                {
                    this.roomReservationRepository = new GenericRepository<RoomReservation>(context);
                }
                return roomReservationRepository;
            }
        }

        public GenericRepository<User> UserRepository
        {
            get
            {

                if (this.userRepository == null)
                {
                    this.userRepository = new GenericRepository<User>(context);
                }
                return userRepository;
            }
        }

        public GenericRepository<UserConversation> UserConversationRepository
        {
            get
            {

                if (this.userConversationRepository == null)
                {
                    this.userConversationRepository = new GenericRepository<UserConversation>(context);
                }
                return userConversationRepository;
            }
        }

        public void Save()
        {
            context.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
