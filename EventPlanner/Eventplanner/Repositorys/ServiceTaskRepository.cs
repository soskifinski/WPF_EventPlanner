using Eventplanner.DataAccess;
using Eventplanner.Interfaces;
using Eventplanner.Model;
using System;

namespace Eventplanner.Repository
{
    public class ServiceTaskRepository : GenericRepository<ServiceTask, EventPlannerDbContext>, IServiceTaskRepository
    {
        public Func<EventPlannerDbContext> _contextCreator;

        public ServiceTaskRepository(EventPlannerDbContext context) : base(context)
        {

        }
    }
}
