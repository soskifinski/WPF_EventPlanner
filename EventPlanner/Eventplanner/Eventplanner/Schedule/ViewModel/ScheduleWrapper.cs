using Eventplanner.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eventplanner.UI.Wrapper
{
    public class ScheduleWrapper
    {
        private Model.Schedule Model {get;}
        public ScheduleWrapper(Model.Schedule model) { Model = model; }

        public int Id { get { return Model.Id; } }

        public List<ServiceTask> ServiceTasks
        {
            get; set;
        }
    }
}
