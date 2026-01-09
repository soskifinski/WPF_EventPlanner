using Eventplanner.DataAccess;
using Eventplanner.Model;
using System;

namespace Eventplanner.UI.Data
{
    public class ServiceTaskRepository : GenericRepository<ServiceTask, EventPlannerDbContext>, IServiceTaskRepository
    {
        public Func<EventPlannerDbContext> _contextCreator;

        public ServiceTaskRepository(EventPlannerDbContext context) : base(context)
        {

        }
    }
}
