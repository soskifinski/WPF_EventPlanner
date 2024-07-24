using Eventplanner.Model;
using System.ComponentModel;

namespace Eventplanner.UI.Wrapper
{
    public class StatusWrapper : ModelWrapper<Status>
    {
        public StatusWrapper(Status model) : base(model) { }

        public Status Status
        {
            get { return GetValue<Status>(); }
            set { SetValue(value); }
        }

        public string StatusString
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }
    }
}
