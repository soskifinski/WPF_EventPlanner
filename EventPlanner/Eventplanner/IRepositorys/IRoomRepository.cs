using Eventplanner.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Eventplanner.Interfaces
{
    public interface IRoomRepository : IGenericRepository<Room>
    {
        Room FindById(int Id);
        Task<List<Room>> GetAllRoomsAsync();
        Task<bool> IsReferenceByEventAsync(int Id);
    }
}