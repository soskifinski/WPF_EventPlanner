using Eventplanner.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Eventplanner.UI.Data
{
    public interface IEventRepository : IGenericRepository<Event>
    {
        Event FindById(int Id);
        Task<List<Event>> GetAllEventsAsync();
        Task<List<Person>> GetAllPersonsAsync();
        Task<List<Event>> GetEventsLookupAsync();
    }
}