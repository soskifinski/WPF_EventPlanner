using Eventplanner.DataAccess;
using Eventplanner.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace Eventplanner.UI.Data
{
    public class EventRepository : GenericRepository<Event, EventPlannerDbContext>, IEventRepository
    {
        private Func<EventPlannerDbContext> _contextCreator;

        public EventRepository(EventPlannerDbContext context) : base(context)
        {

        }

        public async Task<List<Event>> GetAllEventsAsync()
        {
            return await Context.Set<Event>()
                .ToListAsync();
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

        public async Task<List<Person>> GetAllPersonsAsync()
        {
            return await Context.Set<Person>()
                .ToListAsync();
        }

        public Event FindById(int id)
        {
            Event thisEvent = null;
            thisEvent = Context.Set<Event>().FirstOrDefault(e => e.Id == id);
            
            return thisEvent;
        }
    }
}
