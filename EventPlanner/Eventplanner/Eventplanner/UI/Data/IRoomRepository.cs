using Eventplanner.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Eventplanner.UI.Data
{
    public interface IRoomRepository : IGenericRepository<Room>
    {
        Task<List<Room>> GetAllRoomsAsync();
        Task<bool> IsReferenceByEventAsync(int programmingLanguageId);
    }
}