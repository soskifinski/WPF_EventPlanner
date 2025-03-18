using Prism.Events;

namespace Eventplanner.UI.Events
{
    public class AfterDetailSavedEvent : PubSubEvent<AfterDetailSavedEventArgs>
    {
    }

    public class AfterDetailSavedEventArgs
    {
        public int Id { get; set; }
        public string DisplayMember { get; set; }

        public string DateStart { get; set; }

        public string DateEnd { get; set; }

        public string Status { get; set; }

        public string ViewModelName { get; set; }
    }
}
