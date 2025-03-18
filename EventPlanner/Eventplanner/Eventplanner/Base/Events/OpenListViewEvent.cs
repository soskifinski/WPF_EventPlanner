using Prism.Events;

namespace Eventplanner.UI.Events
{
    public class OpenListViewEvent : PubSubEvent<OpenListViewEventArgs>
    {
    }

    public class OpenListViewEventArgs
    {
        public int Id { get; set; }
        public string ViewModelName { get; set; }
    }
}
