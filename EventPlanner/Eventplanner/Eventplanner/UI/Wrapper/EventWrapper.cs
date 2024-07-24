using Eventplanner.Model;
using System;

namespace Eventplanner.UI.Wrapper
{
    public class EventWrapper : ModelWrapper<Event>
    {
        public EventWrapper(Event model) : base(model)
        {
        }

        public int Id { get { return Model.Id; } }

        public string Title
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }

        public string SubTitle
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }

        public DateTime DateTimeFrom
        {
            get { return GetValue<DateTime>(); }
            set { SetValue(value); }
        }

        public DateTime DateTimeTo
        {
            get { return GetValue<DateTime>(); }
            set { SetValue(value); 
                
                if (DateTimeTo < DateTimeFrom)
                {
                    DateTimeTo = DateTimeFrom;
                }
            }
        }

        public Status Status
        {
            get { return GetValue<Status>(); }
            set { SetValue(value); }
        }

        public int StatusId
        {
            get { return GetValue<int>(); }
            set { SetValue(value); }
        }

        public int TotalTickets
        {
            get { return GetValue<int>(); }
            set { SetValue(value); }
        }

        public int BookedTickets
        {
            get { return GetValue<int>(); }
            set { SetValue(value); }
        }

        public int? RoomId
        {
            get { return GetValue<int?>(); }
            set { SetValue(value); }
        }
    }
}
