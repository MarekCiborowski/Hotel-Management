namespace DataAccessLayer.Migrations
{
    using DomainObjects.Entities;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using static DomainObjects.Enums;

    internal sealed class Configuration : DbMigrationsConfiguration<DataAccessLayer.DatabaseContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(DataAccessLayer.DatabaseContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.

            context.ReservationStatuses.AddOrUpdate(
            x => x.ReservationStatusId,
            Enum.GetValues(typeof(ReservationStatusEnum))
                .OfType<ReservationStatusEnum>()
                .Select(x => new ReservationStatus() { ReservationStatusId = x, Name = x.ToString() })
                .ToArray());

            context.Roles.AddOrUpdate(
            x => x.RoleId,
            Enum.GetValues(typeof(RolesEnum))
                .OfType<RolesEnum>()
                .Select(x => new Role() { RoleId = x, Name = x.ToString() })
                .ToArray());

            context.HotelBookingSites.AddOrUpdate(
            x => x.HotelBookingSiteId,
            Enum.GetValues(typeof(HotelBookingSiteEnum))
                .OfType<HotelBookingSiteEnum>()
                .Select(x => new HotelBookingSite() { HotelBookingSiteId = x, Name = x.ToString() })
                .ToArray());

            context.Users.Add(new User
            {
                Address = "Mickiewicza 1",
                City = "Warszawa",
                Email = "email@email.email",
                FirstName = "Jan",
                LastName = "Kowalski",
                IsConfirmed = true,
                Login = "admin",
                RoleId = RolesEnum.Admin,
                Zipcode = "11-111",
                Password = "21232F297A57A5A743894A0E4A801FC3"
            });
        }
    }
}
