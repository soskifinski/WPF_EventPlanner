using System;
using System.Collections.Generic;

namespace Eventplanner.Model
{
    public class Schedule
    {
        public int Id { get; set; } 
        public List<ServiceTask> Appointments { get; set; }
    }
}
