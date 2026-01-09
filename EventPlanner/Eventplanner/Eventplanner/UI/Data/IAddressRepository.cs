using Eventplanner.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Eventplanner.UI.Data
{
    public interface IAddressRepository : IGenericRepository<Address>
    {
        Task<List<Address>> GetAllAddressesAsync();
        Task<List<Address>> GetAddressLookupAsync();
        Address FindById(int addressId);
    }
}