using DomainObjects.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext() : base("name=HotelManagementDB") { }
        public virtual DbSet<Amenity> Amenities { get; set; }

        public virtual DbSet<Reservation> Reservations { get; set; }

        public virtual DbSet<Review> Reviews { get; set; }

        public virtual DbSet<Room> Rooms { get; set; }

        public virtual DbSet<RoomAmenity> RoomAmenities { get; set; }

        public virtual DbSet<User> Users { get; set; }

        public virtual DbSet<Role> Roles { get; set; }

        public virtual DbSet<ReservationStatus> ReservationStatuses { get; set; }

        public virtual DbSet<Conversation> Conversations { get; set; }

        public virtual DbSet<UserConversation> UserConversations { get; set; }

        public virtual DbSet<Message> Messages { get; set; }

        public virtual DbSet<RoomReservation> RoomReservations { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ReservationStatus>()
                .Property(s => s.ReservationStatusId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            modelBuilder.Entity<Role>()
                .Property(s => s.RoleId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            modelBuilder.Entity<Room>().
                HasMany(p => p.RoomReservations).
                WithRequired(t => t.Room).
                HasForeignKey(t => t.RoomId);

            modelBuilder.Entity<Reservation>().
                HasMany(p => p.RoomReservations).
                WithRequired(t => t.Reservation).
                HasForeignKey(t => t.ReservationId);

            modelBuilder.Entity<Review>().
                HasRequired(p => p.User).
                WithMany().
                WillCascadeOnDelete(false);

            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();
        }
    }
}
