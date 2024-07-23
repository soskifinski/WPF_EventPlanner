using Eventplanner.DataAccess;
using Eventplanner.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace Eventplanner.UI.Data
{
    public class PersonRepository : GenericRepository<Person, EventPlannerDbContext>, IPersonRepository
    {
        private Func<EventPlannerDbContext> _contextCreator;
        public PersonRepository(EventPlannerDbContext context) : base(context)
        {

        }

        public async Task<List<Person>> GetAllPersonsAsync()
        {
            return await Context.Set<Person>()
                .ToListAsync();
        }

        public async Task<bool> HasEventsAsync(int Id)
        {
            return await Context.Schedules.AsNoTracking()
              .Include(m => m.Event)
              .AnyAsync(m => m.Person.Id.Equals(Id));
        }

        public async Task<List<Person>> GetPersonsLookupAsync()
        {
            using (var ctx = _contextCreator())
            {
                var items = await ctx.Persons.AsNoTracking()
                  .Select(m =>
                     new Person
                     {
                         Id = m.Id,
                         FirstName = m.FirstName,
                         LastName = m.LastName,
                         Email = m.Email,
                         TelephoneNumber = m.TelephoneNumber,
                         Address = m.Address,
                         IsEmploee = m.IsEmploee
                     })
                  .ToListAsync();
                return items;
            }
        }

        public Person FindById(int id)
        {
            Person thisPerson = null;
            using (var ctx = _contextCreator())
            {
                thisPerson = ctx.Persons.AsNoTracking().FirstOrDefault(e => e.Id == id);
            }

            return thisPerson;
        }
    }
}
