using Eventplanner.DataAccess;
using Eventplanner.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eventplanner.UI.Data
{
    public class AddressRepository : GenericRepository<Address, EventPlannerDbContext>, IAddressRepository
    {
        public Func<EventPlannerDbContext> _contextCreator;

        public AddressRepository(EventPlannerDbContext context) : base(context)
        {

        }
        public async Task<List<Address>> GetAllAddressesAsync()
        {
            return await Context.Set<Address>()
                .ToListAsync();
        }

        public async Task<List<Address>> GetAddressLookupAsync()
        {
            using (var ctx = _contextCreator())
            {
                var items = await ctx.Addresses.AsNoTracking()
                  .Select(m =>
                     new Address
                     {
                         Id = m.Id,
                         StreetHouseNr = m.StreetHouseNr,
                         City = m.City, 
                         PostalCode = m.PostalCode, 
                         Country = m.Country,   
                     })
                  .ToListAsync();
                return items;
            }
        }

        public Address FindById(int addressId)
        {
            Address address = null;
            address = Context.Set<Address>().FirstOrDefault(e => e.Id == addressId);

            return address;
        }
    }
}
