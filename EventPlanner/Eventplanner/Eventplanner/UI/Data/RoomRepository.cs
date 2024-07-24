using Eventplanner.DataAccess;
using Eventplanner.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace Eventplanner.UI.Data
{
    public class RoomRepository : GenericRepository<Room, EventPlannerDbContext>, IRoomRepository
    {
        private Func<EventPlannerDbContext> _contextCreator;
        public RoomRepository(EventPlannerDbContext context) : base(context)
        {
        }

        public async Task<List<Room>> GetAllRoomsAsync()
        {
            return await Context.Set<Room>()
                .ToListAsync();
        }

        public async Task<List<Room>> GetRoomsLookupAsync()
        {
            using (var ctx = _contextCreator())
            {
                var items = await ctx.Rooms.AsNoTracking()
                  .Select(m =>
                     new Room
                     {
                         Id = m.Id,
                         RoomNumber = m.RoomNumber,
                         Institution = m.Institution,
                         SeatsCapacity = m.SeatsCapacity,
                         Description = m.Description
                     })
                  .ToListAsync();
                return items;
            }
        }

        public async Task<bool> IsReferenceByEventAsync(int Id)
        {
            return await Context.Events.AsNoTracking()
                .AnyAsync(f => f.RoomId == Id);
        }

        public Room FindById(int id)
        {
            Room room = null;
            room = Context.Set<Room>().FirstOrDefault(e => e.Id == id);

            return room;
        }
    }
}
