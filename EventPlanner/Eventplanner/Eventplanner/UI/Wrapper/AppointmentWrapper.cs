using Eventplanner.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eventplanner.UI.Wrapper
{
    public class AppointmentWrapper : ModelWrapper<ServiceTask>
    {
        public AppointmentWrapper(ServiceTask model) : base(model)
        {
        }

        public int Id { get { return Model.Id; } }

        public Person Person
        {
            get { return Model.Person; }
            private set { Model.Person = value; }
        }

        public Event Event
        {
            get { return GetValue<Event>(); }
            set { SetValue(value); }
        }

        public ServiceRole Role
        {
            get { return GetValue<ServiceRole>(); }
            set { SetValue(value); }
        }
        public DateTime AppointmentStart
        {
            get { return GetValue<DateTime>(); }
            set { SetValue(value); }
        }
        public DateTime AppointmentEnd
        {
            get { return GetValue<DateTime>(); }
            set { SetValue(value); }
        }
    }
}
