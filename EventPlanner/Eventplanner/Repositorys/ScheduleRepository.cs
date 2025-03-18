using Eventplanner.DataAccess;
using Eventplanner.Interfaces;
using Eventplanner.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace Eventplanner.Repository
{
    public class ScheduleRepository : GenericRepository<Schedule, EventPlannerDbContext>, IScheduleRepository
    {
        public Func<EventPlannerDbContext> _contextCreator;

        public ScheduleRepository(EventPlannerDbContext context) : base(context)
        {

        }

        public async Task<List<Schedule>> GetAllSchedulesAsync()
        {
            return await _context.Set<Schedule>()
                .ToListAsync();
        }

        public async Task<List<Schedule>> GetSchedulesLookupAsync()
        {
            using (var ctx = _contextCreator())
            {
                var items = await ctx.Schedules.AsNoTracking()
                  .Select(m =>
                     new Schedule
                     {
                         Appointments = m.Appointments
                     })
                  .ToListAsync();
                return items;
            }
        }

        public async Task<bool> HasAppointmentsAsync(int Id)
        {
            return await _context.Schedules.AsNoTracking()
                .Where(m => m.Appointments.Any(a => a.Person.Id.Equals(Id)))
                .AnyAsync();
        }
    }
}
