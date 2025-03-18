using Eventplanner.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Eventplanner.Interfaces
{
    public interface IAddressRepository : IGenericRepository<Address>
    {
        Task<List<Address>> GetAllAddressesAsync();
        Task<List<Address>> GetAddressLookupAsync();
        Address FindById(int addressId);
    }
}