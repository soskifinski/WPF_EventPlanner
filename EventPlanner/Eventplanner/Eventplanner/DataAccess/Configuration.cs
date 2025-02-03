using Eventplanner.Model;
using System;
using System.Data.Entity.Migrations;

namespace Eventplanner.DataAccess
{
    internal sealed class Configuration : DbMigrationsConfiguration<EventPlannerDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }
        protected override void Seed(EventPlannerDbContext context)
        {
            context.Events.AddOrUpdate(
             f => f.Title,
            new Event { Title = "Faschingsfeier", SubTitle = "vom Karnevalsverein Bischberg", DateTimeFrom = DateTime.Now, DateTimeTo = DateTime.Now.AddHours(5) },
            new Event { Title = "Hochzeit", SubTitle = "von Julian und Natalie", DateTimeFrom = DateTime.Now.AddDays(10), DateTimeTo = DateTime.Now.AddDays(10).AddHours(6) },
            new Event { Title = "50. Geburtstag", SubTitle = "von Thomas Müller", DateTimeFrom = DateTime.Now.AddDays(20), DateTimeTo = DateTime.Now.AddDays(20).AddHours(7) },
            new Event { Title = "Jubiläumsfeier", SubTitle = "des Trachtenvereins", DateTimeFrom = DateTime.Now.AddDays(30), DateTimeTo = DateTime.Now.AddDays(30).AddHours(8) }
             );

            context.Persons.AddOrUpdate(
              f => f.FirstName,
              new Person { FirstName = "Emma", LastName = "Schneider" },
              new Person { FirstName = "Markus", LastName = "Roth" },
              new Person { FirstName = "Katharina", LastName = "Müller" },
              new Person { FirstName = "Stefanie", LastName = "Tischler" }
              );

            context.Rooms.AddOrUpdate(
            f => f.RoomNumber,
            new Room { RoomNumber = "Seminarraum 1", SeatsCapacity = 2, LocationId=1 },
            new Room { RoomNumber = "Großer Saal", SeatsCapacity = 500, LocationId = 1 },
            new Room { RoomNumber = "Kleiner Saal", SeatsCapacity = 150, LocationId = 1 },
            new Room { RoomNumber = "Kantine", SeatsCapacity = 45, LocationId = 2}
            );

            context.Addresses.AddOrUpdate(
           f => f.StreetHouseNr,
           new Address { StreetHouseNr = "Zur Müllerin 8", PostalCode = "90456", City = "Fürth", Country = "Deutschland" },
           new Address { StreetHouseNr = "Am Ludwig-Kanal 5", PostalCode = "90549", City = "Nürnberg", Country = "Deutschland" },
           new Address { StreetHouseNr = "Bahnhofsstraße4", PostalCode = "90549", City = "Nürnberg", Country = "Deutschland" },
           new Address { StreetHouseNr = "Theaterstraße 75", PostalCode = "90456", City = "Nürnberg", Country = "Deutschland" },
            new Address { StreetHouseNr = "Wandererstraße 5", PostalCode = "90456", City = "Nürnberg", Country = "Deutschland" }

           );

            context.Locations.AddOrUpdate(
            f => f.Name,
            new Location { Name = "Admiral Hotel", AddressId= 1},
            new Location { Name = "Kulturscheune", AddressId = 2}
             );

        }
    }
}
