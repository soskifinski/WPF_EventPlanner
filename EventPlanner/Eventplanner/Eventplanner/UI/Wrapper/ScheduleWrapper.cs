using Eventplanner.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eventplanner.UI.Wrapper
{
    public class ScheduleWrapper: ModelWrapper<Schedule>
    {
        public ScheduleWrapper(Schedule model) : base(model) { }

        public int Id { get { return Model.Id; } }

        public List<ServiceTask> ServiceTasks
        {
            get { return GetValue<List<ServiceTask>>(); }
            set { SetValue(value); }
        }
    }
}
