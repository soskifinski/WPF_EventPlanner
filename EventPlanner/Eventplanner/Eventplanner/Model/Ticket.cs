using System.Collections.Generic;
using System.Windows.Documents;

namespace Eventplanner.Model
{
    public class Ticket
    {
        public int Id { get; set; }
        public Event Event { get; set; }

        public List<TicketPrices> TicketTypes { get; set; }
        public bool IsBooked { get; set; }
    }

    public class TicketPrices
    {
        public int Id { get; set; }
        public TicketCategory Type { get; set; }

        public decimal Price { get; set; }
    }
}
