using Eventplanner.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Eventplanner.UI.Data
{
    public interface IScheduleRepository : IGenericRepository<Schedule>
    {
        Task<List<Schedule>> GetSchedulesLookupAsync();
        Task<List<Schedule>> GetAllSchedulesAsync();
        Task<bool> HasAppointmentsAsync(int Id);
    }
}