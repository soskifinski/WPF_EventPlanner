using Eventplanner.UI.ViewModel.List;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;


namespace Eventplanner.UI.ViewModel.Detail
{
    public class DashboardViewModel : ViewModelBase, IListViewModel
    {

        //TODO Ticketsübersicht
        //TODO Events vll. bevorstehende, vergangene und mit Statusübersicht?


        //von EventListViewModel oder ähnliches für
        //DashboardEventStatusListItem
        public ObservableCollection<EventListItemViewModel> ActualEventItems { get; }

        public int Id => throw new NotImplementedException();

        public DashboardViewModel()
        {
            ActualEventItems = new ObservableCollection<EventListItemViewModel>();
        }


        public async Task LoadAsync()
        {
            //    List<Event> events = await _eventRepository.GetAllEventsAsync();
            //    ActualEventItems.Clear();
            //    events.Sort((x, y) =>
            //    {
            //        return DateTime.Compare(x.DateTimeFrom, y.DateTimeFrom);
            //    });

            //    foreach (var item in events)
            //    {
            //        if(item.DateTimeFrom > DateTime.Now)
            //        {
            //            ActualEventItems.Add(new EventListItemViewModel(item, nameof(EventDetailViewModel), _eventAggregator));
            //        }

            //    }
        }
    }
}
