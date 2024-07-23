using System;

namespace Eventplanner.Model
{
    public class Schedule
    {
        public int Id { get; set; }
        public Person Person { get; set; }
        public Event Event { get; set; }
        public Role Role { get; set; }
        public DateTime DateTimeFrom { get; set; }
        public DateTime DateTimeUntil { get; set; }
    }
}
