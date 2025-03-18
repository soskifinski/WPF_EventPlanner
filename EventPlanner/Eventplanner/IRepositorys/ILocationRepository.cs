using Eventplanner.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Eventplanner.Interfaces
{
    public interface ILocationRepository : IGenericRepository<Location>
    {
        Task<List<Location>> GetAllLocationsAsync();
    }
}