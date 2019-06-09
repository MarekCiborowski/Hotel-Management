using DomainObjects.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext() : base("name=HotelManagementDB") { }
        public virtual DbSet<Amenity> Amenities { get; set; }

        public virtual DbSet<Company> Companies { get; set; }

        public virtual DbSet<News> News { get; set; }

        public virtual DbSet<Offer> Offers { get; set; }

        public virtual DbSet<Reservation> Reservations { get; set; }

        public virtual DbSet<Review> Reviews { get; set; }

        public virtual DbSet<Room> Rooms { get; set; }

        public virtual DbSet<RoomAmenity> RoomAmenities { get; set; }

        public virtual DbSet<RoomOffer> RoomOffers { get; set; }

        public virtual DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

        }
    }
}
