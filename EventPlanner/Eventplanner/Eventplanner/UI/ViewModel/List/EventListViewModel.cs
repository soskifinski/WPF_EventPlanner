using Eventplanner.Model;
using Eventplanner.UI.Data;
using Eventplanner.UI.Events;
using Eventplanner.UI.ViewModel.Detail;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace Eventplanner.UI.ViewModel.List
{
    public class EventListViewModel : ViewModelBase, IListViewModel
    {
        public int Id { get; }
        private IEventRepository _eventRepo;
        private IEventAggregator _eventAggregator;
        public ObservableCollection<EventListItemViewModel> Events { get; }

        public EventListViewModel(IEventRepository eventRepo,
          IEventAggregator eventAggregator)
        {
            _eventRepo = eventRepo;
            _eventAggregator = eventAggregator;
            Events = new ObservableCollection<EventListItemViewModel>();
            _eventAggregator.GetEvent<AfterDetailSavedEvent>().Subscribe(AfterDetailSaved);
            _eventAggregator.GetEvent<AfterDetailDeletedEvent>().Subscribe(AfterDetailDeleted);           
        }

        public async Task LoadAsync()
        {
            List<Event> events = await _eventRepo.GetAllEventsAsync();
            Events.Clear();
            events.Sort((x, y) =>
            {
                return DateTime.Compare(x.DateTimeFrom, y.DateTimeFrom);
            });

            foreach (var item in events)
            {
                Events.Add(new EventListItemViewModel(item, nameof(EventDetailViewModel), _eventAggregator));
            }
        }



        private void AfterDetailSaved(AfterDetailSavedEventArgs args)
        {
            var Event = Events.SingleOrDefault(l => l.Id == args.Id);
            Event thisEvent =  _eventRepo.FindById(args.Id);
            if (Event == null)
            {
                Event = new EventListItemViewModel(thisEvent,
                  args.ViewModelName,
                  _eventAggregator);
            }
            else
            {
                Event.Title = args.DisplayMember;
                Event.DateTimeFrom = args.DateStart;
                Event.DateTimeTo = args.DateEnd;
                Event.Status = args.Status;
            }
        }

        private void AfterDetailDeleted(AfterDetailDeletedEventArgs args)
        {
            AfterDetailDeleted(Events, args);
        }
        private void AfterDetailDeleted(ObservableCollection<EventListItemViewModel> items,
          AfterDetailDeletedEventArgs args)
        {
            var item = items.SingleOrDefault(f => f.Id == args.Id);
            if (item != null)
            {
                items.Remove(item);
            }
        }
    }
}
