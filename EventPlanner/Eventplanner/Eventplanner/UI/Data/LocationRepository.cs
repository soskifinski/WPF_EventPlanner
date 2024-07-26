using Eventplanner.DataAccess;
using Eventplanner.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace Eventplanner.UI.Data
{
    public class LocationRepository : GenericRepository<Location, EventPlannerDbContext>, ILocationRepository
    {
        private Func<EventPlannerDbContext> _contextCreator;
        public LocationRepository(EventPlannerDbContext context) : base(context)
        {
        }

        public async Task<List<Location>> GetAllLocationsAsync()
        {
            return await Context.Set<Location>()
                .ToListAsync();
        }

        public async Task<List<Location>> GetLocationsLookupAsync()
        {
            using (var ctx = _contextCreator())
            {
                var items = await ctx.Locations.AsNoTracking()
                  .Select(m =>
                     new Location
                     {
                         Id = m.Id,
                         Name = m.Name,
                         Address = m.Address,
                         AddressId = m.AddressId,
                     })
                  .ToListAsync();
                return items;
            }
        }

        public async Task<bool> IsReferenceByRoomAsync(int Id)
        {
            return await Context.Rooms.AsNoTracking()
                .AnyAsync(f => f.LocationId == Id);
        }

        public Room FindById(int id)
        {
            Room room = null;
            room = Context.Set<Room>().FirstOrDefault(e => e.Id == id);

            return room;
        }
    }
}
