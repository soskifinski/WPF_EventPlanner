using Eventplanner.Model;
using Eventplanner.UI.Data;
using Eventplanner.UI.Events;
using Eventplanner.UI.Wrapper;
using FriendOrganizer.UI.ViewModel;
using Prism.Commands;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;


namespace Eventplanner.UI.ViewModel.Detail
{
    public class EventDetailViewModel : DetailViewModelBase, IEventDetailViewModel
    {
        private IEventRepository _eventRepository;
        private IRoomRepository _roomRepo;
        private EventWrapper _event;
        private string _title;
        private string _subTitle;
        public ObservableCollection<Room> Rooms { get; }

        private Status _selectedStatus;
        public ObservableCollection<Status> Stati { get; }

        public EventWrapper Event
        {
            get { return _event; }
            private set
            {
                _event = value;
                OnPropertyChanged();
            }
        }

        public Status SelectedStatus
        {
            get { return _selectedStatus; }
            set
            {
                _selectedStatus = value;
                OnPropertyChanged();
            }
        }

        public EventDetailViewModel(IEventAggregator eventAggregator, IEventRepository eventRepository, IRoomRepository roomRepository) : base(eventAggregator)
        {
            _eventRepository = eventRepository;
            _roomRepo = roomRepository;

            eventAggregator.GetEvent<AfterCollectionSavedEvent>()
               .Subscribe(AfterCollectionSaved);
            Rooms = new ObservableCollection<Room>();
            Stati = new ObservableCollection<Status>();
        }

        public override async Task LoadAsync(int eventId)
        {
            var thisEvent = eventId > 0
              ? await _eventRepository.GetByIdAsync(eventId)
              : CreateNewEvent();

            Id = eventId;
            InitializeEvent(thisEvent);
            await GetRoomsAsync();
            InitializeStatus();
        }

        private void InitializeStatus()
        {
            var statusString = " - ";
            foreach (Status status in Stati)
            {
                switch (status)
                {
                    case Status.REQUESTED:
                        statusString = "Angefragt";
                        break;
                    case Status.PLANNED:
                        statusString = "Geplant";
                        break;
                    case Status.PUBLISHED:
                        statusString = "Veröffentlicht";
                        break;
                    case Status.BOOKEDUP:
                        statusString = "Buchung bestätigt";
                        break;
                    case Status.TOOKPLACE:
                        statusString = "Tatort";
                        break;
                    case Status.BILLED:
                        statusString = "Rechnung erstellt";
                        break;          
                }
                var wrapper = new StatusWrapper(statusString);
               
            }
        }

        private async void AfterCollectionSaved(AfterCollectionSavedEventArgs args)
        {
            if (args.ViewModelName == nameof(RoomDetailViewModel))
            {
                await GetRoomsAsync();
            }
        }

        public Event CreateNewEvent()
        {
            var Event = new Event
            {
                DateTimeFrom = DateTime.Now.Date,
                DateTimeTo = DateTime.Now.Date
            };
            _eventRepository.Add(Event);
            return Event;
        }

        private void InitializeEvent(Event newEvent)
        {
            Event = new EventWrapper(newEvent);

            Event.PropertyChanged += (sender, args) =>
            {
                if (!HasChanges)
                {
                    HasChanges = _eventRepository.HasChanges();
                }

                if (args.PropertyName == nameof(Event.HasErrors))
                {
                    ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
                }
                if (args.PropertyName == nameof(Event.Title))
                {
                    SetTitle();
                }
            };
            ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();

            if (Event.Id == 0)
            {
                // Little trick to trigger the validation
                Event.Title = "";
            }
            SetTitle();
        }


        private void SetTitle()
        {
            Title = Event.Title;
        }

        protected override bool OnSaveCanExecute()
        {
            return Event != null && !Event.HasErrors && HasChanges;
        }

        protected override async void OnSaveExecute()
        {
            await _eventRepository.SaveAsync();
            HasChanges = _eventRepository.HasChanges();
            Id = Event.Id;
            RaiseDetailSavedEvent(Event.Id, Event.Title);
        }

        protected override async void OnDeleteExecute()
        {
            ////erst schauen ob ein Kontakt Dienst hat bei Event
            //if (await _eventRepository.HasMeetingsAsync(Friend.Id))
            //{
            //    MessageDialogService.ShowInfoDialog($"{Friend.FirstName} {Friend.LastName} can't be deleted, as this friend is part of at least one meeting");
            //    return;
            //}

            //var result = MessageDialogService.ShowOkCancelDialog($"Do you really want to delete the friend {Friend.FirstName} {Friend.LastName}?",
            //  "Question");
            //if (result == MessageDialogResult.OK)
            //{
            _eventRepository.Remove(Event.Model);
            await _eventRepository.SaveAsync();
            RaiseDetailDeletedEvent(Event.Id);
            //}
        }

        private async Task GetRoomsAsync()
        {
            Rooms.Clear();
            Rooms.Add(new NullRoom { RoomNumber = " - " });
            var rooms = await _roomRepo.GetAllRoomsAsync();
            foreach (var room in rooms)
            {
                Rooms.Add(room);
            }
        }

    }
}
