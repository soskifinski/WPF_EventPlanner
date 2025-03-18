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
    public class PersonRepository : GenericRepository<Person, EventPlannerDbContext>, IPersonRepository
    {
        public Func<EventPlannerDbContext> _contextCreator;

        public PersonRepository(EventPlannerDbContext context) : base(context)
        {

        }

        public async Task<List<Person>> GetAllPersonsAsync()
        {
            return await _context.Set<Person>()
                .ToListAsync();
        }

        public async Task<List<Person>> GetAllEmpoyeesAsync()
        {
            return await _context.Set<Person>()
                .Where(p => p.IsEmployee == true).ToListAsync();
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
                         AddressId = m.AddressId,
                         IsEmployee = m.IsEmployee
                     })
                  .ToListAsync();
                return items;
            }
        }

        public Person FindById(int personId)
        {
            Person thisPerson = null;
            thisPerson = _context.Persons.FirstOrDefault(e => e.Id == personId);

            return thisPerson;
        }

        public async Task SavePersonAndAddress(Person person, Address address)
        {
            _context.Addresses.Attach(address);
            _context.Persons.Add(person);
            await _context.SaveChangesAsync();
        }
    }
}
