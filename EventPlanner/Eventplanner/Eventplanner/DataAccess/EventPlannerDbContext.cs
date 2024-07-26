using Eventplanner.Model;
using System.Data.Entity;

namespace Eventplanner.DataAccess
{
    public class EventPlannerDbContext : DbContext
    {
        public EventPlannerDbContext() : base("EventPlanner")
        {

        }
   
        public DbSet<Event> Events { get; set; }

        public DbSet<Person> Persons { get; set; }

        public DbSet<Institution> Institutions { get; set; }

        public DbSet<Address> Addresses { get; set; }

        public DbSet<Room> Rooms { get; set; }

        public DbSet<Schedule> Schedules { get; set; }

        public DbSet<Appointment> Appointments { get; set; }

        public DbSet<Ticket> Tickets { get; set; }
    }
}
