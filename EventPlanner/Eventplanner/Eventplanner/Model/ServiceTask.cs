using System;

namespace Eventplanner.Model
{
    public class ServiceTask
    {
        public int Id { get; set; }

        public int? PersonId { get; set; }
        public Person Person { get; set; }
        public int? EventId { get; set; }
        public Event Event { get; set; }
        public ServiceRole ServiceRole { get; set; }
        public DateTime TaskStart { get; set; }
        public DateTime TaskEnd { get; set; }
    }
}