using Eventplanner.DataAccess;
using Eventplanner.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Threading.Tasks;

namespace Eventplanner.UI.Data
{
    public class EventRepository : GenericRepository<Event, EventPlannerDbContext>, IEventRepository
    {
        public Func<EventPlannerDbContext> _contextCreator;

        public EventRepository(EventPlannerDbContext context) : base(context)
        {

        }

        public async Task<List<Event>> GetAllEventsAsync()
        {
            return await _context.Set<Event>().ToListAsync();
        }

        public async Task<List<Event>> GetEventsLookupAsync()
        {
            using (var ctx = _contextCreator())
            {
                var items = await ctx.Events.AsNoTracking()
                  .Select(m =>
                     new Event
                     {
                         Id = m.Id,
                         Title = m.Title,
                         Status = m.Status,
                         SubTitle = m.SubTitle,
                         DateTimeFrom = m.DateTimeFrom,
                         DateTimeTo = m.DateTimeTo,
                         BookedTickets = m.BookedTickets,
                         TotalTickets = m.TotalTickets
                     })
                  .ToListAsync();
                return items;
            }
        }

        public Event FindById(int eventId)
        {
            return _context.Set<Event>().FirstOrDefault(e => e.Id == eventId);
        }
    }
}
