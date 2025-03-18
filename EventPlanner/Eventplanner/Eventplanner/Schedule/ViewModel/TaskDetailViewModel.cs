using Eventplanner.Model;
using System;

namespace Eventplanner.UI.Wrapper
{
    public class TaskDetailViewModel
    {
        private ServiceTask Model { get; set; }
        public TaskDetailViewModel(ServiceTask model)
        {
            Model = model;  
        }

        public int Id { get { return Model.Id; } }

        public Model.Person Person
        {
            get { return Model.Person; }
            private set { Model.Person = value; }
        }

        public Model.Event Event
        {
            get; set;
        }

        public ServiceRole Role
        {
            get; set;
        }
        public DateTime AppointmentStart
        {
            get; set;
        }
        public DateTime AppointmentEnd
        {
            get; set;
        }
    }
}
