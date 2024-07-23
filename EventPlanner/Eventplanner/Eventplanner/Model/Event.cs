using Eventplanner.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Eventplanner.Model
{
    public class Event
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Title { get; set; }
        public string SubTitle { get; set; }
        public DateTime DateTimeFrom { get; set; }
        public DateTime DateTimeTo { get; set; }
        public Status Status { get; set; }
        public int? StatusId { get; set; }
        public int TotalTickets { get; set; }
        public int BookedTickets { get; set; }
        public Room Room { get; set; }
        public int? RoomId { get; set; }

        public Event()
        {
        }
    }
}
